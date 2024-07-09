
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WidgetsApp
{
    public partial class ShortcutForm : UserControl
    {
        public ShortcutForm()
        {
            InitializeComponent();

            if (this.Parent is MainForm mainForm)
            {
                mainForm.HideFlow(true);
            }
        }

        #region UrlBox
        private void urlBox_TextChanged(object sender, EventArgs e)
        {
            EnableDoneButton(UrlTextBox.TextLength > 0);
        }

        private void urlBox_KeyPress(object sender, KeyPressEventArgs e)
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
            if (this.Parent is MainForm mainForm)
            {
                mainForm.AddShortcut(this);
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
            if (this.Parent is MainForm mainForm)
            {
                mainForm.HideFlow(false);
                mainForm.Controls.Remove(this);
            }
        }
    }
}
