using System;
using System.Drawing;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace WidgetsApp
{
    internal class ResizablePanel : Panel
    {
        //moving part
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
            this.ResumeLayout(false);

            this.MouseDown += delegate (object sender, MouseEventArgs e)
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
            };

            this.MouseUp += delegate (object sender, MouseEventArgs e)
            {
                Dragging = false;
                Resizing = false;
            };

            this.MouseMove += delegate (object sender, MouseEventArgs e)
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
                        this.Height = (e.Y - CursorStartPoint.Y)  + CurrentStartSize.Height;
                    }
                }


                if (MouseIsInLeftEdge)
                {
                    if (MouseIsInTopEdge)
                    {
                        this.Cursor = Cursors.SizeNWSE;
                    }
                    else if (MouseIsInBottomEdge)
                    {
                        this.Cursor = Cursors.SizeNESW;
                    }
                    else
                    {
                        this.Cursor = Cursors.SizeWE;
                    }
                }
                else if (MouseIsInRightEdge)
                {
                    if (MouseIsInTopEdge)
                    {
                        this.Cursor = Cursors.SizeNESW;
                    }
                    else if (MouseIsInBottomEdge)
                    {
                        this.Cursor = Cursors.SizeNWSE;
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
            };
        }
    }
}
