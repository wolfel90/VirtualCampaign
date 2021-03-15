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
using VirtualCampaign.net;

namespace VirtualCampaign.window {
    public partial class MagicEffectTable : UserControl, SQLListener {
        private SQLParser Parser;
        private bool loaded, editable;
        private List<DataGridViewRow> newRows;
        private List<DataGridViewRow> changedRows;

        public MagicEffectTable() {
            InitializeComponent();
            loaded = false;
            editable = UserManager.currentUser.userRank >= 4;

            newRows = new List<DataGridViewRow>();
            changedRows = new List<DataGridViewRow>();
            Parser = new SQLParser();
            SQLManager.runQuery("vc_magic_effects",
                new string[] { "id", "type", "title", "description", "quality", "rarity", "properties", "creator" },
                null,
                this,
                SQLManager.LOAD_MAGIC_EFFECT
            );

            newButton.Visible = editable;
            saveButton.Visible = editable;
        }

        public void HandleData(int action, Dictionary<string, object> d) {
            if(action == SQLManager.LOAD_MAGIC_EFFECT) {
                if(d != null) {
                    MagicEffect m = Parser.parseMagicEffect(d);
                    string t, q;
                    switch((MagicEffect.EffectType)m.Type) {
                        case MagicEffect.EffectType.Triggered_Effect:
                            t = "Triggered (Generic)";
                            break;
                        case MagicEffect.EffectType.Continuous_Effect:
                            t = "Continuous (Generic)";
                            break;
                        case MagicEffect.EffectType.Triggered_Armor_Effect:
                            t = "Triggered (Armor)";
                            break;
                        case MagicEffect.EffectType.Continuous_Armor_Effect:
                            t = "Continuous (Armor)";
                            break;
                        case MagicEffect.EffectType.Triggered_Weapon_Effect:
                            t = "Triggered (Weapon)";
                            break;
                        case MagicEffect.EffectType.Continuous_Weapon_Effect:
                            t = "Continuous (Armor)";
                            break;
                        default:
                            t = "Triggered (Generic)";
                            break;
                    }
                    switch((MagicEffect.EffectQuality)m.Quality) {
                        case MagicEffect.EffectQuality.Neutral:
                            q = "Neutral";
                            break;
                        case MagicEffect.EffectQuality.Negative:
                            q = "Negative";
                            break;
                        case MagicEffect.EffectQuality.Positive:
                            q = "Positive";
                            break;
                        default:
                            q = "Neutral";
                            break;
                    }
                    dataGrid.Rows.Add(new object[] { m.netID, m.title, t, m.School, q, m.Rarity, m.Prefix, m.Suffix, m.Effect, m.GenerateModsString() });
                }
            } else if(action == SQLManager.DONE) {
                loaded = true;
            }
        }

        private void newButton_Click(object sender, EventArgs e) {
            dataGrid.Rows.Add(new object[] { -1, "", "Triggered (Generic)", "", "Neutral", 0, "", "", "" });
        }

        private void saveButton_Click(object sender, EventArgs e) {
            dataGrid.Enabled = false;
            if(newRows.Count > 0) {
                long newID;
                foreach (DataGridViewRow row in newRows) {
                    MagicEffect newEffect = new MagicEffect();
                    newEffect.title = (row.Cells["nameColumn"].Value ?? "").ToString();
                    newEffect.School = (row.Cells["schoolColumn"].Value ?? "").ToString();
                    newEffect.Rarity = int.TryParse((row.Cells["rarityColumn"].Value ?? "0").ToString(), out int i) ? i : 0;
                    newEffect.Prefix = (row.Cells["prefixColumn"].Value ?? "").ToString();
                    newEffect.Suffix = (row.Cells["suffixColumn"].Value ?? "").ToString();
                    newEffect.Effect = (row.Cells["effectColumn"].Value ?? "").ToString();
                    newEffect.AddModifiers((row.Cells["modsColumn"].Value ?? "").ToString());
                    switch (row.Cells["typeColumn"].Value) {
                        case "Triggered (Generic)":
                            newEffect.SetEffectType(MagicEffect.EffectType.Triggered_Effect);
                            break;
                        case "Continuous (Generic)":
                            newEffect.SetEffectType(MagicEffect.EffectType.Continuous_Effect);
                            break;
                        case "Triggered (Armor)":
                            newEffect.SetEffectType(MagicEffect.EffectType.Triggered_Armor_Effect);
                            break;
                        case "Continuous (Armor)":
                            newEffect.SetEffectType(MagicEffect.EffectType.Continuous_Armor_Effect);
                            break;
                        case "Triggered (Weapon)":
                            newEffect.SetEffectType(MagicEffect.EffectType.Triggered_Weapon_Effect);
                            break;
                        case "Continuous (Weapon)":
                            newEffect.SetEffectType(MagicEffect.EffectType.Continuous_Weapon_Effect);
                            break;
                    }
                    switch(row.Cells["qualityColumn"].Value) {
                        case "Neutral":
                            newEffect.SetQuality(MagicEffect.EffectQuality.Neutral);
                            break;
                        case "Positive":
                            newEffect.SetQuality(MagicEffect.EffectQuality.Positive);
                            break;
                        case "Negative":
                            newEffect.SetQuality(MagicEffect.EffectQuality.Negative);
                            break;
                    }
                    if (SQLManager.runInsert("vc_magic_effects",
                        new string[] { "type", "title", "description", "quality", "rarity", "properties", "creator" },
                        new string[] { ((int)(newEffect.Type)).ToString(), newEffect.title, newEffect.Effect, ((int)(newEffect.Quality)).ToString(),
                            newEffect.Rarity.ToString(), newEffect.GeneratePropertiesString(), UserManager.currentUser.userID.ToString() },
                        out newID)) {
                        row.Cells["idColumn"].Value = newID;
                    }
                    row.DefaultCellStyle.BackColor = Color.White;
                }
                newRows.Clear();
            }
            if(changedRows.Count > 0) {
                foreach(DataGridViewRow row in changedRows) {
                    MagicEffect newEffect = new MagicEffect();
                    newEffect.title = (row.Cells["nameColumn"].Value ?? "").ToString();
                    newEffect.School = (row.Cells["schoolColumn"].Value ?? "").ToString();
                    newEffect.Rarity = int.TryParse((row.Cells["rarityColumn"].Value ?? "0").ToString(), out int i) ? i : 0;
                    newEffect.Prefix = (row.Cells["prefixColumn"].Value ?? "").ToString();
                    newEffect.Suffix = (row.Cells["suffixColumn"].Value ?? "").ToString();
                    newEffect.Effect = (row.Cells["effectColumn"].Value ?? "").ToString();
                    newEffect.AddModifiers((row.Cells["modsColumn"].Value ?? "").ToString());
                    switch (row.Cells["typeColumn"].Value) {
                        case "Triggered (Generic)":
                            newEffect.SetEffectType(MagicEffect.EffectType.Triggered_Effect);
                            break;
                        case "Continuous (Generic)":
                            newEffect.SetEffectType(MagicEffect.EffectType.Continuous_Effect);
                            break;
                        case "Triggered (Armor)":
                            newEffect.SetEffectType(MagicEffect.EffectType.Triggered_Armor_Effect);
                            break;
                        case "Continuous (Armor)":
                            newEffect.SetEffectType(MagicEffect.EffectType.Continuous_Armor_Effect);
                            break;
                        case "Triggered (Weapon)":
                            newEffect.SetEffectType(MagicEffect.EffectType.Triggered_Weapon_Effect);
                            break;
                        case "Continuous (Weapon)":
                            newEffect.SetEffectType(MagicEffect.EffectType.Continuous_Weapon_Effect);
                            break;
                    }
                    switch (row.Cells["qualityColumn"].Value) {
                        case "Neutral":
                            newEffect.SetQuality(MagicEffect.EffectQuality.Neutral);
                            break;
                        case "Positive":
                            newEffect.SetQuality(MagicEffect.EffectQuality.Positive);
                            break;
                        case "Negative":
                            newEffect.SetQuality(MagicEffect.EffectQuality.Negative);
                            break;
                    }
                    SQLManager.runUpdate("vc_magic_effects",
                        new string[] { "type", "title", "description", "quality", "rarity", "properties", "creator" },
                        new string[] { ((int)(newEffect.Type)).ToString(), newEffect.title, newEffect.Effect, ((int)(newEffect.Quality)).ToString(),
                            newEffect.Rarity.ToString(), newEffect.GeneratePropertiesString(), UserManager.currentUser.userID.ToString() },
                        "`id`=" + row.Cells["idColumn"].Value.ToString()
                    );
                    row.DefaultCellStyle.BackColor = Color.White;
                }
                changedRows.Clear();
            }
            dataGrid.Enabled = true;
        }

        private void dataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if(loaded && editable) {
                DataGridViewRow row = dataGrid.Rows[e.RowIndex];
                row.DefaultCellStyle.BackColor = Color.LightGreen;
                if(!newRows.Contains(row)) changedRows.Add(row);
            }
        }

        private void dataGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) {
            if(loaded && editable) {
                for (int i = 0; i < e.RowCount; ++i) {
                    DataGridViewRow row = dataGrid.Rows[e.RowIndex + i];
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                    newRows.Add(row);
                }
            }
        }
    }
}
