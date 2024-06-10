using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WidgetsApp
{
    internal class WidgetPanel : ResizablePanel
    {
        private Button editButton;
        private Button closeButton;

        public WidgetPanel() {
            this.Editable = false;

            closeButton = new Button()
            {
                Size = new Size(20, 20),
                BackColor = Color.Red,
                Location = new Point(this.Width - 20, 0)
            };

            closeButton.Click += (sender, e) =>
            {
                this.Parent.Controls.Remove(this);
            };

            editButton = new Button()
            {
                Size = new Size(20, 20),
                BackColor = Color.Teal,
                Location = new Point(closeButton.Left - 20, 0)
            };

            editButton.Click += (sender, e) =>
            {
                Edit();
            };

 

            this.Controls.Add(editButton);
            this.Controls.Add(closeButton);
        }

        private void Edit()
        {
            this.Editable = !Editable;

            if (this.Editable)
            {


            }
        }
    }
}
