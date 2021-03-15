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
using VirtualCampaign.window;

namespace VirtualCampaign.controls {
    public partial class KeynoteGrid : UserControl {
        public static float CELL_WIDTH = 12f, CELL_HEIGHT = 25f, LABEL_WIDTH = 150f;
        private static Font KEY_FONT = new Font(new FontFamily("Arial"), 8, FontStyle.Regular, GraphicsUnit.Pixel);
        private static Font LABEL_FONT = new Font(new FontFamily("Arial"), 12, FontStyle.Regular, GraphicsUnit.Pixel);
        private static Image keyholeBitmap = Properties.Resources.keyhole;
        public event EventHandler SelectionChanged;
        
        private List<KeyValuePair<string, List<Keynote>>> keynotes;
        private Brush gridPrimaryBrush, gridAltBrush, gridDarkBrush, selectBrush;
        private float _zoom, _position;
        private int _max;
        private bool _branchLabels;
        private Point _select = new Point(0, 0);
        private Point dragPoint = new Point();
        private bool dragging = false, validClick = false;
        private long clickTime = 0;
        public float Branches { get { return keynotes.Count; } }
        public float Zoom { get { return _zoom; } set { setZoom(value); } }
        public float Position { get { return _position; } set { setPosition(value); } }
        public Point SelectedValue { get { return _select; } set { setSelect(value); } }
        public bool BranchLabelsVisible { get { return _branchLabels; } set { setBranchLabelsVisible(value); } }
        public int Max { get { return _max; } set { setMax(value); } }

        public KeynoteGrid() {
            InitializeComponent();
            Zoom = 1;
            Position = 0;
            Max = 100000;
            BranchLabelsVisible = false;

            keynotes = new List<KeyValuePair<string, List<Keynote>>>();
            AddBranch("Branch 0");
            gridPrimaryBrush = new SolidBrush(SystemColors.Control);
            gridAltBrush = new SolidBrush(SystemColors.ControlDark);
            gridDarkBrush = new SolidBrush(SystemColors.ControlDarkDark);
            selectBrush = new SolidBrush(SystemColors.Highlight);

            this.MouseWheel += MouseWheelScroll;
        }

        private void setZoom(float val) {
            _zoom = val < 0.1f ? 0.1f : (val > 10f ? 10f : val);
        }

        private void setPosition(float val) {
            _position = val > 0 ? 0 : val < -Max * CELL_WIDTH * Zoom ? -Max * CELL_WIDTH * Zoom : val;
        }

        private void setMax(int val) {
            _max = val;
        }

        private void setSelect(Point val) {
            Point old = _select;
            val.X = val.X < 0 ? 0 : val.X > Max ? Max : val.X;
            val.Y = val.Y < 0 ? 0 : val.Y > keynotes.Count - 1 ? keynotes.Count - 1 : val.Y;
            _select = val;
            if (_select != old) OnSelectionChanged(new BasicValueEventArgs(_select));
        }

        private void setBranchLabelsVisible(bool val) {
            _branchLabels = val;
        }

        public void AddBranch() {
            int i = 0;
            while(ContainsBranch("Branch " + i)) {
                ++i;
            }
            AddBranch("Branch " + i);
        }

        public void AddBranch(string name) {
            AddBranchAt(name, keynotes.Count);
        }

        public void AddBranchAt(string name, int index) {
            KeyValuePair<string, List<Keynote>> newBranch = new KeyValuePair<string, List<Keynote>>(name, new List<Keynote>());
            keynotes.Insert(index, newBranch);
            this.Size = new Size(Width, (int)(CELL_HEIGHT * keynotes.Count));
            Invalidate();
        }

        public bool ContainsBranch(string name) {
            return GetBranchIndex(name) >= 0;
        }

        public int GetBranchIndex(string name) {
            for (int i = 0; i < keynotes.Count; ++i) {
                if (keynotes[i].Key.Equals(name)) {
                    return i;
                }
            }
            return -1;
        }

        public void RemoveBranch(int branch) {
            if(branch < keynotes.Count) {
                keynotes.Remove(keynotes.ElementAt(branch));
                if (SelectedValue.Y >= keynotes.Count && SelectedValue.Y > 0) SelectedValue = new Point(SelectedValue.X, SelectedValue.Y - 1);
                Size = new Size(Width, (int)(CELL_HEIGHT * keynotes.Count));
                Invalidate();
            }
        }

        public void AddKeynote(int position, Keynote newKeynote) {
            AddKeynote(0, position, newKeynote);
        }

        public void AddKeynote(int branch, int position, Keynote newKeynote) {
            if(branch < keynotes.Count) {
                RemoveKeynoteAt(branch, position);
                if(!keynotes.ElementAt(branch).Value.Contains(newKeynote)) {
                    keynotes.ElementAt(branch).Value.Add(newKeynote);
                    newKeynote.Coordinate = new Point(position, branch);
                    Invalidate();
                }
            } else {
                Console.Out.WriteLine("Branch " + branch + " does not exist.");
            }
        }

        public bool HasKeynote(int branch, Keynote keynote) {
            if(branch < keynotes.Count) {
                return keynotes.ElementAt(branch).Value.Contains(keynote);
            }
            return false;
        }

        public bool HasKeynoteAt(int branch, int position) {
            return GetKeynoteAt(branch, position) != null;
        }

        public bool HasKeynoteAt(int branch, int position, out Keynote keynote) {
            keynote = GetKeynoteAt(branch, position);
            return keynote != null;
        }

        public Keynote GetKeynoteAt(int position) {
            return GetKeynoteAt(0, position);
        }

        public Keynote GetKeynoteAt(int branch, int position) {
            if(branch < keynotes.Count) {
                foreach (Keynote k in keynotes[branch].Value) {
                    if (k.Coordinate.X == position) {
                        return k;
                    }
                }
            }
            return null;
        }

        public void RemoveKeynoteAt(int position) {
            RemoveKeynoteAt(0, position);
        }

        public void RemoveKeynoteAt(int branch, int position) {
            Keynote target = GetKeynoteAt(branch, position);
            if(target != null) {
                RemoveKeynote(target);
            }
        }

        public void RemoveKeynote(Keynote keynote) {
            for(int i = 0; i < keynotes.Count; ++i) {
                for(int n = 0; n < keynotes.ElementAt(i).Value.Count; ++n) {
                    if(keynotes.ElementAt(i).Value[n] == keynote) {
                        keynotes.ElementAt(i).Value[n].Coordinate = new Point();
                        keynotes.ElementAt(i).Value.RemoveAt(n);
                        return;
                    }
                }
            }
        }

        public int GetFirstKeynote(int branch) {
            if(branch < keynotes.Count) {
                if (keynotes.ElementAt(branch).Value.Count == 0) return -1;
                int earliest = Max;
                foreach(Keynote k in keynotes.ElementAt(branch).Value) {
                    if (k.Coordinate.X < earliest) earliest = k.Coordinate.X;
                }
                return earliest;
            }
            return -1;
        }
        
        private void KeynotePanel_KeyDown(object sender, KeyEventArgs e) {
            if(e.KeyCode == Keys.Right) {
                setSelect(new Point(_select.X + 1, _select.Y));
            } else if(e.KeyCode == Keys.Left) {
                setSelect(new Point(_select.X - 1, _select.Y));
            } else if(e.KeyCode == Keys.Up) {
                setSelect(new Point(_select.X, _select.Y - 1));
            } else if(e.KeyCode == Keys.Down) {
                setSelect(new Point(_select.X, _select.Y + 1));
            }
            Invalidate();
        }

        private void KeynotePanel_MouseDown(object sender, MouseEventArgs e) {
            if(e.Button == MouseButtons.Left) {
                dragging = true;
                validClick = true;
                dragPoint = e.Location;
                Focus();
                clickTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            } else if(e.Button == MouseButtons.Right) {
                setSelect(new Point((int)(((e.Location.X - _position - (_branchLabels ? LABEL_WIDTH : 0)) * _zoom) / (_zoom * CELL_WIDTH)), (int)(e.Location.Y / CELL_HEIGHT)));
                Invalidate();
            }
        }

        private void KeynotePanel_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                dragging = false;

                if(validClick && (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - clickTime < 500) {
                    if((_branchLabels && e.Location.X > LABEL_WIDTH) || !_branchLabels) {
                        setSelect(new Point((int)(((e.Location.X - _position - (_branchLabels ? LABEL_WIDTH : 0)) * _zoom) / (_zoom * CELL_WIDTH)), (int)(e.Location.Y / CELL_HEIGHT)));
                    } else {
                        setSelect(new Point(SelectedValue.X, (int)(e.Location.Y / CELL_HEIGHT)));
                    }
                    
                    Invalidate();
                }
                Focus();
            }
        }

        private void KeynotePanel_MouseMove(object sender, MouseEventArgs e) {
            if (dragging) {
                float old = Position;
                Position += (e.Location.X - dragPoint.X);
                dragPoint = e.Location;
                if (Math.Abs(old - Position) >= 5) validClick = false;
                if (old != Position) {
                    if(_branchLabels) {
                        Invalidate(new Rectangle((int)LABEL_WIDTH, 0, (int)(Width - LABEL_WIDTH), Height));
                    } else {
                        Invalidate();
                    }
                }
            }
        }

        private void createKeynoteToolStripMenuItem_Click(object sender, EventArgs e) {
            if(SelectedValue.Y < keynotes.Count) {
                AddKeynote(SelectedValue.Y, SelectedValue.X, new Keynote());
            }
        }
        
        private void deleteKeynoteToolStripMenuItem_Click(object sender, EventArgs e) {
            RemoveKeynoteAt(SelectedValue.Y, SelectedValue.X);
        }

        private void createBranchToolStripMenuItem_Click(object sender, EventArgs e) {
            AddBranch();
            Invalidate();
        }

        private void renameBranchToolStripMenuItem_Click(object sender, EventArgs e) {
            int targetBranch = SelectedValue.Y;
            SimpleMessageBox renameInput = new SimpleMessageBox();
            DialogResult result = renameInput.ShowDialog();
            if(result == DialogResult.OK) {
                if(targetBranch < keynotes.Count) {
                    KeyValuePair<string, List<Keynote>> newPair = new KeyValuePair<string, List<Keynote>>(renameInput.result, keynotes.ElementAt(targetBranch).Value);
                    keynotes.RemoveAt(targetBranch);
                    keynotes.Insert(targetBranch, newPair);
                    Invalidate();
                }
            }
        }

        private void deleteBranchToolStripMenuItem_Click(object sender, EventArgs e) {
            if(keynotes.Count > 1) {
                RemoveBranch(SelectedValue.Y);
            } else {
                MessageBox.Show("Cannot delete last branch");
            }
        }
        
        protected void MouseWheelScroll(object sender, MouseEventArgs e) {
            //float old = Position;
            //Position -= e.Delta / 10f;
            //if(Position != old) Invalidate();
        }

        protected void OnSelectionChanged(BasicValueEventArgs e) {
            EventHandler handler = SelectionChanged;
            if(handler != null) {
                handler(this, e);
            }
        }

        protected override bool IsInputKey(Keys keyData) {
            switch(keyData) {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            
            if (_zoom > 0) {
                float beginMark = 0;
                int beginIndex = 0, level;

                beginIndex = (int)Math.Floor((-Position / (CELL_WIDTH * Zoom)));
                if (Position % (CELL_WIDTH * Zoom) == 0) {
                    beginMark = _branchLabels ? LABEL_WIDTH : 0;
                } else {
                    beginMark = Position % (CELL_WIDTH * Zoom) + (_branchLabels ? LABEL_WIDTH : 0);
                }

                for(int branch = 0; branch < keynotes.Count; ++branch) {
                    int first = GetFirstKeynote(branch);
                    level = beginIndex;
                    for (float f = beginMark; f < Width && level <= _max; f += CELL_WIDTH * _zoom) {
                        if (_select.X == level && _select.Y == branch) {
                            if (selectBrush != null) e.Graphics.FillRectangle(selectBrush,
                                new Rectangle((int)Math.Round(f), (int)(CELL_HEIGHT * branch), (int)Math.Round((CELL_WIDTH * _zoom)), (int)CELL_HEIGHT));
                        } else if(level < first || first < 0) {
                            if (selectBrush != null) e.Graphics.FillRectangle(gridDarkBrush,
                                new Rectangle((int)Math.Round(f), (int)(CELL_HEIGHT * branch), (int)Math.Round((CELL_WIDTH * _zoom)), (int)CELL_HEIGHT));
                        } else if(level % 2 == 0) {
                            if (selectBrush != null) e.Graphics.FillRectangle(gridAltBrush,
                                new Rectangle((int)Math.Round(f), (int)(CELL_HEIGHT * branch), (int)Math.Round((CELL_WIDTH * _zoom)), (int)CELL_HEIGHT));
                        }
                        if(HasKeynoteAt(branch, level, out Keynote k)) {
                            e.Graphics.DrawImage(keyholeBitmap, f + 2, (int)(CELL_HEIGHT * branch) + (CELL_HEIGHT - keyholeBitmap.Height - 2), 8f, 8f);
                        }
                        ++level;
                    }
                    if(branch > 0) {
                        e.Graphics.DrawLine(Pens.Black, new Point(0, (int)(branch * CELL_HEIGHT)), new Point(Width, (int)(branch * CELL_HEIGHT)));
                    }
                }

                level = beginIndex;
                for (float f = beginMark; f < Width && level <= _max; f += CELL_WIDTH * _zoom) {
                    if (level % 10 == 0) {
                        if (level > 0) {
                            e.Graphics.DrawLine(Pens.Black, new Point((int)f, 0), new Point((int)f, Height));
                        }
                    }
                    if (level % 5 == 0) {
                        e.Graphics.DrawString(level.ToString(), KEY_FONT, Brushes.Black, new PointF(f, 2));
                    }
                    ++level;
                }

                if(_branchLabels) {
                    e.Graphics.FillRectangle(gridPrimaryBrush, new Rectangle(0, 0, (int)LABEL_WIDTH, Height));
                    e.Graphics.DrawLine(Pens.Black, new Point((int)LABEL_WIDTH, 0), new Point((int)LABEL_WIDTH, Height));
                    for (int i = 0; i < keynotes.Count; ++i) {
                        if(SelectedValue.Y == i) {
                            e.Graphics.FillRectangle(selectBrush, new Rectangle(0, (int)(CELL_HEIGHT * i), (int)LABEL_WIDTH, (int)CELL_HEIGHT));
                        }
                        e.Graphics.DrawString(keynotes.ElementAt(i).Key, LABEL_FONT, Brushes.Black, new Point(2, (int)(CELL_HEIGHT * (i + 0.5f)) - LABEL_FONT.Height / 2));
                        if (i > 0) {
                            e.Graphics.DrawLine(Pens.Black, new Point(0, (int)(CELL_HEIGHT * i)), new Point((int)LABEL_WIDTH, (int)(CELL_HEIGHT * i)));
                        }
                    }
                }
            }
        }
    }
}
