namespace VirtualCampaign.controls {
    partial class ProficiencyPanel {
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
            this.modifierLabel = new System.Windows.Forms.Label();
            this.addButton = new System.Windows.Forms.Button();
            this.subtractButton = new System.Windows.Forms.Button();
            this.rollButton = new System.Windows.Forms.Button();
            this.rollOptionsMenu = new VirtualCampaign.controls.DiceRollerContextMenu();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(6, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(141, 16);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Proficiency";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // valueLabel
            // 
            this.valueLabel.BackColor = System.Drawing.Color.Transparent;
            this.valueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueLabel.Location = new System.Drawing.Point(31, 27);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(89, 40);
            this.valueLabel.TabIndex = 0;
            this.valueLabel.Text = "0";
            this.valueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.valueLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.valueLabel_MouseClick);
            this.valueLabel.MouseEnter += new System.EventHandler(this.valueLabel_MouseEnter);
            this.valueLabel.MouseLeave += new System.EventHandler(this.valueLabel_MouseLeave);
            // 
            // modifierLabel
            // 
            this.modifierLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.modifierLabel.Location = new System.Drawing.Point(37, 79);
            this.modifierLabel.Name = "modifierLabel";
            this.modifierLabel.Size = new System.Drawing.Size(76, 15);
            this.modifierLabel.TabIndex = 1;
            this.modifierLabel.Text = "+0";
            this.modifierLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(120, 35);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(20, 24);
            this.addButton.TabIndex = 2;
            this.addButton.Tag = "add";
            this.addButton.Text = "+";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Visible = false;
            this.addButton.Leave += new System.EventHandler(this.button_Leave);
            this.addButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.addButton.MouseLeave += new System.EventHandler(this.button_Leave);
            this.addButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // subtractButton
            // 
            this.subtractButton.Location = new System.Drawing.Point(10, 35);
            this.subtractButton.Name = "subtractButton";
            this.subtractButton.Size = new System.Drawing.Size(20, 24);
            this.subtractButton.TabIndex = 3;
            this.subtractButton.Tag = "subtract";
            this.subtractButton.Text = "-";
            this.subtractButton.UseVisualStyleBackColor = true;
            this.subtractButton.Visible = false;
            this.subtractButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.subtractButton.MouseLeave += new System.EventHandler(this.button_Leave);
            this.subtractButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // rollButton
            // 
            this.rollButton.BackColor = System.Drawing.Color.Transparent;
            this.rollButton.FlatAppearance.BorderSize = 0;
            this.rollButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rollButton.Image = global::VirtualCampaign.Properties.Resources.rollIcon;
            this.rollButton.Location = new System.Drawing.Point(0, 70);
            this.rollButton.Name = "rollButton";
            this.rollButton.Size = new System.Drawing.Size(34, 24);
            this.rollButton.TabIndex = 4;
            this.rollButton.UseVisualStyleBackColor = false;
            this.rollButton.Visible = false;
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
            // ProficiencyPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.rollButton);
            this.Controls.Add(this.subtractButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.modifierLabel);
            this.Controls.Add(this.valueLabel);
            this.Controls.Add(this.titleLabel);
            this.Name = "ProficiencyPanel";
            this.Size = new System.Drawing.Size(150, 95);
            this.SizeChanged += new System.EventHandler(this.ProficiencyPanel_SizeChanged);
            this.MouseEnter += new System.EventHandler(this.ProficiencyPanel_Enter);
            this.MouseLeave += new System.EventHandler(this.ProficiencyPanel_Leave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.Label modifierLabel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button subtractButton;
        private System.Windows.Forms.Button rollButton;
        private DiceRollerContextMenu rollOptionsMenu;
    }
}
