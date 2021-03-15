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
    public partial class ProficiencyPanel : UserControl, IDisposable {
        private static System.Timers.Timer clickTimer;
        private static Button clickTarget;
        private static Random rand;
        private enum DisplayMode { NetValue, BaseValue }
        private DisplayMode displayMode = DisplayMode.NetValue;
        private CompoundedStat ProficiencyValue;
        private CompoundedStat _Modifier;
        public CompoundedStat Modifier { get { return _Modifier; } }
        public int value { get { return ProficiencyValue.Value; } }
        public string Title { get { return titleLabel.Text; } set { titleLabel.Text = value; } }
        private Rectangle ringBounds;
        private delegate void delSetValue(int newValue);
        public event EventHandler ValueChanged;
        public int Baseline { get { return getBaseline(); } set { setBaseline(value); } }
        public int minValue, maxValue;
        public bool _editable;
        public bool editable { get { return _editable; } set { setEditable(value); } }
        public bool _editing;
        private bool editing { get { return _editing; } set { setEditing(value); } }
        public bool generateTooltip;

        static ProficiencyPanel() {
            clickTimer = new System.Timers.Timer();
            clickTimer.Interval = 500;
            clickTimer.Elapsed += OnTimedEvent;
            clickTimer.AutoReset = true;
            clickTarget = null;
            rand = new Random(DateTime.Now.Millisecond);
        }

        public ProficiencyPanel() : base() {
            InitializeComponent();
            minValue = -99999;
            maxValue = 99999;
            editable = true;
            editing = false;
            generateTooltip = true;
            ProficiencyValue = new CompoundedStat();
            _Modifier = new CompoundedStat();
            ProficiencyValue.ValueChanged += Proficiency_ValueChanged;
            _Modifier.ValueChanged += Modifier_ValueChanged;
            rollButton.Visible = true;
            addButton.LostFocus += ValueButton_LostFocus;
            subtractButton.LostFocus += ValueButton_LostFocus;
            valueLabel.LostFocus += ValueButton_LostFocus;
            ringBounds = new Rectangle(3, titleLabel.Bottom, this.Width - 6, modifierLabel.Location.Y - titleLabel.Bottom - 2);
        }

        private void ValueButton_LostFocus(object sender, EventArgs e) {
            if(editing) {
                if(!addButton.Focused && !subtractButton.Focused && !valueLabel.Focused) {
                    setEditing(false);
                }
            }
        }

        public void setBaseline(int newValue) {
            int oldValue = ProficiencyValue.Baseline;
            newValue = (newValue > maxValue ? maxValue : (newValue < minValue ? minValue : newValue));
            if (newValue != ProficiencyValue.Baseline) {
                ProficiencyValue.Baseline = newValue;
                _Modifier.Baseline = ProficiencyValue.Value / 2;

                OnValueChanged(EventArgs.Empty);
            }
        }

        public int getBaseline() {
            return ProficiencyValue.Baseline;
        }

        public int getAddend(string key) {
            return ProficiencyValue.getAddend(key);
        }

        public void setAddend(string key, int newValue) {
            setAddend(key, newValue, null);
        }

        public void setAddend(string key, int newValue, string hint) {
            int oldValue = ProficiencyValue.getAddend(key);
            string oldHint = ProficiencyValue.getHint(key);
            if (newValue != oldValue || hint != oldHint) {
                ProficiencyValue.setAddend(key, newValue, hint);
                _Modifier.Baseline = ProficiencyValue.Value / 2;

                OnValueChanged(EventArgs.Empty);
            }
        }

        public bool hasAddend(string key) {
            return ProficiencyValue.hasAddend(key);
        }

        public void removeAddend(string key) {
            ProficiencyValue.removeAddend(key);
        }

        public string getHint(string key) {
            return ProficiencyValue.getHint(key);
        }

        public string[] GetValueAddends() {
            return ProficiencyValue.GetAllAddends();
        }

        public string[] GetModifierAddends() {
            return _Modifier.GetAllAddends();
        }

        private void setEditable(bool val) {
            _editable = val;
            if(val) {
                valueLabel.Cursor = Cursors.Hand;
            } else {
                valueLabel.Cursor = Cursors.Default;
            }
        }

        private void setEditing(bool val) {
            if (val == _editing) return;
            _editing = val;
            subtractButton.Enabled = val;
            subtractButton.Visible = val;
            addButton.Enabled = val;
            addButton.Visible = val;
            if (val) {
                displayMode = DisplayMode.BaseValue;
                valueLabel.Text = ProficiencyValue.Baseline.ToString();
                valueLabel.BackColor = Color.White;
                valueLabel.BorderStyle = BorderStyle.FixedSingle;
            } else {
                displayMode = DisplayMode.NetValue;
                valueLabel.Text = ProficiencyValue.Value.ToString();
                valueLabel.BackColor = Color.Transparent;
                valueLabel.BorderStyle = BorderStyle.None;
            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            using(SolidBrush brush = new SolidBrush(SystemColors.WindowText)) {
                e.Graphics.DrawEllipse(new Pen(brush), ringBounds);
            }
        }

        private void button_MouseDown(object sender, MouseEventArgs e) {
            if(sender is Button) {
                if(((Button)sender).Tag.Equals("add")) {
                    setBaseline(ProficiencyValue.Baseline + 1);
                } else if(((Button)sender).Tag.Equals("subtract")) {
                    setBaseline(ProficiencyValue.Baseline - 1);
                }
                clickTarget = (Button)sender;
                clickTimer.Interval = 500;
                clickTimer.Enabled = true;
            }
        }

        private static void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e) {
            if (clickTarget != null) {
                if(clickTarget.Parent is ProficiencyPanel) {
                    if(((ProficiencyPanel)clickTarget.Parent).InvokeRequired) {
                        int i = 0;
                        if(clickTarget.Tag.Equals("add")) {
                            i = 1;
                        } else if(clickTarget.Tag.Equals("subtract")) {
                            i = -1;
                        }
                        ((ProficiencyPanel)clickTarget.Parent).Invoke(
                            new delSetValue(((ProficiencyPanel)clickTarget.Parent).setBaseline), new object[] { ((ProficiencyPanel)clickTarget.Parent).getBaseline() + i });
                    }
                }
            }
            clickTimer.Interval = 50;
        }

        private void button_Leave(object sender, EventArgs e) {
            if(sender is Button) {
                if(clickTarget == sender) {
                    clickTimer.Interval = 500;
                    clickTimer.Enabled = false;
                    clickTarget = null;
                }
            }
        }

        private void ProficiencyPanel_Leave(object sender, EventArgs e) {
            //addButton.Visible = false;
            //subtractButton.Visible = false;
        }

        private void ProficiencyPanel_Enter(object sender, EventArgs e) {
            //addButton.Visible = _editable;
            //subtractButton.Visible = _editable;
        }

        private void button_MouseUp(object sender, MouseEventArgs e) {
            clickTimer.Interval = 500;
            clickTimer.Enabled = false;
            clickTarget = null;
        }

        protected virtual void OnValueChanged(EventArgs e) {
            ValueChanged?.Invoke(this, e);
        }

        public void Proficiency_ValueChanged(object source, EventArgs e) {
            if(displayMode == DisplayMode.NetValue) {
                valueLabel.Text = ProficiencyValue.Value.ToString();
            } else if(displayMode == DisplayMode.BaseValue) {
                valueLabel.Text = ProficiencyValue.Baseline.ToString();
            }
            if(Modifier.Baseline != ProficiencyValue.Value / 2) {
                Modifier.Baseline = ProficiencyValue.Value / 2;
            }
        }

        public void Modifier_ValueChanged(object source, EventArgs e) {
            modifierLabel.Text = ((Modifier.Value < 0) ? "-" : "+") + Modifier.Value.ToString();
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
            total += Modifier.Value;
            output += ") " + (Modifier.Value < 0 ? "- " : "+ ") + (Math.Abs(Modifier.Value)) + " = " + total;
            MessageBox.Show(output);
        }

        private void rollButton_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                rollOptionsMenu.baseline = Modifier.Value;
                rollOptionsMenu.Show(rollButton, rollButton.Width, 0);
            }
        }

        private void valueLabel_MouseClick(object sender, MouseEventArgs e) {
            if (editable) {
                setEditing(!editing);
                if(editing) {
                    if (AdaptableTooltipHandler.CurrentControl == this) {
                        AdaptableTooltipHandler.CurrentControl = null;
                        AdaptableTooltipHandler.Tooltip.Hide();
                    }
                    addButton.Focus();
                }
            }
        }

        private void ProficiencyPanel_SizeChanged(object sender, EventArgs e) {
            ringBounds = new Rectangle(3, titleLabel.Bottom, this.Width - 6, modifierLabel.Location.Y - titleLabel.Bottom - 2);
        }

        private void valueLabel_MouseEnter(object sender, EventArgs e) {
            if(generateTooltip && !editing) {
                AdaptableTooltipHandler.CurrentControl = this;
                AdaptableTooltipHandler.Tooltip.generateTooltip(this);
                AdaptableTooltipHandler.Tooltip.Show();
                AdaptableTooltipHandler.Tooltip.Location = this.PointToScreen(new Point(0 + this.Width + 5, this.Height / 2));
            }
        }

        private void valueLabel_MouseLeave(object sender, EventArgs e) {
            if (AdaptableTooltipHandler.CurrentControl == this) {
                AdaptableTooltipHandler.CurrentControl = null;
                AdaptableTooltipHandler.Tooltip.Hide();
            }
        }

        public override string ToString() {
            return "Proficiency Panel: " + Title;
        }
    }
}
