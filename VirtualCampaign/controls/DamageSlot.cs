using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualCampaign.controls {
    public partial class DamageSlot : UserControl {
        public DamageSlot() {
            InitializeComponent();
        }

        private void basicTypeCombo_SelectedIndexChanged(object sender, EventArgs e) {
            switch(basicTypeCombo.SelectedText) {
                case "Psionic":
                    specificTypeList.Enabled = false;
                    baseDMGCheck.Enabled = false;
                    powerSpinner.Enabled = false;
                    dmgLabel.Text = "DMG";
                    powLabel.Text = "POW";
                    break;
                case "Magical":
                    specificTypeList.Enabled = true;
                    baseDMGCheck.Enabled = true;
                    powerSpinner.Enabled = true;
                    dmgLabel.Text = "MDMG";
                    powLabel.Text = "MPOW";
                    break;
                case "Physical":
                default:
                    specificTypeList.Enabled = true;
                    baseDMGCheck.Enabled = true;
                    powerSpinner.Enabled = true;
                    dmgLabel.Text = "DMG";
                    powLabel.Text = "POW";
                    break;
            }
        }
    }
}
