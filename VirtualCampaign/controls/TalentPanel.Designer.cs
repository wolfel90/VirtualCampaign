namespace VirtualCampaign.controls {
    partial class TalentPanel {
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
            this.components = new System.ComponentModel.Container();
            this.titleLabel = new System.Windows.Forms.Label();
            this.increaseButton = new System.Windows.Forms.Button();
            this.decreaseButton = new System.Windows.Forms.Button();
            this.rankLabel = new System.Windows.Forms.Label();
            this.rankNumberLevel = new System.Windows.Forms.Label();
            this.removeButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.rollButton = new System.Windows.Forms.Button();
            this.rollOptionsMenu = new VirtualCampaign.controls.DiceRollerContextMenu();
            this.progressMeter = new VirtualCampaign.controls.Meter();
            this.rankMeter = new VirtualCampaign.controls.Meter();
            this.progressTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Location = new System.Drawing.Point(23, 1);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(142, 23);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "Talent";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.titleLabel.Click += new System.EventHandler(this.titleLabel_Click);
            this.titleLabel.MouseEnter += new System.EventHandler(this.titleLabel_MouseEnter);
            this.titleLabel.MouseLeave += new System.EventHandler(this.titleLabel_MouseLeave);
            // 
            // increaseButton
            // 
            this.increaseButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.increaseButton.Location = new System.Drawing.Point(385, 1);
            this.increaseButton.Name = "increaseButton";
            this.increaseButton.Size = new System.Drawing.Size(21, 23);
            this.increaseButton.TabIndex = 7;
            this.increaseButton.Tag = "add";
            this.increaseButton.Text = "˃";
            this.increaseButton.UseVisualStyleBackColor = true;
            this.increaseButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.increaseButton.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.increaseButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // decreaseButton
            // 
            this.decreaseButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.decreaseButton.Location = new System.Drawing.Point(203, 1);
            this.decreaseButton.Name = "decreaseButton";
            this.decreaseButton.Size = new System.Drawing.Size(21, 23);
            this.decreaseButton.TabIndex = 8;
            this.decreaseButton.Tag = "subtract";
            this.decreaseButton.Text = "˂";
            this.decreaseButton.UseVisualStyleBackColor = true;
            this.decreaseButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.decreaseButton.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            this.decreaseButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // rankLabel
            // 
            this.rankLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.rankLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rankLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rankLabel.Location = new System.Drawing.Point(447, 1);
            this.rankLabel.Name = "rankLabel";
            this.rankLabel.Size = new System.Drawing.Size(89, 23);
            this.rankLabel.TabIndex = 9;
            this.rankLabel.Text = "Untrained";
            this.rankLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rankNumberLevel
            // 
            this.rankNumberLevel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.rankNumberLevel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.rankNumberLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rankNumberLevel.Location = new System.Drawing.Point(408, 1);
            this.rankNumberLevel.Name = "rankNumberLevel";
            this.rankNumberLevel.Size = new System.Drawing.Size(34, 23);
            this.rankNumberLevel.TabIndex = 10;
            this.rankNumberLevel.Text = "0";
            this.rankNumberLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(3, 1);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(16, 24);
            this.removeButton.TabIndex = 11;
            this.removeButton.Text = "X";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // downButton
            // 
            this.downButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downButton.Location = new System.Drawing.Point(539, 11);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(18, 14);
            this.downButton.TabIndex = 13;
            this.downButton.Text = "˅";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // upButton
            // 
            this.upButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.upButton.Location = new System.Drawing.Point(539, -1);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(18, 14);
            this.upButton.TabIndex = 12;
            this.upButton.Text = "˄";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // rollButton
            // 
            this.rollButton.BackColor = System.Drawing.Color.Transparent;
            this.rollButton.FlatAppearance.BorderSize = 0;
            this.rollButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rollButton.Image = global::VirtualCampaign.Properties.Resources.roll_button_icon;
            this.rollButton.Location = new System.Drawing.Point(168, 0);
            this.rollButton.Margin = new System.Windows.Forms.Padding(0);
            this.rollButton.Name = "rollButton";
            this.rollButton.Size = new System.Drawing.Size(34, 24);
            this.rollButton.TabIndex = 14;
            this.rollButton.UseVisualStyleBackColor = false;
            this.rollButton.Click += new System.EventHandler(this.rollButton_Click);
            this.rollButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rollButton_MouseDown);
            // 
            // rollOptionsMenu
            // 
            this.rollOptionsMenu.baseline = 0;
            this.rollOptionsMenu.Name = "rollOptionsMenu";
            this.rollOptionsMenu.rollSource = "";
            this.rollOptionsMenu.Size = new System.Drawing.Size(211, 126);
            // 
            // progressMeter
            // 
            this.progressMeter.bgColor = System.Drawing.Color.Black;
            this.progressMeter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.progressMeter.current = 0;
            this.progressMeter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.progressMeter.curValEditable = true;
            this.progressMeter.drawText = false;
            this.progressMeter.editable = true;
            this.progressMeter.fillColor = System.Drawing.Color.White;
            this.progressMeter.Location = new System.Drawing.Point(225, 17);
            this.progressMeter.max = 1;
            this.progressMeter.maxValEditable = true;
            this.progressMeter.min = 0;
            this.progressMeter.Name = "progressMeter";
            this.progressMeter.negativeColor = System.Drawing.Color.DarkGray;
            this.progressMeter.segmentInterval = 1;
            this.progressMeter.Size = new System.Drawing.Size(160, 6);
            this.progressMeter.TabIndex = 15;
            // 
            // rankMeter
            // 
            this.rankMeter.bgColor = System.Drawing.Color.Gray;
            this.rankMeter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rankMeter.current = 0;
            this.rankMeter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rankMeter.curValEditable = true;
            this.rankMeter.drawText = false;
            this.rankMeter.editable = true;
            this.rankMeter.fillColor = System.Drawing.Color.LimeGreen;
            this.rankMeter.Location = new System.Drawing.Point(225, 2);
            this.rankMeter.max = 10;
            this.rankMeter.maxValEditable = true;
            this.rankMeter.min = 0;
            this.rankMeter.Name = "rankMeter";
            this.rankMeter.negativeColor = System.Drawing.Color.DarkGray;
            this.rankMeter.segmentInterval = 1;
            this.rankMeter.Size = new System.Drawing.Size(160, 14);
            this.rankMeter.TabIndex = 16;
            // 
            // TalentPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.rankMeter);
            this.Controls.Add(this.progressMeter);
            this.Controls.Add(this.rollButton);
            this.Controls.Add(this.downButton);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.rankNumberLevel);
            this.Controls.Add(this.rankLabel);
            this.Controls.Add(this.decreaseButton);
            this.Controls.Add(this.increaseButton);
            this.Controls.Add(this.titleLabel);
            this.Name = "TalentPanel";
            this.Size = new System.Drawing.Size(560, 25);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button increaseButton;
        private System.Windows.Forms.Button decreaseButton;
        private System.Windows.Forms.Label rankLabel;
        private System.Windows.Forms.Label rankNumberLevel;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button rollButton;
        private DiceRollerContextMenu rollOptionsMenu;
        private Meter progressMeter;
        private Meter rankMeter;
        private System.Windows.Forms.ToolTip progressTooltip;
    }
}
