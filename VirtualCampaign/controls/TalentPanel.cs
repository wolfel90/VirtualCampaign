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

namespace VirtualCampaign.controls {
    public partial class TalentPanel : UserControl {
        private static Random rand;
        private static System.Timers.Timer clickTimer;
        private static Button clickTarget;
        private delegate void delSetValue(int rank, int prog);
        public event EventHandler ValueChanged;
        public event EventHandler TalentClicked;
        public TalentList ParentList;
        public bool refreshing { get; set; }
        private Talent _talent;
        public Talent talent { get { return _talent; } set { setTalent(value); } }
        private bool _editable;
        public bool editable { get { return _editable; } set { setEditable(value); } }
        public bool generateTooltip;

        static TalentPanel() {
            clickTimer = new System.Timers.Timer();
            clickTimer.Interval = 500;
            clickTimer.Elapsed += OnTimedEvent;
            clickTimer.AutoReset = true;
            clickTarget = null;
            rand = new Random();
        }

        public TalentPanel() {
            InitializeComponent();
            _talent = new Talent();
            _editable = true;
            refreshing = false;
            ParentList = null;
            rankMeter.editable = false;
            progressMeter.editable = false;
            generateTooltip = true;
            progressTooltip.SetToolTip(rankMeter, progressMeter.current + " / " + progressMeter.max);
            progressTooltip.SetToolTip(progressMeter, progressMeter.current + " / " + progressMeter.max);
        }

        public void setTalent(Talent t) {
            _talent = t;
            titleLabel.Text = t.title;
            progressMeter.max = t.rank >= 0 ? (t.rank <= 9 ? t.rank+1 : 10) : 1;
            progressMeter.current = t.progress;
            updateBlips();
            progressTooltip.SetToolTip(rankMeter, progressMeter.current + " / " + progressMeter.max);
            progressTooltip.SetToolTip(progressMeter, progressMeter.current + " / " + progressMeter.max);
            if (!refreshing) OnChanged(EventArgs.Empty);
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

        private void button_MouseDown(object sender, MouseEventArgs e) {
            if (sender is Button) {
                if(((Button)sender).Parent is TalentPanel) {
                    Talent t = ((TalentPanel)((Button)sender).Parent).talent;
                    int r = 0, p = 0;
                    if (((Button)sender).Tag.Equals("add")) {
                        if (t.rank < Talent.TALENT_MAX) {
                            if ((t.rank < 10 && t.progress == t.rank) || (t.rank >= 10 && t.progress == 9)) {
                                r = t.rank + 1;
                                p = 0;
                            } else {
                                r = t.rank;
                                p = t.progress + 1;
                            }
                            setTalentScore(r, p);
                        } else {
                            return;
                        }
                    } else if (((Button)sender).Tag.Equals("subtract")) {
                        
                        if (t.progress > 0 || t.rank > 0) {
                            if (t.progress == 0) {
                                if (t.rank > 0) {
                                    r = t.rank - 1;
                                    p = t.rank - 1 < 10 ? t.rank - 1 : 9;
                                }
                            } else {
                                r = t.rank;
                                p = t.progress - 1;
                            }
                            setTalentScore(r, p);
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

        private void button_MouseUp(object sender, MouseEventArgs e) {
            clickTimer.Interval = 500;
            clickTimer.Enabled = false;
            clickTarget = null;
        }

        private void setTalentScore(int rank, int progress) {
            if(rank != _talent.rank || progress != _talent.progress) {
                _talent.rank = rank;
                if (_talent.rank < 10) {
                    _talent.progress = progress >= 0 ? (progress <= _talent.rank ? progress : _talent.rank) : 0;
                } else {
                    _talent.progress = progress >= 0 ? (progress <= 9 ? progress : 9) : 0;
                }
                progressTooltip.SetToolTip(rankMeter, progressMeter.current + " / " + progressMeter.max);
                progressTooltip.SetToolTip(progressMeter, progressMeter.current + " / " + progressMeter.max);
                updateBlips();
                if (!refreshing) OnChanged(EventArgs.Empty);
            }
        }

        private void updateBlips() {
            int baseline, rankProgress;
            baseline = _talent.rank / 10;
            rankProgress = _talent.rank % 10;
            switch(baseline) {
                case 0:
                    rankMeter.bgColor = Color.Gray;
                    rankMeter.fillColor = Color.Blue;
                    break;
                case 1:
                    rankMeter.bgColor = Color.Blue;
                    rankMeter.fillColor = Color.ForestGreen;
                    break;
                case 2:
                    rankMeter.bgColor = Color.ForestGreen;
                    rankMeter.fillColor = Color.Gold;
                    break;
                case 3:
                    rankMeter.bgColor = Color.Gold;
                    rankMeter.fillColor = Color.Purple;
                    break;
                default:
                    rankMeter.bgColor = Color.Purple;
                    rankMeter.fillColor = Color.Cyan;
                    break;
            }
            rankMeter.current = rankProgress;

            progressMeter.max = _talent.rank >= 0 ? (_talent.rank <= 9 ? _talent.rank + 1 : 10) : 1;
            progressMeter.current = _talent.progress <= progressMeter.max ? _talent.progress : progressMeter.max;

            if(_talent.rank < 0) {
                rankLabel.Text = "";
            } else if(_talent.rank <= 1) {
                rankLabel.Text = "Untrained";
            } else if(_talent.rank <= 3) {
                rankLabel.Text = "Novice";
            } else if(_talent.rank <= 5) {
                rankLabel.Text = "Apprentice";
            } else if(_talent.rank <= 7) {
                rankLabel.Text = "Journeyman";
            } else if(_talent.rank <= 9) {
                rankLabel.Text = "Expert";
            } else if(_talent.rank == 10) {
                rankLabel.Text = "Master";
            } else if(_talent.rank < 20) {
                rankLabel.Text = "Master +" + (_talent.rank - 10);
            } else if(_talent.rank == 20) {
                rankLabel.Text = "Grandmaster";
            } else if(_talent.rank < 30) {
                rankLabel.Text = "Grandmaster +" + (_talent.rank - 20);
            } else if(_talent.rank == 30) {
                rankLabel.Text = "Paragon";
            } else {
                rankLabel.Text = "Paragon +" + (_talent.rank - 30);
            }
            rankNumberLevel.Text = _talent.rank.ToString();
            Invalidate();
        }

        private void removeButton_Click(object sender, EventArgs e) {
            TalentList pList;
            if (Parent is TalentList) {
                pList = (TalentList)Parent;
                pList.RemoveTalentAt(pList.IndexOf(this));
            }
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

        protected virtual void OnChanged(EventArgs e) {
            ValueChanged?.Invoke(this, e);
        }

        private void upButton_Click(object sender, EventArgs e) {
            TalentList pList;
            if (Parent is TalentList) {
                pList = (TalentList)Parent;
                if(pList.IndexOf(this) > 0) pList.SwapTalents(pList.IndexOf(this), pList.IndexOf(this) - 1);
            }
        }

        private void downButton_Click(object sender, EventArgs e) {
            TalentList pList;
            if (Parent is TalentList) {
                pList = (TalentList)Parent;
                if(pList.IndexOf(this) < pList.GetTalentCount() - 1) pList.SwapTalents(pList.IndexOf(this), pList.IndexOf(this) + 1);
            }
        }

        private void titleLabel_Click(object sender, EventArgs e) {
            OnTalentClicked(e);
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

            int baseline = talent.rank;
            string rollSource = "";
            if(!String.IsNullOrWhiteSpace(talent.proficiency)) {
                if(ParentList != null) {
                    if(ParentList.ParentSheet != null) {
                        string[] profs;
                        if (talent.proficiency.Contains("|")) {
                            profs = talent.proficiency.Split('|');
                        } else {
                            profs = new string[] { talent.proficiency };
                        }
                        foreach (string p in profs) {
                            switch (p.ToLower().Trim()) {
                                case "strength":
                                    if(ParentList.ParentSheet.charaDat.strength / 4 > baseline) {
                                        baseline = ParentList.ParentSheet.charaDat.strength / 4;
                                        rollSource = "(STR)";
                                    }
                                    break;
                                case "agility":
                                    if (ParentList.ParentSheet.charaDat.agility / 4 > baseline) {
                                        baseline = ParentList.ParentSheet.charaDat.agility / 4;
                                        rollSource = "(AGI)";
                                    }
                                    break;
                                case "intelligence":
                                    if (ParentList.ParentSheet.charaDat.intelligence / 4 > baseline) {
                                        baseline = ParentList.ParentSheet.charaDat.intelligence / 4;
                                        rollSource = "(INT)";
                                    }
                                    break;
                                case "fortitude":
                                    if (ParentList.ParentSheet.charaDat.fortitude / 4 > baseline) {
                                        baseline = ParentList.ParentSheet.charaDat.fortitude / 4;
                                        rollSource = "(FOR)";
                                    }
                                    break;
                                case "charisma":
                                    if (ParentList.ParentSheet.charaDat.charisma / 4 > baseline) {
                                        baseline = ParentList.ParentSheet.charaDat.charisma / 4;
                                        rollSource = "(CHA)";
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            total += baseline;
            output += ") " + (baseline < 0 ? "- " : "+ ") + (Math.Abs(baseline)) + (String.IsNullOrEmpty(rollSource) ? "" : " " + rollSource) + " = " + total;
            MessageBox.Show(output);
        }

        private void rollButton_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                int baseline = talent.rank;
                string rollSource = "";
                if (!String.IsNullOrWhiteSpace(talent.proficiency)) {
                    if (ParentList != null) {
                        if (ParentList.ParentSheet != null) {
                            string[] profs;
                            if (talent.proficiency.Contains("|")) {
                                profs = talent.proficiency.Split('|');
                            } else {
                                profs = new string[] { talent.proficiency };
                            }
                            foreach (string p in profs) {
                                switch (p.ToLower().Trim()) {
                                    case "strength":
                                        if (ParentList.ParentSheet.charaDat.strength / 4 > baseline) {
                                            baseline = ParentList.ParentSheet.charaDat.strength / 4;
                                            rollSource = "(STR)";
                                        }
                                        break;
                                    case "agility":
                                        if (ParentList.ParentSheet.charaDat.agility / 4 > baseline) {
                                            baseline = ParentList.ParentSheet.charaDat.agility / 4;
                                            rollSource = "(AGI)";
                                        }
                                        break;
                                    case "intelligence":
                                        if (ParentList.ParentSheet.charaDat.intelligence / 4 > baseline) {
                                            baseline = ParentList.ParentSheet.charaDat.intelligence / 4;
                                            rollSource = "(INT)";
                                        }
                                        break;
                                    case "fortitude":
                                        if (ParentList.ParentSheet.charaDat.fortitude / 4 > baseline) {
                                            baseline = ParentList.ParentSheet.charaDat.fortitude / 4;
                                            rollSource = "(FOR)";
                                        }
                                        break;
                                    case "charisma":
                                        if (ParentList.ParentSheet.charaDat.charisma / 4 > baseline) {
                                            baseline = ParentList.ParentSheet.charaDat.charisma / 4;
                                            rollSource = "(CHA)";
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
                rollOptionsMenu.baseline = baseline;
                rollOptionsMenu.rollSource = rollSource;
                rollOptionsMenu.Show(rollButton, rollButton.Width, 0);
            }
        }

        protected virtual void OnTalentClicked(EventArgs e) {
            TalentClicked?.Invoke(this, e);
        }

        private static void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e) {
            if (clickTarget != null) {
                if (clickTarget.Parent is TalentPanel) {
                    if (((TalentPanel)clickTarget.Parent).InvokeRequired) {
                        Talent t = ((TalentPanel)clickTarget.Parent).talent;
                        int r = 0, p = 0;
                        if (clickTarget.Tag.Equals("add")) {
                            if (t.rank < Talent.TALENT_MAX) {
                                if ((t.rank < 10 && t.progress == t.rank) || (t.rank >= 10 && t.progress == 9)) {
                                    r = t.rank + 1;
                                    p = 0;
                                } else {
                                    r = t.rank;
                                    p = t.progress + 1;
                                }
                            } else {
                                return;
                            }
                        } else if (clickTarget.Tag.Equals("subtract")) {
                            if (t.progress > 0 || t.rank > 0) {
                                if (t.progress == 0) {
                                    if (t.rank > 0) {
                                        r = t.rank - 1;
                                        p = t.rank - 1 < 10 ? t.rank - 1 : 9;
                                    }
                                } else {
                                    r = t.rank;
                                    p = t.progress - 1;
                                }
                            } else {
                                return;
                            }
                        }
                        ((TalentPanel)clickTarget.Parent).Invoke(
                            new delSetValue(((TalentPanel)clickTarget.Parent).setTalentScore), new object[] { r, p });
                    }
                }
            }
            clickTimer.Interval = 50;
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            Graphics g = e.Graphics;
        }

        private void titleLabel_MouseEnter(object sender, EventArgs e) {
            if (generateTooltip && talent != null) {
                AdaptableTooltipHandler.CurrentControl = this;
                AdaptableTooltipHandler.Tooltip.generateTooltip(talent);
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
