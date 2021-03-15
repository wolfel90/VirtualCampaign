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
using VirtualCampaign.window;

namespace VirtualCampaign.controls {
    public partial class SchoolList : UserControl {
        public List<SchoolPanel> panels { get; }
        public event EventHandler ValueChanged;
        public event EventHandler SchoolClicked;
        public string ModuleFilters;
        private bool _editable;
        public bool editable { get { return _editable; } set { setEditable(value); } }
        public bool refreshing { get; set; }

        public SchoolList() {
            InitializeComponent();
            panels = new List<SchoolPanel>();
            refreshing = false;
            _editable = true;
            ModuleFilters = "";
        }

        public string generateString() {
            string result = "";
            foreach(SchoolPanel p in panels) {
                result += p.school.generateString();
            }
            return result;
        }

        public void AddSchool(School s) {
            AddSchools(new School[] { s });
        }

        public void AddSchools(School[] schools) {
            if (schools.Length > 0) {
                SchoolPanel newPanel = null;
                foreach (School s in schools) {
                    newPanel = new SchoolPanel();
                    newPanel.refreshing = true;
                    newPanel.setSchool(s);
                    newPanel.setEditable(_editable);
                    newPanel.ValueChanged += schoolPanel_Changed;
                    newPanel.SchoolClicked += schoolPanel_Clicked;
                    newPanel.refreshing = false;
                    panels.Add(newPanel);
                    Controls.Add(newPanel);
                }
                positionSchoolPanels();
                if (!refreshing) OnChanged(EventArgs.Empty);
            }
        }

        public void SetSchoolAt(int index, School s) {
            if (index < panels.Count) {
                panels[index].setSchool(s);
                if (!refreshing) OnChanged(EventArgs.Empty);
            }
        }

        public int GetSchoolCount() {
            return panels.Count;
        }

        public void RemoveSchoolAt(int index) {
            if (index >= 0 && index < panels.Count) {
                SchoolPanel p = panels[index];
                p.ValueChanged -= schoolPanel_Changed;
                p.SchoolClicked -= schoolPanel_Clicked;
                panels.RemoveAt(index);
                p.Dispose();
                Controls.Remove(p);
                positionSchoolPanels();
                if (!refreshing) OnChanged(EventArgs.Empty);
            }
        }

        public void SwapSchools(int index1, int index2) {
            if (index1 >= 0 && index1 < panels.Count && index2 >= 0 && index2 < panels.Count) {
                School tempSchool = panels[index2].school;
                SetSchoolAt(index2, panels[index1].school);
                SetSchoolAt(index1, tempSchool);
                if (!refreshing) OnChanged(EventArgs.Empty);
            }
        }

        public int IndexOf(SchoolPanel panel) {
            return panels.IndexOf(panel);
        }

        private void positionSchoolPanels() {
            if (panels.Count > 0) {
                int yMark = 0;
                for (int i = 0; i < panels.Count; ++i) {
                    panels[i].Location = new Point(5, yMark);
                    yMark += panels[i].Height;
                }
                this.Height = yMark + 30;
            } else {
                this.Height = 25;
            }
        }

        protected virtual void OnChanged(EventArgs e) {
            ValueChanged?.Invoke(this, e);
        }

        public void setEditable(bool val) {
            _editable = val;
            for (int i = 0; i < panels.Count; ++i) {
                panels[i].setEditable(val);
            }
        }

        private void addButton_Click(object sender, EventArgs e) {
            using (var schoolLoadMenu = new TTALoader(TTALoader.SCHOOL_TYPE, TTALoader.MULTI_SELECT)) {
                if (ModuleFilters != null) {
                    if (!String.IsNullOrWhiteSpace(ModuleFilters)) {
                        schoolLoadMenu.SetModuleFilters(ModuleFilters);
                    }
                }
                foreach (SchoolPanel p in panels) {
                    schoolLoadMenu.blockIDs.Add(p.school.netID);
                }
                schoolLoadMenu.ReloadList();
                var result = schoolLoadMenu.ShowDialog();
                if (result == DialogResult.OK) {
                    if (schoolLoadMenu.resultSchools != null) {
                        AddSchools(schoolLoadMenu.resultSchools);
                    }
                }
            }
        }

        public void schoolPanel_Changed(object sender, EventArgs e) {
            if (!refreshing) {
                OnChanged(EventArgs.Empty);
            }
        }

        public void schoolPanel_Clicked(object sender, EventArgs e) {
            OnSchoolClicked(sender, e);
        }

        protected virtual void OnSchoolClicked(object source, EventArgs e) {
            SchoolClicked?.Invoke(source, e);
        }
    }
}
