using CefSharp.WinForms;
using System.Windows.Forms;

namespace WidgetsApp
{
    public partial class WidgetForm : Form
    {
        public WidgetForm()
        {
            InitializeComponent();

            ChromiumWebBrowser browser = new ChromiumWebBrowser("https://www.google.com");
            this.Controls.Add(browser);
        }
    }
}
