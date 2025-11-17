// Configuration is loaded from config.js
const stopConfigs = TRANSIT_CONFIG.stops;
const API_URL = TRANSIT_CONFIG.apiUrl;

// Fetch stop information from API
async function getStopInfo(stopId) {
    try {
        const response = await fetch(API_URL, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ stop: stopId.toString() })
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json();
        return data;
    } catch (error) {
        console.error(`Error calling API for stop ${stopId}:`, error);
        return null;
    }
}

// Calculate time difference in minutes
function calculateMinutes(actualTimeStr, now) {
    // Parse the time string - could be "HH:MM:SS AM/PM" or "HH:MM AM/PM"
    let timeStr = actualTimeStr.trim().toUpperCase();
    const isPM = timeStr.includes('PM');
    const isAM = timeStr.includes('AM');

    // Remove AM/PM from the string
    timeStr = timeStr.replace(/\s*(AM|PM)\s*/i, '');

    const timeParts = timeStr.split(':');
    let hours = parseInt(timeParts[0], 10);
    const minutes = parseInt(timeParts[1] || 0, 10);
    const seconds = parseInt(timeParts[2] || 0, 10);

    // Convert to 24-hour format
    if (isPM && hours !== 12) {
        hours += 12;
    } else if (isAM && hours === 12) {
        hours = 0;
    }

    // Create a date for today with the parsed time
    const today = new Date(now);
    today.setHours(hours, minutes, seconds, 0);

    // If the time is in the past, it means it's for tomorrow
    if (today < now) {
        today.setDate(today.getDate() + 1);
    }

    const diffMs = today - now;
    const calculatedMinutes = Math.round(diffMs / (1000 * 60));

    // If somehow we get a negative or very large value, clamp it
    if (calculatedMinutes < 0) {
        return 0;
    }
    if (calculatedMinutes > 1440) { // More than 24 hours seems wrong
        console.warn(`Suspicious time calculation: ${calculatedMinutes} minutes for time ${actualTimeStr}`);
    }

    return calculatedMinutes;
}

// Get transit information
async function getTransitInfo() {
    // Group by stop ID to avoid duplicate API calls
    const uniqueStops = {};
    stopConfigs.forEach(config => {
        if (!uniqueStops[config.stopId]) {
            uniqueStops[config.stopId] = [];
        }
        uniqueStops[config.stopId].push(config);
    });

    // Make all API calls in parallel
    const apiPromises = Object.keys(uniqueStops).map(stopId =>
        getStopInfo(stopId).then(result => ({
            stopId: parseInt(stopId),
            configs: uniqueStops[stopId],
            result: result
        }))
    );

    const apiResults = await Promise.all(apiPromises);
    const routes = [];
    const now = new Date();

    // Process all results
    apiResults.forEach(({ stopId, configs, result }) => {
        if (result && result.vehicles) {
            configs.forEach(config => {
                const vehicle = result.vehicles.find(v => v.route === config.routeNumber);
                if (vehicle && vehicle.actual) {
                    console.log(`Route ${config.routeNumber}: actual time="${vehicle.actual}", current time="${now.toTimeString()}"`);
                    const minutes = calculateMinutes(vehicle.actual, now);
                    console.log(`Route ${config.routeNumber}: calculated ${minutes} minutes`);

                    // Parse the message to extract route number, route name and destination
                    // Format: "16 McCowan to Scarborough Centre Station in {0} min"
                    // Format: "995 York Mills Express to UTSC in {0} min"
                    const messageParts = config.message.split(' in ');
                    const routeAndDest = messageParts[0]; // "16 McCowan to Scarborough Centre Station"
                    const routeParts = routeAndDest.split(' to ');
                    const routePrefix = routeParts[0]; // "16 McCowan" or "995 York Mills Express"
                    const routeNumber = routePrefix.split(' ')[0]; // "16" or "995"
                    const routeName = routePrefix.substring(routeNumber.length + 1).trim(); // "McCowan" or "York Mills Express"
                    const destination = routeParts[1] || ''; // "Scarborough Centre Station" or "UTSC"

                    routes.push({
                        routeNumber: routeNumber,
                        routeName: routeName,
                        destination: destination,
                        minutes: minutes
                    });
                }
            });
        }
    });

    return routes;
}

// Display transit information
async function displayTransitInfo() {
    const loadingEl = document.getElementById('loading');
    const errorEl = document.getElementById('error');
    const transitInfoEl = document.getElementById('transit-info');

    try {
        // Keep old information visible, just show loading indicator
        loadingEl.style.display = 'block';
        errorEl.style.display = 'none';
        // Don't hide transitInfoEl - keep old data visible

        const routes = await getTransitInfo();

        loadingEl.style.display = 'none';

        if (routes.length === 0) {
            // Only show error if we don't have any existing data
            if (!transitInfoEl.innerHTML || transitInfoEl.innerHTML.trim() === '') {
                errorEl.textContent = 'No transit information available';
                errorEl.style.display = 'block';
            }
        } else {
            transitInfoEl.innerHTML = routes.map(route => {
                const bgColor = route.routeNumber === '995' ? '#4CAF50' : '#F44336'; // Green for 995, red for others
                return `
                <div class="transit-card" style="background-color: ${bgColor};">
                    <div class="transit-card-header">
                        <div class="transit-card-left">
                            <div class="transit-route">${route.routeNumber} ${route.routeName}</div>
                            <div class="transit-destination">${route.destination}</div>
                        </div>
                        <div class="transit-time">${route.minutes} min</div>
                    </div>
                </div>
            `;
            }).join('');
            transitInfoEl.style.display = 'flex';
        }
    } catch (error) {
        loadingEl.style.display = 'none';
        // Only show error if we don't have existing data
        if (!transitInfoEl.innerHTML || transitInfoEl.innerHTML.trim() === '') {
            errorEl.textContent = `Error: ${error.message}`;
            errorEl.style.display = 'block';
        }
    }
}

// Load transit info on page load
displayTransitInfo();

// Refresh at configured interval
setInterval(displayTransitInfo, TRANSIT_CONFIG.refreshInterval);

