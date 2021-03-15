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
    public partial class AtlasLoader : Form, SQLListener {
        private SQLParser parser;
        public AtlasTag resultTag;

        public AtlasLoader() {
            InitializeComponent();
            resultTag = new AtlasTag();

            parser = new SQLParser();

            refreshAtlasList();
        }

        private void loadButton_Click(object sender, EventArgs e) {
            if (atlasListBox.SelectedItem != null) {
                if(atlasListBox.SelectedItem is AtlasTag) {
                    resultTag = (AtlasTag)atlasListBox.SelectedItem;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void refreshAtlasList() {
            SQLManager.runQuery("vc_atlas_maps",
                new string[] { "id", "name", "img", "scale", "creator", "whitelist_protected", "whitelist" },
                "",
                this,
                SQLManager.LOAD_ATLAS_TAG);
        }

        public void HandleData(int action, Dictionary<String, Object> d) {
            if (action == SQLManager.LOAD_ATLAS_TAG) {
                AtlasTag tag = parser.parseAtlasTag(d);
                if (tag.netID >= 0) {
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

                    if (permisson > 0) atlasListBox.Items.Add(tag);
                }
            }
        }

        private void atlasListBox_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (atlasListBox.SelectedItem != null) {
                if (atlasListBox.SelectedItem is AtlasTag) {
                    resultTag = (AtlasTag)atlasListBox.SelectedItem;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void atlasListBox_SelectedValueChanged(object sender, EventArgs e) {
            if (atlasListBox.SelectedItem == null) {
                loadButton.Enabled = false;
            } else {
                loadButton.Enabled = true;
            }
        }
    }

    
}
