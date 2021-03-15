using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualCampaign.events;
using VirtualCampaign.sys;

namespace VirtualCampaign.controls {
    public partial class AttributePanel : UserControl {
        public string Title { get { return titleLabel.Text; } set { titleLabel.Text = value; } }
        //public string Value { get { return valueLabel.Text; } set { valueLabel.Text = value; } }
        public event EventHandler<AttributePanelEventArgs> ValueChanged;
        private static Random rand;
        private CompoundedStat _val;
        public CompoundedStat Value { get { return _val; } }
        public bool rollable { get { return rollButton.Visible;  } set { rollButton.Visible = value; } }

        static AttributePanel() {
            rand = new Random(DateTime.Now.Millisecond);
        }

        public AttributePanel() : this("") {
            
        }

        public AttributePanel(string tag) {
            InitializeComponent();
            _val = new CompoundedStat(tag);
            _val.ValueChanged += cs_ValueChanged;
        }

        private void cs_ValueChanged(object sender, EventArgs e) {
            valueLabel.Text = _val.Value.ToString();
            OnValueChanged(new AttributePanelEventArgs(Value.statKey));
        }

        public void setAddend(string key, int value) {
            _val.setAddend(key, value, null);
        }

        public void setAddend(string key, int value, string hint) {
            _val.setAddend(key, value, hint);
        }

        public bool hasAddend(string key) {
            return _val.hasAddend(key);
        }

        public void removeAddend(string key) {
            _val.removeAddend(key);
        }

        public int getAddend(string key) {
            return _val.getAddend(key);
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            using (SolidBrush brush = new SolidBrush(SystemColors.WindowText)) {
                Pen pen = new Pen(brush);
                //e.Graphics.DrawEllipse(pen, valueLabel.Bounds);
                Rectangle r = new Rectangle(0, 0, Width, Height);
                r.Inflate(-5, -5);
                e.Graphics.DrawArc(pen, new Rectangle(r.Location, new Size(20, 20)), 180, 90);
                e.Graphics.DrawArc(pen, new Rectangle(new Point(r.Right - 20, r.Top), new Size(20, 20)), 270, 90);
                e.Graphics.DrawArc(pen, new Rectangle(new Point(r.Right - 20, r.Bottom - 20), new Size(20, 20)), 0, 90);
                e.Graphics.DrawArc(pen, new Rectangle(new Point(r.Left, r.Bottom - 20), new Size(20, 20)), 90, 90);
                e.Graphics.DrawLine(pen, r.Location.X + 10, r.Top, r.Right - 10, r.Top);
                e.Graphics.DrawLine(pen, r.Right, r.Top + 10, r.Right, r.Bottom - 10);
                e.Graphics.DrawLine(pen, r.Location.X + 10, r.Bottom, r.Right - 10, r.Bottom);
                e.Graphics.DrawLine(pen, r.Left, r.Top + 10, r.Left, r.Bottom - 10);

                e.Graphics.DrawArc(pen, new Rectangle(valueLabel.Location, new Size(20, 20)), 180, 90);
                e.Graphics.DrawArc(pen, new Rectangle(new Point(valueLabel.Right - 20, valueLabel.Location.Y), new Size(20, 20)), 270, 90);
                e.Graphics.DrawArc(pen, new Rectangle(new Point(valueLabel.Right - 20, valueLabel.Bottom - 20), new Size(20, 20)), 0, 90);
                e.Graphics.DrawArc(pen, new Rectangle(new Point(valueLabel.Location.X, valueLabel.Bottom - 20), new Size(20, 20)), 90, 90);
                e.Graphics.DrawLine(pen, valueLabel.Location.X + 10, valueLabel.Location.Y, valueLabel.Right - 10, valueLabel.Location.Y);
                e.Graphics.DrawLine(pen, valueLabel.Right, valueLabel.Location.Y + 10, valueLabel.Right, valueLabel.Bottom - 10);
                e.Graphics.DrawLine(pen, valueLabel.Location.X + 10, valueLabel.Bottom, valueLabel.Right - 10, valueLabel.Bottom);
                e.Graphics.DrawLine(pen, valueLabel.Location.X, valueLabel.Location.Y + 10, valueLabel.Location.X, valueLabel.Bottom - 10);
            }
        }

        private void rollButton_Click(object sender, EventArgs e) {
            int total = 0;
            string output = "(";
            for (int i = 0; i < 3; ++i) {
                if (i > 0) output += " + ";
                int ra = rand.Next(1, 7);
                total += ra;

                output += ra.ToString();
            }
            total += Value.Value;
            output += ") " + (Value.Value < 0 ? "- " : "+ ") + (Math.Abs(Value.Value)) + " = " + total;
            MessageBox.Show(output);
        }

        private void rollButton_MouseDown(object sender, MouseEventArgs e) {
            if(e.Button == MouseButtons.Right) {
                rollOptionsMenu.baseline = Value.Value;
                rollOptionsMenu.Show(rollButton, rollButton.Width, 0);
            }
        }

        protected virtual void OnValueChanged(AttributePanelEventArgs e) {
            ValueChanged?.Invoke(this, e);
        }
    }
}
