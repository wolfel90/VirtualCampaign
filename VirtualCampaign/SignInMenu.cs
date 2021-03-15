using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualCampaign.net;

namespace VirtualCampaign {
    public partial class SignInMenu : Form {
        private static List<char> illegal = new List<char>();
        private bool newAccount = false;

        static SignInMenu() {
            illegal = new List<char>();
            string ill = "'`<>\"@#$% \\|()/,.;:*{}[]";
            foreach(char c in ill) {
                illegal.Add(c);
            }
        }

        public SignInMenu() {
            InitializeComponent();
        }

        private void signinButton_Click(object sender, EventArgs e) {
            if (containsIllegal(usernameField.Text) || containsIllegal(pwdField.Text) || (newAccount && containsIllegal(repwdField.Text))) {
                errorLabel.Text = "Username/Password contains illegal characters";
                Console.Out.WriteLine("Username/Password contains illegal characters");
            } else if(String.IsNullOrWhiteSpace(usernameField.Text) || String.IsNullOrWhiteSpace(pwdField.Text) || (newAccount && String.IsNullOrWhiteSpace(repwdField.Text))) {
                errorLabel.Text = "Username and Password cannot be empty";
            } else {
                if(newAccount) {
                    if(pwdField.Text.Equals(repwdField.Text)) {
                        string response;
                        bool result = UserManager.attemptRegistration(usernameField.Text, pwdField.Text, out response);
                        errorLabel.Text = response;
                        if(result) {
                            signin_Triggered();
                        }
                    } else {
                        errorLabel.Text = "Passwords do not match";
                    }
                } else {
                    signin_Triggered();
                }
                
            }
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void textboxKey_Press(object sender, KeyPressEventArgs e) {
            
        }

        private void signin_Triggered() {
            string rem = "";
            if (UserManager.attemptSignIn(usernameField.Text, pwdField.Text, out rem)) {
                try {
                    if (rememberCheck.Checked) {
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + "StrangeRatio" + Path.DirectorySeparatorChar + "prefs";
                        System.IO.Directory.CreateDirectory(path);
                        path += Path.DirectorySeparatorChar + "user.txt";
                        using (StreamWriter writer = new StreamWriter(path, false)) {
                            writer.WriteLine(usernameField.Text);
                            writer.WriteLine(rem);
                        }
                    } else {
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + "StrangeRatio" + Path.DirectorySeparatorChar + "prefs"
                            + Path.DirectorySeparatorChar + "user.txt";
                        if (File.Exists(path)) {
                            System.IO.File.Delete(path);
                        }
                    }
                } catch (Exception x) {
                    Console.Error.WriteLine(x);
                }
                this.Close();
            } else {
                errorLabel.Text = "Failed to sign in";
                Console.Out.WriteLine("Failed to sign in");
            }
        }

        private bool containsIllegal(string check) {
            foreach(char c in illegal) {
                if(check.Contains(c)) {
                    return true;
                }
            }
            return false;
        }

        private void newAccountButton_Click(object sender, EventArgs e) {
            repwdPanel.Visible = true;
            repwdPanel.Location = new Point(0, 90);
            rememberCheck.Location = new Point(rememberCheck.Location.X, repwdPanel.Bottom + 5);
            signinButton.Location = new Point(signinButton.Location.X, rememberCheck.Bottom + 5);
            cancelButton.Location = new Point(cancelButton.Location.X, rememberCheck.Bottom + 5);
            errorLabel.Location = new Point(errorLabel.Location.X, signinButton.Bottom + 5);
            signinButton.Text = "Register";
            newAccountButton.Visible = false;
            newAccount = true;
        }
    }
}
