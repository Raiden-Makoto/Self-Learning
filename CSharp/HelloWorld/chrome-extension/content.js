// Prevent multiple injections
if (window.transitExtensionLoaded) {
    console.log('[Content] Script already loaded, skipping');
} else {
    window.transitExtensionLoaded = true;

    let transitWidget = null;
    let isVisible = false;

    console.log('[Content] Transit extension content script loaded');

    // Create draggable widget
    function createWidget() {
        if (transitWidget) {
            console.log('[Content] Widget already exists, showing it');
            transitWidget.style.display = 'block';
            isVisible = true;
            return;
        }

        console.log('[Content] Creating new widget');
        const widget = document.createElement('div');
        widget.id = 'transit-info-widget';
        const popupUrl = chrome.runtime.getURL('popup.html');
        console.log('[Content] Popup URL:', popupUrl);
        
        widget.innerHTML = `
        <div class="widget-header">
          <span>Transit Information</span>
          <button id="transit-widget-close">×</button>
        </div>
        <div class="widget-content">
          <iframe id="transit-iframe" src="${popupUrl}"></iframe>
        </div>
      `;

        // Make widget draggable
        let isDragging = false;
        let currentX;
        let currentY;
        let initialX;
        let initialY;

        const header = widget.querySelector('.widget-header');

        header.addEventListener('mousedown', (e) => {
            if (e.target.id === 'transit-widget-close') return;
            initialX = e.clientX - widget.offsetLeft;
            initialY = e.clientY - widget.offsetTop;
            isDragging = true;
        });

        document.addEventListener('mousemove', (e) => {
            if (isDragging) {
                e.preventDefault();
                currentX = e.clientX - initialX;
                currentY = e.clientY - initialY;
                widget.style.left = currentX + 'px';
                widget.style.top = currentY + 'px';
            }
        });

        document.addEventListener('mouseup', () => {
            isDragging = false;
        });

        // Close button
        widget.querySelector('#transit-widget-close').addEventListener('click', () => {
            toggleWidget();
        });

        document.body.appendChild(widget);
        transitWidget = widget;
        isVisible = true;
        console.log('[Content] Widget created and added to page');
    }

    function toggleWidget() {
        console.log('[Content] Toggle widget called, isVisible:', isVisible, 'transitWidget:', transitWidget);
        if (!transitWidget) {
            createWidget();
        } else {
            if (isVisible) {
                transitWidget.style.display = 'none';
                isVisible = false;
                console.log('[Content] Widget hidden');
            } else {
                transitWidget.style.display = 'block';
                isVisible = true;
                console.log('[Content] Widget shown');
            }
        }
    }

    // Listen for messages from background script
    chrome.runtime.onMessage.addListener((request, sender, sendResponse) => {
        console.log('[Content] Message received:', request);
        if (request.action === 'toggle') {
            toggleWidget();
            sendResponse({ success: true });
            return true;
        }
        return false;
    });

    console.log('[Content] Content script initialized, waiting for messages');
}
