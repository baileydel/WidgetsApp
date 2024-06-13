using System;
using System.Drawing;
using System.Windows.Forms;

namespace WidgetsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.LimeGreen;
            this.TransparencyKey = BackColor;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Panel pane = new WidgetPanel();
            this.Controls.Add(pane);

        }
    }
}
