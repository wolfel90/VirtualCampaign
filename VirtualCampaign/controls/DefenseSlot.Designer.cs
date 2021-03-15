namespace VirtualCampaign.controls {
    partial class DefenseSlot {
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
            this.nameField = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.defSpinner = new System.Windows.Forms.NumericUpDown();
            this.mdefSpinner = new System.Windows.Forms.NumericUpDown();
            this.equipSlot1 = new VirtualCampaign.controls.EquipSlot();
            this.defCompField = new VirtualCampaign.controls.CompoundedStatField();
            this.defLabel = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.mdefLabel = new System.Windows.Forms.Label();
            this.mdefCompField = new VirtualCampaign.controls.CompoundedStatField();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.baseDEFSlot = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.defSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mdefSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // nameField
            // 
            this.nameField.Location = new System.Drawing.Point(63, 4);
            this.nameField.Name = "nameField";
            this.nameField.Size = new System.Drawing.Size(144, 20);
            this.nameField.TabIndex = 0;
            // 
            // nameLabel
            // 
            this.nameLabel.Location = new System.Drawing.Point(15, 4);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(42, 20);
            this.nameLabel.TabIndex = 2;
            this.nameLabel.Text = "Name";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // defSpinner
            // 
            this.defSpinner.Location = new System.Drawing.Point(63, 30);
            this.defSpinner.Name = "defSpinner";
            this.defSpinner.Size = new System.Drawing.Size(55, 20);
            this.defSpinner.TabIndex = 3;
            // 
            // mdefSpinner
            // 
            this.mdefSpinner.Location = new System.Drawing.Point(63, 56);
            this.mdefSpinner.Name = "mdefSpinner";
            this.mdefSpinner.Size = new System.Drawing.Size(55, 20);
            this.mdefSpinner.TabIndex = 4;
            // 
            // equipSlot1
            // 
            this.equipSlot1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.equipSlot1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.equipSlot1.DropMode = 2;
            this.equipSlot1.editable = true;
            this.equipSlot1.emptyIconSrc = null;
            this.equipSlot1.GrabMode = 2;
            this.equipSlot1.itemData = null;
            this.equipSlot1.Location = new System.Drawing.Point(341, 17);
            this.equipSlot1.Margin = new System.Windows.Forms.Padding(0);
            this.equipSlot1.Name = "equipSlot1";
            this.equipSlot1.Size = new System.Drawing.Size(66, 66);
            this.equipSlot1.TabIndex = 5;
            // 
            // defCompField
            // 
            this.defCompField.Cursor = System.Windows.Forms.Cursors.Hand;
            this.defCompField.Editable = true;
            this.defCompField.Location = new System.Drawing.Point(124, 30);
            this.defCompField.Max = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.defCompField.Min = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.defCompField.Name = "defCompField";
            this.defCompField.Size = new System.Drawing.Size(83, 20);
            this.defCompField.statKey = "";
            this.defCompField.TabIndex = 1;
            // 
            // defLabel
            // 
            this.defLabel.Location = new System.Drawing.Point(15, 30);
            this.defLabel.Name = "defLabel";
            this.defLabel.Size = new System.Drawing.Size(42, 20);
            this.defLabel.TabIndex = 52;
            this.defLabel.Text = "DEF";
            this.defLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // deleteButton
            // 
            this.deleteButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.deleteButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.deleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.Location = new System.Drawing.Point(0, 0);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(12, 98);
            this.deleteButton.TabIndex = 53;
            this.deleteButton.Text = "X";
            this.deleteButton.UseVisualStyleBackColor = true;
            // 
            // mdefLabel
            // 
            this.mdefLabel.Location = new System.Drawing.Point(15, 56);
            this.mdefLabel.Name = "mdefLabel";
            this.mdefLabel.Size = new System.Drawing.Size(42, 20);
            this.mdefLabel.TabIndex = 54;
            this.mdefLabel.Text = "MDEF";
            this.mdefLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mdefCompField
            // 
            this.mdefCompField.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mdefCompField.Editable = true;
            this.mdefCompField.Location = new System.Drawing.Point(124, 56);
            this.mdefCompField.Max = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.mdefCompField.Min = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.mdefCompField.Name = "mdefCompField";
            this.mdefCompField.Size = new System.Drawing.Size(83, 20);
            this.mdefCompField.statKey = "";
            this.mdefCompField.TabIndex = 56;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(283, 56);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(55, 20);
            this.numericUpDown1.TabIndex = 57;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(213, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 58;
            this.label1.Text = "Hit Difficulty";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // baseDEFSlot
            // 
            this.baseDEFSlot.AutoSize = true;
            this.baseDEFSlot.Location = new System.Drawing.Point(213, 33);
            this.baseDEFSlot.Name = "baseDEFSlot";
            this.baseDEFSlot.Size = new System.Drawing.Size(96, 17);
            this.baseDEFSlot.TabIndex = 59;
            this.baseDEFSlot.Text = "Add Base DEF";
            this.baseDEFSlot.UseVisualStyleBackColor = true;
            // 
            // DefenseSlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.baseDEFSlot);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.mdefCompField);
            this.Controls.Add(this.mdefLabel);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.defLabel);
            this.Controls.Add(this.equipSlot1);
            this.Controls.Add(this.mdefSpinner);
            this.Controls.Add(this.defSpinner);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.defCompField);
            this.Controls.Add(this.nameField);
            this.Name = "DefenseSlot";
            this.Size = new System.Drawing.Size(413, 98);
            ((System.ComponentModel.ISupportInitialize)(this.defSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mdefSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameField;
        private CompoundedStatField defCompField;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.NumericUpDown defSpinner;
        private System.Windows.Forms.NumericUpDown mdefSpinner;
        private EquipSlot equipSlot1;
        private System.Windows.Forms.Label defLabel;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Label mdefLabel;
        private CompoundedStatField mdefCompField;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox baseDEFSlot;
    }
}
