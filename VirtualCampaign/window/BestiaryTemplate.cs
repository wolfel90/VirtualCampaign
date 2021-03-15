using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualCampaign.data;

namespace VirtualCampaign.window {
    public partial class BestiaryTemplate : UserControl {
        public int SelectedLevel { get { return keynotePanel.SelectedValue.X; } }

        public BestiaryTemplate() {
            InitializeComponent();

            keynotePanel.BranchLabelsVisible = true;
            statsTablePanel.HorizontalScroll.Visible = false;
        }

        private void keynotePanel_SelectionChanged(object sender, EventArgs e) {
            keyNoteLevelLabel.Text = SelectedLevel.ToString();
        }

        private void conductBar_Scroll(object sender, EventArgs e) {
            conductValLabel.Text = conductBar.Value.ToString();
        }

        private void moralityBar_Scroll(object sender, EventArgs e) {
            moralityValLabel.Text = moralityBar.Value.ToString();
        }

        private void anyProfPanel_ValueChanged(object sender, EventArgs e) {
            int pp = 0;
            pp += strProfPanel.Baseline + agiProfPanel.Baseline + intProfPanel.Baseline + forProfPanel.Baseline + chaProfPanel.Baseline;
            ppLabel.Text = pp.ToString();
        }

        public BestiaryTemplateKeynoteData GenerateBestiaryKeynote() {
            BestiaryTemplateKeynoteData result = new BestiaryTemplateKeynoteData();

            return result;
        }
    }
}
