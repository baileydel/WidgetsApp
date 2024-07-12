using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using System;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using WidgetsApp.src.controls;
using WidgetsApp.src.Util;

namespace WidgetsApp
{
    public partial class MainForm : Form
    { 
        public static readonly PrivateFontCollection privateFonts = new PrivateFontCollection();

        private static readonly FileManager FileManager = new FileManager();

        public MainForm()
        {
            InitializeComponent();

            privateFonts.AddFontFile(FileManager.PATH + @"\OpenSans-Regular.ttf");

            InitializeCefSharp();
            LoadPrevious();
        }

        private void InitializeCefSharp()
        {
            CefSettingsBase settings = new CefSettings
            {
                CachePath = FileManager.BROWSERPATH
            };

            settings.CefCommandLineArgs.Add("enable-persistent-cookies", "1");

            CefSharpSettings.ConcurrentTaskExecution = true;
            CefSharpSettings.ShutdownOnExit = true;
            Cef.Initialize(settings);
        }

        #region ShortcutManagement

        private void LoadPrevious()
        {
           foreach (WidgetData data in FileManager.GetShortcutSaves())
            {
                AddShortcut(data);
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
                OuterText  = data.Name,
                InnerText  = data.Url,
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
            FileManager.Delete(control.OuterText); 
        }

        public void SaveShortcut(WidgetData data, ShortcutControl edit)
        {
            if (edit != null)
            {
                DeleteShortcut(edit);
            }

            FileManager.Save(data);
        }
 
        public void LaunchShortcut(WidgetData data)
        {
            Console.WriteLine("Launching shortcut");
            WidgetForm form = new WidgetForm(data);
            form.Show();
        }

        #endregion

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
