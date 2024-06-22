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

            string savepath = @"C:\Users\Bailey\Desktop\WidgetsApp\save";
            string[] files = Directory.GetFiles(savepath);

            foreach (string file in files)
            {
                string json = File.ReadAllText(file);
                WidgetData data = JsonConvert.DeserializeObject<WidgetData>(json);

                this.Controls.Add(new WidgetPanel(data));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control panel in this.Controls)
            {
                if (panel is WidgetPanel)
                {
                    ((WidgetPanel)panel).save();
                }
                
            }

            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Panel pane = new WidgetPanel();
            this.Controls.Add(pane);
        }
    }
}