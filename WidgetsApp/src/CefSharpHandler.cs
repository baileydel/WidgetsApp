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

        public async Task<bool> WaitUntilSelector(string selector)
        {
            string script = $@"
                function waitForElm(selector) {{
                    return new Promise(resolve => {{
                        if (document.querySelector(selector)) {{
                            return resolve(document.querySelector(selector));
                        }}

                        const observer = new MutationObserver(mutations => {{
                            if (document.querySelector(selector)) {{
                                observer.disconnect();
                                resolve(document.querySelector(selector));
                            }}
                        }});

                        observer.observe(document.body, {{
                            childList: true,
                            subtree: true
                        }});
                    }});
                }}              

                async function mkwe() {{
                    const elm = await waitForElm('{selector}');
                    
                    if (elm != null) {{
                        return true;
                    }}
                }}

                mkwe();
            ";

            dynamic response = await browser.EvaluateScriptAsync(script);

            return response.Result;

        }

        public async Task<bool> Input(string selector, string input)
        {
            string script = $@"
                function waitForElm(selector) {{
                    return new Promise(resolve => {{
                        if (document.querySelector(selector)) {{
                            return resolve(document.querySelector(selector));
                        }}

                        const observer = new MutationObserver(mutations => {{
                            if (document.querySelector(selector)) {{
                                observer.disconnect();
                                resolve(document.querySelector(selector));
                            }}
                        }});

                        observer.observe(document.body, {{
                            childList: true,
                            subtree: true
                        }});
                    }});
                }}              

                async function mkwe() {{
                    const elm = await waitForElm('{selector}');
                    
                    if (elm != null) {{
                        elm.select();
                        return true;
                    }}
                }}

                mkwe();
            ";

            dynamic response = await browser.EvaluateScriptAsync(script);

            if (response.Result)
            {
                await browser.GetDevToolsClient()?.Input?.InsertTextAsync(input);
            }
          
            return response.Result;
        }
    }
}
