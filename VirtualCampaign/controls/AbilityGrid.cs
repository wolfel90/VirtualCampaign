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
    public partial class AbilityGrid : UserControl {
        public List<AbilitySlot> abilitySlots { get; }
        public int maxGridWidth;
        public int maxAbilityCount;
        public int slotWidth, slotHeight;
        public bool refreshing { get; set; }
        public event EventHandler ValueChanged;
        public event EventHandler AbilityClicked;
        public string ModuleFilters;
        private bool _editable;
        public bool editable { get { return _editable; } set { setEditable(value);  } }

        public AbilityGrid() {
            InitializeComponent();
            abilitySlots = new List<AbilitySlot>();
            maxGridWidth = 10;
            maxAbilityCount = 10;
            slotWidth = 70;
            slotHeight = 70;
            refreshing = false;
            _editable = true;
            ModuleFilters = "";
            arrangeSlots();
        }

        public void addAbility(Ability a) {
            addAbilities(new Ability[] { a });
        }

        public void addAbilities(Ability[] abilities) {
            if(abilities.Length > 0) {
                foreach(Ability a in abilities) {
                    if (abilitySlots.Count >= maxAbilityCount) break;
                    AbilitySlot newSlot = new AbilitySlot();
                    newSlot.refreshing = true;
                    newSlot.adaptableTooltips = true;
                    newSlot.setAbility(a);
                    newSlot.ValueChanged += abilitySlot_Changed;
                    newSlot.AbilityClicked += abilitySlot_Clicked;
                    newSlot.refreshing = false;
                    Controls.Add(newSlot);
                    abilitySlots.Add(newSlot);
                }
                arrangeSlots();
                if (!refreshing) OnChanged(EventArgs.Empty);
            }
        }

        public void setAbilityAt(int index, Ability ability) {
            if(index >= 0 && index < abilitySlots.Count) {
                abilitySlots[index].setAbility(ability);
                if (!refreshing) OnChanged(EventArgs.Empty);
            }
        }

        public int getAbilityCount() {
            return abilitySlots.Count;
        }

        public void removeAbilityAt(int index) {
            if(index >= 0 && index < abilitySlots.Count) {
                AbilitySlot s = abilitySlots[index];
                s.ValueChanged -= abilitySlot_Changed;
                s.AbilityClicked -= abilitySlot_Clicked;
                abilitySlots.RemoveAt(index);
                s.Dispose();
                arrangeSlots();
                if (!refreshing) OnChanged(EventArgs.Empty);
            }
        }

        public void SwapAbilities(int index1, int index2) {
            if(index1 >= 0 && index2 >= 0 && index1 < abilitySlots.Count &&index2 < abilitySlots.Count) {
                Ability tempAb = abilitySlots[index2].ability;
                setAbilityAt(index2, abilitySlots[index1].ability);
                setAbilityAt(index1, tempAb);
                if (!refreshing) OnChanged(EventArgs.Empty);
            }
        }

        public int IndexOf(AbilitySlot slot) {
            return abilitySlots.IndexOf(slot);
        }

        private void arrangeSlots() {
            int gX = 0, gY = 0;
            int w = 0, h = 0;
            for(int i = 0; i < abilitySlots.Count; ++i) {
                abilitySlots[i].SetBounds(gX * slotWidth, gY * slotHeight, slotWidth, slotHeight);
                gX++;
                if (gX >= maxGridWidth) {
                    gX = 0;
                    gY++;
                }
                w = (abilitySlots[i].Location.X + slotWidth > w) ? abilitySlots[i].Location.X + slotWidth : w;
                h = (abilitySlots[i].Location.Y + slotHeight > h) ? abilitySlots[i].Location.Y + slotHeight : h;
            }
            if(abilitySlots.Count < maxAbilityCount) {
                addButton.SetBounds(gX * slotWidth, gY * slotHeight, slotWidth, slotHeight);
                w = (addButton.Location.X + slotWidth > w) ? addButton.Location.X + slotWidth : w;
                h = (addButton.Location.Y + slotHeight > h) ? addButton.Location.Y + slotHeight : h;
                addButton.Visible = _editable;
                addButton.Enabled = _editable;
            } else {
                addButton.Visible = false;
                addButton.Enabled = false;
            }
            this.Size = new Size(w + 2, h + 2);
        }

        public string generateString() {
            string result = "";
            foreach(AbilitySlot s in abilitySlots) {
                result += s.ability.generateString();
            }
            return result;
        }

        protected virtual void OnChanged(EventArgs e) {
            ValueChanged?.Invoke(this, e);
        }

        public void abilitySlot_Changed(object source, EventArgs e) {
            if (!refreshing) OnChanged(EventArgs.Empty);
        }

        public void abilitySlot_Clicked(object source, EventArgs e) {
            AbilityClicked?.Invoke(source, e);
        }
        
        private void addButton_Click(object sender, EventArgs e) {
            using (var abilityLoadMenu = new TTALoader(TTALoader.ABILITY_TYPE, TTALoader.MULTI_SELECT)) {
                if (ModuleFilters != null) {
                    if (!String.IsNullOrWhiteSpace(ModuleFilters)) {
                        abilityLoadMenu.SetModuleFilters(ModuleFilters);
                    }
                }
                abilityLoadMenu.ReloadList();
                var result = abilityLoadMenu.ShowDialog();
                if (result == DialogResult.OK) {
                    if (abilityLoadMenu.resultAbilities != null) {
                        if (abilityLoadMenu.resultAbilities.Length > 0) {
                            addAbilities(abilityLoadMenu.resultAbilities);
                        }
                    }
                }
            }
        }

        public void setEditable(bool value) {
            _editable = value;
            addButton.Enabled = value && abilitySlots.Count < maxAbilityCount;
            addButton.Visible = addButton.Enabled;
            arrangeSlots();
        }
    }
}
