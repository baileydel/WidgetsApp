using System.Windows.Forms;
using System.Drawing;
using System;

namespace WidgetsApp.src.controls
{
    internal class WidgetPanelController
    {
        private WidgetPanel parent;

        private Button closeButton;
        private Button lockButton;
        private Button stayOnTopButton;
        private Timer Timer;

        private TextBox urlBox;
        private bool locked = false;

        public WidgetPanelController(WidgetPanel parent) { 
            this.parent = parent;
            Size size = new Size(25,25);

            parent.Resize += (sender, args) =>
            {
                resize();
            };

            closeButton = new Button()
            {
                Size = size,
                BackColor = Color.Red
            };

            closeButton.Click += (sender, args) =>
            {
                parent.Close();
            };

            lockButton = new Button()
            {
                Size = size,
                BackColor = Color.Aqua
            };

            lockButton.Click += (sender, args) =>
            {
                locked = !locked;
            };

            stayOnTopButton = new Button()
            {
                Size = size,
                BackColor = Color.White
            };

            urlBox = new TextBox()
            {
                Location = new Point(10, 10),
            };

            urlBox.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    parent.browser.Load(urlBox.Text);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };

            parent.browser.FrameLoadEnd += (sender, e) =>
            {
                Action safeWrite = null;

                safeWrite = delegate
                {
                    urlBox.Text = parent.browser.Address;
                };

                if (safeWrite != null)
                {
                    parent.Invoke(safeWrite);
                }
            };


            resize();

            parent.Controls.Add(closeButton);
            parent.Controls.Add(lockButton);
            parent.Controls.Add(stayOnTopButton);
            parent.Controls.Add(urlBox);
        }

        public void show(bool b)
        {
            parent.Editable = b;

            if (parent.Editable)
            {
                lockButton.BackColor = Color.Lime;
                parent.browser.Location = new Point(1, 40);
                parent.browser.Size = new Size(parent.Width - 2, parent.Height - 42);  
            }
            else
            {
                lockButton.BackColor = Color.Aqua;
                parent.browser.Location = new Point(1, 1);
                parent.browser.Size = new Size(parent.Width - 2, parent.Height - 2);
            }
        }

        private void resize()
        {
            closeButton.Location = new Point(parent.Width - 40, 5);
            lockButton.Location = new Point(closeButton.Left - 40, 5);
            stayOnTopButton.Location = new Point(lockButton.Left - 40, 5);
            urlBox.Size = new Size(parent.Width - 160, 40);
            parent.browser.Size = new Size(parent.Width - 2, parent.Height - 2);
        }
    }
}
