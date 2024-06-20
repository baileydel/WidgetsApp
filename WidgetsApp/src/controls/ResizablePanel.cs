using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WidgetsApp
{
    internal class ResizablePanel : Panel
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        bool Dragging = false;
        Point DragStart = Point.Empty;

        bool Resizing = false;

        bool MouseIsInLeftEdge = false;
        bool MouseIsInRightEdge = false;
        bool MouseIsInTopEdge = false;
        bool MouseIsInBottomEdge = false;

        Point CursorStartPoint = new Point();
        Size CurrentStartSize = new Size();

        public bool Editable = true;

        public ResizablePanel()
        {
            this.SuspendLayout();
            this.BackColor = Color.Black;
            this.ForeColor = SystemColors.ControlLightLight;
            this.Size = new Size(600, 400);
            this.BorderStyle = BorderStyle.None;
            Region = Region.FromHrgn(CreateRoundRectRgn(this.Left, this.Top, this.Width, this.Height, 20, 20));
            this.ResumeLayout(false);

            this.MouseDown += panel_MouseDown;
            this.MouseUp += panel_MouseUp;
            this.MouseMove += panel_MouseMove;
        }

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseIsInLeftEdge || MouseIsInRightEdge || MouseIsInTopEdge || MouseIsInBottomEdge)
            {
                Resizing = true;
                CursorStartPoint = new Point(e.X, e.Y);
                CurrentStartSize = this.Size;
            }
            else
            {
                Dragging = true;
                DragStart = new Point(e.X, e.Y);
            }
        }

        private void panel_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
            Resizing = false;
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (!Editable)
            {
                return;
            }

            if (!Dragging && !Resizing)
            {
                MouseIsInLeftEdge = Math.Abs(e.X) <= 5;
                MouseIsInRightEdge = Math.Abs(e.X - this.Width) <= 5;
                MouseIsInTopEdge = Math.Abs(e.Y) <= 5;
                MouseIsInBottomEdge = Math.Abs(e.Y - this.Height) <= 5;
            }

            if (Resizing)
            {
                if (MouseIsInLeftEdge)
                {
                    this.Width -= (e.X - CursorStartPoint.X);
                    this.Left += (e.X - CursorStartPoint.X);
                }
                else if (MouseIsInRightEdge)
                {
                    this.Width = (e.X - CursorStartPoint.X) + CurrentStartSize.Width;
                }

                if (MouseIsInTopEdge)
                {
                    this.Height -= (e.Y - CursorStartPoint.Y);
                    this.Top += (e.Y - CursorStartPoint.Y);
                }
                else if (MouseIsInBottomEdge)
                {
                    this.Height = (e.Y - CursorStartPoint.Y) + CurrentStartSize.Height;
                }
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            }


            if (MouseIsInLeftEdge || MouseIsInRightEdge)
            {
                if (MouseIsInTopEdge)
                {
                    this.Cursor = MouseIsInLeftEdge ? Cursors.SizeNWSE : Cursors.SizeNESW;
                }
                else if (MouseIsInBottomEdge)
                {
                    this.Cursor = MouseIsInLeftEdge ? Cursors.SizeNESW : Cursors.SizeNWSE;
                }
                else
                {
                    this.Cursor = Cursors.SizeWE;
                }
            }
            else if (MouseIsInTopEdge || MouseIsInBottomEdge)
            {
                this.Cursor = Cursors.SizeNS;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }

            if (Dragging && !Resizing)
            {
                this.Left = Math.Max(0, e.X + this.Left - DragStart.X);
                this.Top = Math.Max(0, e.Y + this.Top - DragStart.Y);
            }
        }
    }
}
