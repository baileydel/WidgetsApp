﻿using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using WidgetsApp.src.controls;

namespace WidgetsApp
{
    internal class WidgetPanel : ResizablePanel
    {
        public ChromiumWebBrowser browser;
        private WidgetPanelController controller;
        private WidgetData data;

        public WidgetPanel(WidgetData data)
        {
            Editable = false;
            if (data != null)
            {
                this.data = data;
                Size = data.size;
                Location = data.location;
            }
            else
            {
                data = new WidgetData(Size, Location, "https://www.google.com");
            }

            InitializeChromium(data.url);

            controller = new WidgetPanelController(this);
        }

        private void InitializeChromium(string url)
        {
            browser = new ChromiumWebBrowser(url)
            {
                Dock = DockStyle.None,
                Size = new Size(Width, Height),
                Location = new Point(1, 1)
            };

            browser.FrameLoadEnd += Browser_FrameLoadEnd;
            browser.JavascriptMessageReceived += Browser_JavascriptMessageReceived;

            browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            browser.JavascriptObjectRepository.Register("CefSharpHandler", new CefSharpHandler(browser), isAsync: true);

            Controls.Add(browser);
        }

        private async void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (browser == null)
            {
                return;
            }

            if (e.Frame.IsMain)
            {
                string script =
                   $@"
                        let b = false;
                        let func = function(e) {{
                            let o = e.clientY < 20;

                            if (b !== o) {{
                                b = o;
                                CefSharp.PostMessage({{Type: 'Hover', Data: b}});
                            }}
                        }}

                        document.addEventListener('mousemove', func);
                   ";

                await browser.EvaluateScriptAsync(script);

                string f = e.Url.Replace("https://", "").Replace(".com", "");
                string[] j = f.Split('/');

                string r = j[0].Replace('.', '\\').Replace("www.", "") + "\\";
                string p = Form1.PATH + @"\scripts\" + r;
                
                if (Directory.Exists(p))
                {
                    string[] files = Directory.GetFiles(p);
                    foreach (var item in files)
                    {
                        if (item.Contains(".js"))
                        {
                            await SendJavaScript(item);
                        }                        
                    }
                }
            }
        }

        private void Browser_JavascriptMessageReceived(object sender, JavascriptMessageReceivedEventArgs e)
        {
            dynamic msg = e.Message;
            var type = msg.Type;
            var data = msg.Data;

            if (msg != null)
            {
                Action safeWrite = null;
                switch (type)
                {
                    case "Hover":
                        {
                            safeWrite = delegate
                            {
                                controller.show(data);
                            };
                            break;
                        }
                }

                if (this.InvokeRequired && safeWrite != null)
                {
                    this.Invoke(safeWrite);
                }
            }
        }

        private async Task<bool> SendJavaScript(string path)
        {
            if (browser == null)
            {
                return false;
            }

            Console.WriteLine("Sending " + path);
            string script = File.ReadAllText(path);

            var m = await browser.EvaluateScriptAsync(script);

            if (!m.Success)
            {
                Console.Error.WriteLine(m.Message);
            }
            return false;
        }

        public void Save()
        {
            data.location = Location;
            data.size = Size;
            data.url = browser.Address;

            string json = JsonConvert.SerializeObject(data);

            string f = data.url.Replace("https://", "").Replace(".com", "").Replace("app.", "").Replace("www.", "");
            string[] j = f.Split('/');

            File.WriteAllText(Form1.PATH + @"\save\" + j[0] + ".json", json);
        }

        internal void Delete()
        {
            string f = data.url.Replace("https://", "").Replace(".com", "").Replace("app.", "").Replace("www.", "");
            string[] j = f.Split('/');

            File.Delete(Form1.PATH + @"\save\" + j[0] + ".json");

            Parent.Controls.Remove(this);
        }
    }
}
