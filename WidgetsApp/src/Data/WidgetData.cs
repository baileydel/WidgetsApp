﻿using Newtonsoft.Json;
using System;
using System.Drawing;

namespace WidgetsApp
{
    public class WidgetData
    {
        public string Name { get; set; }   
        public Point Location { get; set; }
        public Size Size { get; set; }
        public string Url { get; set; }
        public Color Color { get; set; }
        public string SavePath { get; set; }

        [JsonConstructor]
        public WidgetData(string name, Size size, Point location, string url, Color color)
        {
            Name = name;
            Size = size;
            Location = location;
            Url = url;
            Color = color;
        }

        public WidgetData(string name, string url)
        {
            Random random = new Random();
            Color = Color.FromArgb(random.Next(150, 256), random.Next(150, 256), random.Next(150, 256));
            Name = name;
            Url = url;
            Size = new Size(526, 337);
            Location = new Point(0, 0);
        }

        public string GetValidName()
        {
            string name = Name;
            name = name.Replace("http://", "").Replace("https://", "").Split('/')[0];
            return name;
        }

        public string GetValidURL()
        {
            string url = Url;
            url = url.Replace("http://", "").Replace("https://", "").Split('/')[0];
            return url;
        }
    }
}
