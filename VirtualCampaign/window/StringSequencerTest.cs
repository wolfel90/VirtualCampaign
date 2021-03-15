using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualCampaign.sys;

namespace VirtualCampaign.window {
    public partial class StringSequencerTest : Form {
        public StringSequencerTest() {
            InitializeComponent();
        }

        private void inBox_TextChanged(object sender, EventArgs e) {
            outBox.Text = StringFunctions.simplifyDiceString(inBox.Text);
        }
    }
}
