using CefSharp.WinForms;
using System.Drawing;

namespace WidgetsApp
{
    partial class WidgetForm
    {
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(931, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // WidgetForm
            // 
            this.ClientSize = new System.Drawing.Size(776, 489);
            this.Controls.Add(this.button1);
            this.Name = "WidgetForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WidgetForm_FormClosing);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button button1;
    }
}


