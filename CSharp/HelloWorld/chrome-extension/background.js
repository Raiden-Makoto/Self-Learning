chrome.action.onClicked.addListener(async (tab) => {
    console.log('[Background] Extension icon clicked for tab:', tab.id);

    try {
        // Inject content script
        await chrome.scripting.executeScript({
            target: { tabId: tab.id },
            files: ['content.js']
        });

        // Inject CSS
        await chrome.scripting.insertCSS({
            target: { tabId: tab.id },
            files: ['content.css']
        });

        // Wait a moment for script to initialize
        await new Promise(resolve => setTimeout(resolve, 150));

        // Send message to toggle widget
        chrome.tabs.sendMessage(tab.id, { action: "toggle" }, (response) => {
            if (chrome.runtime.lastError) {
                console.error('[Background] Error:', chrome.runtime.lastError.message);
            } else {
                console.log('[Background] Response:', response);
            }
        });
    } catch (error) {
        console.error('[Background] Error:', error);
    }
});
