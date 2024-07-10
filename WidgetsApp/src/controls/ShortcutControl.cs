using System;

using System.Drawing;
using System.Drawing.Drawing2D;

using System.Windows.Forms;

namespace WidgetsApp.src.controls
{
    public partial class ShortcutControl : UserControl
    {
        public Color OuterColor;
        public Color InnerColor;
        private bool Elapsed;

        public string InnerText;
        public string OuterText;

        public Color InnerTextColor;

        public int InnerFontSize;

        public int state;

        public ShortcutControl()
        {
            InitializeComponent();

            OuterText = "Outer: ";
            InnerText = OuterText[0].ToString().ToUpper();

            InnerTextColor = Color.Black;

            InnerFontSize = 12;

            Width = 120;
            Height = 120;

            Elapsed = false;
            state = 0;

            MouseEnter += (sender, e) => Hover_Event();
            MouseLeave += (sender, e) => Leave_Event();

            SettingsButton.MouseEnter += (sender, e) => Hover_Event();

            Random random = new Random();
            InnerColor = Color.FromArgb(random.Next(150, 256), random.Next(150, 256), random.Next(150, 256));
            OuterColor = Color.FromArgb(40, 40, 40);
        }   

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;

            int dimension = Math.Min(this.Width, this.Height); // Use the smaller dimension
            int outerCircleSize = (int)(dimension * 0.5);

            int outerCircleX = (this.Width - outerCircleSize) / 2;
            int centerYOffset = Math.Max(0, (this.Height - dimension) / 2) - dimension / 8; // Offset both circles up
            int outerCircleY = centerYOffset + dimension / 4;

            using (SolidBrush outerBrush = new SolidBrush(OuterColor))
            {
                graphics.FillEllipse(outerBrush, outerCircleX, outerCircleY, outerCircleSize, outerCircleSize);
            }

            int innerCircleSize = outerCircleSize * 2 / 3;
            int innerCircleX = outerCircleX + (outerCircleSize - innerCircleSize) / 2;
            int innerCircleY = outerCircleY + (outerCircleSize - innerCircleSize) / 2;

            using (SolidBrush innerBrush = new SolidBrush(InnerColor))
            {
                graphics.FillEllipse(innerBrush, innerCircleX, innerCircleY, innerCircleSize, innerCircleSize);
            }

            Font innerFont = new Font("Arial", InnerFontSize);

            SizeF innerTextSize = graphics.MeasureString(InnerText, innerFont);
            Brush innerTextBrush = new SolidBrush(InnerTextColor);

            float innerTextX = innerCircleX + (innerCircleSize - innerTextSize.Width) / 2;
            float innerTextY = innerCircleY + (innerCircleSize - innerTextSize.Height) / 2 + (InnerFontSize * 0.1f);

            graphics.DrawString(InnerText, innerFont, innerTextBrush, innerTextX, innerTextY);


            float outerFontSize = Math.Max(this.Width, this.Height) * 0.06f; // Adjust the multiplier for desired font size
            Font outerFont = new Font("Arial", outerFontSize);
            SizeF outerTextSize = graphics.MeasureString(OuterText, outerFont);
            Brush outerTextBrush = new SolidBrush(Color.White);

            float outerTextX = outerCircleX + (outerCircleSize - outerTextSize.Width) / 2;
            float outerTextY = outerCircleY + outerCircleSize + outerCircleSize / 5; // Increase the offset

            graphics.DrawString(OuterText, outerFont, outerTextBrush, outerTextX, outerTextY);
        }

        private void Leave_Event()
        {
            this.BackColor = Color.FromArgb(32, 32, 32);
            Elapsed = false;

            HideTimer.Start();
            ShowTimer.Stop();
        }

        private void Hover_Event()
        {
            this.BackColor = Color.FromArgb(50, 50, 50);

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

        private void button1_Click(object sender, EventArgs e)
        {
            // create dropdown menu
            Console.WriteLine("Button clicked");
        }

        private void ShowTimer_Tick(object sender, EventArgs e)
        {
            SettingsButton.Show();
        }

        private void HideTimer_Tick(object sender, EventArgs e)
        {
            SettingsButton.Hide();
        }
    }
}
