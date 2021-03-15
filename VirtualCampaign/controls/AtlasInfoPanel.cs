using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace VirtualCampaign.controls {
    public partial class AtlasInfoPanel : UserControl {
        public event EventHandler<MouseEventArgs> StartDrag;
        public event EventHandler<MouseEventArgs> EndDrag;
        public event EventHandler<MouseEventArgs> Drag;
        private Dictionary<PictureBox, string> OptionsLookup;
        public AtlasMarker TargetMarker;
        private bool dragging;
        private Point origin;
        private Point _shift;
        public Point DragAmount { get { return _shift; } }
        private bool _editable;
        public bool Editable { get { return _editable;  } set { SetEditable(value);  } }
        private string _bg, _icon;
        public string BGSrc { get { return _bg;  } set { SetBGSrc(value); } }
        public string IconSrc { get { return _icon; } set { SetIconSrc(value); } }
        private bool editMode;

        public AtlasInfoPanel() {
            InitializeComponent();
            dragging = false;
            origin = new Point();
            _shift = new Point();
            OptionsLookup = new Dictionary<PictureBox, string>();
            TargetMarker = null;
            editMode = false;
            
            using (StringReader reader = new StringReader(Properties.Resources.atlas_bg_options)) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    AddBGOption(line);
                }
            }

            PictureBox newPB = new PictureBox();
            newPB.Image = null;
            newPB.BorderStyle = BorderStyle.FixedSingle;
            iconOptionsPanel.Controls.Add(newPB);
            newPB.Bounds = new Rectangle(0, 0, 25, 25);
            newPB.Click += IconOption_Clicked;

            using (StringReader reader = new StringReader(Properties.Resources.atlas_icon_options)) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    AddIconOption(line);
                }
            }

            SetEditMode(false);
        }

        public void DisplayInfo(AtlasMarker mark, string content) {
            titleLabel.Text = mark.Title;
            contentBox.DocumentText = VirtualCampaign.sys.StringFunctions.resolveToHTML(content);
            SetBGSrc(mark.bgSrc);
            SetIconSrc(mark.iconSrc);
        }

        private void closeButton_Click(object sender, EventArgs e) {
            this.Visible = false;
        }

        protected virtual void OnStartDrag(MouseEventArgs e) {
            StartDrag?.Invoke(this, e);
        }

        protected virtual void OnEndDrag(MouseEventArgs e) {
            EndDrag?.Invoke(this, e);
        }

        protected virtual void OnDrag(MouseEventArgs e) {
            Drag?.Invoke(this, e);
        }

        private void titleLabel_MouseDown(object sender, MouseEventArgs e) {
            dragging = true;
            origin = e.Location;
            OnStartDrag(e);
        }

        private void titleLabel_MouseUp(object sender, MouseEventArgs e) {
            dragging = false;
            OnEndDrag(e);
        }

        private void titleLabel_MouseMove(object sender, MouseEventArgs e) {
            if(dragging) {
                _shift = new Point(e.X - origin.X, e.Y - origin.Y);
                OnDrag(e);
            }
        }

        public void SetEditMode(bool val) {
            editMode = val;
            if(editMode) {
                tablePanel.Controls.Remove(contentBox);
                tablePanel.Controls.Add(editPanel);
                tablePanel.SetRow(editPanel, 1);
                tablePanel.SetColumn(editPanel, 0);
                tablePanel.SetColumnSpan(editPanel, 2);
                editPanel.Dock = DockStyle.Fill;
                editButton.Text = "Save Marker";
            } else {
                tablePanel.Controls.Remove(editPanel);
                tablePanel.Controls.Add(contentBox);
                tablePanel.SetRow(contentBox, 1);
                tablePanel.SetColumn(contentBox, 0);
                tablePanel.SetColumnSpan(contentBox, 2);
                contentBox.Dock = DockStyle.Fill;
                editButton.Text = "Edit Marker";
            }
        }

        public void SetTargetMarker(AtlasMarker mark) {
            SetTargetMarker(mark, "");
        }

        public void SetTargetMarker(AtlasMarker mark, string description) {
            SetEditMode(false);
            TargetMarker = mark;
            DisplayInfo(mark, description);
        }

        public void SetBGSrc(string src) {
            _bg = src;
            if(src == "") {
                bgPictureBox.Image = null;
            } else {
                object o = Properties.Resources.ResourceManager.GetObject(src);
                if (o != null) {
                    bgPictureBox.Image = (Image)o;
                }
            }
        }

        public void SetIconSrc(string src) {
            _icon = src;
            if(src == "") {
                iconPictureBox.Image = null;
            } else {
                object o = Properties.Resources.ResourceManager.GetObject(src);
                if (o != null) {
                    iconPictureBox.Image = (Image)o;
                }
            }
        }

        private void editButton_Click(object sender, EventArgs e) {
            if(_editable) {
                if(editMode) {
                    if(TargetMarker != null) {
                        TargetMarker.bgSrc = BGSrc;
                        TargetMarker.iconSrc = IconSrc;
                        VirtualCampaign.net.SQLManager.runUpdate("vc_articles", new string[] { "properties" }, 
                            new string[] { TargetMarker.GeneratePropertiesString() }, "`id` = " + TargetMarker.NetID + " LIMIT 1");
                    }
                    SetEditMode(false);
                } else {
                    SetEditMode(true);
                }
            }
        }

        private void contentBox_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {

        }

        public void SetEditable(bool val) {
            _editable = val;
            editButton.Visible = val;
        }

        public void BGOption_Clicked(object sender, EventArgs e) {
            if(sender is PictureBox) {
                if(OptionsLookup.ContainsKey((PictureBox)sender)) {
                    SetBGSrc(OptionsLookup[(PictureBox)sender]);
                } else {
                    SetBGSrc("");
                }
            }
        }

        public void IconOption_Clicked(object sender, EventArgs e) {
            if (sender is PictureBox) {
                if (OptionsLookup.ContainsKey((PictureBox)sender)) {
                    SetIconSrc(OptionsLookup[(PictureBox)sender]);
                } else {
                    SetIconSrc("");
                }
            }
        }

        private void AddBGOption(string src) {
            object o = Properties.Resources.ResourceManager.GetObject(src);
            if(o != null) {
                if(o is Image) {
                    PictureBox newPB = new PictureBox();
                    newPB.Image = (Image)o;
                    newPB.BorderStyle = BorderStyle.FixedSingle;
                    int tX = bgOptionsPanel.Controls.Count * 26;
                    bgOptionsPanel.Controls.Add(newPB);
                    newPB.Bounds = new Rectangle(tX, 0, 25, 25);
                    newPB.Click += BGOption_Clicked;
                    OptionsLookup.Add(newPB, src);
                }
            }
        }

        private void AddIconOption(string src) {
            object o = Properties.Resources.ResourceManager.GetObject(src);
            if (o != null) {
                if (o is Image) {
                    PictureBox newPB = new PictureBox();
                    newPB.Image = (Image)o;
                    newPB.BorderStyle = BorderStyle.FixedSingle;
                    int tX = iconOptionsPanel.Controls.Count * 26;
                    iconOptionsPanel.Controls.Add(newPB);
                    newPB.Bounds = new Rectangle(tX, 0, 25, 25);
                    newPB.Click += IconOption_Clicked;
                    OptionsLookup.Add(newPB, src);
                }
            }
        }
    }
}
