using System.Windows.Forms;
using System.Drawing;
using System;
using CefSharp;

namespace WidgetsApp.src.controls
{
    internal class WidgetPanelController
    {
        private WidgetPanel parent;


        private Button closeButton;
        private Button lockButton;
        private Button stayOnTopButton;

        private TextBox urlBox;

        int height = 40;

        bool Dragging = false;
        Point DragStart = Point.Empty;

        public WidgetPanelController(WidgetPanel parent) { 
            this.parent = parent;
            Size size = new Size(25,25);

            closeButton = new Button()
            {
                Size = size,
                Location = new Point(parent.Width - 40, 5),
                BackColor = Color.Red
            };

            closeButton.Click += (sender, args) =>
            {
                parent.Parent.Controls.Remove(parent);
            };

            lockButton = new Button()
            {
                Size = size,
                Location = new Point(closeButton.Left - 40, 5),
                BackColor = Color.Aqua
            };

            lockButton.Click += (sender, args) =>
            {
                parent.Editable =  !parent.Editable;

                if (parent.Editable)
                {
                    lockButton.BackColor = Color.Lime;
                    parent.browser.Top = 40;
                }
                else
                {
                    lockButton.BackColor = Color.Aqua ;
                    parent.browser.Top = 0;
                }
            

            };

            stayOnTopButton = new Button()
            {
                Size = size,
                Location = new Point(lockButton.Left - 40, 5),
                BackColor = Color.White
            };

            urlBox = new TextBox()
            {
                Size = new Size(parent.Width - 160, 40),
                Location = new Point(10, 10),
            };


            parent.Controls.Add(closeButton);
            parent.Controls.Add(lockButton);
            parent.Controls.Add(stayOnTopButton);
            parent.Controls.Add(urlBox);

            lockButton.BringToFront();
        }

        private void resize()
        {

        }

        public void show()
        {
            
        }

        public void hide()
        {

        }

        public void timerToggle()
        {

        }

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragStart = new Point(e.X, e.Y);
        }

        private void panel_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging)
            {
                parent.Left = Math.Max(0, e.X + parent.Left - DragStart.X);
                parent.Top = Math.Max(0, e.Y + parent.Top - DragStart.Y);
            }
        }
    }
}
