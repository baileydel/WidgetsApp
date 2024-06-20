using CefSharp;
using CefSharp.WinForms;
using System;
using System.Threading;
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

        public async Task<bool> Input(string input)
        {
            await browser.GetDevToolsClient()?.Input?.InsertTextAsync(input);
            return true;
        }
    }
}
