using CefSharp;
using CefSharp.WinForms;
using System.Threading.Tasks;

namespace WidgetsApp
{
    public class CefSharpHandler
    {
        private readonly ChromiumWebBrowser browser;

        public CefSharpHandler(ChromiumWebBrowser browser)
        {
            this.browser = browser;
        }

        // Method to handle PostMessage from JavaScript
        public async Task<bool> Input(string input)
        {
            await browser.GetDevToolsClient()?.Input?.InsertTextAsync(input);
            return true;
        }
    }
}
