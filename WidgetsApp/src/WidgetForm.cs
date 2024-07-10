using CefSharp.WinForms;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace WidgetsApp
{
    public partial class WidgetForm : Form
    {
        private ChromiumWebBrowser browser;

        public WidgetForm(WidgetData data)
        {
            InitializeComponent();
            if (data == null)
            {
                data = new WidgetData(new Size(526, 337), new Point(0, 0), "https://youtube.com");
            }

            Location = data.Location;
            Size = data.Size;

            browser = new ChromiumWebBrowser(data.Url);

            Controls.Add(browser);
        }

        private void Save()
        {
            WidgetData data = new WidgetData(Size, Location, browser.Address);

            string json = JsonConvert.SerializeObject(data);

            string f = data.Url.Replace("https://", "").Replace(".com", "").Replace("app.", "").Replace("www.", "");
            string[] j = f.Split('/');

            File.WriteAllText(MainForm.PATH + @"\save\" + j[0] + ".json", json);
        }

        private void WidgetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save();

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
