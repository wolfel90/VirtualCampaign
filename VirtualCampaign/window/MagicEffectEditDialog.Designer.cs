namespace VirtualCampaign.window {
    partial class MagicEffectEditDialog {
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
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.nameLabel = new System.Windows.Forms.Label();
            this.typeLabel = new System.Windows.Forms.Label();
            this.schoolLabel = new System.Windows.Forms.Label();
            this.qualityLabel = new System.Windows.Forms.Label();
            this.rarityLabel = new System.Windows.Forms.Label();
            this.prefixLabel = new System.Windows.Forms.Label();
            this.suffixLabel = new System.Windows.Forms.Label();
            this.effectLabel = new System.Windows.Forms.Label();
            this.nameField = new System.Windows.Forms.TextBox();
            this.schoolField = new System.Windows.Forms.TextBox();
            this.prefixField = new System.Windows.Forms.TextBox();
            this.suffixField = new System.Windows.Forms.TextBox();
            this.effectField = new System.Windows.Forms.TextBox();
            this.typeCombo = new System.Windows.Forms.ComboBox();
            this.raritySpinner = new System.Windows.Forms.NumericUpDown();
            this.qualityCombo = new System.Windows.Forms.ComboBox();
            this.idLabel = new System.Windows.Forms.Label();
            this.idValueLabel = new System.Windows.Forms.Label();
            this.modsField = new System.Windows.Forms.TextBox();
            this.modsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.raritySpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(254, 332);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(173, 332);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // nameLabel
            // 
            this.nameLabel.Location = new System.Drawing.Point(12, 28);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(57, 20);
            this.nameLabel.TabIndex = 5;
            this.nameLabel.Text = "Name";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // typeLabel
            // 
            this.typeLabel.Location = new System.Drawing.Point(12, 50);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(57, 20);
            this.typeLabel.TabIndex = 6;
            this.typeLabel.Text = "Type";
            this.typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // schoolLabel
            // 
            this.schoolLabel.Location = new System.Drawing.Point(12, 72);
            this.schoolLabel.Name = "schoolLabel";
            this.schoolLabel.Size = new System.Drawing.Size(57, 20);
            this.schoolLabel.TabIndex = 7;
            this.schoolLabel.Text = "School";
            this.schoolLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // qualityLabel
            // 
            this.qualityLabel.Location = new System.Drawing.Point(12, 94);
            this.qualityLabel.Name = "qualityLabel";
            this.qualityLabel.Size = new System.Drawing.Size(57, 20);
            this.qualityLabel.TabIndex = 8;
            this.qualityLabel.Text = "Quality";
            this.qualityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rarityLabel
            // 
            this.rarityLabel.Location = new System.Drawing.Point(12, 116);
            this.rarityLabel.Name = "rarityLabel";
            this.rarityLabel.Size = new System.Drawing.Size(57, 20);
            this.rarityLabel.TabIndex = 9;
            this.rarityLabel.Text = "Rarity";
            this.rarityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // prefixLabel
            // 
            this.prefixLabel.Location = new System.Drawing.Point(12, 138);
            this.prefixLabel.Name = "prefixLabel";
            this.prefixLabel.Size = new System.Drawing.Size(57, 20);
            this.prefixLabel.TabIndex = 10;
            this.prefixLabel.Text = "Prefix";
            this.prefixLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // suffixLabel
            // 
            this.suffixLabel.Location = new System.Drawing.Point(12, 160);
            this.suffixLabel.Name = "suffixLabel";
            this.suffixLabel.Size = new System.Drawing.Size(57, 20);
            this.suffixLabel.TabIndex = 11;
            this.suffixLabel.Text = "Suffix";
            this.suffixLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // effectLabel
            // 
            this.effectLabel.Location = new System.Drawing.Point(12, 205);
            this.effectLabel.Name = "effectLabel";
            this.effectLabel.Size = new System.Drawing.Size(57, 20);
            this.effectLabel.TabIndex = 12;
            this.effectLabel.Text = "Effect";
            this.effectLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nameField
            // 
            this.nameField.Location = new System.Drawing.Point(75, 30);
            this.nameField.Name = "nameField";
            this.nameField.Size = new System.Drawing.Size(256, 20);
            this.nameField.TabIndex = 13;
            this.nameField.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // schoolField
            // 
            this.schoolField.Location = new System.Drawing.Point(75, 74);
            this.schoolField.Name = "schoolField";
            this.schoolField.Size = new System.Drawing.Size(256, 20);
            this.schoolField.TabIndex = 14;
            this.schoolField.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // prefixField
            // 
            this.prefixField.Location = new System.Drawing.Point(75, 140);
            this.prefixField.Name = "prefixField";
            this.prefixField.Size = new System.Drawing.Size(256, 20);
            this.prefixField.TabIndex = 15;
            this.prefixField.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // suffixField
            // 
            this.suffixField.Location = new System.Drawing.Point(75, 162);
            this.suffixField.Name = "suffixField";
            this.suffixField.Size = new System.Drawing.Size(256, 20);
            this.suffixField.TabIndex = 16;
            this.suffixField.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // effectField
            // 
            this.effectField.Location = new System.Drawing.Point(75, 207);
            this.effectField.Multiline = true;
            this.effectField.Name = "effectField";
            this.effectField.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.effectField.Size = new System.Drawing.Size(256, 113);
            this.effectField.TabIndex = 17;
            this.effectField.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // typeCombo
            // 
            this.typeCombo.FormattingEnabled = true;
            this.typeCombo.Items.AddRange(new object[] {
            "Triggered (Generic)",
            "Continuous (Generic)",
            "Triggered (Armor)",
            "Continuous (Armor)",
            "Triggered (Weapon)",
            "Continuous (Weapon)"});
            this.typeCombo.Location = new System.Drawing.Point(75, 51);
            this.typeCombo.Name = "typeCombo";
            this.typeCombo.Size = new System.Drawing.Size(256, 21);
            this.typeCombo.TabIndex = 18;
            this.typeCombo.Text = "Triggered (Generic)";
            this.typeCombo.SelectedIndexChanged += new System.EventHandler(this.value_Changed);
            // 
            // raritySpinner
            // 
            this.raritySpinner.Location = new System.Drawing.Point(75, 118);
            this.raritySpinner.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.raritySpinner.Name = "raritySpinner";
            this.raritySpinner.Size = new System.Drawing.Size(256, 20);
            this.raritySpinner.TabIndex = 20;
            this.raritySpinner.ValueChanged += new System.EventHandler(this.value_Changed);
            // 
            // qualityCombo
            // 
            this.qualityCombo.FormattingEnabled = true;
            this.qualityCombo.Items.AddRange(new object[] {
            "Neutral",
            "Negative",
            "Positive"});
            this.qualityCombo.Location = new System.Drawing.Point(75, 95);
            this.qualityCombo.Name = "qualityCombo";
            this.qualityCombo.Size = new System.Drawing.Size(256, 21);
            this.qualityCombo.TabIndex = 21;
            this.qualityCombo.Text = "Neutral";
            this.qualityCombo.SelectedIndexChanged += new System.EventHandler(this.value_Changed);
            // 
            // idLabel
            // 
            this.idLabel.Location = new System.Drawing.Point(12, 7);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(57, 20);
            this.idLabel.TabIndex = 22;
            this.idLabel.Text = "ID";
            this.idLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // idValueLabel
            // 
            this.idValueLabel.Location = new System.Drawing.Point(72, 7);
            this.idValueLabel.Name = "idValueLabel";
            this.idValueLabel.Size = new System.Drawing.Size(257, 20);
            this.idValueLabel.TabIndex = 23;
            this.idValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // modsField
            // 
            this.modsField.Location = new System.Drawing.Point(75, 184);
            this.modsField.Name = "modsField";
            this.modsField.Size = new System.Drawing.Size(256, 20);
            this.modsField.TabIndex = 25;
            this.modsField.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // modsLabel
            // 
            this.modsLabel.Location = new System.Drawing.Point(12, 182);
            this.modsLabel.Name = "modsLabel";
            this.modsLabel.Size = new System.Drawing.Size(57, 20);
            this.modsLabel.TabIndex = 24;
            this.modsLabel.Text = "Mods";
            this.modsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MagicEffectEditDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(341, 367);
            this.Controls.Add(this.modsField);
            this.Controls.Add(this.modsLabel);
            this.Controls.Add(this.idValueLabel);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.qualityCombo);
            this.Controls.Add(this.raritySpinner);
            this.Controls.Add(this.typeCombo);
            this.Controls.Add(this.effectField);
            this.Controls.Add(this.suffixField);
            this.Controls.Add(this.prefixField);
            this.Controls.Add(this.schoolField);
            this.Controls.Add(this.nameField);
            this.Controls.Add(this.effectLabel);
            this.Controls.Add(this.suffixLabel);
            this.Controls.Add(this.prefixLabel);
            this.Controls.Add(this.rarityLabel);
            this.Controls.Add(this.qualityLabel);
            this.Controls.Add(this.schoolLabel);
            this.Controls.Add(this.typeLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MagicEffectEditDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Magic Effect";
            ((System.ComponentModel.ISupportInitialize)(this.raritySpinner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.Label schoolLabel;
        private System.Windows.Forms.Label qualityLabel;
        private System.Windows.Forms.Label rarityLabel;
        private System.Windows.Forms.Label prefixLabel;
        private System.Windows.Forms.Label suffixLabel;
        private System.Windows.Forms.Label effectLabel;
        private System.Windows.Forms.TextBox nameField;
        private System.Windows.Forms.TextBox schoolField;
        private System.Windows.Forms.TextBox prefixField;
        private System.Windows.Forms.TextBox suffixField;
        private System.Windows.Forms.TextBox effectField;
        private System.Windows.Forms.ComboBox typeCombo;
        private System.Windows.Forms.NumericUpDown raritySpinner;
        private System.Windows.Forms.ComboBox qualityCombo;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label idValueLabel;
        private System.Windows.Forms.TextBox modsField;
        private System.Windows.Forms.Label modsLabel;
    }
}