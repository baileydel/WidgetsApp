
using System;
using System.Drawing;
using System.Security.Cryptography;
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
            NameTextBox.Text = control.OuterText;
            UrlTextBox.Text = control.InnerText;
        }

        #region UrlBox
        private void UrlBox_TextChanged(object sender, EventArgs e)
        {
            EnableDoneButton(UrlTextBox.TextLength > 0);
        }

        private void UrlBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                DoneButton_Click(sender, e);
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
            if (Parent is MainForm mainForm)
            {
                string t = UrlTextBox.Text;

                if (t.Length > 0)
                {
                    if (editing != null)
                    {
                        ShortcutControl old = editing;
                        old.OuterText = NameTextBox.Text;
                        old.InnerText = UrlTextBox.Text;

                        if (!t.StartsWith("https://"))
                        {
                            old.InnerText = "https://" + t + "/";
                        }

                        if (NameTextBox.Text.Length == 0)
                        {
                            old.Name = UrlTextBox.Text;
                        }

                        mainForm.SaveShortcut(old.GetWidgetData(), editing);
                    }
                    else
                    {
                        if (!t.StartsWith("https://"))
                        {
                            t = "https://" + t + "/";
                        }

                        if (NameTextBox.Text.Length == 0)
                        {
                            NameTextBox.Text = UrlTextBox.Text;
                        }

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
    }
}
