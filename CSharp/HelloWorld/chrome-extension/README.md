# Transit Info Chrome Extension (Blazor WebAssembly)

This Chrome extension displays transit information using C# Blazor WebAssembly. The C# code runs entirely in the browser - no server needed!

## Setup

1. **Build the Blazor app:**
   ```bash
   cd /Users/maxcui/Downloads/Self-Learning/CSharp/HelloWorld
   dotnet publish -c Release -o publish
   cp -r publish/wwwroot/* chrome-extension/
   ```

2. **Load the Chrome Extension:**
   - Open Chrome and go to `chrome://extensions/`
   - Enable "Developer mode" (toggle in top right)
   - Click "Load unpacked"
   - Select the `chrome-extension` folder

3. **Use the Extension:**
   - Click the extension icon in the Chrome toolbar
   - The Blazor app will load and fetch transit info immediately
   - It automatically refreshes every 45 seconds
   - Each transit route is displayed as an h2 element

## How It Works

- The C# code is compiled to WebAssembly and runs in the browser
- The `TransitService` class makes API calls directly from the browser
- The `Index.razor` component displays the transit info and refreshes every 45 seconds
- No local server required - everything runs client-side!
