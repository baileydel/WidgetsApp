using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Windows.Forms;
using WidgetsApp.src.controls;
using WidgetsApp.src.Handlers;
using WidgetsApp.src.Util;

namespace WidgetsApp
{
    public partial class MainForm : Form
    { 
        public static readonly PrivateFontCollection privateFonts = new PrivateFontCollection();
        private readonly FileManager FileManager = new FileManager();
        private readonly CefSharpManager CefSharpManager = new CefSharpManager();
        public readonly List<string> URLS = new List<string>(); 

        public MainForm()
        {
            privateFonts.AddFontFile(FileManager.PATH + @"\OpenSans-Regular.ttf");
            InitializeComponent();
            LoadPrevious();
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
            FileManager.Save(data);
        }

        public void AddShortcut(WidgetData data)
        {
            URLS.Add(data.Url);

            ShortcutControl control = new ShortcutControl(data);

            control.MouseClick += (sender, e) => LaunchShortcut(data);
      
            FlowPanel.Controls.Add(control);
            FlowPanel.Controls.SetChildIndex(AddShortcutControl, FlowPanel.Controls.Count - 1);
        }

        public void EditShortcut(ShortcutControl shortcutControl)
        {
            FlowPanel.Hide();
            Controls.Add(new ShortcutForm(shortcutControl));
        }

        public void RemoveShortcut(ShortcutControl control)
        {
            FlowPanel.Controls.Remove(control);

            URLS.Remove(control.Data.Url);

            FileManager.Delete(control.Data);
        }

        public void SaveShortcut(WidgetData data)
        {
            FileManager.Save(data);
        }
 
        public void LaunchShortcut(WidgetData data)
        {
            if (!CefSharpManager.IsInitialized())
            {
                CefSharpManager.Initialize();
            }

            Console.WriteLine("Launching shortcut");
            //TODO Store widgetform in a list
            new WidgetForm(data);
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

        public bool ContainsURL(string url)
        {
            return URLS.Contains(url);
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
    }
}
