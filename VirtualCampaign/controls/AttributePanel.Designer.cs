namespace VirtualCampaign.controls {
    partial class AttributePanel {
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
            this.valueLabel = new System.Windows.Forms.Label();
            this.rollButton = new System.Windows.Forms.Button();
            this.rollOptionsMenu = new VirtualCampaign.controls.DiceRollerContextMenu();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Location = new System.Drawing.Point(48, 8);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(101, 23);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Attribute";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // valueLabel
            // 
            this.valueLabel.BackColor = System.Drawing.Color.Transparent;
            this.valueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueLabel.Location = new System.Drawing.Point(156, 8);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(56, 23);
            this.valueLabel.TabIndex = 1;
            this.valueLabel.Text = "0";
            this.valueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rollButton
            // 
            this.rollButton.BackColor = System.Drawing.Color.Transparent;
            this.rollButton.FlatAppearance.BorderSize = 0;
            this.rollButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rollButton.Image = global::VirtualCampaign.Properties.Resources.rollIcon;
            this.rollButton.Location = new System.Drawing.Point(10, 7);
            this.rollButton.Name = "rollButton";
            this.rollButton.Size = new System.Drawing.Size(34, 27);
            this.rollButton.TabIndex = 2;
            this.rollButton.UseVisualStyleBackColor = false;
            this.rollButton.Visible = false;
            this.rollButton.Click += new System.EventHandler(this.rollButton_Click);
            this.rollButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rollButton_MouseDown);
            // 
            // rollOptionsMenu
            // 
            this.rollOptionsMenu.baseline = 0;
            this.rollOptionsMenu.Name = "rollOptionsMenu";
            this.rollOptionsMenu.Size = new System.Drawing.Size(211, 126);
            // 
            // AttributePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rollButton);
            this.Controls.Add(this.valueLabel);
            this.Controls.Add(this.titleLabel);
            this.Name = "AttributePanel";
            this.Size = new System.Drawing.Size(220, 40);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.Button rollButton;
        private DiceRollerContextMenu rollOptionsMenu;
    }
}
