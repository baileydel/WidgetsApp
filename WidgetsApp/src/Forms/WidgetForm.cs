
using CefSharp.WinForms;
using System;
using System.Windows.Forms;


namespace WidgetsApp
{
    public partial class WidgetForm : Form
    {
        private readonly ChromiumWebBrowser browser;

        public WidgetForm(WidgetData data)
        {
            InitializeComponent();

            Location = data.Location;
            Size = data.Size;

            browser = new ChromiumWebBrowser(data.Url);

            Controls.Add(browser);

            this.Show();
        }
        private void WidgetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (browser != null)
            {
                browser.Dispose();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
        }
    }
}
