using System.Drawing;

namespace WidgetsApp.src.controls
{
    internal class WidgetData
    {
        public Point location;
        public Size size;
        public string url;

        public WidgetData(Size size, Point location, string url)
        {
            this.size = size;
            this.location = location;
            this.url = url;
        }



        
    }
}
