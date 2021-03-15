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
using VirtualCampaign.controls;
using VirtualCampaign.data;
using VirtualCampaign.net;
using VirtualCampaign.sys;

namespace VirtualCampaign.window {
    public partial class ArticleBrowser : UserControl, SQLListener {
        private static string header = "";
        private Dictionary<long, List<TreeNode>> valueLookup;
        private List<object> history;
        private TreeNode ruleNode, ruleNode2, loreNode, loreNode2, atlasNode, timelineNode, traitNode, talentNode, schoolNode, schoolNode2, abilityNode, traitNode2, 
            talentNode2, abilityNode2, uncategorizedNode;
        private Article activeArticle = null;
        private SQLParser parser;
        private bool navigationOverride;
        private int historyPosition;
        private string legalTitleChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_!'()+,. ";

        static ArticleBrowser() {
            using(WebClient client = new WebClient()) {
                try {
                    header = "<style>" + client.DownloadString("http://ayaseye.com/vc/style.css") + "</style>";
                    Console.Out.WriteLine("Retrieved Article Browser stylesheet");
                } catch(Exception) {
                    header = "";
                    Console.Error.WriteLine("Failed to retrieve Article Browser CSS Style sheet from server");
                }
            }
        }

        public ArticleBrowser() {
            InitializeComponent();
            valueLookup = new Dictionary<long, List<TreeNode>>();
            parser = new SQLParser();
            history = new List<object>();
            navigationOverride = false;
            historyPosition = 0;
             
            articleTree.BeginUpdate();
            ruleNode = new SystemTreeNode("Rule Book: 1.0");
            ruleNode2 = new SystemTreeNode("Rule Book: 2.0");
            loreNode = new SystemTreeNode("Lore Book: Ayaseye");
            loreNode2 = new SystemTreeNode("Lore Book: Eigolyn");
            atlasNode = new SystemTreeNode("Atlas");
            timelineNode = new SystemTreeNode("Timeline");
            traitNode = new SystemTreeNode("Traits");
            talentNode = new SystemTreeNode("Talents");
            schoolNode = new SystemTreeNode("Schools");
            abilityNode = new SystemTreeNode("Abilities");
            traitNode2 = new SystemTreeNode("Traits");
            talentNode2 = new SystemTreeNode("Talents");
            schoolNode2 = new SystemTreeNode("Schools");
            abilityNode2 = new SystemTreeNode("Abilities");
            uncategorizedNode = new SystemTreeNode("Uncategorized");
            catsList.Items.Add(new CheckListTag("guidebook", "Rule Book: 1.0"));
            catsList.Items.Add(new CheckListTag("guidebook2", "Rule Book: 2.0"));
            catsList.Items.Add(new CheckListTag("lorebook", "Lore Book: Ayaseye"));
            catsList.Items.Add(new CheckListTag("lorebook2", "Lore Book: Eigolyn"));
            catsList.Items.Add(new CheckListTag("atlas", "Atlas"));
            catsList.Items.Add(new CheckListTag("timeline", "Timeline"));
            catsList.Items.Add(new CheckListTag("trait", "Traits"));
            catsList.Items.Add(new CheckListTag("talent", "Talents"));
            catsList.Items.Add(new CheckListTag("school", "Schools"));
            catsList.Items.Add(new CheckListTag("ability", "Abilities"));
            catsList.Items.Add(new CheckListTag("redirect", "Redirect"));
            articleTree.Nodes.Add(ruleNode);
                ruleNode.Nodes.Add(traitNode);
                ruleNode.Nodes.Add(talentNode);
                ruleNode.Nodes.Add(schoolNode);
                ruleNode.Nodes.Add(abilityNode);
            articleTree.Nodes.Add(ruleNode2);
                ruleNode2.Nodes.Add(traitNode2);
                ruleNode2.Nodes.Add(talentNode2);
                ruleNode2.Nodes.Add(schoolNode2);
                ruleNode2.Nodes.Add(abilityNode2);
            articleTree.Nodes.Add(loreNode);
            articleTree.Nodes.Add(loreNode2);
            articleTree.Nodes.Add(atlasNode);
            articleTree.Nodes.Add(timelineNode);
            articleTree.Nodes.Add(uncategorizedNode);
            articleTree.EndUpdate();

            refreshArticleList();

            setEditMode(false, true);
        }

        public void refreshArticleList() {
            articleTree.BeginUpdate();
            SQLManager.runQuery("vc_articles", 
                new string[] { "id", "title", "guidebook", "guidebook2", "lorebook", "lorebook2", "atlas", "timeline", "trait", "talent", "school", "ability", "redirect", "type", "properties",
                    "creator", "creation_time", "editor", "edit_time", "whitelist_protected", "whitelist" }, 
                "", 
                this, 
                SQLManager.LOAD_ARTICLE_TAG);
            articleTree.EndUpdate();
        }
        
        private void articleTree_AfterSelect(Object sender, TreeViewEventArgs e) {
            long artID = -1;
            foreach (KeyValuePair<long, List<TreeNode>> entry in valueLookup) {
                if(entry.Value.Contains(articleTree.SelectedNode)) {
                    artID = entry.Key;
                    break;
                }
            }
            if(artID >= 0) {
                displayArticle(artID);
                updateHistory(artID);
            }
        }

        public string performArticleSearch(string search) {
            if (String.IsNullOrWhiteSpace(search)) return "";
            string likes = "";
            
            if (search.Contains(' ')) {
                string[] split = search.Split(' ');
                int count = 0;
                foreach(string s in split) {
                    if(String.IsNullOrWhiteSpace(s)) continue;
                    if(s.Length > 2) {
                        if (count > 0) likes += " OR ";
                        likes += " `title` LIKE '%" + SQLManager.filterForSQL(s) + "%' OR `description` LIKE '%" + SQLManager.filterForSQL(s) + "%' ";
                        if (++count > 10) break;
                    }
                }
            }
            if (!String.IsNullOrWhiteSpace(likes)) likes += " OR ";
            likes += "`title` LIKE '%" + SQLManager.filterForSQL(search) + "%' OR `description` LIKE '%" + SQLManager.filterForSQL(search) + "%'";

            List<Dictionary<string, object>> results = SQLManager.runImmediateQuery("vc_articles", 
                new string[] { "title", "guidebook", "guidebook2", "lorebook", "lorebook2", "atlas", "timeline", "trait", "talent", "school", "ability", "properties" }, likes + " LIMIT 300;");

            Dictionary<string, List<string>> categorizedResults = new Dictionary<string, List<string>>();
            foreach(string c in new string[] { "Rule Book: 1.0", "Rule Book: 2.0", "Lore Book: Ayaseye", "Lore Book: Eigolyn", "Atlas", "Timeline", "Traits (1.0)", "Traits (2.0)",
                "Talents (1.0)", "Talents (2.0)", "Schools (1.0)", "Schools (2.0)", "Abilities (1.0)", "Abilities (2.0)", "Uncategorized" }) {
                categorizedResults.Add(c, new List<string>());
            }
            SQLParser parser = new SQLParser();
            foreach(Dictionary<string, object> d in results) {
                ArticleTag tempTag = parser.parseArticle(d);
                if (tempTag.bGuide && !tempTag.bTrait && !tempTag.bTalent && !tempTag.bAbility) categorizedResults["Rule Book: 1.0"].Add(tempTag.name);
                if (tempTag.bGuide2 && !tempTag.bTrait && !tempTag.bTalent && !tempTag.bAbility) categorizedResults["Rule Book: 2.0"].Add(tempTag.name);
                if (tempTag.bLore) categorizedResults["Lore Book: Ayaseye"].Add(tempTag.name);
                if (tempTag.bLore2) categorizedResults["Lore Book: Eigolyn"].Add(tempTag.name);
                if (tempTag.bAtlas) categorizedResults["Atlas"].Add(tempTag.name);
                if (tempTag.bTimeline) categorizedResults["Timeline"].Add(tempTag.name);
                if (tempTag.bTrait && tempTag.bGuide) categorizedResults["Traits (1.0)"].Add(tempTag.name);
                if (tempTag.bTrait && tempTag.bGuide2) categorizedResults["Traits (2.0)"].Add(tempTag.name);
                if (tempTag.bTalent && tempTag.bGuide) categorizedResults["Talents (1.0)"].Add(tempTag.name);
                if (tempTag.bTalent && tempTag.bGuide2) categorizedResults["Talents (2.0)"].Add(tempTag.name);
                if (tempTag.bSchool && tempTag.bGuide) categorizedResults["Schools (1.0)"].Add(tempTag.name);
                if (tempTag.bSchool && tempTag.bGuide2) categorizedResults["Schools (2.0)"].Add(tempTag.name);
                if (tempTag.bAbility && tempTag.bGuide) categorizedResults["Abilities (1.0)"].Add(tempTag.name);
                if (tempTag.bAbility && tempTag.bGuide2) categorizedResults["Abilities (2.0)"].Add(tempTag.name);

                if (!tempTag.bGuide && !tempTag.bGuide2 && !tempTag.bLore && !tempTag.bLore2 && !tempTag.bAtlas && !tempTag.bTimeline) categorizedResults["Uncategorized"].Add(tempTag.name);
            }
            string result = "";
            if (results.Count < 300) {
                result += "<center>" + results.Count + (results.Count == 1 ? " result" : " results") + " found</center><br>";
            } else {
                result += "<center>Over 300 results found, truncating to top 300 results</center><br>";
            }
            foreach (string c in categorizedResults.Keys) {
                if(categorizedResults[c].Count > 0) {
                    result += "<b><center>" + c + "</center></b><br>";
                    categorizedResults[c].Sort();
                    foreach(string a in categorizedResults[c]) {
                        result += "[[" + a + "]]<br>";
                    }
                    result += "<br><br>";
                }
            }
            return result;
        }

        private void newButton_Click(object sender, EventArgs e) {
            newButtonMenuStrip.Show(newButton, ((MouseEventArgs)e).X, ((MouseEventArgs)e).Y);
        }

        private void editButton_Click(object sender, EventArgs e) {
            setEditMode(true, false);
        }

        private void saveButton_Click(object sender, EventArgs e) {
            bool valid = true;
            foreach(char c in titleField.Text) {
                if(!legalTitleChars.Contains(c)) {
                    valid = false;
                    MessageBox.Show("Title contains illegal characters. Remove them before saving. Legal characters include:\n" + legalTitleChars);
                    break;
                }
            }
            if(valid) {
                for (int i = 0; i < catsList.Items.Count; ++i) {
                    if (catsList.Items[i] is CheckListTag) {
                        switch (((CheckListTag)catsList.Items[i]).tag) {
                            case "guidebook":
                                activeArticle.bGuide = catsList.GetItemChecked(i);
                                break;
                            case "guidebook2":
                                activeArticle.bGuide2 = catsList.GetItemChecked(i);
                                break;
                            case "lorebook":
                                activeArticle.bLore = catsList.GetItemChecked(i);
                                break;
                            case "lorebook2":
                                activeArticle.bLore2 = catsList.GetItemChecked(i);
                                break;
                            case "atlas":
                                activeArticle.bAtlas = catsList.GetItemChecked(i);
                                break;
                            case "timeline":
                                activeArticle.bTimeline = catsList.GetItemChecked(i);
                                break;
                            case "trait":
                                activeArticle.bTrait = catsList.GetItemChecked(i);
                                break;
                            case "talent":
                                activeArticle.bTalent = catsList.GetItemChecked(i);
                                break;
                            case "school":
                                activeArticle.bSchool = catsList.GetItemChecked(i);
                                break;
                            case "ability":
                                activeArticle.bAbility = catsList.GetItemChecked(i);
                                break;
                            case "redirect":
                                activeArticle.bRedirect = catsList.GetItemChecked(i);
                                break;
                        }
                    }
                }

                activeArticle.name = titleField.Text;
                activeArticle.content = contentTextEditor.Text;
                activeArticle.properties = generatePropertiesString();
                saveArticle(activeArticle);
                if (activeArticle.netID >= 0) {
                    Article tempArticle = activeArticle;
                    if (valueLookup.Keys.Contains(tempArticle.netID)) {
                        removeArticle(tempArticle.netID);
                    }
                    categorize((ArticleTag)tempArticle);
                    displayArticle(tempArticle);
                    updateHistory(tempArticle.netID);
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            displayArticle(activeArticle.netID);
            setEditMode(false, true);
        }

        private void deleteButton_Click(object sender, EventArgs e) {
            if(activeArticle != null) {
                if(activeArticle.netID >= 0) {
                    if (activeArticle.creator == UserManager.currentUser.userID) {
                        if (UserManager.currentUser.userRank >= 3) {
                            var result = MessageBox.Show("Delete article " + activeArticle.name + "?", "Confirm Delete", MessageBoxButtons.YesNo);
                            if(result == DialogResult.Yes) {
                                SQLManager.runDelete("vc_articles", "id = " + activeArticle.netID);
                                removeArticle(activeArticle.netID);
                                displayArticle(new Article());
                            }
                        }
                    }
                }
            }
        }

        private void atlasPinButton_Click(object sender, EventArgs e) {
            PageHandler.RequestAtlasLoader(activeArticle);
        }

        private void contentTextBox_Navigated(object sender, WebBrowserNavigatedEventArgs e) {
        }

        private void contentTextBox_Navigating(object sender, WebBrowserNavigatingEventArgs e) {
            if(navigationOverride) {
                e.Cancel = true;
                navigationOverride = false;
            }
        }

        private void contentTextBox_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
            for(int i = 0; i < contentTextBox.Document.Links.Count; i++) {
                contentTextBox.Document.Links[i].Click += new HtmlElementEventHandler(this.linkClicked);
            }
        }

        private void linkClicked(object sender, EventArgs e) {
            HtmlElement element = ((HtmlElement)sender);
            string titleMatch = element.GetAttribute("href").Replace("about:", "");
            navigationOverride = true;
            if(titleMatch.IndexOf("syscat:") == 0) {
                Article catArt = new Article();
                catArt.name = titleMatch.Substring("syscat:".Length);
                catArt.content = "{{list|" + titleMatch.Substring("syscat:".Length) + "}}";
                catArt.netID = -2;
                catArt.creator = -1;
                displayArticle(catArt);
                updateHistory(catArt);
                return;
            } else {
                Article art = retrieveArticle(titleMatch);
                if (art != null) {
                    displayArticle(art);
                    updateHistory(art.netID);
                    return;
                }
            }
            
            // Default behavior if all the above fails
            Article a = new Article();
            a.name = "Invalid Page";
            a.content = "No article found with the name \"" + titleMatch + "\"";
            a.netID = -2;
            displayArticle(a);
            updateHistory(a);
        }

        private void searchButton_Click(object sender, EventArgs e) {
            if(!String.IsNullOrEmpty(searchTextBox.Text)) {
                string search = performArticleSearch(searchTextBox.Text);
                Article a = new Article();
                a.name = "Search Results for \"" + searchTextBox.Text + "\"";
                a.content = search;
                a.netID = -2;
                displayArticle(a);
                updateHistory(a);
            }
        }

        private void searchTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            if(e.KeyChar == (char)13) {
                searchButton.PerformClick();
                e.Handled = true;
            }
        }

        private void backButton_Click(object sender, EventArgs e) {
            if(historyPosition > 0) {
                historyPosition--;
                if(historyPosition < history.Count) {
                    if(history[historyPosition] is long) {
                        displayArticle((long)history[historyPosition]);
                    } else if(history[historyPosition] is Article) {
                        displayArticle((Article)history[historyPosition]);
                        setEditMode(false, true);
                    }
                }
            }
            if (historyPosition >= history.Count - 1) {
                forwardButton.Enabled = false;
            } else {
                forwardButton.Enabled = true;
            }
            if (historyPosition <= 0) {
                backButton.Enabled = false;
            } else {
                backButton.Enabled = true;
            }
        }

        private void forwardButton_Click(object sender, EventArgs e) {
            if(historyPosition < history.Count - 1) {
                historyPosition++;
                if (historyPosition < history.Count) {
                    if (history[historyPosition] is long) {
                        displayArticle((long)history[historyPosition]);
                    } else if (history[historyPosition] is Article) {
                        displayArticle((Article)history[historyPosition]);
                    }
                }
            }
            if (historyPosition >= history.Count - 1) {
                forwardButton.Enabled = false;
            } else {
                forwardButton.Enabled = true;
            }
            if (historyPosition <= 0) {
                backButton.Enabled = false;
            } else {
                backButton.Enabled = true;
            }
        }

        private void whitelistButton_Click(object sender, EventArgs e) {
            if(activeArticle != null) {
                using (var whitelistMenu = new WhitelistMenu()) {
                    whitelistMenu.setActive(activeArticle.useWhitelist);
                    whitelistMenu.setData(activeArticle.whitelistString);
                    var result = whitelistMenu.ShowDialog();
                    if (result == DialogResult.OK) {
                        activeArticle.useWhitelist = whitelistMenu.resultUse;
                        activeArticle.whitelistString = whitelistMenu.resultString;
                    }
                }
            }
        }

        public void HandleData(int action, Dictionary<String, Object> d) {
            if(action == SQLManager.LOAD_ARTICLE_TAG) {
                ArticleTag art = parser.parseArticleTag(d);
                
                if (art != null) {
                    bool grant = false;
                    if (art.useWhitelist) {
                        grant = false;
                        if (art.creator == UserManager.currentUser.userID) grant = true;
                        if (sys.WhitelistHandler.getCurrentUserPermissionLevel(art.whitelistString) > 0) grant = true;
                    } else {
                        grant = true;
                    }

                    if(grant) {
                        categorize(art);

                        if (art.bGuide2 && art.bSchool) {
                            SystemTreeNode newSchoolNode = new SystemTreeNode(art.name);
                            insertSystemNode(abilityNode2, newSchoolNode);
                        }
                    }
                }
            }
        }

        private void newArticleMenuItem_Click(object sender, EventArgs e) {
            Article newArticle = new Article();
            newArticle.netID = -1;
            activeArticle = newArticle;
            for (int i = 0; i < catsList.Items.Count; ++i) catsList.SetItemChecked(i, false);
            displayArticle(activeArticle);
            setEditMode(true, false);
        }

        private void newAbilityMenuItem_Click(object sender, EventArgs e) {
            Article newArticle = new Article();
            newArticle.netID = -1;
            newArticle.bAbility = true;
            activeArticle = newArticle;
            for (int i = 0; i < catsList.Items.Count; ++i) catsList.SetItemChecked(i, false);
            displayArticle(activeArticle);
            propertyGridView.Rows.Add("type");
            propertyGridView.Rows.Add("school");
            propertyGridView.Rows.Add("vocation");
            propertyGridView.Rows.Add("level");
            propertyGridView.Rows.Add("cost");
            propertyGridView.Rows.Add("exhaust");
            propertyGridView.Rows.Add("range");
            propertyGridView.Rows.Add("duration");
            propertyGridView.Rows.Add("accuracy_check");
            propertyGridView.Rows.Add("icon");
            propertyGridView.Rows.Add("module");
            setEditMode(true, false);
        }

        private void newSchoolMenuItem_Click(object sender, EventArgs e) {
            Article newArticle = new Article();
            newArticle.netID = -1;
            newArticle.bSchool = true;
            activeArticle = newArticle;
            for (int i = 0; i < catsList.Items.Count; ++i) catsList.SetItemChecked(i, false);
            displayArticle(activeArticle);
            propertyGridView.Rows.Add("module");
            setEditMode(true, false);
        }

        private void newTalentMenuItem_Click(object sender, EventArgs e) {
            Article newArticle = new Article();
            newArticle.netID = -1;
            newArticle.bTalent = true;
            activeArticle = newArticle;
            for (int i = 0; i < catsList.Items.Count; ++i) catsList.SetItemChecked(i, false);
            displayArticle(activeArticle);
            propertyGridView.Rows.Add("type");
            propertyGridView.Rows.Add("module");
            setEditMode(true, false);
        }
        
        private void newTraitMenuItem_Click(object sender, EventArgs e) {
            Article newArticle = new Article();
            newArticle.netID = -1;
            newArticle.bTrait = true;
            activeArticle = newArticle;
            for (int i = 0; i < catsList.Items.Count; ++i) catsList.SetItemChecked(i, false);
            displayArticle(activeArticle);
            propertyGridView.Rows.Add("type");
            propertyGridView.Rows.Add("cost", "0");
            propertyGridView.Rows.Add("module");
            setEditMode(true, false);
        }

        public Article retrieveArticle(string title) {
            Article art = null;
            List<Dictionary<string, object>> queryResult =
                    SQLManager.runImmediateQuery("vc_articles",
                    new string[] { "id", "title", "guidebook", "guidebook2", "lorebook", "lorebook2", "atlas", "timeline",  "trait", "talent", "school", "ability", "redirect", "type", "description",
                        "properties", "creator", "creation_time", "editor", "edit_time", "whitelist_protected", "whitelist" },
                    "`title` = '" + title + "' LIMIT 1");
            if (queryResult.Count > 0) {
                art = parser.parseArticle(queryResult[0]);
            }
            return art;
        }

        public Article retrieveArticle(long id) {
            Article art = null;
            List<Dictionary<string, object>> queryResult =
                    SQLManager.runImmediateQuery("vc_articles",
                    new string[] { "id", "title", "guidebook", "guidebook2", "lorebook", "lorebook2", "atlas", "timeline",  "trait", "talent", "school", "ability", "redirect", "type", "description",
                        "properties", "creator", "creation_time", "editor", "edit_time", "whitelist_protected", "whitelist" },
                    "id = " + id + " LIMIT 1");
            if (queryResult.Count > 0) {
                art = parser.parseArticle(queryResult[0]);
            }
            return art;
        }

        public void displayArticle(long id) {
            Article art = retrieveArticle(id);
            if(art != null) {
                displayArticle(art);
            }
        }

        private void displayArticle(Article art) {
            activeArticle = art;
            int permission = getPermissionLevel(art);
            
            switch(permission) {
                case 0:
                    titleField.Text = "Insufficient Permission";
                    contentTextBox.DocumentText = header + "<div class=\"content_full\">" + "You do not have sufficient permissions to view this article." + "</div>";
                    propertyGridView.Rows.Clear();
                    creditLabel.Text = "";
                    setEditMode(false, true);
                    break;
                case 1:
                    titleField.Text = art.name;
                    contentTextEditor.Text = art.content;
                    if(art.bRedirect) {
                        contentTextBox.DocumentText = header + "<div class=\"content_full\">Redirects to: " + StringFunctions.resolveToHTML("[[" + generateFormattedString(art) + "]]") + "</div>";
                    } else {
                        contentTextBox.DocumentText = header + "<div class=\"content_full\">" + StringFunctions.resolveToHTML(generateFormattedString(art)) + "</div>";
                    }
                    populatePropertiesTable(art.properties);
                    if (art.editor > 0) {
                        creditLabel.Text = "Last edit by " + UserManager.getUserDisplayName(art.editor) + " on " + art.editTime;
                    } else {
                        creditLabel.Text = "";
                    }
                    setEditMode(false, true);
                    break;
                case 2:
                    titleField.Text = art.name;
                    contentTextEditor.Text = art.content;
                    if(art.bRedirect) {
                        contentTextBox.DocumentText = header + "<div class=\"content_full\">Redirects to: " + VirtualCampaign.sys.StringFunctions.resolveToHTML("[[" + generateFormattedString(art) + "]]") + "</div>";
                    } else {
                        contentTextBox.DocumentText = header + "<div class=\"content_full\">" + VirtualCampaign.sys.StringFunctions.resolveToHTML(generateFormattedString(art)) + "</div>";
                    }
                    populatePropertiesTable(art.properties);
                    if (art.editor > 0) {
                        creditLabel.Text = "Last edit by " + UserManager.getUserDisplayName(art.editor) + " on " + art.editTime;
                    } else {
                        creditLabel.Text = "";
                    }
                    setEditMode(false, false);
                    break;
            }

            for (int i = 0; i < catsList.Items.Count; ++i) {
                if (catsList.Items[i] is CheckListTag) {
                    switch (((CheckListTag)catsList.Items[i]).tag) {
                        case "guidebook":
                            catsList.SetItemChecked(i, art.bGuide);
                            break;
                        case "guidebook2":
                            catsList.SetItemChecked(i, art.bGuide2);
                            break;
                        case "lorebook":
                            catsList.SetItemChecked(i, art.bLore);
                            break;
                        case "lorebook2":
                            catsList.SetItemChecked(i, art.bLore2);
                            break;
                        case "atlas":
                            catsList.SetItemChecked(i, art.bAtlas);
                            break;
                        case "timeline":
                            catsList.SetItemChecked(i, art.bTimeline);
                            break;
                        case "trait":
                            catsList.SetItemChecked(i, art.bTrait);
                            break;
                        case "talent":
                            catsList.SetItemChecked(i, art.bTalent);
                            break;
                        case "school":
                            catsList.SetItemChecked(i, art.bSchool);
                            break;
                        case "ability":
                            catsList.SetItemChecked(i, art.bAbility);
                            break;
                        case "redirect":
                            catsList.SetItemChecked(i, art.bRedirect);
                            break;
                    }
                }
            }
        }

        private string generateFormattedString(Article art) {
            string result = "";

            if (art.bAbility) {
                string acc = StringFunctions.ReadTag(art.properties, "accuracy_check").ToLower();
                result += "Level " + StringFunctions.ReadTag(art.properties, "level") + " " + StringFunctions.ReadTag(art.properties, "school") + " " +
                    StringFunctions.ReadTag(art.properties, "type") + " - " + StringFunctions.ReadTag(art.properties, "vocation") + Environment.NewLine +
                    "Cost: " + StringFunctions.ReadTag(art.properties, "cost") + Environment.NewLine +
                    "Exhaustion: " + StringFunctions.ReadTag(art.properties, "exhaust") + Environment.NewLine +
                    "Range: " + StringFunctions.ReadTag(art.properties, "range") + Environment.NewLine +
                    "Duration: " + StringFunctions.ReadTag(art.properties, "duration") + Environment.NewLine +
                    "Acc. Check: " + (acc == "t" || acc == "true" || acc == "1" || acc == "yes" || acc == "y" ? "Yes" : "No") + Environment.NewLine +
                    Environment.NewLine + art.content;
            } else if (art.bTrait) {
                string type = StringFunctions.ReadTag(art.properties, "type");
                result += type + " Trait" + Environment.NewLine +
                    (type.ToLower().Equals("learned") ? "Cost: " + StringFunctions.ReadTag(art.properties, "cost") + Environment.NewLine : "") +
                    Environment.NewLine + art.content;
            } else if(art.bTalent) {
                result += art.GetProperty("type") + " Talent" + Environment.NewLine;
                if(art.HasProperty("proficiency")) {
                    result += "Proficiency: " + art.GetProperty("proficiency") + Environment.NewLine;
                } else {
                    result += "No Proficiency" + Environment.NewLine;
                }
                result += Environment.NewLine + art.content;
                //result += StringFunctions.ReadTag(art.properties, "type") + " Talent" + Environment.NewLine + Environment.NewLine + art.content;
            } else {
                result = art.content;
            }

            return result;
        }

        private void saveArticle(Article art) {
            if (art.netID < 0) {
                long newID;
                SQLManager.runInsert("vc_articles",
                    new string[] { "title", "guidebook", "guidebook2", "lorebook", "lorebook2", "atlas", "timeline", "trait", "talent", "school", "ability", "redirect", 
                        "description", "whitelist_protected", "whitelist", "properties", "creator", "creation_time", "editor", "edit_time" },
                    new string[] { titleField.Text, (art.bGuide ? "1" : "0"), (art.bGuide2 ? "1" : "0"), (art.bLore ? "1" : "0"), (art.bLore2 ? "1" : "0"), (art.bAtlas ? "1" : "0"),
                        (art.bTimeline ? "1" : "0"), (art.bTrait ? "1" : "0"), (art.bTalent ? "1" : "0"), (art.bSchool ? "1" : "0"), (art.bAbility ? "1" : "0"), (art.bRedirect ? "1" : "0"),
                        contentTextEditor.Text, (art.useWhitelist ? "1" : "0"), art.whitelistString, generatePropertiesString(),
                        UserManager.currentUser.userID.ToString(),
                        "NOW()", UserManager.currentUser.userID.ToString(), "NOW()" },
                    out newID);
                art.netID = newID;
            } else {
                SQLManager.runUpdate("vc_articles",
                    new string[] { "title", "guidebook", "guidebook2", "lorebook", "lorebook2", "atlas", "timeline", "trait", "talent", "school", "ability", "redirect", 
                        "description", "whitelist_protected", "whitelist", "properties", "editor", "edit_time" },
                    new string[] { titleField.Text, (art.bGuide ? "1" : "0"), (art.bGuide2 ? "1" : "0"), (art.bLore ? "1" : "0"), (art.bLore2 ? "1" : "0"), (art.bAtlas ? "1" : "0"),
                        (art.bTimeline ? "1" : "0"), (art.bTrait ? "1" : "0"), (art.bTalent ? "1" : "0"), (art.bSchool ? "1" : "0"), (art.bAbility ? "1" : "0"), (art.bRedirect ? "1" : "0"),
                        contentTextEditor.Text, (art.useWhitelist ? "1" : "0"), art.whitelistString, generatePropertiesString(),
                        UserManager.currentUser.userID.ToString(), "NOW()" },
                    "id = " + art.netID);
            }
        }

        private string generatePropertiesString() {
            string result = "";
            string property, value;
            foreach(DataGridViewRow r in propertyGridView.Rows) {
                if (r.Cells[0].Value == null || r.Cells[1].Value == null) continue;
                property = r.Cells[0].Value.ToString();
                value = r.Cells[1].Value.ToString();
                if(property.Equals("modifier")) {
                    if(value.Contains(':')) {
                        string[] split = value.Split(':');
                        if(split.Length == 3) {
                            result += "[modifier]";
                            result += "[stat]" + split[0] + "[/stat]";
                            switch(split[1]) {
                                case "x":
                                    result += "[mode]" + StatModifier.MULTIPLY_MODE + "[/mode]";
                                    break;
                                case "+":
                                default:
                                    result += "[mode]" + StatModifier.ADD_MODE + "[/mode]";
                                    break;
                            }
                            result += "[value]" + split[2] + "[/value][/modifier]";
                        }
                    }
                } else {
                    result += "[" + property + "]" + value + "[/" + property + "]";
                }
            }
            return result;
        }

        private void populatePropertiesTable(String str) {
            propertyGridView.Rows.Clear();
            if(str != null) {
                string[] tags = StringFunctions.TagFinder(str);
                foreach (string tag in tags) {
                    string[] vals = StringFunctions.ReadTagMulti(str, tag);
                    foreach (string v in vals) {
                        if (tag.Equals("modifier")) {
                            string data = StringFunctions.ReadTag(v, "stat") + ":";
                            switch (int.TryParse(StringFunctions.ReadTag(v, "mode"), out int i) ? i : StatModifier.ADD_MODE) {
                                case StatModifier.MULTIPLY_MODE:
                                    data += "x:";
                                    break;
                                case StatModifier.ADD_MODE:
                                default:
                                    data += "+:";
                                    break;
                            }
                            data += StringFunctions.ReadTag(v, "value");
                            propertyGridView.Rows.Add(tag, data);
                        } else {
                            propertyGridView.Rows.Add(tag, v);
                        }
                    }
                }
            }
        }

        private void setEditMode(bool mode, bool viewOnly) {
            if(mode) {
                titleField.ReadOnly = false;
                contentPanel.Controls.Remove(contentTextBox);
                contentPanel.Controls.Add(contentTextEditor);
                contentTextBox.Visible = false;
                contentTextEditor.Visible = true;
                contentTextEditor.Dock = DockStyle.Fill;
                toolPanel.Controls.Remove(articleTree);
                toolPanel.Controls.Add(detailsPanel);
                articleTree.Visible = false;
                detailsPanel.Visible = true;
                detailsPanel.Dock = DockStyle.Fill;
                editButton.Visible = false;
                newButton.Visible = false;
                saveButton.Visible = !viewOnly;
                cancelButton.Visible = true;
                deleteButton.Visible = false;
                atlasPinButton.Visible = false;
            } else {
                titleField.ReadOnly = true;
                contentPanel.Controls.Remove(contentTextEditor);
                contentPanel.Controls.Add(contentTextBox);
                contentTextEditor.Visible = false;
                contentTextBox.Visible = true;
                contentTextBox.Dock = DockStyle.Fill;
                toolPanel.Controls.Remove(detailsPanel);
                toolPanel.Controls.Add(articleTree);
                detailsPanel.Visible = false;
                articleTree.Visible = true;
                articleTree.Dock = DockStyle.Fill;
                editButton.Visible = !viewOnly;
                newButton.Visible = UserManager.currentUser.userID > 0 && UserManager.currentUser.userRank >= 3;
                saveButton.Visible = false;
                cancelButton.Visible = false;
                atlasPinButton.Visible = !viewOnly;
                deleteButton.Visible = activeArticle?.creator == UserManager.currentUser.userID;
            }
            searchTextBox.Enabled = !mode;
            searchButton.Enabled = !mode;
        }

        private int getPermissionLevel(Article art) {
            int permission = 0;
            if(art.netID == -2) { // ID of -2 reserved for System messages
                return 1;
            }
            if (art.useWhitelist) {
                if(art.creator == UserManager.currentUser.userID) {
                    permission = 2;
                } else {
                    permission = sys.WhitelistHandler.getCurrentUserPermissionLevel(art.whitelistString);
                }
            } else {
                if (UserManager.currentUser.userID > 0) {
                    permission = 2;
                } else {
                    permission = 1;
                }
            }
            return permission;
        }

        private void categorize(ArticleTag a) {
            TreeNode newNode;
            if (!valueLookup.ContainsKey(a.netID)) {
                valueLookup.Add(a.netID, new List<TreeNode>());
            }
            if(a.bGuide) {
                if (a.bTrait) {
                    newNode = new TreeNode(a.name);
                    insertAlphabetical(traitNode, newNode);
                    if (!valueLookup[a.netID].Contains(newNode)) {
                        valueLookup[a.netID].Add(newNode);
                    }
                } else if (a.bTalent) {
                    newNode = new TreeNode(a.name);
                    insertAlphabetical(talentNode, newNode);
                    if (!valueLookup[a.netID].Contains(newNode)) {
                        valueLookup[a.netID].Add(newNode);
                    }
                } else if(a.bSchool) {
                    newNode = new TreeNode(a.name);
                    insertAlphabetical(schoolNode, newNode);
                    if (!valueLookup[a.netID].Contains(newNode)) {
                        valueLookup[a.netID].Add(newNode);
                    }
                } else if (a.bAbility) {
                    newNode = new TreeNode(a.name);
                    insertAlphabetical(abilityNode, newNode);
                    if (!valueLookup[a.netID].Contains(newNode)) {
                        valueLookup[a.netID].Add(newNode);
                    }
                } else {
                    newNode = new TreeNode(a.name);
                    insertAlphabetical(ruleNode, newNode);
                    if (!valueLookup[a.netID].Contains(newNode)) {
                        valueLookup[a.netID].Add(newNode);
                    }
                }
            }
            if (a.bGuide2) {
                if (a.bTrait) {
                    newNode = new TreeNode(a.name);
                    insertAlphabetical(traitNode2, newNode);
                    if (!valueLookup[a.netID].Contains(newNode)) {
                        valueLookup[a.netID].Add(newNode);
                    }
                } else if (a.bTalent) {
                    newNode = new TreeNode(a.name);
                    insertAlphabetical(talentNode2, newNode);
                    if (!valueLookup[a.netID].Contains(newNode)) {
                        valueLookup[a.netID].Add(newNode);
                    }
                } else if (a.bSchool) {
                    newNode = new TreeNode(a.name);
                    insertAlphabetical(schoolNode2, newNode);
                    if (!valueLookup[a.netID].Contains(newNode)) {
                        valueLookup[a.netID].Add(newNode);
                    }
                } else if (a.bAbility) {
                    bool schoolFound = false;
                    if(a.HasProperty("school")) {
                        foreach(SystemTreeNode sn in abilityNode2.Nodes) {
                            if(sn.Text.ToLower().Equals(a.GetProperty("school").ToLower())) {
                                schoolFound = true;
                                newNode = new TreeNode(a.name);
                                insertAlphabetical(sn, newNode);
                                if (!valueLookup[a.netID].Contains(newNode)) {
                                    valueLookup[a.netID].Add(newNode);
                                }
                                break;
                            }
                        }
                        if(!schoolFound) {
                            newNode = new TreeNode(a.name);
                            insertAlphabetical(abilityNode2, newNode);
                            if (!valueLookup[a.netID].Contains(newNode)) {
                                valueLookup[a.netID].Add(newNode);
                            }
                        }
                    }
                } else {
                    newNode = new TreeNode(a.name);
                    insertAlphabetical(ruleNode2, newNode);
                    if (!valueLookup[a.netID].Contains(newNode)) {
                        valueLookup[a.netID].Add(newNode);
                    }
                }
            }
            if (a.bLore) {
                newNode = new TreeNode(a.name);
                insertAlphabetical(loreNode, newNode);
                if (!valueLookup[a.netID].Contains(newNode)) {
                    valueLookup[a.netID].Add(newNode);
                }
            }
            if (a.bLore2) {
                newNode = new TreeNode(a.name);
                insertAlphabetical(loreNode2, newNode);
                if (!valueLookup[a.netID].Contains(newNode)) {
                    valueLookup[a.netID].Add(newNode);
                }
            }
            if (a.bAtlas) {
                newNode = new TreeNode(a.name);
                insertAlphabetical(atlasNode, newNode);
                if (!valueLookup[a.netID].Contains(newNode)) {
                    valueLookup[a.netID].Add(newNode);
                }
            }
            if (a.bTimeline) {
                newNode = new TreeNode(a.name);
                insertAlphabetical(timelineNode, newNode);
                if (!valueLookup[a.netID].Contains(newNode)) {
                    valueLookup[a.netID].Add(newNode);
                }
            }
            if(!a.bGuide && !a.bGuide2 && !a.bLore && !a.bLore2 && !a.bAtlas && !a.bTimeline && !a.bRedirect) {
                newNode = new TreeNode(a.name);
                insertAlphabetical(uncategorizedNode, newNode);
                if (!valueLookup[a.netID].Contains(newNode)) {
                    valueLookup[a.netID].Add(newNode);
                }
            }
        }

        private void insertSystemNode(TreeNode parent, SystemTreeNode child) {
            if(parent.GetNodeCount(false) == 0) {
                parent.Nodes.Add(child);
            } else {
                foreach(TreeNode n in parent.Nodes) {
                    if(n is SystemTreeNode) {
                        int c = child.Text.CompareTo(n.Text);
                        if(c < 0) {
                            parent.Nodes.Insert(n.Index, child);
                            break;
                        } else {
                            if(n.Index == parent.Nodes.Count - 1) {
                                parent.Nodes.Add(child);
                                break;
                            }
                        }
                    } else {
                        parent.Nodes.Insert(n.Index, child);
                        break;
                    }
                }
            }
        }

        private void insertAlphabetical(TreeNode parent, TreeNode newNode) {
            if (parent.GetNodeCount(false) == 0) {
                parent.Nodes.Add(newNode);
            } else {
                foreach (TreeNode n in parent.Nodes) {
                    if(n is SystemTreeNode) {
                        if(n.Index == parent.Nodes.Count - 1) {
                            parent.Nodes.Add(newNode);
                            break;
                        } else {
                            continue;
                        }
                    }
                    int c = newNode.Text.CompareTo(n.Text);
                    if (c < 0) {
                        parent.Nodes.Insert(n.Index, newNode);
                        break;
                    } else {
                        if (n.Index == parent.Nodes.Count - 1) {
                            parent.Nodes.Add(newNode);
                            break;
                        }
                    }
                }
            }
        }

        private void removeArticle(long id) {
            articleTree.BeginUpdate();
            if(valueLookup.ContainsKey(id)) {
                foreach(TreeNode node in valueLookup[id]) {
                    node.Remove();
                }
            }
            articleTree.EndUpdate();
        }

        private void updateHistory(object o) {
            if(history.Count == 0) {
                historyPosition = 0;
                history.Add(o);
            } else {
                while (history.Count > historyPosition + 1 && history.Count > 0) {
                    history.RemoveAt(history.Count - 1);
                }
                history.Add(o);
                historyPosition++;
            }
            backButton.Enabled = true;
            forwardButton.Enabled = false;
        }
    }

    public class CheckListTag {
        public string tag { get; set; }
        public string title { get; set; }

        public CheckListTag(string tag, string title) {
            this.tag = tag;
            this.title = title;
        }

        public override string ToString() {
            return title;
        }
    }
}
