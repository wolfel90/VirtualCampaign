using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualCampaign.data;
using VirtualCampaign.net;

namespace VirtualCampaign.window {
    public partial class WhitelistMenu : Form {
        public const int FULL_MODE = 0, BINARY_MODE = 1;
        public bool resultUse { get; set; }
        public string resultString { get; set; }
        private int mode;
        private bool optional;

        public WhitelistMenu() : this(FULL_MODE, true) {}

        public WhitelistMenu(int mode) : this(mode, true) {
            
        }

        public WhitelistMenu(int mode, bool optional) {
            InitializeComponent();

            this.mode = mode;
            this.optional = optional;
            if (mode == BINARY_MODE) {
                viewList.Bounds = new Rectangle(viewList.Bounds.X, viewList.Bounds.Y, viewList.Width, editList.Bounds.Y + editList.Height);
                editLabel.Visible = false;
                editList.Visible = false;
                editList.Enabled = false;
                veAddButton.Visible = false;
                veAddButton.Enabled = false;
                veRemoveButton.Visible = false;
                veRemoveButton.Enabled = false;
                reAddButton.Visible = false;
                reAddButton.Enabled = false;
                reRemoveButton.Visible = false;
                reRemoveButton.Enabled = false;
                rvAddButton.Location = new Point(
                    noneList.Location.X + noneList.Width + ((viewList.Location.X - (noneList.Location.X + noneList.Width)) / 2) - (rvAddButton.Width / 2),
                    noneList.Location.Y + (noneList.Height / 2) - rvAddButton.Height - 2
                    );
                rvRemoveButton.Location = new Point(rvAddButton.Location.X, rvAddButton.Location.Y + rvAddButton.Height + 5);
            }
            if(optional) {
                useCheck.Enabled = true;
            } else {
                setActive(true);
                useCheck.Enabled = false;
            }

            List<UserTag> users = UserManager.getIdentifiedUsers();
            foreach (UserTag t in users) {
                noneList.Items.Add(t);
            }
        }

        public void setActive(bool active) {
            useCheck.Checked = active;
            noneList.Enabled = active;
            viewList.Enabled = active;
            editList.Enabled = active && mode == FULL_MODE;
            rvAddButton.Enabled = active;
            rvRemoveButton.Enabled = active;
            reAddButton.Enabled = active && mode == FULL_MODE;
            reRemoveButton.Enabled = active && mode == FULL_MODE;
            veAddButton.Enabled = active && mode == FULL_MODE;
            veRemoveButton.Enabled = active && mode == FULL_MODE;
        }

        public void setData(string data) {
            string[] split;
            if (data.Contains('~')) {
                split = data.Split('~');
            } else {
                split = new string[] { data };
            }
            for(int i = 0; i < split.Length && i < 2; ++i) {
                string[] ids;
                if(split[i].Contains(',')) {
                    ids = split[i].Split(',');
                } else {
                    ids = new string[] { split[i] };
                }
                for(int i2 = 0; i2 < ids.Length; ++i2) {
                    int id;
                    if(Int32.TryParse(ids[i2], out id)) {
                        UserTag tag = UserManager.getUserTag(id);
                        if(tag != null) {
                            if(mode == FULL_MODE) {
                                if (i == 0) {
                                    viewList.Items.Add(tag);
                                    if (noneList.Items.Contains(tag)) noneList.Items.Remove(tag);
                                } else if (i == 1) {
                                    editList.Items.Add(tag);
                                    if (noneList.Items.Contains(tag)) noneList.Items.Remove(tag);
                                }
                            } else if(mode == BINARY_MODE) {
                                viewList.Items.Add(tag);
                                if (noneList.Items.Contains(tag)) noneList.Items.Remove(tag);
                            }
                        }
                    }
                        
                }
            }
        }

        public string generateWhitelistString() {
            string result = "";
            bool first = true;
            foreach(object o in viewList.Items) {
                if(!first) result += ",";
                if(o is UserTag) {
                    result += ((UserTag)o).userID;
                    first = false;
                }
            }
            if(mode == FULL_MODE) {
                result += "~";
                first = true;
                foreach (object o in editList.Items) {
                    if (!first) result += ",";
                    if (o is UserTag) {
                        result += ((UserTag)o).userID;
                        first = false;
                    }
                }
            }
            return result;
        }

        private void rvAddButton_Click(object sender, EventArgs e) {
            if(noneList.SelectedItem != null) {
                viewList.Items.Add(noneList.SelectedItem);
                noneList.Items.Remove(noneList.SelectedItem);
            }
        }

        private void rvRemoveButton_Click(object sender, EventArgs e) {
            if(viewList.SelectedItem != null) {
                noneList.Items.Add(viewList.SelectedItem);
                viewList.Items.Remove(viewList.SelectedItem);
            }
        }

        private void reAddButton_Click(object sender, EventArgs e) {
            if (noneList.SelectedItem != null) {
                editList.Items.Add(noneList.SelectedItem);
                noneList.Items.Remove(noneList.SelectedItem);
            }
        }

        private void reRemoveButton_Click(object sender, EventArgs e) {
            if (editList.SelectedItem != null) {
                noneList.Items.Add(editList.SelectedItem);
                editList.Items.Remove(editList.SelectedItem);
            }
        }

        private void veAddButton_Click(object sender, EventArgs e) {
            if (viewList.SelectedItem != null) {
                editList.Items.Add(viewList.SelectedItem);
                viewList.Items.Remove(viewList.SelectedItem);
            }
        }

        private void veRemoveButton_Click(object sender, EventArgs e) {
            if (editList.SelectedItem != null) {
                viewList.Items.Add(editList.SelectedItem);
                editList.Items.Remove(editList.SelectedItem);
            }
        }

        private void okButton_Click(object sender, EventArgs e) {
            resultString = generateWhitelistString();
            resultUse = useCheck.Checked;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void useCheck_CheckedChanged(object sender, EventArgs e) {
            setActive(useCheck.Checked);
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
