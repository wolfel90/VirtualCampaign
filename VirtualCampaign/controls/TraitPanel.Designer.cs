namespace VirtualCampaign.controls {
    partial class TraitPanel {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.titleLabel = new System.Windows.Forms.Label();
            this.costLabel = new System.Windows.Forms.Label();
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.activeCheck = new System.Windows.Forms.CheckBox();
            this.removeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Location = new System.Drawing.Point(49, 2);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(183, 23);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Trait";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.titleLabel.Click += new System.EventHandler(this.titleLabel_Click);
            this.titleLabel.MouseEnter += new System.EventHandler(this.titleLabel_MouseEnter);
            this.titleLabel.MouseLeave += new System.EventHandler(this.titleLabel_MouseLeave);
            // 
            // costLabel
            // 
            this.costLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.costLabel.Location = new System.Drawing.Point(238, 1);
            this.costLabel.Name = "costLabel";
            this.costLabel.Size = new System.Drawing.Size(35, 23);
            this.costLabel.TabIndex = 2;
            this.costLabel.Text = "0";
            this.costLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // upButton
            // 
            this.upButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.upButton.Location = new System.Drawing.Point(279, -1);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(16, 14);
            this.upButton.TabIndex = 3;
            this.upButton.Text = "˄";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // downButton
            // 
            this.downButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downButton.Location = new System.Drawing.Point(279, 11);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(16, 14);
            this.downButton.TabIndex = 4;
            this.downButton.Text = "˅";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // activeCheck
            // 
            this.activeCheck.AutoSize = true;
            this.activeCheck.Checked = true;
            this.activeCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.activeCheck.Cursor = System.Windows.Forms.Cursors.Default;
            this.activeCheck.Location = new System.Drawing.Point(29, 6);
            this.activeCheck.Name = "activeCheck";
            this.activeCheck.Size = new System.Drawing.Size(15, 14);
            this.activeCheck.TabIndex = 5;
            this.activeCheck.UseVisualStyleBackColor = true;
            this.activeCheck.CheckedChanged += new System.EventHandler(this.activeCheck_CheckedChanged);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(3, 1);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(18, 24);
            this.removeButton.TabIndex = 6;
            this.removeButton.Text = "X";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // TraitPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.activeCheck);
            this.Controls.Add(this.downButton);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.costLabel);
            this.Controls.Add(this.titleLabel);
            this.Name = "TraitPanel";
            this.Size = new System.Drawing.Size(300, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label costLabel;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.CheckBox activeCheck;
        private System.Windows.Forms.Button removeButton;
    }
}
