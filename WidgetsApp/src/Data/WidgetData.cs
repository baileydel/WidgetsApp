using Newtonsoft.Json;
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

        public WidgetData(Size size, Point location, string url)
        {
            Name = url; 
            Size = size;
            Location = location;
            Url = url;
        }

        public WidgetData(string name, string url, Color color)
        {
            Name = name;
            Url = url;
            Size = new Size(526, 337);
            Location = new Point(0, 0);
            Color = color;
        }

        public WidgetData(string name, string url)
        {
            Name = name;
            Url = url;
            Size = new Size(526, 337);
            Location = new Point(0, 0);
        }
    }
}
