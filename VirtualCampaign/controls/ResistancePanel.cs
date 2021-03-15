using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualCampaign.sys;

namespace VirtualCampaign.controls {
    public partial class ResistancePanel : UserControl {
        private CompoundedStat stat;
        public event EventHandler ValueChanged;
        public int Baseline { get { return stat.Baseline; } set { setAddend(CompoundedStat.BASELINE, value); } }
        private bool _editable;
        public bool Editable { get { return _editable; } set { _editable = value; } }
        public string Title { get { return titleLabel.Text; } set { titleLabel.Text = value; } }

        public ResistancePanel() {
            InitializeComponent();

            stat = new CompoundedStat();
            _editable = true;

            stat.ValueChanged += Stat_ValueChanged;
        }

        public void setAddend(string key, int newValue) {
            setAddend(key, newValue, null);
        }

        public void setAddend(string key, int newValue, string hint) {
            int oldValue = stat.getAddend(key);
            string oldHint = stat.getHint(key);
            if (newValue != oldValue || hint != oldHint) {
                stat.setAddend(key, newValue, hint);

                OnValueChanged(EventArgs.Empty);
            }
        }

        public bool hasAddend(string key) {
            return stat.hasAddend(key);
        }

        public void removeAddend(string key) {
            stat.removeAddend(key);
        }

        protected virtual void OnValueChanged(EventArgs e) {
            ValueChanged?.Invoke(this, e);
        }

        public void Stat_ValueChanged(object source, EventArgs e) {
            if(stat.Value <= -2) {
                valueLabel.Text = "x2";
            } else if(stat.Value == -1) {
                valueLabel.Text = "x1.5";
            } else if(stat.Value == 0) {
                valueLabel.Text = " ";
            } else if(stat.Value == 1) {
                valueLabel.Text = "1/2";
            } else {
                valueLabel.Text = "X";
            }
        }

        private void valueLabel_MouseClick(object sender, MouseEventArgs e) {
            if(Editable) {
                Baseline = Baseline >= 2 ? -2 : Baseline + 1;
            }
        }
    }
}
