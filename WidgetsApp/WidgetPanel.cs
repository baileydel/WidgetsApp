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

        public WidgetPanel(string url) : this()
        {

        }

        private void Edit()
        {
            this.Editable = !Editable;

            if (this.Editable)
            {


            }
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
                                CefSharp.PostMessage({Type: ""Hover"", Data: b});
                            }
                        }

                        document.addEventListener('mousemove', func);
                   ";


                await browser.EvaluateScriptAsync(script);
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

                if (this.InvokeRequired)
                {
                    this.Invoke(safeWrite);
                }
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
                Edit();
            };

            editButton.MouseHover += hm;

            this.Controls.Add(editButton);
            this.Controls.Add(closeButton);
        }

        private void InitializeChromium()
        {
            CefSharpSettings.ConcurrentTaskExecution = true;
            browser = new ChromiumWebBrowser("https://app.rocketmoney.com/");
            browser.FrameLoadEnd += Browser_FrameLoadEnd;
            browser.JavascriptMessageReceived += Browser_JavascriptMessageReceived;

            // Register the JavaScript object and bind it to C#
            browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            browser.JavascriptObjectRepository.Register("CefSharpHandler", new CefSharpHandler(browser), isAsync: true);

            this.Controls.Add(browser);
        }
    }
}
