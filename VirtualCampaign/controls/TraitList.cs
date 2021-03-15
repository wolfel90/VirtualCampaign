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
using VirtualCampaign.events;
using VirtualCampaign.window;

namespace VirtualCampaign.controls {
    public partial class TraitList : UserControl {
        private List<TraitPanel> panels;
        public event EventHandler ValueChanged;
        public event EventHandler<TraitListEventArgs> TraitAdded;
        public event EventHandler<TraitListEventArgs> TraitRemoved;
        public event EventHandler<TraitListEventArgs> TraitChanged;
        public event EventHandler TraitClicked;
        public string ModuleFilters;
        public bool refreshing { get; set; }
        private bool _editable;
        public bool editable { get { return _editable; } set { setEditable(value); } }

        public TraitList() : base() {
            InitializeComponent();
            panels = new List<TraitPanel>();
            refreshing = false;
            _editable = true;
            ModuleFilters = "";
        }

        private void addButton_Click(object sender, EventArgs e) {
            using(var traitLoadMenu = new TTALoader(TTALoader.TRAIT_TYPE, TTALoader.MULTI_SELECT)) {
                if (ModuleFilters != null) {
                    if (!String.IsNullOrWhiteSpace(ModuleFilters)) {
                        traitLoadMenu.SetModuleFilters(ModuleFilters);
                    }
                }
                foreach (TraitPanel p in panels) {
                    traitLoadMenu.blockIDs.Add(p.trait.netID);
                }
                traitLoadMenu.ReloadList();
                var result = traitLoadMenu.ShowDialog();
                if(result == DialogResult.OK) {
                    if(traitLoadMenu.resultTraits != null) {
                        AddTraits(traitLoadMenu.resultTraits);
                    }
                }
            }
        }

        public void AddTrait(Trait t) {
            AddTraits(new Trait[] { t });
        }

        public void AddTraits(Trait[] traits) {
            if (traits.Length > 0) {
                TraitPanel newPanel = null;
                foreach (Trait t in traits) {
                    newPanel = new TraitPanel();
                    newPanel.refreshing = true;
                    newPanel.setTrait(t);
                    newPanel.setEditable(_editable);
                    newPanel.ValueChanged += traitPanel_Changed;
                    newPanel.ActivationChanged += traitPanel_ActivationChanged;
                    newPanel.TraitClicked += traitPanel_Clicked;
                    newPanel.refreshing = false;
                    panels.Add(newPanel);
                    newPanel.Location = new Point(5, panels.IndexOf(newPanel) * newPanel.Height);
                    Controls.Add(newPanel);
                    OnTraitAdded(new TraitListEventArgs(newPanel));
                }
                if (newPanel != null) this.Height = newPanel.Location.Y + newPanel.Height + 30;
                if (!refreshing) OnChanged(EventArgs.Empty);
            }
        }

        public void SetTraitAt(int index, Trait t) {
            if(index < panels.Count && index >= 0) {
                if(t == null) {
                    RemoveTraitAt(index);
                } else {
                    Trait old = panels[index].trait;
                    panels[index].setTrait(t);
                    OnTraitChanged(new TraitListEventArgs(panels[index], old));
                    if (!refreshing) OnChanged(EventArgs.Empty);
                }
            }
        }

        public int GetTraitCount() {
            return panels.Count;
        }

        public void RemoveTraitAt(int index) {
            if(index >= 0 && index < panels.Count) {
                TraitPanel p = panels[index];
                p.ValueChanged -= traitPanel_Changed;
                p.ActivationChanged -= traitPanel_ActivationChanged;
                p.TraitClicked -= traitPanel_Clicked;
                panels.RemoveAt(index);
                OnTraitRemoved(new TraitListEventArgs(p));
                p.Dispose();
                positionTraitPanels();
                EventArgs e = new EventArgs();
                
                OnChanged(EventArgs.Empty);
            }
        }

        public void SwapTraits(int index1, int index2) {
            if(index1 >= 0 && index1 < panels.Count && index2 >= 0 && index2 < panels.Count) {
                Trait tempTrait = panels[index2].trait;
                SetTraitAt(index2, panels[index1].trait);
                SetTraitAt(index1, tempTrait);
                if (!refreshing) OnChanged(EventArgs.Empty);
            }
        }

        public int IndexOf(TraitPanel panel) {
            return panels.IndexOf(panel);
        }

        

        public string generateString() {
            string result = "";
            foreach(TraitPanel p in panels) {
                result += p.trait.generateString();
            }
            return result;
        }

        private void positionTraitPanels() {
            if(panels.Count > 0) {
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
            for(int i = 0; i < panels.Count; ++i) {
                panels[i].setEditable(val);
            }
        }

        public void traitPanel_Changed(object sender, EventArgs e) {
            if (!refreshing) {
                OnChanged(EventArgs.Empty);
            }
        }

        public void traitPanel_ActivationChanged(object sender, EventArgs e) {
            if(!refreshing) {
                if(sender != null) {
                    if(sender is TraitPanel) {
                        OnTraitChanged(new TraitListEventArgs((TraitPanel)sender));
                    }
                }
            }
        }

        public void traitPanel_Clicked(object sender, EventArgs e) {
            OnTraitClicked(sender, e);
        }

        protected virtual void OnTraitClicked(object source, EventArgs e) {
            TraitClicked?.Invoke(source, e);
        }

        protected virtual void OnChanged(EventArgs e) {
            ValueChanged?.Invoke(this, e);
        }

        protected virtual void OnTraitAdded(TraitListEventArgs e) {
            TraitAdded?.Invoke(this, e);
        }

        protected virtual void OnTraitRemoved(TraitListEventArgs e) {
            TraitRemoved?.Invoke(this, e);
        }

        protected virtual void OnTraitChanged(TraitListEventArgs e) {
            TraitChanged?.Invoke(this, e);
        }
    }
}
