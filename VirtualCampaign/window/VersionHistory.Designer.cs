namespace VirtualCampaign.window {
    partial class VersionHistory {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.logTextField = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // logTextField
            // 
            this.logTextField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextField.Location = new System.Drawing.Point(0, 0);
            this.logTextField.Multiline = true;
            this.logTextField.Name = "logTextField";
            this.logTextField.ReadOnly = true;
            this.logTextField.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextField.Size = new System.Drawing.Size(481, 227);
            this.logTextField.TabIndex = 0;
            this.logTextField.TabStop = false;
            // 
            // VersionHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 227);
            this.Controls.Add(this.logTextField);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "VersionHistory";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Version History";
            this.Load += new System.EventHandler(this.VersionHistory_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox logTextField;
    }
}