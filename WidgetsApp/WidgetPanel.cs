using CefSharp;
using CefSharp.WinForms;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace WidgetsApp
{
    internal class WidgetPanel : ResizablePanel
    {
        private ChromiumWebBrowser browser;

        private Button editButton;
        private Button closeButton;
        private System.Windows.Forms.Timer timer;

        public WidgetPanel() {
            this.Editable = false;
            this.Location = new Point(0, 120);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1500;
            timer.Tick += timer1_Tick;

            closeButton = new Button()
            {
                Size = new Size(20, 20),
                BackColor = Color.Red,
                Location = new Point(this.Width - 20, 0)
            };

            closeButton.Click += (sender, e) =>
            {
                this.Parent.Controls.Remove(this);
            };

            editButton = new Button()
            {
                Size = new Size(20, 20),
                BackColor = Color.Teal,
                Location = new Point(closeButton.Left - 20, 0)
            };

            editButton.Click += (sender, e) =>
            {
                Edit();
            };

            editButton.MouseHover += hm;
            closeButton.MouseHover += hm;
           
            editButton.Hide();
            closeButton.Hide();

            this.Controls.Add(editButton);
            this.Controls.Add(closeButton);

            browser = new ChromiumWebBrowser("https://app.rocketmoney.com/");
            browser.FrameLoadEnd += Browser_FrameLoadEnd;
            browser.JavascriptMessageReceived += Browser_JavascriptMessageReceived;

            this.Controls.Add(browser);
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

        private void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                browser.ExecuteScriptAsync(@"
                        document.addEventListener('mousemove', function(e) {
                            if (e.clientX > 560 && e.clientY < 20) {
                                CefSharp.PostMessage(""HoverTrue"")
                            }
                            else {
                                CefSharp.PostMessage(""HoverFalse"")
                            }
                        }); 
                 ");
            }
        }

        private void Browser_JavascriptMessageReceived(object sender, JavascriptMessageReceivedEventArgs e)
        {
            if (e.Message != null)
            {    
                string message = e.Message.ToString();

                if (message == "HoverTrue" ||  message == "HoverFalse")
                {
                    Action safeWrite;
                    if (message == "HoverFalse")
                    {
                        safeWrite = delegate
                        {
                            if (editButton.Visible)
                            {
                                timer.Start();
                            }
                            else
                            {
                                // Timer could already be started from just hovering, stop it
                                timer.Stop();
                            }
                        };
                    }
                    else
                    {
                        safeWrite = delegate
                        {
                            if (!editButton.Visible)
                            {
                                timer.Start();
                            }
                        };
                    }

                    if (this.InvokeRequired)
                    {
                        this.Invoke(safeWrite);
                    }
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

        private void timer1_Tick(object sender, EventArgs e)
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
        }
    }
}
