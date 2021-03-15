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
using VirtualCampaign.sys;

namespace VirtualCampaign.window {
    public partial class TTALoader : Form, SQLListener {
        public const int TRAIT_TYPE = 0, TALENT_TYPE = 1, SCHOOL_TYPE = 2, ABILITY_TYPE = 3;
        public const int MULTI_SELECT = 1, SINGLE_SELECT = 0;
        public Trait[] resultTraits { get; set;  }
        public Talent[] resultTalents { get; set; }
        public School[] resultSchools { get; set; }
        public Ability[] resultAbilities { get; set; }
        private List<object> Options;
        private List<string> ModuleFilters;
        private SQLParser parser;
        public int loadType { get; }
        public List<long> blockIDs;

        public TTALoader(int type) : this(type, SINGLE_SELECT) {
        }

        public TTALoader(int type, int selectMode) {
            InitializeComponent();
            ModuleFilters = new List<string>();
            Options = new List<object>();
            loadType = type;
            switch(selectMode) {
                case MULTI_SELECT:
                    itemGrid.MultiSelect = true;
                    break;
                case SINGLE_SELECT:
                default:
                    itemGrid.MultiSelect = false;
                    break;
            }
            
            switch(loadType) {
                case TRAIT_TYPE:
                    itemGrid.Columns.Add("titleColumn", "Title");
                    itemGrid.Columns.Add("typeColumn", "Type");
                    itemGrid.Columns.Add("costColumn", "Cost");
                    this.Text = (selectMode == MULTI_SELECT) ? "Select Trait(s)" : "Select Trait";
                    break;
                case TALENT_TYPE:
                    itemGrid.Columns.Add("titleColumn", "Title");
                    itemGrid.Columns.Add("typeColumn", "Type");
                    itemGrid.Columns.Add("profColumn", "Proficiency");
                    this.Text = (selectMode == MULTI_SELECT) ? "Select Talent(s)" : "Select Talent";
                    break;
                case SCHOOL_TYPE:
                    itemGrid.Columns.Add("titleColumn", "Title");
                    this.Text = (selectMode == MULTI_SELECT) ? "Select School(s)" : "Select School";
                    break;
                case ABILITY_TYPE:
                    itemGrid.Columns.Add("titleColumn", "Title");
                    itemGrid.Columns.Add("typeColumn", "Type");
                    itemGrid.Columns.Add("schoolColumn", "School");
                    itemGrid.Columns.Add("vocationColumn", "Vocation");
                    itemGrid.Columns.Add("levelColumn", "Level");
                    this.Text = (selectMode == MULTI_SELECT) ? "Select Ability/Abilities" : "Select Ability";
                    break;
            }

            parser = new SQLParser();
            blockIDs = new List<long>();
        }

        // Load options from network
        public void ReloadList() {
            switch(loadType) {
                case TRAIT_TYPE:
                    SQLManager.runQuery("vc_articles",
                        new string[] { "id", "title", "description", "properties", "whitelist_protected", "whitelist" },
                        "`trait` = 1",
                        this,
                        SQLManager.LOAD_TRAIT);
                    break;
                case TALENT_TYPE:
                    SQLManager.runQuery("vc_articles",
                        new string[] { "id", "title", "description", "properties", "whitelist_protected", "whitelist" },
                        "`talent` = 1",
                        this,
                        SQLManager.LOAD_TALENT);
                    break;
                case SCHOOL_TYPE:
                    SQLManager.runQuery("vc_articles",
                        new string[] { "id", "title", "description", "properties", "whitelist_protected", "whitelist" },
                        "`school` = 1",
                        this,
                        SQLManager.LOAD_SCHOOL);
                    break;
                case ABILITY_TYPE:
                    SQLManager.runQuery("vc_articles",
                        new string[] { "id", "title", "description", "properties", "whitelist_protected", "whitelist" },
                        "`ability` = 1",
                        this,
                        SQLManager.LOAD_ABILITY);
                    break;
            }
        }

        // Update visible options based on filters/etc
        public void RefreshList() {
            itemGrid.Rows.Clear();
            foreach(Object o in Options) {
                if(FilterCheck(o)) addToGrid(o);
            }
        }

        private void addToGrid(object o) {
            switch (loadType) {
                case TRAIT_TYPE:
                    if (o is Trait) {
                        itemGrid.Rows.Add(new object[] { (Trait)o, ((Trait)o).title, ((Trait)o).type, ((Trait)o).cost });
                    }
                    break;
                case TALENT_TYPE:
                    if (o is Talent) {
                        itemGrid.Rows.Add(new object[] { (Talent)o, ((Talent)o).title, ((Talent)o).type, ((Talent)o).proficiency });
                    }
                    break;
                case SCHOOL_TYPE:
                    if (o is School) {
                        itemGrid.Rows.Add(new object[] { (School)o, ((School)o).title });
                    }
                    break;
                case ABILITY_TYPE:
                    if (o is Ability) {
                        itemGrid.Rows.Add(new object[] { (Ability)o, ((Ability)o).title, ((Ability)o).type, ((Ability)o).school, ((Ability)o).vocation, ((Ability)o).level });
                    }
                    break;
            }
        }
        
        private bool FilterCheck(Object o) {
            bool accept = true;
            if(o is GamePrimitive) {
                if (ModuleFilters.Count > 0) {
                    bool moduleMatch = false;
                    foreach (string s in ModuleFilters) {
                        if (((GamePrimitive)o).IsInModule(s)) {
                            moduleMatch = true;
                        }
                    }
                    if (!moduleMatch) accept = false;
                }
            }
            return accept;
        }

        public void HandleData(int action, Dictionary<String, Object> d) {
            if(action == SQLManager.LOAD_TRAIT && loadType == TRAIT_TYPE) {
                Trait t = parser.parseTrait(d);
                if(blockIDs.Contains(t.netID)) return;
                if(t.netID >= 0) {
                    int permission = 0;
                    if (t.useWhitelist) {
                        permission = sys.WhitelistHandler.getCurrentUserPermissionLevel(t.whitelistString);
                    } else {
                        permission = 2;
                    }

                    if(permission > 0) Options.Add(t);
                    if (FilterCheck(t)) addToGrid(t);
                }
            } else if(action == SQLManager.LOAD_TALENT && loadType == TALENT_TYPE) {
                Talent t = parser.parseTalent(d);
                if (blockIDs.Contains(t.netID)) return;
                if (t.netID >= 0) {
                    int permission = 0;
                    if (t.useWhitelist) {
                        permission = sys.WhitelistHandler.getCurrentUserPermissionLevel(t.whitelistString);
                    } else {
                        permission = 2;
                    }

                    if(permission > 0) Options.Add(t);
                    if (FilterCheck(t)) addToGrid(t);
                }
            } else if (action == SQLManager.LOAD_SCHOOL && loadType == SCHOOL_TYPE) {
                School s = parser.parseSchool(d);
                if (blockIDs.Contains(s.netID)) return;
                if (s.netID >= 0) {
                    int permission = 0;
                    if (s.useWhitelist) {
                        permission = sys.WhitelistHandler.getCurrentUserPermissionLevel(s.whitelistString);
                    } else {
                        permission = 2;
                    }

                    if(permission > 0) Options.Add(s);
                    if (FilterCheck(s)) addToGrid(s);
                }
            } else if(action == SQLManager.LOAD_ABILITY && loadType == ABILITY_TYPE) {
                Ability a = parser.parseAbility(d);
                if (blockIDs.Contains(a.netID)) return;
                if(a.netID >= 0) {
                    int permission = 0;
                    if(a.useWhitelist) {
                        permission = sys.WhitelistHandler.getCurrentUserPermissionLevel(a.whitelistString);
                    } else {
                        permission = 2;
                    }

                    if(permission > 0) Options.Add(a);
                    if (FilterCheck(a)) addToGrid(a);
                }
            }
        }

        public void SetModuleFilters(string str) {
            ModuleFilters.Clear();
            if(str != null) {
                if(!String.IsNullOrWhiteSpace(str)) {
                    string[] mods = str.Split('|');
                    foreach (string s in mods) {
                        AddModuleFilter(s);
                    }
                }
            }
            
        }

        public void AddModuleFilter(string module) {
            if(!ModuleFilters.Contains(module)) {
                ModuleFilters.Add(module);
            }
        }

        private void itemGrid_DoubleClick(object sender, EventArgs e) {
            if(loadButton.Enabled) {
                loadButton_Click(this, EventArgs.Empty);
            }
        }

        private void itemGrid_SelectionChanged(object sender, EventArgs e) {
            if(itemGrid.SelectedRows.Count > 0) {
                loadButton.Enabled = true;
                object target = itemGrid.SelectedRows[0].Cells[0].Value;
                if (target is Trait) {
                    Trait t = (Trait)target;
                    infoField.Text = t.title + Environment.NewLine;
                    if (!string.IsNullOrWhiteSpace(t.type)) infoField.Text += Environment.NewLine + t.type + " Trait" + Environment.NewLine;
                    if (t.type.ToLower().Equals("learned")) {
                        infoField.Text += "Cost: " + t.cost + Environment.NewLine;
                    }
                    infoField.Text += Environment.NewLine + t.description;
                } else if (target is Talent) {
                    Talent t = (Talent)target;
                    infoField.Text = t.title + Environment.NewLine;

                    if (!string.IsNullOrWhiteSpace(t.type)) infoField.Text += t.type + Environment.NewLine;
                    infoField.Text += t.proficiency == "" ? "" : "(" + t.proficiency + ")" + Environment.NewLine;
                    infoField.Text += Environment.NewLine + t.description;
                } else if (target is School) {
                    School s = (School)target;
                    infoField.Text = s.title + Environment.NewLine + Environment.NewLine + s.description;
                } else if (target is Ability) {
                    Ability a = (Ability)target;

                    int mpCost = StringFunctions.resolveCost(a.cost, "mp");

                    infoField.Text = a.title + Environment.NewLine +
                        "Level " + a.level + " " + a.school + " " + a.type + " - " + a.vocation + Environment.NewLine +
                        "Cost: " + a.cost + Environment.NewLine +
                        "Exhaustion: " + a.exhaustion + Environment.NewLine +
                        "Range: " + a.range + Environment.NewLine +
                        "Duration: " + a.duration + Environment.NewLine +
                        "Acc. Check: " + (a.accCheck ? "Yes" : "No") + Environment.NewLine +
                        Environment.NewLine + a.description;
                }
            } else {
                loadButton.Enabled = false;
                infoField.Text = "";
            }
        }

        public void RemoveModuleFilter(string module) {
            ModuleFilters.Remove(module);
        }

        private void loadButton_Click(object sender, EventArgs e) {
            if(itemGrid.SelectedRows.Count > 0) {
                switch(loadType) {
                    case TRAIT_TYPE:
                        List<Trait> tr = new List<Trait>();
                        for(int i = 0; i < itemGrid.SelectedRows.Count; ++i) {
                            if(itemGrid.SelectedRows[i].Cells[0].Value is Trait) {
                                tr.Add((Trait)itemGrid.SelectedRows[i].Cells[0].Value);
                            }
                        }
                        resultTraits = tr.ToArray<Trait>();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    case TALENT_TYPE:
                        List<Talent> ta = new List<Talent>();
                        for (int i = 0; i < itemGrid.SelectedRows.Count; ++i) {
                            if (itemGrid.SelectedRows[i].Cells[0].Value is Talent) {
                                ta.Add((Talent)itemGrid.SelectedRows[i].Cells[0].Value);
                            }
                        }
                        resultTalents = ta.ToArray<Talent>();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    case SCHOOL_TYPE:
                        List<School> sc = new List<School>();
                        for (int i = 0; i < itemGrid.SelectedRows.Count; ++i) {
                            if (itemGrid.SelectedRows[i].Cells[0].Value is School) {
                                sc.Add((School)itemGrid.SelectedRows[i].Cells[0].Value);
                            }
                        }
                        resultSchools = sc.ToArray<School>();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    case ABILITY_TYPE:
                        List<Ability> ab = new List<Ability>();
                        for (int i = 0; i < itemGrid.SelectedRows.Count; ++i) {
                            if (itemGrid.SelectedRows[i].Cells[0].Value is Ability) {
                                ab.Add((Ability)itemGrid.SelectedRows[i].Cells[0].Value);
                            }
                        }
                        resultAbilities = ab.ToArray<Ability>();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                }
            }
            /*
            if (itemListBox.SelectedItems.Count > 0) {
                switch(loadType) {
                    case TRAIT_TYPE:
                        resultTraits = new Trait[itemListBox.SelectedItems.Count];
                        for (int i = 0; i < itemListBox.SelectedItems.Count; ++i) {
                            resultTraits[i] = (Trait)itemListBox.SelectedItems[i];
                        }
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    case TALENT_TYPE:
                        resultTalents = new Talent[itemListBox.SelectedItems.Count];
                        for (int i = 0; i < itemListBox.SelectedItems.Count; ++i) {
                            resultTalents[i] = (Talent)itemListBox.SelectedItems[i];
                        }
                        this.DialogResult = DialogResult.OK;
                        this.Close();

                        break;
                    case SCHOOL_TYPE:
                        resultSchools = new School[itemListBox.SelectedItems.Count];
                        for (int i = 0; i < itemListBox.SelectedItems.Count; ++i) {
                            resultSchools[i] = (School)itemListBox.SelectedItems[i];
                        }
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    case ABILITY_TYPE:
                        resultAbilities = new Ability[itemListBox.SelectedItems.Count];
                        for(int i = 0; i < itemListBox.SelectedItems.Count; ++i) {
                            resultAbilities[i] = (Ability)itemListBox.SelectedItems[i];
                        }
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                }
            }
            */
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
