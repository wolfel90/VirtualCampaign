namespace VirtualCampaign.window {
    partial class DeleteMarkerDialog {
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
            this.messageLabel = new System.Windows.Forms.Label();
            this.deleteMarkerButton = new System.Windows.Forms.Button();
            this.deleteAllButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // messageLabel
            // 
            this.messageLabel.Location = new System.Drawing.Point(12, 9);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(465, 61);
            this.messageLabel.TabIndex = 0;
            this.messageLabel.Text = "Delete the Article associated with this Marker?";
            this.messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // deleteMarkerButton
            // 
            this.deleteMarkerButton.Location = new System.Drawing.Point(12, 73);
            this.deleteMarkerButton.Name = "deleteMarkerButton";
            this.deleteMarkerButton.Size = new System.Drawing.Size(151, 23);
            this.deleteMarkerButton.TabIndex = 1;
            this.deleteMarkerButton.Text = "Delete Marker";
            this.deleteMarkerButton.UseVisualStyleBackColor = true;
            this.deleteMarkerButton.Click += new System.EventHandler(this.deleteMarkerButton_Click);
            // 
            // deleteAllButton
            // 
            this.deleteAllButton.Location = new System.Drawing.Point(169, 73);
            this.deleteAllButton.Name = "deleteAllButton";
            this.deleteAllButton.Size = new System.Drawing.Size(151, 23);
            this.deleteAllButton.TabIndex = 2;
            this.deleteAllButton.Text = "Delete Marker and Article";
            this.deleteAllButton.UseVisualStyleBackColor = true;
            this.deleteAllButton.Click += new System.EventHandler(this.deleteAllButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(326, 73);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(151, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // DeleteMarkerDialog
            // 
            this.AcceptButton = this.deleteMarkerButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(486, 102);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.deleteAllButton);
            this.Controls.Add(this.deleteMarkerButton);
            this.Controls.Add(this.messageLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DeleteMarkerDialog";
            this.Text = "Delete Marker";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Button deleteMarkerButton;
        private System.Windows.Forms.Button deleteAllButton;
        private System.Windows.Forms.Button cancelButton;
    }
}