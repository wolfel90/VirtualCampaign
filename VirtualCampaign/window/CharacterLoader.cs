using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualCampaign.net;
using VirtualCampaign.data;

namespace VirtualCampaign.window {
    public partial class CharacterLoader : Form, SQLListener {
        public const string CLASSIC_TABLE = "vc_chars", NEW_TABLE = "vc_character_sheets_2";
        public CharacterTag resultTag { get; set; }
        private SQLParser parser;
        private string table;

        public CharacterLoader() : this(NEW_TABLE) {}

        public CharacterLoader(string targetTable) {
            InitializeComponent();
            parser = new SQLParser();
            table = targetTable;
            resultTag = null;
        }

        private void loadButton_Click(object sender, EventArgs e) {
            if(characterListBox.SelectedItem != null) {
                if(characterListBox.SelectedItem is CharacterTag) {
                    resultTag = (CharacterTag)characterListBox.SelectedItem;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void refreshCharacterList() {
            if(table.Equals(CLASSIC_TABLE)) {
                SQLManager.runQuery(table,
                new string[] { "char_id", "name", "family", "owner", "gm", "shared", "whitelist" },
                "",
                this,
                SQLManager.LOAD_CLASSIC_CHARACTER_TAG);
            } else if(table.Equals(NEW_TABLE)) {
                SQLManager.runQuery(table,
                new string[] { "id", "name", "creator", "whitelist_protected", "whitelist" },
                "",
                this,
                SQLManager.LOAD_CHARACTER_TAG);
            } else {

            }
        }

        public void HandleData(int action, Dictionary<String, Object> d) {
            if(action == SQLManager.LOAD_CHARACTER_TAG) {
                CharacterTag tag = parser.parseCharacterTag(d);
                if(tag.netID >= 0) {
                    int permisson = 0;
                    if (tag.creator == UserManager.currentUser.userID) {
                        permisson = 2;
                    } else {
                        if (tag.useWhitelist) {
                            permisson = sys.WhitelistHandler.getCurrentUserPermissionLevel(tag.whitelistString);
                        } else {
                            permisson = 2;
                        }
                    }
                    
                    if (permisson > 0) characterListBox.Items.Add(tag);
                }
            } else if(action == SQLManager.LOAD_CLASSIC_CHARACTER_TAG) {
                CharacterTag tag = parser.parseCharacterTag(d, true);
                if (tag.netID >= 0) {
                    int permisson = 0;
                    if (tag.owner.ToLower() == UserManager.currentUser.displayName.ToLower() || tag.gm.ToLower() == UserManager.currentUser.displayName.ToLower()) {
                        permisson = 2;
                    } else {
                        if (tag.useWhitelist) {
                            permisson = sys.WhitelistHandler.getCurrentUserPermissionLevel(tag.whitelistString);
                        } else {
                            permisson = 2;
                        }
                    }

                    if (permisson > 0) characterListBox.Items.Add(tag);
                }
            }
        }

        private void characterListBox_SelectedValueChanged(object sender, EventArgs e) {
            if(characterListBox.SelectedItem == null) {
                loadButton.Enabled = false;
            } else {
                loadButton.Enabled = true;
            }
        }

        private void CharacterLoader_Load(object sender, EventArgs e) {
            refreshCharacterList();
        }

        private void characterListBox_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (characterListBox.SelectedItem != null) {
                if (characterListBox.SelectedItem is CharacterTag) {
                    resultTag = (CharacterTag)characterListBox.SelectedItem;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }
    }
}
