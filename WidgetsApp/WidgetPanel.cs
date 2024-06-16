using CefSharp;
using CefSharp.WinForms;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WidgetsApp
{
    internal class WidgetPanel : ResizablePanel
    {
        private ChromiumWebBrowser browser;

        private Button editButton;
        private Button closeButton;
        private System.Windows.Forms.Timer timer;

        public WidgetPanel()
        {
            InitializeComponent();
            InitializeChromium();
        }

        public WidgetPanel(string url) : this() { }

        private void InitializeComponent()
        {
            this.Editable = false;
            this.Location = new Point(0, 120);

            timer = new System.Windows.Forms.Timer()
            {
                Interval = 1500
            };

            timer.Tick += (sender, e) =>
            {
                timer.Stop();

                if (editButton.Visible)
                {
                    editButton.Hide();
                    closeButton.Hide();
                }
                else
                {
                    editButton.Show();
                    closeButton.Show();
                }
            };

            closeButton = new Button()
            {
                Size = new Size(20, 20),
                BackColor = Color.Red,
                Location = new Point(this.Width - 20, 0),
                Visible = false
            };

            closeButton.Click += (sender, e) =>
            {
                this.Parent.Controls.Remove(this);
                browser = null;
            };

            closeButton.MouseHover += hm;

            editButton = new Button()
            {
                Size = new Size(20, 20),
                BackColor = Color.Teal,
                Location = new Point(closeButton.Left - 20, 0),
                Visible = false
            };

            editButton.Click += (sender, e) =>
            {
                browser.ShowDevTools();
                Edit();
            };

            editButton.MouseHover += hm;



            this.Controls.Add(editButton);
            this.Controls.Add(closeButton);
        }

        private void InitializeChromium()
        {
            browser = new ChromiumWebBrowser("https://app.rocketmoney.com/");

            browser.FrameLoadEnd += Browser_FrameLoadEnd;
            browser.JavascriptMessageReceived += Browser_JavascriptMessageReceived;

            browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            browser.JavascriptObjectRepository.Register("CefSharpHandler", new CefSharpHandler(browser), isAsync: true);



            this.Controls.Add(browser);
        }


        private async void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
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
                                timer.Enabled = property;

                                if (editButton.Visible)
                                {
                                    timer.Enabled = !property;
                                }
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
            Thread.Sleep(1000);
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

            if (this.Editable)
            {


            }
        }

        // Only to be activated when mouseover button
        private void hm(object sender, EventArgs e)
        {
            if (editButton.Visible && timer.Enabled)
            {
                timer.Stop();
            }
        }
    }
}
