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
    public partial class VersionHistory : Form {
        public VersionHistory() {
            InitializeComponent();
        }

        private void VersionHistory_Load(object sender, EventArgs e) {
            logTextField.Text = VirtualCampaign.Properties.Resources.version_history;
        }
    }
}
