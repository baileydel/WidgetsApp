
using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using WidgetsApp.src.controls;
using WidgetsApp.src.Util;

namespace WidgetsApp
{
    public partial class ShortcutForm : UserControl
    {
        readonly ShortcutControl editing;

        public ShortcutForm()
        {
            InitializeComponent();
        }

        public ShortcutForm(ShortcutControl control)
        {
            InitializeComponent();

            editing = control;

            TitleLabel.Text = "Edit Shortcut";
            NameTextBox.Text = control.OuterText;
            UrlTextBox.Text = control.InnerText;
        }

        #region UrlBox
        private void UrlBox_TextChanged(object sender, EventArgs e)
        {
            EnableDoneButton(UrlTextBox.TextLength > 0);
        }

        private void UrlTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (UrlTextBox.Text.Length > 0)
                {
                    DoneButton_Click(sender, e);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }
        }

        #endregion

        #region Done Button
        private void EnableDoneButton(bool b)
        {
            DoneButton.Enabled = b;

            if (b)
            {
                DoneButton.ForeColor = Color.FromArgb(6, 46, 111);
                DoneButton.BackColor = Color.FromArgb(160, 189, 237);
            }
            else
            {
                DoneButton.ForeColor = Color.FromArgb(112, 112, 112);
                DoneButton.BackColor = Color.FromArgb(81, 83, 83);
            }
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            string t = UrlTextBox.Text;

            if (t.Length == 0)
            {
                return;
            }

            if (Parent is MainForm mainForm)
            {
                if (editing != null)
                {
                    ShortcutControl old = editing;
                    old.OuterText = NameTextBox.Text;
                    old.InnerText = UrlTextBox.Text;

                    if (NameTextBox.Text.Length == 0)
                    {
                        old.Name = UrlTextBox.Text;
                    }

                    if (!t.StartsWith("https://"))
                    {
                        old.InnerText = "https://" + t + "/";
                    }
                    mainForm.SaveShortcut(old.GetWidgetData(), editing);
                }
                else
                {
                    if (NameTextBox.Text.Length == 0)
                    {
                        NameTextBox.Text = UrlTextBox.Text;
                    }

                    if (!t.StartsWith("https://"))
                    {
                        t = "https://" + t + "/";
                    }

                    Task task = DownloadImageAsync("https://t3.gstatic.com/faviconV2?client=SOCIAL&type=FAVICON&fallback_opts=TYPE,SIZE,URL&url=http://youtube.com&size=24", Path.Combine(FileManager.SAVEPATH, NameTextBox.Text + ".png"));

                    Random random = new Random();
                    Color InnerColor = Color.FromArgb(random.Next(150, 256), random.Next(150, 256), random.Next(150, 256));
                    mainForm.CreateShortcut(new WidgetData(NameTextBox.Text, t, InnerColor));
                }
                Close();
            }
        }
        #endregion

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close(); 
        }

        public void Close()
        {
            if (Parent is MainForm mainForm)
            {
                mainForm.HideFlow(false);
                mainForm.Controls.Remove(this);
            }
        }

        private void NameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                if (UrlTextBox.TextLength > 0)
                {
                    DoneButton_Click(sender, e);
                }
                else
                {
                    UrlTextBox.Focus();
                }
            }

        }

        public async Task DownloadImageAsync(string imageUrl, string savePath)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Send a request to the URL
                    HttpResponseMessage response = await client.GetAsync(imageUrl);
                    response.EnsureSuccessStatusCode();

                    // Read the image data
                    byte[] imageData = await response.Content.ReadAsByteArrayAsync();

                    // Save the image data to a file
                    File.WriteAllBytes(savePath, imageData);

                    Console.WriteLine("Image downloaded and saved successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
