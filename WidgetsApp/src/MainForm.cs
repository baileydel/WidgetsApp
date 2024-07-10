﻿using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using System;
using System.Drawing;
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

        public MainForm()
        {
            MakePaths();
            InitializeComponent();

            shortcutControl2.state = 1;
            shortcutControl2.OuterText = "Add shortcut";
            shortcutControl2.InnerText = "+";
            shortcutControl2.MouseClick += ShortcutButton_Click;

            shortcutControl2.OuterColor = Color.FromArgb(0, 74, 119);
            shortcutControl2.InnerColor = Color.FromArgb(0, 74, 119);    

            shortcutControl2.InnerTextColor = Color.White;
            shortcutControl2.InnerFontSize = 24;


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
                AddShortcut(JsonConvert.DeserializeObject<WidgetData>(json));
            }
        }

        private void ShortcutButton_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Hide();

            UserControl shortuct = new ShortcutForm();
            this.Controls.Add(shortuct);
        }

        public void AddShortcut(WidgetData data)
        {
            ShortcutControl control = new ShortcutControl()
            {
                OuterText = data.Name,
            };

            control.MouseClick += (sender, e) => LaunchShortcut(data);
      
            flowLayoutPanel1.Controls.Add(control);
            flowLayoutPanel1.Controls.SetChildIndex(shortcutControl2, flowLayoutPanel1.Controls.Count - 1);
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
                flowLayoutPanel1.Hide();
            }
            else
            {
                flowLayoutPanel1.Show();
            }
        }

        private void import(object sender, EventArgs e)
        {
            /**
             * 
            DialogResult r = openFileDialog1.ShowDialog();

            if (r == DialogResult.OK)
            {
                openFileDialog1.CheckFileExists = true;
                string json = File.ReadAllText(openFileDialog1.FileName);
                WidgetData data = JsonConvert.DeserializeObject<WidgetData>(json);

                File.Delete(openFileDialog1.FileName);
            }
            */
        }

    }
}
