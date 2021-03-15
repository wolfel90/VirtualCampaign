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
    public partial class CompoundedStatField : UserControl {
        private CompoundedStat CompoundedValue = new CompoundedStat();
        private bool _editable = true;
        public bool Editable { get { return _editable; } set { setEditable(value); } }
        public string statKey { get { return CompoundedValue.statKey; } set { CompoundedValue.statKey = value; } }
        public decimal Min { get { return valueSpinner.Minimum; } set { valueSpinner.Minimum = value; } }
        public decimal Max { get { return valueSpinner.Maximum; } set { valueSpinner.Maximum = value; } }

        public CompoundedStatField() {
            InitializeComponent();
            
            CompoundedValue.ValueChanged += CompoundedValue_ValueChanged;

            float newSize = Height * 0.4f;
            valueLabel.Font = new Font(valueLabel.Font.FontFamily, newSize, valueLabel.Font.Style, valueLabel.Font.Unit);
            valueSpinner.Font = new Font(valueSpinner.Font.FontFamily, newSize, valueSpinner.Font.Style, valueSpinner.Font.Unit);
        }

        private void CompoundedStatField_SizeChanged(object sender, EventArgs e) {
            Graphics g = this.CreateGraphics();
            float newSize = Height * 0.4f;
            valueLabel.Font = new Font(valueLabel.Font.FontFamily, newSize, valueLabel.Font.Style, valueLabel.Font.Unit);
            valueSpinner.Font = new Font(valueSpinner.Font.FontFamily, newSize, valueSpinner.Font.Style, valueSpinner.Font.Unit);
        }

        private void setEditable(bool val) {
            _editable = val;
            if(val) {
                this.Cursor = Cursors.Hand;
            } else {
                this.Cursor = Cursors.Arrow;
                ToggleEditMode(false);
            }
        }

        private void confirmButton_Click(object sender, EventArgs e) {
            ToggleEditMode(false);
        }

        private void valueLabel_MouseClick(object sender, MouseEventArgs e) {
            if(_editable) {
                ToggleEditMode(true);
            }
        }

        public void ToggleEditMode(bool val) {
            if(val) {
                valueLabel.Visible = false;
                valueLabel.Dock = DockStyle.None;
                editorPane.Visible = true;
                editorPane.Dock = DockStyle.Fill;
            } else {
                editorPane.Visible = false;
                editorPane.Dock = DockStyle.None;
                valueLabel.Visible = true;
                valueLabel.Dock = DockStyle.Fill;
            }
        }

        public void CompoundedValue_ValueChanged(object source, EventArgs e) {
            valueLabel.Text = CompoundedValue.Value.ToString();
        }

        private void valueSpinner_ValueChanged(object sender, EventArgs e) {
            CompoundedValue.setAddend(CompoundedStat.ADJUST, (int)valueSpinner.Value);
        }
    }
}
