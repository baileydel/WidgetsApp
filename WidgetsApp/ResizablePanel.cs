using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WidgetsApp
{
    internal class ResizablePanel : Panel
    {

        public ResizablePanel()
        {
            this.BackColor = Color.Black;
            this.Size = new Size(600, 400);
        }
    }
}
