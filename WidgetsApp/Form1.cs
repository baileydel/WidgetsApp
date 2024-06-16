using CefSharp;
using CefSharp.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WidgetsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.LimeGreen;
            this.TransparencyKey = BackColor;

            CefSharpSettings.ConcurrentTaskExecution = true;
            CefSettingsBase settings = new CefSettings
            {
                CachePath = @"C:\Users\Bailey\Desktop\WidgetsApp\browser"
            };

            settings.CefCommandLineArgs.Add("enable-persistent-cookies", "1");
            Cef.Initialize(settings);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Panel pane = new WidgetPanel();
            this.Controls.Add(pane);

        }
    }
}
