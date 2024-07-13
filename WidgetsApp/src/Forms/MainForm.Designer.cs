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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.FlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.AddShortcutControl = new WidgetsApp.src.controls.ShortcutControl();
            this.FlowPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // FlowPanel
            // 
            this.FlowPanel.Controls.Add(this.AddShortcutControl);
            this.FlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlowPanel.Location = new System.Drawing.Point(0, 0);
            this.FlowPanel.Name = "FlowPanel";
            this.FlowPanel.Size = new System.Drawing.Size(558, 287);
            this.FlowPanel.TabIndex = 1;
            // 
            // AddShortcutControl
            // 
            this.AddShortcutControl.BackColor = System.Drawing.Color.Transparent;
            this.AddShortcutControl.ForeColor = System.Drawing.Color.White;
            this.AddShortcutControl.Icon = null;
            this.AddShortcutControl.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(74)))), ((int)(((byte)(119)))));
            this.AddShortcutControl.InnerFontSize = 24;
            this.AddShortcutControl.InnerText = "+";
            this.AddShortcutControl.InnerTextColor = System.Drawing.Color.White;
            this.AddShortcutControl.Location = new System.Drawing.Point(3, 3);
            this.AddShortcutControl.Name = "AddShortcutControl";
            this.AddShortcutControl.OuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(74)))), ((int)(((byte)(119)))));
            this.AddShortcutControl.OuterText = "Add shortcut";
            this.AddShortcutControl.Size = new System.Drawing.Size(120, 120);
            this.AddShortcutControl.State = 1;
            this.AddShortcutControl.TabIndex = 2;
            this.AddShortcutControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AddShortcutControl_MouseClick);
            // 
            // MainForm
            // 
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(558, 287);
            this.Controls.Add(this.FlowPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FlowPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel FlowPanel;
        private src.controls.ShortcutControl AddShortcutControl;
    }
}

