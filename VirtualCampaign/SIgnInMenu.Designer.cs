namespace VirtualCampaign {
    partial class SignInMenu {
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
            this.usernameLabel = new System.Windows.Forms.Label();
            this.usernameField = new System.Windows.Forms.TextBox();
            this.pwdLabel = new System.Windows.Forms.Label();
            this.pwdField = new System.Windows.Forms.TextBox();
            this.signinButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.rememberCheck = new System.Windows.Forms.CheckBox();
            this.errorLabel = new System.Windows.Forms.Label();
            this.newAccountButton = new System.Windows.Forms.Button();
            this.repwdField = new System.Windows.Forms.TextBox();
            this.repwdLabel = new System.Windows.Forms.Label();
            this.repwdPanel = new System.Windows.Forms.Panel();
            this.repwdPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.Location = new System.Drawing.Point(12, 9);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(149, 13);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "Username";
            this.usernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // usernameField
            // 
            this.usernameField.Location = new System.Drawing.Point(12, 25);
            this.usernameField.Name = "usernameField";
            this.usernameField.Size = new System.Drawing.Size(147, 20);
            this.usernameField.TabIndex = 1;
            this.usernameField.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textboxKey_Press);
            // 
            // pwdLabel
            // 
            this.pwdLabel.Location = new System.Drawing.Point(12, 48);
            this.pwdLabel.Name = "pwdLabel";
            this.pwdLabel.Size = new System.Drawing.Size(147, 13);
            this.pwdLabel.TabIndex = 2;
            this.pwdLabel.Text = "Password";
            this.pwdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pwdField
            // 
            this.pwdField.Location = new System.Drawing.Point(12, 64);
            this.pwdField.Name = "pwdField";
            this.pwdField.PasswordChar = '●';
            this.pwdField.Size = new System.Drawing.Size(147, 20);
            this.pwdField.TabIndex = 2;
            this.pwdField.UseSystemPasswordChar = true;
            this.pwdField.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textboxKey_Press);
            // 
            // signinButton
            // 
            this.signinButton.Location = new System.Drawing.Point(8, 116);
            this.signinButton.Name = "signinButton";
            this.signinButton.Size = new System.Drawing.Size(75, 23);
            this.signinButton.TabIndex = 4;
            this.signinButton.Text = "Sign In";
            this.signinButton.UseVisualStyleBackColor = true;
            this.signinButton.Click += new System.EventHandler(this.signinButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(89, 116);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // rememberCheck
            // 
            this.rememberCheck.AutoSize = true;
            this.rememberCheck.Location = new System.Drawing.Point(38, 90);
            this.rememberCheck.Name = "rememberCheck";
            this.rememberCheck.Size = new System.Drawing.Size(95, 17);
            this.rememberCheck.TabIndex = 12;
            this.rememberCheck.TabStop = false;
            this.rememberCheck.Text = "Remember Me";
            this.rememberCheck.UseVisualStyleBackColor = true;
            // 
            // errorLabel
            // 
            this.errorLabel.Location = new System.Drawing.Point(6, 177);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(155, 54);
            this.errorLabel.TabIndex = 7;
            this.errorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // newAccountButton
            // 
            this.newAccountButton.Location = new System.Drawing.Point(8, 145);
            this.newAccountButton.Name = "newAccountButton";
            this.newAccountButton.Size = new System.Drawing.Size(156, 23);
            this.newAccountButton.TabIndex = 8;
            this.newAccountButton.Text = "New User?";
            this.newAccountButton.UseVisualStyleBackColor = true;
            this.newAccountButton.Click += new System.EventHandler(this.newAccountButton_Click);
            // 
            // repwdField
            // 
            this.repwdField.Location = new System.Drawing.Point(12, 18);
            this.repwdField.Name = "repwdField";
            this.repwdField.PasswordChar = '●';
            this.repwdField.Size = new System.Drawing.Size(147, 20);
            this.repwdField.TabIndex = 3;
            this.repwdField.UseSystemPasswordChar = true;
            // 
            // repwdLabel
            // 
            this.repwdLabel.Location = new System.Drawing.Point(12, 2);
            this.repwdLabel.Name = "repwdLabel";
            this.repwdLabel.Size = new System.Drawing.Size(147, 13);
            this.repwdLabel.TabIndex = 10;
            this.repwdLabel.Text = "Re-Type Password";
            this.repwdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // repwdPanel
            // 
            this.repwdPanel.Controls.Add(this.repwdLabel);
            this.repwdPanel.Controls.Add(this.repwdField);
            this.repwdPanel.Location = new System.Drawing.Point(-3, 236);
            this.repwdPanel.Name = "repwdPanel";
            this.repwdPanel.Size = new System.Drawing.Size(176, 41);
            this.repwdPanel.TabIndex = 3;
            this.repwdPanel.Visible = false;
            // 
            // SignInMenu
            // 
            this.AcceptButton = this.signinButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(171, 249);
            this.ControlBox = false;
            this.Controls.Add(this.repwdPanel);
            this.Controls.Add(this.newAccountButton);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.rememberCheck);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.signinButton);
            this.Controls.Add(this.pwdField);
            this.Controls.Add(this.pwdLabel);
            this.Controls.Add(this.usernameField);
            this.Controls.Add(this.usernameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SignInMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sign In";
            this.repwdPanel.ResumeLayout(false);
            this.repwdPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TextBox usernameField;
        private System.Windows.Forms.Label pwdLabel;
        private System.Windows.Forms.TextBox pwdField;
        private System.Windows.Forms.Button signinButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox rememberCheck;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Button newAccountButton;
        private System.Windows.Forms.TextBox repwdField;
        private System.Windows.Forms.Label repwdLabel;
        private System.Windows.Forms.Panel repwdPanel;
    }
}