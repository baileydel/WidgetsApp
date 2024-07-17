using System;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Windows.Forms;
using WidgetsApp.src.Util;

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
        public Color InnerColor { get => innerColor; set { innerColor = value; Invalidate(); } }
        public string InnerText { get => innerText; set { innerText = value; Invalidate(); } }
        public string OuterText { get => outerText; set { outerText = value; Invalidate(); } }
        public Color InnerTextColor { get => innerTextColor; set { innerTextColor = value; Invalidate(); } }
        public int InnerFontSize { get => innerFontSize; set { innerFontSize = value; Invalidate(); } }
        public int State { get => state; set => state = value; }

        public Image Icon { get; set; }
        public readonly WidgetData Data;

        public string HttpLike;
        public string BaseDomain;
        public string SubDomain;

        public ShortcutControl()
        {
            InitializeComponent();
        }

        public ShortcutControl(WidgetData data)
        {
            InitializeComponent();

            Data = data;

            string url = Data.Url;
            string[] parts = url.Split(new string[] { "//" }, StringSplitOptions.None);

            if (parts.Length > 1)
            {
                HttpLike = parts[0] + "//";
            }

            parts = parts[parts.Length - 1].Split('.');
            if (parts.Length > 2)
            {
                SubDomain = parts[0];
                BaseDomain = parts[parts.Length - 2] + "." + parts[parts.Length - 1];
            }
            else if (parts.Length == 1)
            {
                BaseDomain = parts[0];
            }
           
            InnerColor = data.Color;
            InnerText = BaseDomain[0].ToString().ToUpper();
            OuterText = data.Name;

            Icon = GetIcon(data.GetValidName());
            if (Icon == null)
            {
                Task task = DownloadImageAsync();

                task.ContinueWith(t =>
                {
                    Icon = GetIcon(data.GetValidName());
                });
            }
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
                    string inner = InnerText;

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

        public async Task DownloadImageAsync()
        {
            if (Data == null)
            {
                return;
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string ur = Data.Url;
                    if (!ur.StartsWith("https://") || ur.StartsWith("http://"))
                    {
                        ur = "https://" + ur;
                    }

                    string[] s = ur.Split('.');

                    if (s.Length <= 2)
                    {
                        ur = "https://" + ur.Replace("https://", "www.");
                    }

                    string encodedUrl = WebUtility.UrlEncode(ur);
                    string faviconUrl = $"https://t3.gstatic.com/faviconV2?client=SOCIAL&type=FAVICON&fallback_opts=TYPE,SIZE,URL&url={encodedUrl}&size=16";

                    // Send a request to the URL
                    HttpResponseMessage response = await client.GetAsync(faviconUrl);
                    response.EnsureSuccessStatusCode();

                    // Read the image data
                    byte[] imageData = await response.Content.ReadAsByteArrayAsync();

                    string savePath = Path.Combine(FileManager.SAVEPATH, Data.GetValidURL() + ".png");

                    // Save the image data to a file
                    File.WriteAllBytes(savePath, imageData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        public Image GetIcon(string name)
        {
            string path = FileManager.SAVEPATH + $"\\{name}.png";
            if (File.Exists(path))
            {
                return Image.FromFile(path);
            }
            return null;
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

        private void SettingsButton_MouseEnter(object sender, EventArgs e)
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

        private void ShortcutControl_MouseEnter(object sender, EventArgs e)
        {
            SettingsButton_MouseEnter(sender, e);
        }

        private void ShortcutControl_Leave(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(32, 32, 32);
            Elapsed = false;

            HideTimer.Start();
            ShowTimer.Stop();
        }
    }
}
