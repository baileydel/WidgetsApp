using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WidgetsApp.src.controls;

namespace WidgetsApp
{
    public partial class Form1 : Form
    {
        public static readonly string PATH = AppDomain.CurrentDomain.BaseDirectory;

        public Form1()
        {
            InitializeComponent();

            BackColor = Color.LimeGreen;
            TransparencyKey = BackColor;

            CefSettingsBase settings = new CefSettings
            {
                CachePath = PATH + @"\browser"
            };

            CefSharpSettings.ConcurrentTaskExecution = true;
            CefSharpSettings.ShutdownOnExit = true;

            settings.CefCommandLineArgs.Add("enable-persistent-cookies", "1");

            Cef.Initialize(settings);

            loadPrevious();
        }

        private void loadPrevious()
        {
            string savepath = PATH + @"\save";
            string[] files = Directory.GetFiles(savepath);

            foreach (string file in files)
            {
                string json = File.ReadAllText(file);
                WidgetData data = JsonConvert.DeserializeObject<WidgetData>(json);

                Controls.Add(new WidgetPanel(data));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Panel pane = new WidgetPanel(null);
            Controls.Add(pane);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult r = openFileDialog1.ShowDialog();

            if (r == DialogResult.OK)
            {
                openFileDialog1.CheckFileExists = true;      
                string json = File.ReadAllText(openFileDialog1.FileName);
                WidgetData data = JsonConvert.DeserializeObject<WidgetData>(json);

                File.Delete(openFileDialog1.FileName);

                WidgetPanel panel = new WidgetPanel(data);
                panel.Save();

                Controls.Add(panel);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Control panel in Controls)
            {
                if (panel is WidgetPanel)
                {
                    ((WidgetPanel)panel).Save();
                }
            }
        }
    }
}
