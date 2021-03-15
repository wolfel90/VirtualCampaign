namespace VirtualCampaign.controls {
    partial class SchoolPanel {
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
            this.removeButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.levelNumberLabel = new System.Windows.Forms.Label();
            this.decreaseButton = new System.Windows.Forms.Button();
            this.increaseButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.specialLevellabel = new System.Windows.Forms.Label();
            this.levelMeter = new VirtualCampaign.controls.Meter();
            this.progressMeter = new VirtualCampaign.controls.Meter();
            this.SuspendLayout();
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(3, 1);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(26, 23);
            this.removeButton.TabIndex = 12;
            this.removeButton.Text = "X";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.Location = new System.Drawing.Point(72, 1);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(213, 23);
            this.titleLabel.TabIndex = 13;
            this.titleLabel.Text = "School";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.titleLabel.Click += new System.EventHandler(this.titleLabel_Click);
            // 
            // levelNumberLabel
            // 
            this.levelNumberLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.levelNumberLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.levelNumberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelNumberLabel.Location = new System.Drawing.Point(476, 1);
            this.levelNumberLabel.Name = "levelNumberLabel";
            this.levelNumberLabel.Size = new System.Drawing.Size(34, 23);
            this.levelNumberLabel.TabIndex = 24;
            this.levelNumberLabel.Text = "0";
            this.levelNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // decreaseButton
            // 
            this.decreaseButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.decreaseButton.Location = new System.Drawing.Point(292, 1);
            this.decreaseButton.Name = "decreaseButton";
            this.decreaseButton.Size = new System.Drawing.Size(21, 23);
            this.decreaseButton.TabIndex = 25;
            this.decreaseButton.Tag = "subtract";
            this.decreaseButton.Text = "˂";
            this.decreaseButton.UseVisualStyleBackColor = true;
            this.decreaseButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.decreaseButton.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.decreaseButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // increaseButton
            // 
            this.increaseButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.increaseButton.Location = new System.Drawing.Point(510, 1);
            this.increaseButton.Name = "increaseButton";
            this.increaseButton.Size = new System.Drawing.Size(21, 23);
            this.increaseButton.TabIndex = 26;
            this.increaseButton.Tag = "add";
            this.increaseButton.Text = "˃";
            this.increaseButton.UseVisualStyleBackColor = true;
            this.increaseButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.increaseButton.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.increaseButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // downButton
            // 
            this.downButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.downButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downButton.Location = new System.Drawing.Point(537, 11);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(29, 14);
            this.downButton.TabIndex = 28;
            this.downButton.Text = "˅";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // upButton
            // 
            this.upButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.upButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.upButton.Location = new System.Drawing.Point(537, -1);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(29, 14);
            this.upButton.TabIndex = 27;
            this.upButton.Text = "˄";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // specialLevellabel
            // 
            this.specialLevellabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.specialLevellabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.specialLevellabel.Location = new System.Drawing.Point(32, 1);
            this.specialLevellabel.Name = "specialLevellabel";
            this.specialLevellabel.Size = new System.Drawing.Size(34, 23);
            this.specialLevellabel.TabIndex = 29;
            this.specialLevellabel.Text = "0";
            this.specialLevellabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // levelMeter
            // 
            this.levelMeter.bgColor = System.Drawing.Color.Gray;
            this.levelMeter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.levelMeter.current = 0;
            this.levelMeter.Cursor = System.Windows.Forms.Cursors.Default;
            this.levelMeter.curValEditable = true;
            this.levelMeter.drawText = false;
            this.levelMeter.editable = false;
            this.levelMeter.fillColor = System.Drawing.Color.Blue;
            this.levelMeter.Location = new System.Drawing.Point(314, 2);
            this.levelMeter.max = 10;
            this.levelMeter.maxValEditable = true;
            this.levelMeter.min = 0;
            this.levelMeter.Name = "levelMeter";
            this.levelMeter.negativeColor = System.Drawing.Color.DarkGray;
            this.levelMeter.segmentInterval = 1;
            this.levelMeter.Size = new System.Drawing.Size(160, 14);
            this.levelMeter.TabIndex = 30;
            // 
            // progressMeter
            // 
            this.progressMeter.bgColor = System.Drawing.Color.Black;
            this.progressMeter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.progressMeter.current = 0;
            this.progressMeter.Cursor = System.Windows.Forms.Cursors.Default;
            this.progressMeter.curValEditable = true;
            this.progressMeter.drawText = false;
            this.progressMeter.editable = false;
            this.progressMeter.fillColor = System.Drawing.Color.White;
            this.progressMeter.Location = new System.Drawing.Point(314, 17);
            this.progressMeter.max = 1;
            this.progressMeter.maxValEditable = true;
            this.progressMeter.min = 0;
            this.progressMeter.Name = "progressMeter";
            this.progressMeter.negativeColor = System.Drawing.Color.DarkGray;
            this.progressMeter.segmentInterval = 1;
            this.progressMeter.Size = new System.Drawing.Size(160, 6);
            this.progressMeter.TabIndex = 31;
            // 
            // SchoolPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressMeter);
            this.Controls.Add(this.levelMeter);
            this.Controls.Add(this.specialLevellabel);
            this.Controls.Add(this.downButton);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.increaseButton);
            this.Controls.Add(this.decreaseButton);
            this.Controls.Add(this.levelNumberLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.removeButton);
            this.Name = "SchoolPanel";
            this.Size = new System.Drawing.Size(570, 25);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label levelNumberLabel;
        private System.Windows.Forms.Button decreaseButton;
        private System.Windows.Forms.Button increaseButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Label specialLevellabel;
        private Meter levelMeter;
        private Meter progressMeter;
    }
}
