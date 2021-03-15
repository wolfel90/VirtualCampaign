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
    public partial class VocationPanel : UserControl {
        private bool _editable;
        public bool editable { get { return _editable; } set { setEditable(value); } }
        public String Title { get { return nameLabel.Text; } set { nameLabel.Text = value; } }

        public VocationPanel() {
            InitializeComponent();
        }

        public VocationPanel(String name) : base() {
            nameLabel.Text = name;
        }

        public void setEditable(bool val) {
            _editable = val;
            removeButton.Enabled = val;
            removeButton.Visible = val;
            upButton.Enabled = val;
            upButton.Visible = val;
            downButton.Enabled = val;
            downButton.Visible = val;
        }

        private void upButton_Click(object sender, EventArgs e) {
            VocationList pList;
            if (Parent is VocationList) {
                pList = (VocationList)Parent;
                if (pList.IndexOf(this) > 0) pList.SwapVocations(pList.IndexOf(this), pList.IndexOf(this) - 1);
            }
        }

        private void downButton_Click(object sender, EventArgs e) {
            VocationList pList;
            if (Parent is VocationList) {
                pList = (VocationList)Parent;
                if (pList.IndexOf(this) < pList.GetVocationCount() - 1) pList.SwapVocations(pList.IndexOf(this), pList.IndexOf(this) + 1);
            }
        }

        private void removeButton_Click(object sender, EventArgs e) {
            VocationList p;
            if (this.Parent is VocationList) {
                p = (VocationList)this.Parent;
                p.RemoveVocationAt(p.IndexOf(this));
            }
        }
    }
}
