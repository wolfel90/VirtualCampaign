using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualCampaign.window {
    public partial class SimpleMessageBox : Form {
        public string result = "";

        public SimpleMessageBox() {
            InitializeComponent();
            
        }

        public SimpleMessageBox(String title) {
            InitializeComponent();
            this.Text = title;
        }

        private void okButton_Click(object sender, EventArgs e) {
            result = textField.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
