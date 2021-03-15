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

namespace VirtualCampaign.controls {
    public partial class SchoolPanel : UserControl {
        private static System.Timers.Timer clickTimer;
        private static Button clickTarget;
        private delegate void delSetValue(int rank, int prog);
        public event EventHandler ValueChanged;
        public event EventHandler SchoolClicked;
        public bool refreshing { get; set; }
        private School _school;
        public School school { get { return _school; } set { setSchool(value); } }
        private bool _editable;
        public bool editable { get { return _editable; } set { setEditable(value); } }

        static SchoolPanel() {
            clickTimer = new System.Timers.Timer();
            clickTimer.Interval = 500;
            clickTimer.Elapsed += OnTimedEvent;
            clickTimer.AutoReset = true;
            clickTarget = null;
        }

        public SchoolPanel() {
            InitializeComponent();
            _school = new School();
            refreshing = false;
        }

        public void setSchool(School sch) {
            _school = sch;
            updateFields();
            if (!refreshing) OnChanged(EventArgs.Empty);
        }

        public void updateFields() {
            titleLabel.Text = school.title;
            specialLevellabel.Text = school.special.ToString();
            updateBlips();
        }

        private void updateBlips() {
            int baseline, lvlProgress;
            baseline = _school.level / 10;
            lvlProgress = _school.level % 10;

            switch (baseline) {
                case 0:
                    levelMeter.bgColor = Color.Gray;
                    levelMeter.fillColor = Color.Blue;
                    break;
                case 1:
                    levelMeter.bgColor = Color.Blue;
                    levelMeter.fillColor = Color.ForestGreen;
                    break;
                case 2:
                    levelMeter.bgColor = Color.ForestGreen;
                    levelMeter.fillColor = Color.Gold;
                    break;
                case 3:
                    levelMeter.bgColor = Color.Gold;
                    levelMeter.fillColor = Color.Purple;
                    break;
                default:
                    levelMeter.bgColor = Color.Purple;
                    levelMeter.fillColor = Color.Cyan;
                    break;
            }
            levelMeter.current = lvlProgress;

            progressMeter.max = _school.level >= 0 ? (_school.level <= 9 ? _school.level + 1 : 10) : 1;
            progressMeter.current = _school.progress <= progressMeter.max ? _school.progress : progressMeter.max;
            
            levelNumberLabel.Text = _school.level.ToString();
        }

        private void upButton_Click(object sender, EventArgs e) {
            SchoolList pList;
            if (Parent is SchoolList) {
                pList = (SchoolList)Parent;
                if (pList.IndexOf(this) > 0) pList.SwapSchools(pList.IndexOf(this), pList.IndexOf(this) - 1);
            }
        }

        private void downButton_Click(object sender, EventArgs e) {
            SchoolList pList;
            if (Parent is SchoolList) {
                pList = (SchoolList)Parent;
                if (pList.IndexOf(this) < pList.GetSchoolCount() - 1) pList.SwapSchools(pList.IndexOf(this), pList.IndexOf(this) + 1);
            }
        }

        private void button_MouseDown(object sender, MouseEventArgs e) {
            if (sender is Button) {
                if (((Button)sender).Parent is SchoolPanel) {
                    School s = ((SchoolPanel)((Button)sender).Parent).school;
                    int r = 0, p = 0;
                    if (((Button)sender).Tag.Equals("add")) {
                        if (s.level < School.SCHOOL_MAX) {
                            if ((s.level < 10 && s.progress == s.level) || (s.level >= 10 && s.progress == 9)) {
                                r = s.level + 1;
                                p = 0;
                            } else {
                                r = s.level;
                                p = s.progress + 1;
                            }
                            setSchoolScore(r, p);
                        } else {
                            return;
                        }
                    } else if (((Button)sender).Tag.Equals("subtract")) {

                        if (s.progress > 0 || s.level > 0) {
                            if (s.progress == 0) {
                                if (s.level > 0) {
                                    r = s.level - 1;
                                    p = s.level - 1 < 10 ? s.level - 1 : 9;
                                }
                            } else {
                                r = s.level;
                                p = s.progress - 1;
                            }
                            setSchoolScore(r, p);
                        } else {
                            return;
                        }
                    }
                    clickTarget = (Button)sender;
                    clickTimer.Interval = 500;
                    clickTimer.Enabled = true;
                }
            }
        }

        private void button_MouseLeave(object sender, EventArgs e) {
            if (sender is Button) {
                if (clickTarget == sender) {
                    clickTimer.Interval = 500;
                    clickTimer.Enabled = false;
                    clickTarget = null;
                }
            }
        }

        private void button_MouseUp(object sender, MouseEventArgs e) {
            clickTimer.Interval = 500;
            clickTimer.Enabled = false;
            clickTarget = null;
        }

        public void setEditable(bool val) {
            _editable = val;
            removeButton.Enabled = val;
            removeButton.Visible = val;
            decreaseButton.Enabled = val;
            decreaseButton.Visible = val;
            increaseButton.Enabled = val;
            increaseButton.Visible = val;
        }

        private void decreaseButton_Click(object sender, EventArgs e) {

        }

        private void increaseButton_Click(object sender, EventArgs e) {

        }

        private void setSchoolScore(int level, int progress) {
            if (level != _school.level || progress != _school.progress) {
                _school.level = level;
                if (_school.level < 10) {
                    _school.progress = progress >= 0 ? (progress <= _school.level ? progress : _school.level) : 0;
                } else {
                    _school.progress = progress >= 0 ? (progress <= 9 ? progress : 9) : 0;
                }
                updateBlips();
                if (!refreshing) OnChanged(EventArgs.Empty);
            }
        }

        private void removeButton_Click(object sender, EventArgs e) {
            SchoolList p;
            if(this.Parent is SchoolList) {
                p = (SchoolList)this.Parent;
                p.RemoveSchoolAt(p.IndexOf(this));
            }
        }

        protected virtual void OnChanged(EventArgs e) {
            ValueChanged?.Invoke(this, e);
        }

        private void titleLabel_Click(object sender, EventArgs e) {
            OnSchoolClicked(e);
        }

        protected virtual void OnSchoolClicked(EventArgs e) {
            SchoolClicked?.Invoke(this, e);
        }

        private static void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e) {
            if (clickTarget != null) {
                if (clickTarget.Parent is SchoolPanel) {
                    if (((SchoolPanel)clickTarget.Parent).InvokeRequired) {
                        School s = ((SchoolPanel)clickTarget.Parent).school;
                        int r = 0, p = 0;
                        if (clickTarget.Tag.Equals("add")) {
                            if (s.level < School.SCHOOL_MAX) {
                                if ((s.level < 10 && s.progress == s.level) || (s.level >= 10 && s.progress == 9)) {
                                    r = s.level + 1;
                                    p = 0;
                                } else {
                                    r = s.level;
                                    p = s.progress + 1;
                                }
                            } else {
                                return;
                            }
                        } else if (clickTarget.Tag.Equals("subtract")) {
                            if (s.progress > 0 || s.level > 0) {
                                if (s.progress == 0) {
                                    if (s.level > 0) {
                                        r = s.level - 1;
                                        p = s.level - 1 < 10 ? s.level - 1 : 9;
                                    }
                                } else {
                                    r = s.level;
                                    p = s.progress - 1;
                                }
                            } else {
                                return;
                            }
                        }
                        ((SchoolPanel)clickTarget.Parent).Invoke(
                            new delSetValue(((SchoolPanel)clickTarget.Parent).setSchoolScore), new object[] { r, p });
                    }
                }
            }
            clickTimer.Interval = 50;
        }
    }
}
