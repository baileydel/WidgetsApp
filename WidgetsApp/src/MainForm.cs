using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WidgetsApp
{
    public partial class MainForm : Form
    { 
        public static readonly string PATH = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string SAVEPATH = PATH + @"\save";
        public static readonly string SCRIPTPATH = PATH + @"\scripts";
        public static readonly string BROWSERPATH = PATH + @"\browser";

        public MainForm()
        {
            MakePaths();
            InitializeComponent();
            InitializeCefSharp();
            LoadPrevious();
        }

        private void InitializeCefSharp()
        {
            CefSettingsBase settings = new CefSettings
            {
                CachePath = BROWSERPATH
            };

            CefSharpSettings.ConcurrentTaskExecution = true;
            CefSharpSettings.ShutdownOnExit = true;

            settings.CefCommandLineArgs.Add("enable-persistent-cookies", "1");

            Cef.Initialize(settings);
        }

        private void MakePaths()
        {
            if (!Directory.Exists(SAVEPATH))
            {
                Directory.CreateDirectory(SAVEPATH);
            }

            if (!Directory.Exists(SCRIPTPATH))
            {
                Directory.CreateDirectory(SCRIPTPATH);
            }

            if (!Directory.Exists(BROWSERPATH))
            {
                Directory.CreateDirectory(BROWSERPATH);
            }
        }

        private void LoadPrevious()
        {
            string[] files = Directory.GetFiles(SAVEPATH);

            foreach (string file in files)
            {
                string json = File.ReadAllText(file);
                WidgetData data = JsonConvert.DeserializeObject<WidgetData>(json);

                CreateChild(data);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
        
            CreateChild(null);
        }


        private void CreateChild(WidgetData data)
        {
            WidgetForm form = new WidgetForm(data);
            form.Show();
        }

        private void import(object sender, EventArgs e)
        {
            DialogResult r = openFileDialog1.ShowDialog();

            if (r == DialogResult.OK)
            {
                openFileDialog1.CheckFileExists = true;
                string json = File.ReadAllText(openFileDialog1.FileName);
                WidgetData data = JsonConvert.DeserializeObject<WidgetData>(json);

                File.Delete(openFileDialog1.FileName);
            }
        }
    }
}
