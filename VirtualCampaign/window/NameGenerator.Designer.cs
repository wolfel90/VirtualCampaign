namespace VirtualCampaign.window {
    partial class NameGenerator {
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
            this.genderSelection = new System.Windows.Forms.ComboBox();
            this.generateButton = new System.Windows.Forms.Button();
            this.nameListPanel = new System.Windows.Forms.Panel();
            this.nameTextField = new System.Windows.Forms.TextBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.genderLabel = new System.Windows.Forms.Label();
            this.phoneticCheck = new System.Windows.Forms.CheckBox();
            this.nameListPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // genderSelection
            // 
            this.genderSelection.FormattingEnabled = true;
            this.genderSelection.Items.AddRange(new object[] {
            "Either",
            "Male",
            "Female"});
            this.genderSelection.Location = new System.Drawing.Point(93, 10);
            this.genderSelection.Name = "genderSelection";
            this.genderSelection.Size = new System.Drawing.Size(118, 21);
            this.genderSelection.TabIndex = 0;
            this.genderSelection.Text = "Either";
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(12, 70);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(75, 23);
            this.generateButton.TabIndex = 1;
            this.generateButton.Text = "Generate";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // nameListPanel
            // 
            this.nameListPanel.AutoScroll = true;
            this.nameListPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.nameListPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nameListPanel.Controls.Add(this.nameTextField);
            this.nameListPanel.Location = new System.Drawing.Point(11, 99);
            this.nameListPanel.Name = "nameListPanel";
            this.nameListPanel.Size = new System.Drawing.Size(200, 153);
            this.nameListPanel.TabIndex = 3;
            // 
            // nameTextField
            // 
            this.nameTextField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameTextField.Location = new System.Drawing.Point(0, 0);
            this.nameTextField.Multiline = true;
            this.nameTextField.Name = "nameTextField";
            this.nameTextField.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.nameTextField.Size = new System.Drawing.Size(198, 151);
            this.nameTextField.TabIndex = 3;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(135, 70);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 4;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // genderLabel
            // 
            this.genderLabel.Location = new System.Drawing.Point(8, 10);
            this.genderLabel.Name = "genderLabel";
            this.genderLabel.Size = new System.Drawing.Size(79, 23);
            this.genderLabel.TabIndex = 5;
            this.genderLabel.Text = "Gender";
            this.genderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // phoneticCheck
            // 
            this.phoneticCheck.AutoSize = true;
            this.phoneticCheck.Location = new System.Drawing.Point(60, 47);
            this.phoneticCheck.Name = "phoneticCheck";
            this.phoneticCheck.Size = new System.Drawing.Size(98, 17);
            this.phoneticCheck.TabIndex = 6;
            this.phoneticCheck.Text = "Phonetic Mode";
            this.phoneticCheck.UseVisualStyleBackColor = true;
            // 
            // NameGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 264);
            this.Controls.Add(this.phoneticCheck);
            this.Controls.Add(this.genderLabel);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.nameListPanel);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.genderSelection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NameGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Name Generator";
            this.nameListPanel.ResumeLayout(false);
            this.nameListPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox genderSelection;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.Panel nameListPanel;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Label genderLabel;
        private System.Windows.Forms.TextBox nameTextField;
        private System.Windows.Forms.CheckBox phoneticCheck;
    }
}