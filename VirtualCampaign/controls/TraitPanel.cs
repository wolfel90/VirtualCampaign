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
using VirtualCampaign.sys;
using VirtualCampaign.events;
using VirtualCampaign.window;

namespace VirtualCampaign.controls {
    public partial class TraitPanel : UserControl {
        public event EventHandler ValueChanged;
        public event EventHandler TraitClicked;
        public event EventHandler ActivationChanged;
        public bool refreshing { get; set; }
        private Trait _trait;
        public Trait trait { get { return _trait; } set { setTrait(value); } }
        public long localID { get; set; } = -1;
        private bool _editable;
        public bool editable { get { return _editable; } set { setEditable(value); } }
        public bool generateTooltip;

        public TraitPanel() : base() {
            InitializeComponent();
            trait = new Trait();
            _editable = true;
            refreshing = false;
            generateTooltip = true;
        }

        public void setTrait(Trait t) {
            _trait = t;
            titleLabel.Text = t.title;
            costLabel.Text = t.cost.ToString();
            activeCheck.Checked = t.active;
        }

        public string generateString() {
            return trait.generateString();
        }

        private void upButton_Click(object sender, EventArgs e) {
            TraitList pList;
            if(Parent is TraitList) {
                pList = (TraitList)Parent;
                if(pList.IndexOf(this) > 0) pList.SwapTraits(pList.IndexOf(this), pList.IndexOf(this) - 1);
            }
        }

        private void downButton_Click(object sender, EventArgs e) {
            TraitList pList;
            if (Parent is TraitList) {
                pList = (TraitList)Parent;
                if(pList.IndexOf(this) < pList.GetTraitCount() - 1) pList.SwapTraits(pList.IndexOf(this), pList.IndexOf(this) + 1);
            }
        }

        private void removeButton_Click(object sender, EventArgs e) {
            TraitList pList;
            if (Parent is TraitList) {
                pList = (TraitList)Parent;
                pList.RemoveTraitAt(pList.IndexOf(this));
            }
        }

        private void activeCheck_CheckedChanged(object sender, EventArgs e) {
            _trait.active = activeCheck.Checked;
            OnChanged(EventArgs.Empty);
            OnActivationChanged(EventArgs.Empty);
        }

        public void setEditable(bool val) {
            _editable = val;
            removeButton.Enabled = val;
            removeButton.Visible = val;
            activeCheck.Enabled = val;
            upButton.Enabled = val;
            upButton.Visible = val;
            downButton.Enabled = val;
            downButton.Visible = val;
        }

        protected virtual void OnChanged(EventArgs e) {
            ValueChanged?.Invoke(this, e);
        }

        private void titleLabel_Click(object sender, EventArgs e) {
            OnTraitClicked(e);
        }

        protected virtual void OnTraitClicked(EventArgs e) {
            TraitClicked?.Invoke(this, e);
        }

        protected virtual void OnActivationChanged(EventArgs e) {
            ActivationChanged?.Invoke(this, e);
        }

        private void titleLabel_MouseEnter(object sender, EventArgs e) {
            if(generateTooltip && trait != null) {
                AdaptableTooltipHandler.CurrentControl = this;
                AdaptableTooltipHandler.Tooltip.generateTooltip(trait);
                AdaptableTooltipHandler.Tooltip.Show();
                AdaptableTooltipHandler.Tooltip.Location = this.PointToScreen(new Point(0 + this.Width + 5, 0));
            }
        }

        private void titleLabel_MouseLeave(object sender, EventArgs e) {
            if (AdaptableTooltipHandler.CurrentControl == this) {
                AdaptableTooltipHandler.CurrentControl = null;
                AdaptableTooltipHandler.Tooltip.Hide();
            }
        }
    }
}
