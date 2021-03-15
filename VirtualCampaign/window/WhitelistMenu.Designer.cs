namespace VirtualCampaign.window {
    partial class WhitelistMenu {
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
            this.noneList = new System.Windows.Forms.ListBox();
            this.viewList = new System.Windows.Forms.ListBox();
            this.editList = new System.Windows.Forms.ListBox();
            this.noneLabel = new System.Windows.Forms.Label();
            this.viewLabel = new System.Windows.Forms.Label();
            this.editLabel = new System.Windows.Forms.Label();
            this.rvAddButton = new System.Windows.Forms.Button();
            this.rvRemoveButton = new System.Windows.Forms.Button();
            this.reAddButton = new System.Windows.Forms.Button();
            this.reRemoveButton = new System.Windows.Forms.Button();
            this.useCheck = new System.Windows.Forms.CheckBox();
            this.veAddButton = new System.Windows.Forms.Button();
            this.veRemoveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(12, 310);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cancelButton.Location = new System.Drawing.Point(93, 310);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // noneList
            // 
            this.noneList.FormattingEnabled = true;
            this.noneList.Location = new System.Drawing.Point(12, 51);
            this.noneList.Name = "noneList";
            this.noneList.Size = new System.Drawing.Size(156, 238);
            this.noneList.TabIndex = 2;
            // 
            // viewList
            // 
            this.viewList.FormattingEnabled = true;
            this.viewList.Location = new System.Drawing.Point(204, 51);
            this.viewList.Name = "viewList";
            this.viewList.Size = new System.Drawing.Size(156, 95);
            this.viewList.TabIndex = 3;
            // 
            // editList
            // 
            this.editList.FormattingEnabled = true;
            this.editList.Location = new System.Drawing.Point(204, 194);
            this.editList.Name = "editList";
            this.editList.Size = new System.Drawing.Size(156, 95);
            this.editList.TabIndex = 4;
            // 
            // noneLabel
            // 
            this.noneLabel.AutoSize = true;
            this.noneLabel.Location = new System.Drawing.Point(42, 35);
            this.noneLabel.Name = "noneLabel";
            this.noneLabel.Size = new System.Drawing.Size(85, 13);
            this.noneLabel.TabIndex = 5;
            this.noneLabel.Text = "Restricted Users";
            // 
            // viewLabel
            // 
            this.viewLabel.AutoSize = true;
            this.viewLabel.Location = new System.Drawing.Point(225, 35);
            this.viewLabel.Name = "viewLabel";
            this.viewLabel.Size = new System.Drawing.Size(115, 13);
            this.viewLabel.TabIndex = 6;
            this.viewLabel.Text = "Users with View Rights";
            // 
            // editLabel
            // 
            this.editLabel.AutoSize = true;
            this.editLabel.Location = new System.Drawing.Point(211, 178);
            this.editLabel.Name = "editLabel";
            this.editLabel.Size = new System.Drawing.Size(145, 13);
            this.editLabel.TabIndex = 7;
            this.editLabel.Text = "Users with View + Edit Rights";
            // 
            // rvAddButton
            // 
            this.rvAddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rvAddButton.Location = new System.Drawing.Point(174, 75);
            this.rvAddButton.Name = "rvAddButton";
            this.rvAddButton.Size = new System.Drawing.Size(24, 23);
            this.rvAddButton.TabIndex = 8;
            this.rvAddButton.Text = "˃";
            this.rvAddButton.UseVisualStyleBackColor = true;
            this.rvAddButton.Click += new System.EventHandler(this.rvAddButton_Click);
            // 
            // rvRemoveButton
            // 
            this.rvRemoveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rvRemoveButton.Location = new System.Drawing.Point(174, 104);
            this.rvRemoveButton.Name = "rvRemoveButton";
            this.rvRemoveButton.Size = new System.Drawing.Size(24, 23);
            this.rvRemoveButton.TabIndex = 9;
            this.rvRemoveButton.Text = "˂";
            this.rvRemoveButton.UseVisualStyleBackColor = true;
            this.rvRemoveButton.Click += new System.EventHandler(this.rvRemoveButton_Click);
            // 
            // reAddButton
            // 
            this.reAddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reAddButton.Location = new System.Drawing.Point(174, 216);
            this.reAddButton.Name = "reAddButton";
            this.reAddButton.Size = new System.Drawing.Size(24, 23);
            this.reAddButton.TabIndex = 10;
            this.reAddButton.Text = "˃";
            this.reAddButton.UseVisualStyleBackColor = true;
            this.reAddButton.Click += new System.EventHandler(this.reAddButton_Click);
            // 
            // reRemoveButton
            // 
            this.reRemoveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reRemoveButton.Location = new System.Drawing.Point(174, 245);
            this.reRemoveButton.Name = "reRemoveButton";
            this.reRemoveButton.Size = new System.Drawing.Size(24, 23);
            this.reRemoveButton.TabIndex = 11;
            this.reRemoveButton.Text = "˂";
            this.reRemoveButton.UseVisualStyleBackColor = true;
            this.reRemoveButton.Click += new System.EventHandler(this.reRemoveButton_Click);
            // 
            // useCheck
            // 
            this.useCheck.AutoSize = true;
            this.useCheck.Location = new System.Drawing.Point(12, 12);
            this.useCheck.Name = "useCheck";
            this.useCheck.Size = new System.Drawing.Size(88, 17);
            this.useCheck.TabIndex = 12;
            this.useCheck.Text = "Use Whitelist";
            this.useCheck.UseVisualStyleBackColor = true;
            this.useCheck.CheckedChanged += new System.EventHandler(this.useCheck_CheckedChanged);
            // 
            // veAddButton
            // 
            this.veAddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.veAddButton.Location = new System.Drawing.Point(254, 152);
            this.veAddButton.Name = "veAddButton";
            this.veAddButton.Size = new System.Drawing.Size(24, 23);
            this.veAddButton.TabIndex = 13;
            this.veAddButton.Text = "˅";
            this.veAddButton.UseVisualStyleBackColor = true;
            this.veAddButton.Click += new System.EventHandler(this.veAddButton_Click);
            // 
            // veRemoveButton
            // 
            this.veRemoveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.veRemoveButton.Location = new System.Drawing.Point(284, 152);
            this.veRemoveButton.Name = "veRemoveButton";
            this.veRemoveButton.Size = new System.Drawing.Size(24, 23);
            this.veRemoveButton.TabIndex = 14;
            this.veRemoveButton.Text = "˄";
            this.veRemoveButton.UseVisualStyleBackColor = true;
            this.veRemoveButton.Click += new System.EventHandler(this.veRemoveButton_Click);
            // 
            // WhitelistMenu
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(369, 342);
            this.Controls.Add(this.veRemoveButton);
            this.Controls.Add(this.veAddButton);
            this.Controls.Add(this.useCheck);
            this.Controls.Add(this.reRemoveButton);
            this.Controls.Add(this.reAddButton);
            this.Controls.Add(this.rvRemoveButton);
            this.Controls.Add(this.rvAddButton);
            this.Controls.Add(this.editLabel);
            this.Controls.Add(this.viewLabel);
            this.Controls.Add(this.noneLabel);
            this.Controls.Add(this.editList);
            this.Controls.Add(this.viewList);
            this.Controls.Add(this.noneList);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "WhitelistMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Whitelist";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ListBox noneList;
        private System.Windows.Forms.ListBox viewList;
        private System.Windows.Forms.ListBox editList;
        private System.Windows.Forms.Label noneLabel;
        private System.Windows.Forms.Label viewLabel;
        private System.Windows.Forms.Label editLabel;
        private System.Windows.Forms.Button rvAddButton;
        private System.Windows.Forms.Button rvRemoveButton;
        private System.Windows.Forms.Button reAddButton;
        private System.Windows.Forms.Button reRemoveButton;
        private System.Windows.Forms.CheckBox useCheck;
        private System.Windows.Forms.Button veAddButton;
        private System.Windows.Forms.Button veRemoveButton;
    }
}