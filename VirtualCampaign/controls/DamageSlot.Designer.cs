namespace VirtualCampaign.controls {
    partial class DamageSlot {
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.basicTypeCombo = new System.Windows.Forms.ComboBox();
            this.specificTypeList = new System.Windows.Forms.CheckedListBox();
            this.powerSpinner = new System.Windows.Forms.NumericUpDown();
            this.rngSpinner = new System.Windows.Forms.NumericUpDown();
            this.dmgField = new System.Windows.Forms.TextBox();
            this.baseDMGCheck = new System.Windows.Forms.CheckBox();
            this.dmgLabel = new System.Windows.Forms.Label();
            this.baseDMGLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.powLabel = new System.Windows.Forms.Label();
            this.rngLabel = new System.Windows.Forms.Label();
            this.typeLabel = new System.Windows.Forms.Label();
            this.equipmentSlot = new VirtualCampaign.controls.EquipSlot();
            this.deleteButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.powerSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rngSpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(61, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(145, 20);
            this.textBox1.TabIndex = 0;
            // 
            // basicTypeCombo
            // 
            this.basicTypeCombo.FormattingEnabled = true;
            this.basicTypeCombo.Items.AddRange(new object[] {
            "Physical",
            "Magical",
            "Psionic"});
            this.basicTypeCombo.Location = new System.Drawing.Point(252, 5);
            this.basicTypeCombo.Name = "basicTypeCombo";
            this.basicTypeCombo.Size = new System.Drawing.Size(84, 21);
            this.basicTypeCombo.TabIndex = 1;
            this.basicTypeCombo.Text = "Physical";
            this.basicTypeCombo.SelectedIndexChanged += new System.EventHandler(this.basicTypeCombo_SelectedIndexChanged);
            // 
            // specificTypeList
            // 
            this.specificTypeList.FormattingEnabled = true;
            this.specificTypeList.Items.AddRange(new object[] {
            "Blunt",
            "Cold",
            "Dark",
            "Force",
            "Heat",
            "Light",
            "Piercing",
            "Slashing",
            "Shock"});
            this.specificTypeList.Location = new System.Drawing.Point(252, 31);
            this.specificTypeList.Name = "specificTypeList";
            this.specificTypeList.Size = new System.Drawing.Size(84, 64);
            this.specificTypeList.TabIndex = 2;
            // 
            // powerSpinner
            // 
            this.powerSpinner.Location = new System.Drawing.Point(60, 76);
            this.powerSpinner.Name = "powerSpinner";
            this.powerSpinner.Size = new System.Drawing.Size(58, 20);
            this.powerSpinner.TabIndex = 3;
            // 
            // rngSpinner
            // 
            this.rngSpinner.Location = new System.Drawing.Point(172, 76);
            this.rngSpinner.Name = "rngSpinner";
            this.rngSpinner.Size = new System.Drawing.Size(59, 20);
            this.rngSpinner.TabIndex = 4;
            // 
            // dmgField
            // 
            this.dmgField.Location = new System.Drawing.Point(60, 30);
            this.dmgField.Name = "dmgField";
            this.dmgField.Size = new System.Drawing.Size(90, 20);
            this.dmgField.TabIndex = 5;
            // 
            // baseDMGCheck
            // 
            this.baseDMGCheck.AutoSize = true;
            this.baseDMGCheck.Location = new System.Drawing.Point(60, 56);
            this.baseDMGCheck.Name = "baseDMGCheck";
            this.baseDMGCheck.Size = new System.Drawing.Size(115, 17);
            this.baseDMGCheck.TabIndex = 6;
            this.baseDMGCheck.Text = "Add Base Damage";
            this.baseDMGCheck.UseVisualStyleBackColor = true;
            // 
            // dmgLabel
            // 
            this.dmgLabel.Location = new System.Drawing.Point(13, 31);
            this.dmgLabel.Name = "dmgLabel";
            this.dmgLabel.Size = new System.Drawing.Size(42, 20);
            this.dmgLabel.TabIndex = 7;
            this.dmgLabel.Text = "DMG";
            this.dmgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // baseDMGLabel
            // 
            this.baseDMGLabel.Location = new System.Drawing.Point(156, 30);
            this.baseDMGLabel.Name = "baseDMGLabel";
            this.baseDMGLabel.Size = new System.Drawing.Size(90, 20);
            this.baseDMGLabel.TabIndex = 8;
            this.baseDMGLabel.Text = "(+0)";
            this.baseDMGLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nameLabel
            // 
            this.nameLabel.Location = new System.Drawing.Point(13, 4);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(42, 20);
            this.nameLabel.TabIndex = 9;
            this.nameLabel.Text = "Name";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // powLabel
            // 
            this.powLabel.Location = new System.Drawing.Point(13, 77);
            this.powLabel.Name = "powLabel";
            this.powLabel.Size = new System.Drawing.Size(42, 20);
            this.powLabel.TabIndex = 10;
            this.powLabel.Text = "POW";
            this.powLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rngLabel
            // 
            this.rngLabel.Location = new System.Drawing.Point(124, 76);
            this.rngLabel.Name = "rngLabel";
            this.rngLabel.Size = new System.Drawing.Size(42, 20);
            this.rngLabel.TabIndex = 11;
            this.rngLabel.Text = "Range";
            this.rngLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // typeLabel
            // 
            this.typeLabel.Location = new System.Drawing.Point(212, 5);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(34, 20);
            this.typeLabel.TabIndex = 12;
            this.typeLabel.Text = "Type";
            this.typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // equipmentSlot
            // 
            this.equipmentSlot.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.equipmentSlot.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.equipmentSlot.DropMode = 2;
            this.equipmentSlot.editable = true;
            this.equipmentSlot.emptyIconSrc = null;
            this.equipmentSlot.GrabMode = 2;
            this.equipmentSlot.itemData = null;
            this.equipmentSlot.Location = new System.Drawing.Point(341, 17);
            this.equipmentSlot.Margin = new System.Windows.Forms.Padding(0);
            this.equipmentSlot.Name = "equipmentSlot";
            this.equipmentSlot.Size = new System.Drawing.Size(66, 66);
            this.equipmentSlot.TabIndex = 13;
            // 
            // deleteButton
            // 
            this.deleteButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.deleteButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.deleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.Location = new System.Drawing.Point(0, 0);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(12, 100);
            this.deleteButton.TabIndex = 14;
            this.deleteButton.Text = "X";
            this.deleteButton.UseVisualStyleBackColor = true;
            // 
            // DamageSlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.equipmentSlot);
            this.Controls.Add(this.typeLabel);
            this.Controls.Add(this.rngLabel);
            this.Controls.Add(this.powLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.baseDMGLabel);
            this.Controls.Add(this.dmgLabel);
            this.Controls.Add(this.baseDMGCheck);
            this.Controls.Add(this.dmgField);
            this.Controls.Add(this.rngSpinner);
            this.Controls.Add(this.powerSpinner);
            this.Controls.Add(this.specificTypeList);
            this.Controls.Add(this.basicTypeCombo);
            this.Controls.Add(this.textBox1);
            this.Name = "DamageSlot";
            this.Size = new System.Drawing.Size(415, 100);
            ((System.ComponentModel.ISupportInitialize)(this.powerSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rngSpinner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox basicTypeCombo;
        private System.Windows.Forms.CheckedListBox specificTypeList;
        private System.Windows.Forms.NumericUpDown powerSpinner;
        private System.Windows.Forms.NumericUpDown rngSpinner;
        private System.Windows.Forms.TextBox dmgField;
        private System.Windows.Forms.CheckBox baseDMGCheck;
        private System.Windows.Forms.Label dmgLabel;
        private System.Windows.Forms.Label baseDMGLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label powLabel;
        private System.Windows.Forms.Label rngLabel;
        private System.Windows.Forms.Label typeLabel;
        private EquipSlot equipmentSlot;
        private System.Windows.Forms.Button deleteButton;
    }
}
