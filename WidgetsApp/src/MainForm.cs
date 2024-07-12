using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using System;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using WidgetsApp.src.controls;

namespace WidgetsApp
{
    public partial class MainForm : Form
    { 
        public static readonly string PATH = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string SAVEPATH = PATH + @"\save";
        public static readonly string SCRIPTPATH = PATH + @"\scripts";
        public static readonly string BROWSERPATH = PATH + @"\browser";
        public static readonly PrivateFontCollection privateFonts = new PrivateFontCollection();

        public MainForm()
        {
            MakePaths();
            InitializeComponent();

            privateFonts.AddFontFile(PATH + @"\OpenSans-Regular.ttf");

            InitializeCefSharp();
            LoadPrevious();
        }

        private void InitializeCefSharp()
        {
            CefSettingsBase settings = new CefSettings
            {
                CachePath = BROWSERPATH
            };

            settings.CefCommandLineArgs.Add("enable-persistent-cookies", "1");

            CefSharpSettings.ConcurrentTaskExecution = true;
            CefSharpSettings.ShutdownOnExit = true;
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
                AddShortcut(JsonConvert.DeserializeObject<WidgetData>(json));
            }
        }

        public void CreateShortcut(WidgetData data)
        {
            AddShortcut(data);
            SaveShortcut(data, null);
        }

        public void AddShortcut(WidgetData data)
        {
            ShortcutControl control = new ShortcutControl()
            {
                OuterText = data.Name,
                InnerText = data.Url,
                InnerColor = data.Color
            };

            control.MouseClick += (sender, e) => LaunchShortcut(data);
      
            FlowPanel.Controls.Add(control);
            FlowPanel.Controls.SetChildIndex(AddShortcutControl, FlowPanel.Controls.Count - 1);
        }

        public void EditShortcut(ShortcutControl shortcutControl)
        {
            FlowPanel.Hide();
            UserControl shortcut = new ShortcutForm(shortcutControl);
            Controls.Add(shortcut);
        }

        public void RemoveShortcut(ShortcutControl control)
        {
            FlowPanel.Controls.Remove(control);
            DeleteShortcut(control);
        }

        public void DeleteShortcut(ShortcutControl control)
        {
            if (File.Exists(SAVEPATH + @"\" + control.OuterText + ".json"))
            {
                File.Delete(SAVEPATH + @"\" + control.OuterText + ".json");
            }
        }

        public void SaveShortcut(WidgetData data, ShortcutControl edit)
        {
            if (edit != null)
            {
                DeleteShortcut(edit);
            }

            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(SAVEPATH + @"\" + data.Name + ".json", json);
        }
 
        public void LaunchShortcut(WidgetData data)
        {
            Console.WriteLine("Launching shortcut");
            WidgetForm form = new WidgetForm(data);
            form.Show();
        }

        public void HideFlow(bool b)
        {
            if (b)
            {
                FlowPanel.Hide();
            }
            else
            {
                FlowPanel.Show();
            }
        }

        private void AddShortcutControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                FlowPanel.Hide();
                UserControl shortcut = new ShortcutForm();
                Controls.Add(shortcut);
            }
        }
    }
}
