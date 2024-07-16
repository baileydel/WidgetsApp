
using System;
using System.Drawing;
using System.Windows.Forms;
using WidgetsApp.src.controls;

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
            NameTextBox.Text = control.Data.Name;
            UrlTextBox.Text = control.Data.Url;
        }

        #region UrlBox
        private void UrlBox_TextChanged(object sender, EventArgs e)
        {
            ValidateURL(UrlTextBox.Text);
        }

        private void UrlTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (UrlTextBox.Text.Length > 0)
                {
                    Submit();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
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
                    Submit();
                }
                else
                {
                    UrlTextBox.Focus();
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
                UrlLabel.ForeColor = Color.White;
                ErrorLabel.Hide();
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
            Submit();
        }
        #endregion

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Submit()
        {
            if (!DoneButton.Enabled)
            {
                return;
            }

            if (Parent is MainForm mainForm)
            {
                if (NameTextBox.Text.Length == 0)
                {
                    NameTextBox.Text = UrlTextBox.Text;
                }

                UrlTextBox.Text = CompleteURL(UrlTextBox.Text);

                if (editing != null)
                {
                    mainForm.URLS.Remove(editing.Data.Url);

                    editing.Data.Name = NameTextBox.Text;
                    editing.Data.Url = UrlTextBox.Text;

                    mainForm.URLS.Add(editing.Data.Url);
                    mainForm.SaveShortcut(editing.Data);
                }
                else
                {
                    mainForm.CreateShortcut(new WidgetData(NameTextBox.Text, UrlTextBox.Text));
                }
            }
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

        private void ValidateURL(string url)
        {
            bool enable = true;
            if (Parent is MainForm mainForm)
            {
                if (url.Length == 0)
                {
                    enable = false;
                }

                string[] list = url.Split(':');

                if (list.Length > 1)
                {
                    if (list[0] != "https" || list[1].Length == 0 || list[1].Contains(" ") || !list[1].StartsWith("//"))
                    {
                        UrlLabel.ForeColor = Color.FromArgb(242, 184, 181);
                        ErrorLabel.Text = "Type a valid URL";
                        ErrorLabel.Show();
                        enable = false;
                    }
                }

                if (mainForm.ContainsURL(url) || mainForm.ContainsURL(CompleteURL(url)))
                {
                    UrlLabel.ForeColor = Color.FromArgb(242, 184, 181);
                    ErrorLabel.Text = "URL already exists";
                    ErrorLabel.Show();
                    enable = false;
                }
            }

            EnableDoneButton(enable);
        }

        private string CompleteURL(string url)
        {
            if (!url.StartsWith("https://"))
            {
                return "https://" + url;
            }
            return url;
        }

        private void UrlTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ' || e.KeyChar == 127)
            {
                e.Handled = true;
            }

            if (e.KeyChar == 27)
            {
                e.Handled = true;
                Close();
            }
        }

        private void NameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 127)
            {
                e.Handled = true;
            }

            if (e.KeyChar == 27)
            {
                e.Handled = true;
                Close();
            }
        }
    }
}
