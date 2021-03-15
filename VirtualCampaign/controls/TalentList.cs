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
    public partial class TalentList : UserControl {
        private List<TalentPanel> panels;
        public event EventHandler ValueChanged;
        public event EventHandler TalentClicked;
        public CharacterSheet ParentSheet;
        public string ModuleFilters;
        public bool refreshing { get; set; }
        private bool _editable;
        public bool editable { get { return _editable; } set { setEditable(value); } }

        public TalentList() : base() {
            InitializeComponent();
            panels = new List<TalentPanel>();
            refreshing = false;
            _editable = true;
            ModuleFilters = "";
            ParentSheet = null;
        }

        private void addButton_Click(object sender, EventArgs e) {
            using (var talentLoadMenu = new TTALoader(TTALoader.TALENT_TYPE, TTALoader.MULTI_SELECT)) {
                if(ModuleFilters != null) {
                    if(!String.IsNullOrWhiteSpace(ModuleFilters)) {
                        talentLoadMenu.SetModuleFilters(ModuleFilters);
                    }
                }
                foreach(TalentPanel p in panels) {
                    talentLoadMenu.blockIDs.Add(p.talent.netID);
                }
                talentLoadMenu.ReloadList();
                var result = talentLoadMenu.ShowDialog();
                if (result == DialogResult.OK) {
                    if(talentLoadMenu.resultTalents != null) {
                        AddTalents(talentLoadMenu.resultTalents);
                    }
                }
            }
        }

        public void AddTalent(Talent t) {
            AddTalents(new Talent[] { t });
        }

        public void AddTalents(Talent[] talents) {
            if(talents.Length > 0) {
                TalentPanel newPanel = null;
                foreach (Talent t in talents) {
                    newPanel = new TalentPanel();
                    newPanel.refreshing = true;
                    newPanel.setTalent(t);
                    newPanel.setEditable(_editable);
                    newPanel.ValueChanged += talentPanel_Changed;
                    newPanel.TalentClicked += talentPanel_Clicked;
                    newPanel.refreshing = false;
                    newPanel.ParentList = this;
                    panels.Add(newPanel);
                    newPanel.Location = new Point(5, panels.IndexOf(newPanel) * newPanel.Height);
                    Controls.Add(newPanel);
                }
                if(newPanel != null) this.Height = newPanel.Location.Y + newPanel.Height + 30;
            }
            if(!refreshing) OnChanged(EventArgs.Empty);
        }

        public void SetTalentAt(int index, Talent t) {
            if (index < panels.Count) {
                panels[index].setTalent(t);
                if(!refreshing) OnChanged(EventArgs.Empty);
            }
        }

        public int GetTalentCount() {
            return panels.Count;
        }

        public void RemoveTalentAt(int index) {
            if (index >= 0 && index < panels.Count) {
                TalentPanel p = panels[index];
                p.ValueChanged -= talentPanel_Changed;
                p.TalentClicked -= talentPanel_Clicked;
                p.ParentList = null;
                panels.RemoveAt(index);
                p.Dispose();
                positionTalentPanels();
                if(!refreshing) OnChanged(EventArgs.Empty);
            }
        }

        public void SwapTalents(int index1, int index2) {
            if (index1 >= 0 && index1 < panels.Count && index2 >= 0 && index2 < panels.Count) {
                Talent tempTalent = panels[index2].talent;
                SetTalentAt(index2, panels[index1].talent);
                SetTalentAt(index1, tempTalent);
                if (!refreshing) OnChanged(EventArgs.Empty);
            }
        }

        public int IndexOf(TalentPanel panel) {
            return panels.IndexOf(panel);
        }

        protected virtual void OnChanged(EventArgs e) {
            ValueChanged?.Invoke(this, e);
        }

        public string generateString() {
            string result = "";
            foreach (TalentPanel p in panels) {
                result += p.talent.generateString();
            }
            return result;
        }

        private void positionTalentPanels() {
            if (panels.Count > 0) {
                for (int i = 0; i < panels.Count; ++i) {
                    panels[i].Location = new Point(5, i * panels[i].Height);
                }
                this.Height = panels[panels.Count - 1].Location.Y + panels[panels.Count - 1].Height + 30;
            } else {
                this.Height = 25;
            }
        }

        public void setEditable(bool val) {
            _editable = val;
            for (int i = 0; i < panels.Count; ++i) {
                panels[i].setEditable(val);
            }
        }

        public void talentPanel_Changed(object sender, EventArgs e) {
            if(!refreshing) {
                OnChanged(EventArgs.Empty);
            }
        }

        public void talentPanel_Clicked(object sender, EventArgs e) {
            OnTalentClicked(sender, e);
        }

        protected virtual void OnTalentClicked(object source, EventArgs e) {
            TalentClicked?.Invoke(source, e);
        }
    }
}
