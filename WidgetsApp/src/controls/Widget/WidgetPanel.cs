using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
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

        public WidgetPanel()
        {
            InitializeComponents();
            InitializeChromium();

            this.data = new WidgetData(this.Size, this.Location, "");
            controller = new WidgetPanelController(this);
        }

        public WidgetPanel(WidgetData data) : this() {
            this.data = data;
            this.Size = data.size;
            this.Location = data.location;
        }

        private void InitializeComponents()
        {
            this.Editable = false;
            this.Location = new Point(0, 120);
        }

        private void InitializeChromium()
        {
            browser = new ChromiumWebBrowser("https://app.rocketmoney.com/");
            browser.Dock = DockStyle.None;
            browser.Size = new Size(this.Width + 60, this.Height);

            browser.FrameLoadEnd += Browser_FrameLoadEnd;
            browser.JavascriptMessageReceived += Browser_JavascriptMessageReceived;

            browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            browser.JavascriptObjectRepository.Register("CefSharpHandler", new CefSharpHandler(browser), isAsync: true);

            this.Controls.Add(browser);
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
                   @"
                        let b = false;
                        let func = function(e) {
                            let o = e.clientX > 560 && e.clientY < 20;

                            if (b !== o) {
                                b = o;
                                CefSharp.PostMessage({Type: 'Hover', Data: b});
                            }
                        }

                        document.addEventListener('mousemove', func);
                   ";

                await browser.EvaluateScriptAsync(script);

                string f = e.Url.Replace("https://", "").Replace(".com", "");
                string[] j = f.Split('/');

                string r = j[0].Replace('.', '\\').Replace("www.", "") + "\\";
                string p = @"C:\Users\Bailey\Desktop\WidgetsApp\scripts\" + r;
                
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
            var property = msg.Data;
            //var callback = (IJavascriptCallback)msg.Callback;
            //callback.ExecuteAsync(type);

            if (msg != null)
            {
                Action safeWrite = null;
                switch (type)
                {
                    case "Hover":
                        {
                            safeWrite = delegate
                            {
                               
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

        private void Edit()
        {
            this.Editable = !Editable;

            Bitmap bmp = new Bitmap(browser.Width, browser.Height);
            Graphics g = Graphics.FromImage(bmp);

            if (this.Editable)
            {
                g.CopyFromScreen(PointToScreen(browser.Location), new Point(0, 0), browser.Size);

                var blur = new GaussianBlur(bmp);
                var result = blur.Process(10);

                Rectangle r = new Rectangle(0, 0, result.Width, result.Height);
                using (g = Graphics.FromImage(result))
                {
                    using (Brush cloud_brush = new SolidBrush(Color.FromArgb(128, Color.Black)))
                    {
                        g.FillRectangle(cloud_brush, r);
                    }
                }

                this.BackgroundImage = result;
                this.browser.Hide();
            }
            else
            {
                bmp.Dispose();
                g.Dispose();
                this.BackgroundImage.Dispose();
                this.BackgroundImage = null;
                this.browser.Show();
            }
        }


        public void save()
        {
            this.data.location = this.Location;
            this.data.size = this.Size;
            
            if (browser != null)
            {
          
            }

            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(@"C:\Users\Bailey\Desktop\WidgetsApp\save\window1.json", json);
        }
    }
}
