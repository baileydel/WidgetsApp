using System;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WidgetsApp.src.controls
{
    public partial class ShortcutControl : UserControl
    {
        private Color outerColor;
        private Color innerColor;

        private string innerText;
        private string outerText;

        private Color innerTextColor;
        private int innerFontSize;

        private int state;
        private bool Elapsed;

        public Color OuterColor { get => outerColor; set { outerColor = value; Invalidate(); } }
        public Color InnerColor { get => innerColor; set { innerColor  = value; Invalidate(); }}
        public string InnerText { get => innerText; set { innerText = value; Invalidate(); } }
        public string OuterText { get => outerText; set { outerText = value; Invalidate(); } }
        public Color InnerTextColor { get => innerTextColor; set { innerTextColor = value; Invalidate(); } }
        public int InnerFontSize { get => innerFontSize; set { innerFontSize = value; Invalidate(); } }
        public int State { get => state; set => state = value; }

        public Image Icon { get; set; }

        public ShortcutControl()
        {
            InitializeComponent();
            InnerTextColor = Color.Black;
            InnerFontSize = 12;

            MouseEnter += (sender, e) => Hover_Event();
            MouseLeave += (sender, e) => Leave_Event();

            SettingsButton.MouseEnter += (sender, e) => Hover_Event();

            OuterColor = Color.FromArgb(40, 40, 40);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;

            int dimension = Math.Min(this.Width, this.Height);
            int outerCircleSize = (int)(dimension * 0.5);

            int outerCircleX = (this.Width - outerCircleSize) / 2;
            int centerYOffset = Math.Max(0, (this.Height - dimension) / 2) - dimension / 8;
            int outerCircleY = centerYOffset + dimension / 4;

            using (SolidBrush outerBrush = new SolidBrush(OuterColor))
            {
                graphics.FillEllipse(outerBrush, outerCircleX, outerCircleY, outerCircleSize, outerCircleSize);
            }

            int innerCircleSize = outerCircleSize * 2 / 3;
            int innerCircleX = outerCircleX + (outerCircleSize - innerCircleSize) / 2;
            int innerCircleY = outerCircleY + (outerCircleSize - innerCircleSize) / 2;

            if (Icon != null)
            {
                graphics.DrawImage(Icon, innerCircleX, innerCircleY, innerCircleSize, innerCircleSize);
            }
            else
            {
                using (SolidBrush innerBrush = new SolidBrush(InnerColor))
                {
                    graphics.FillEllipse(innerBrush, innerCircleX, innerCircleY, innerCircleSize, innerCircleSize);
                }

                using (Font innerFont = new Font("Arial", InnerFontSize))
                {
                    string inner = InnerText[0].ToString().ToUpper();

                    SizeF innerTextSize = graphics.MeasureString(inner, innerFont);
                    using (Brush innerTextBrush = new SolidBrush(InnerTextColor))
                    {
                        float innerTextX = innerCircleX + (innerCircleSize - innerTextSize.Width) / 2;
                        float innerTextY = innerCircleY + (innerCircleSize - innerTextSize.Height) / 2 + (InnerFontSize * 0.1f);

                        graphics.DrawString(inner, innerFont, innerTextBrush, innerTextX, innerTextY);
                    }
                }
            }

            float outerFontSize = Math.Max(this.Width, this.Height) * 0.06f;

            Font font = new Font("Arial", outerFontSize);

            SizeF outerTextSize = graphics.MeasureString(OuterText, font);
            using (Brush outerTextBrush = new SolidBrush(Color.White))
            {
                float outerTextX = outerCircleX + (outerCircleSize - outerTextSize.Width) / 2;
                float outerTextY = outerCircleY + outerCircleSize + outerCircleSize / 5; // Increase the offset

                graphics.DrawString(OuterText, font, outerTextBrush, outerTextX, outerTextY);
            }
        }

        private void Leave_Event()
        {
            BackColor = Color.FromArgb(32, 32, 32);
            Elapsed = false;

            HideTimer.Start();
            ShowTimer.Stop();
        }

        private void Hover_Event()
        {
            BackColor = Color.FromArgb(50, 50, 50);

            if (HideTimer.Enabled)
            {
                HideTimer.Stop();
            }

            if (state == 0)
            {
                if (Elapsed)
                {
                    SettingsButton.Show();
                }
                else
                {
                    ShowTimer.Start();
                }
            }
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(SettingsButton, new Point(0, SettingsButton.Height));
        }

        private void ShowTimer_Tick(object sender, EventArgs e)
        {
            SettingsButton.Show();
        }

        private void HideTimer_Tick(object sender, EventArgs e)
        {
            SettingsButton.Hide();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Parent.Parent is MainForm mainForm)
            {
                mainForm.EditShortcut(this);
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Parent.Parent is MainForm mainForm)
            {
                mainForm.RemoveShortcut(this);
            }
        }

        public WidgetData GetWidgetData()
        {
            return new WidgetData(OuterText, InnerText);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += new EventHandler((source, args) => {
                if (this.DesignMode)
                {
                    this.Invalidate();
                }
            });
        }
    }
}
