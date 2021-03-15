using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualCampaign.controls {
    public partial class Meter : UserControl {
        private Color _bgColor, _fillColor, _negativeColor;
        public Color bgColor { get { return _bgColor; } set { _bgColor = value; bgShadowColor = Color.FromArgb(_bgColor.R / 2, _bgColor.G / 2, _bgColor.B / 2); Invalidate(); } }
        private Color bgShadowColor;
        public Color fillColor {
            get { return _fillColor; }
            set { _fillColor = value; fillShadowColor = Color.FromArgb(_fillColor.R / 2, _fillColor.G / 2, _fillColor.B / 2);  Invalidate(); } }
        private Color fillShadowColor;
        public Color negativeColor {
            get { return _negativeColor; }
            set { _negativeColor = value; negativeShadowColor = Color.FromArgb(_negativeColor.R / 2, _negativeColor.G / 2, _negativeColor.B / 2); Invalidate(); } }
        public Color negativeShadowColor;
        private Font textFont;
        public event EventHandler ValueChanged;
        private int _cur, _max, _min, _seg;
        public int current { get { return _cur; } set { _cur = value; Invalidate(); } }
        public int max { get { return _max; } set { _max = value; Invalidate(); } }
        public int min { get { return _min; } set { _min = value; curSpinner.Minimum = value; Invalidate(); } }
        public int segmentInterval { get { return _seg; } set { _seg = value; Invalidate(); } }
        private int fillWidth { get { return getFillWidth(); } }
        private bool _editable, _curEditable, _maxEditable, _drawText;
        public bool editable { get { return _editable; } set { setEditable(value); } }
        public bool drawText { get { return _drawText; } set { _drawText = value; Invalidate(); } }
        public bool curValEditable { get { return _curEditable; } set { _curEditable = value; this.Cursor = (_editable && (_curEditable || _maxEditable)) ? Cursors.Hand : Cursors.Default; } }
        public bool maxValEditable { get { return _maxEditable; } set { _maxEditable = value; this.Cursor = (_editable && (_curEditable || _maxEditable)) ? Cursors.Hand : Cursors.Default; } }
        private bool editing;

        public Meter() {
            InitializeComponent();
            bgColor = Color.Gray;
            fillColor = Color.Red;
            negativeColor = Color.DarkGray;
            _cur = 1;
            _max = 1;
            _min = 0;
            editable = true;
            _curEditable = true;
            _maxEditable = true;
            editing = false;
            drawText = true;
            segmentInterval = 0;
            textFont = new Font(FontFamily.GenericSansSerif, this.Height / 2, FontStyle.Bold);
            this.Cursor = Cursors.Hand;

            curSpinner.LostFocus += Spinner_LostFocus;
            maxSpinner.LostFocus += Spinner_LostFocus;
        }

        private int getFillWidth() {
            if(_cur >= 0) {
                if(_max > 0) {
                    if(_cur < _max) {
                        return this.Width * _cur / _max;
                    } else {
                        return Width;
                    }
                } else {
                    return 0;
                }
            } else {
                if(_min < 0) {
                    if(_min < _cur) {
                        return this.Width * -_cur / -_min;
                    } else {
                        return Width;
                    }
                } else {
                    return 0;
                }
            }
        }

        private void Meter_SizeChanged(object sender, EventArgs e) {
            curSpinner.Width = this.Width / 2;
            maxSpinner.Width = this.Width / 2;
            //curSpinner.Location = new Point(0, (this.Height / 2) - (curSpinner.Height / 2));
            //maxSpinner.Location = new Point(this.Width / 2, curSpinner.Location.Y);
            textFont = new Font(FontFamily.GenericSansSerif, this.Height / 2, FontStyle.Bold);
        }

        private void curSpinner_ValueChanged(object sender, EventArgs e) {
            
        }

        private void maxSpinner_ValueChanged(object sender, EventArgs e) {
            curSpinner.Maximum = maxSpinner.Value;
        }

        private void Meter_Load(object sender, EventArgs e) {

        }

        private void Meter_MouseClick(object sender, MouseEventArgs e) {
            if(e.Button == MouseButtons.Left) {
                if(editing) {
                    if (editable && (_curEditable || _maxEditable)) {
                        if (_curEditable) {
                            _cur = (int)curSpinner.Value;
                        }
                        if (_maxEditable) {
                            _max = (int)maxSpinner.Value;
                        }
                        curSpinner.Visible = false;
                        maxSpinner.Visible = false;
                        Invalidate();
                        OnValueChanged(EventArgs.Empty);
                    }
                    editing = false;
                } else {
                    if (editable && (_curEditable || _maxEditable)) {
                        curSpinner.Value = _cur < curSpinner.Minimum ? curSpinner.Minimum : (_cur > curSpinner.Maximum ? curSpinner.Maximum : _cur);
                        curSpinner.Visible = true;
                        curSpinner.Enabled = _curEditable;
                        maxSpinner.Value = _max < maxSpinner.Minimum ? maxSpinner.Minimum : (_max > maxSpinner.Maximum ? maxSpinner.Maximum : _max);
                        maxSpinner.Visible = true;
                        maxSpinner.Enabled = _maxEditable;
                        curSpinner.Focus();
                        editing = true;
                        Invalidate();
                    }
                }
                
            }
        }

        public void setEditable(bool val) {
            _editable = val;
            this.Cursor = (_editable && (_curEditable || _maxEditable)) ? Cursors.Hand : Cursors.Default;
        }

        private void Spinner_LostFocus(object sender, EventArgs e) {
            if(!curSpinner.ContainsFocus && !maxSpinner.ContainsFocus) {
                if(_curEditable) {
                    _cur = (int)curSpinner.Value;
                }
                if(_maxEditable) {
                    _max = (int)maxSpinner.Value;
                }
                curSpinner.Visible = false;
                curSpinner.Enabled = false;
                maxSpinner.Visible = false;
                maxSpinner.Enabled = false;
                editing = false;
                Invalidate();
                OnValueChanged(EventArgs.Empty);
            }
        }

        protected virtual void OnValueChanged(EventArgs e) {
            ValueChanged?.Invoke(this, e);
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            if(!editing) {
                LinearGradientBrush bgBrush = new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(0, Height),
                    bgShadowColor,
                    _bgColor
                );
                LinearGradientBrush fillBrush;
                if(_cur >= 0) {
                    fillBrush = new LinearGradientBrush(
                        new Point(0, 0),
                        new Point(0, Height),
                        _fillColor,
                        fillShadowColor
                    );
                } else {
                    fillBrush = new LinearGradientBrush(
                        new Point(0, 0),
                        new Point(0, Height),
                        _negativeColor,
                        negativeShadowColor
                    );
                }

                e.Graphics.FillRectangle(bgBrush, new Rectangle(fillWidth, 0, this.Width - fillWidth, this.Height));
                e.Graphics.FillRectangle(fillBrush, new Rectangle(0, 0, fillWidth, this.Height));

                if(segmentInterval > 0) {
                    for(double d = ((double)segmentInterval * (double)this.Width / (double)_max); d < this.Width - 1; d += ((double)segmentInterval * (double)this.Width / (double)_max)) {
                        e.Graphics.DrawLine(Pens.White, new Point((int)d, 0), new Point((int)d, this.Height));
                    }
                }

                if(drawText) {
                    string t;
                    if (_cur >= 0) {
                        t = _cur + " / " + _max;
                    } else {
                        t = _cur + " / " + _min;
                    }
                    SizeF s = e.Graphics.MeasureString(t, textFont);
                    e.Graphics.DrawString(t, textFont, Brushes.White, this.Width / 2 - s.Width / 2, this.Height / 2 - s.Height / 2);
                }
            }
        }
    }
}
