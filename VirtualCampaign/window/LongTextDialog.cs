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
    public partial class LongTextDialog : Form {
        public string resultText;

        public LongTextDialog() {
            InitializeComponent();

            resultText = "";
        }

        private void okButton_Click(object sender, EventArgs e) {
            resultText = textField.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            resultText = "";
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
