using System.Drawing;

namespace WidgetsApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.shortcutControl1 = new WidgetsApp.src.controls.ShortcutControl();
            this.shortcutControl2 = new WidgetsApp.src.controls.ShortcutControl();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.shortcutControl1);
            this.flowLayoutPanel1.Controls.Add(this.shortcutControl2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(558, 287);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // shortcutControl1
            // 
            this.shortcutControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.shortcutControl1.ForeColor = System.Drawing.Color.White;
            this.shortcutControl1.Location = new System.Drawing.Point(3, 3);
            this.shortcutControl1.Name = "shortcutControl1";
            this.shortcutControl1.Size = new System.Drawing.Size(120, 120);
            this.shortcutControl1.TabIndex = 1;
            // 
            // shortcutControl2
            // 
            this.shortcutControl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.shortcutControl2.ForeColor = System.Drawing.Color.White;
            this.shortcutControl2.Location = new System.Drawing.Point(129, 3);
            this.shortcutControl2.Name = "shortcutControl2";
            this.shortcutControl2.Size = new System.Drawing.Size(120, 120);
            this.shortcutControl2.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(558, 287);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private src.controls.ShortcutControl shortcutControl1;
        private src.controls.ShortcutControl shortcutControl2;
    }
}

