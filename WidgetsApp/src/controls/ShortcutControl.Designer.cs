namespace WidgetsApp.src.controls
{
    partial class ShortcutControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SettingsButton = new System.Windows.Forms.Button();
            this.ShowTimer = new System.Windows.Forms.Timer(this.components);
            this.HideTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // SettingsButton
            // 
            this.SettingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SettingsButton.BackColor = System.Drawing.Color.Transparent;
            this.SettingsButton.FlatAppearance.BorderSize = 0;
            this.SettingsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.SettingsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.SettingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsButton.Font = new System.Drawing.Font("Minecraft", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsButton.Location = new System.Drawing.Point(177, 3);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(31, 29);
            this.SettingsButton.TabIndex = 2;
            this.SettingsButton.Text = ":";
            this.SettingsButton.UseVisualStyleBackColor = false;
            this.SettingsButton.Visible = false;
            this.SettingsButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // ShowTimer
            // 
            this.ShowTimer.Interval = 1000;
            this.ShowTimer.Tick += new System.EventHandler(this.ShowTimer_Tick);
            // 
            // HideTimer
            // 
            this.HideTimer.Tick += new System.EventHandler(this.HideTimer_Tick);
            // 
            // ShortcutControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.SettingsButton);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ShortcutControl";
            this.Size = new System.Drawing.Size(211, 246);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button SettingsButton;
        private System.Windows.Forms.Timer ShowTimer;
        private System.Windows.Forms.Timer HideTimer;
    }
}
