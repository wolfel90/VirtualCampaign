using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using VirtualCampaign.data;
using VirtualCampaign.net;
using VirtualCampaign.server;
using VirtualCampaign.sys;
using VirtualCampaign.window;
using MySql.Data.MySqlClient;

namespace VirtualCampaign {
    public partial class MainWindow : Form {
        private DiceBag diceBag = null;
        private SignInMenu signinMenu = null;

        public MainWindow() {
            InitializeComponent();

            //StringSequencerTest sst = new StringSequencerTest();
            //sst.Show();

            PageHandler.TopTabMenu = topTabMenu;
            string baseline = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:" + System.IO.Path.DirectorySeparatorChar, "");
            if (File.Exists(baseline + Path.DirectorySeparatorChar + "VersionCheck.txt")) {
                FileStream fs = new FileStream(baseline + Path.DirectorySeparatorChar + "VersionCheck.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader reader = new StreamReader(fs, System.Text.Encoding.UTF8, true, 128);
                
                string line;
                if ((line = reader.ReadLine()) != null) {
                    versionLabel.Text = line;
                } else {
                    versionLabel.Text = "Debug";
                }
                reader.Dispose();
                fs.Dispose();
            } else {
                versionLabel.Text = "Debug";
            }

            SQLManager.getConnection();
            UserManager.UserEvent += new UserManager.UserChanged(onUserChanged);

            // Create the framework for visually-draggable objects
            DragObject dragObject = new DragObject();
            DragControlHandler.setTargetComponent(dragObject);
            DragControlHandler.DragControlChanged += (s, e) => trashButton.Visible = DragControlHandler.isCarrying();
            dragObject.Hide();

            this.ResizeBegin += (s, e) => { this.SuspendLayout(); };
            this.ResizeEnd += (s, e) => { this.ResumeLayout(true); };

            // Create AppData folder, if it does not already exist
            try {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + "StrangeRatio" + Path.DirectorySeparatorChar + "prefs";
                Directory.CreateDirectory(path);
                
                // Attempt auto sign in, if applicable
                string userPath = path + Path.DirectorySeparatorChar + "user.txt";
                if (System.IO.File.Exists(userPath)) {
                    using (StreamReader reader = new StreamReader(userPath)) {
                        string line, usr, pwd;
                        if ((line = reader.ReadLine()) != null) {
                            usr = line;
                            if ((line = reader.ReadLine()) != null) {
                                pwd = line;
                                UserManager.attemptSignIn(usr, pwd, out string dud, false);
                            }
                        }
                    }
                }

                // Load local settings
                string prefsPath = path + Path.DirectorySeparatorChar + "settings.txt";
                if(System.IO.File.Exists(prefsPath)) {
                    /*
                    using(StreamReader reader = new StreamReader(prefsPath)) {
                        string[] pref;
                        string line;
                        if((line = reader.ReadLine()) != null) {
                            if(line.Contains('=')) {
                                pref = line.Split('=');
                                switch(pref[0]) {
                                    case "Maximized":
                                        if(pref[1] == "1") {
                                            this.WindowState = FormWindowState.Maximized;
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    */
                }
            } catch(Exception x) {
                Console.Error.WriteLine("Exception handled");
                Console.Error.WriteLine(x);
            }

            if(UserManager.currentUser.userRank < 5) {
                topMenu.Items.Remove(serverToolStripMenuItem);
            }
            
        }

        private void MainWindow_Load(object sender, EventArgs e) {
            if(Properties.Settings.Default.WindowLocation != null) {
                this.Location = Properties.Settings.Default.WindowLocation;
            }
            if(Properties.Settings.Default.WindowSize != null) {
                this.Size = Properties.Settings.Default.WindowSize;
            }
            if(Properties.Settings.Default.WindowMaximized) {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void MainWindow_FormClosing(Object sender, FormClosingEventArgs e) {
            Console.Out.WriteLine("Performing clean-up...");

            Properties.Settings.Default.WindowMaximized = this.WindowState == FormWindowState.Maximized;
            if(this.WindowState == FormWindowState.Normal) {
                Properties.Settings.Default.WindowLocation = this.Location;
                Properties.Settings.Default.WindowSize = this.Size;
            } else {
                Properties.Settings.Default.WindowLocation = this.RestoreBounds.Location;
                Properties.Settings.Default.WindowSize = this.RestoreBounds.Size;
            }
            Properties.Settings.Default.Save();
            /*
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + 
                "StrangeRatio" + Path.DirectorySeparatorChar + "prefs" + Path.DirectorySeparatorChar + "settings.txt";
            using (StreamWriter writer = new StreamWriter(path, false)) {
                
            }
            */
            AsynchronousClient.ShutdownActiveClients();
            AsynchronousSocketListener.ShutdownActiveClients();
        }

        private void articleBrowserMenuItem_Click(object sender, EventArgs e) {
            foreach(TabPage page in topTabMenu.TabPages) {
                foreach(Control c in page.Controls) {
                    if(c is ArticleBrowser) {
                        topTabMenu.SelectTab(page);
                        return;
                    }
                }
            }

            TabPage newTab = new TabPage();
            ArticleBrowser ab = new ArticleBrowser();

            newTab.Text = "Article Browser";
            ab.Dock = DockStyle.Fill;

            topTabMenu.TabPages.Add(newTab);
            newTab.Controls.Add(ab);
            topTabMenu.SelectTab(newTab);
        }

        private void diceBagMenuItem_Click(object sender, EventArgs e) {
            if(diceBag == null) diceBag = new DiceBag();
            if(diceBag.IsDisposed) diceBag = new DiceBag();
            diceBag.Show();
            diceBag.Focus();
        }

        private void itemBankMenuItem_Click(object sender, EventArgs e) {
            foreach(TabPage page in topTabMenu.TabPages) {
                foreach(Control c in page.Controls) {
                    if(c is ItemBank) {
                        topTabMenu.SelectTab(page);
                        return;
                    }
                }
            }

            TabPage newTab = new TabPage();
            ItemBank ib = new ItemBank();

            newTab.Text = "Item Bank";
            ib.Dock = DockStyle.Fill;

            topTabMenu.TabPages.Add(newTab);
            newTab.Controls.Add(ib);
            topTabMenu.SelectTab(newTab);
        }
        
        private void magicalEffectsToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (TabPage page in topTabMenu.TabPages) {
                foreach (Control c in page.Controls) {
                    if (c is MagicEffectTable) {
                        topTabMenu.SelectTab(page);
                        return;
                    }
                }
            }

            TabPage newTab = new TabPage();
            MagicEffectTable met = new MagicEffectTable();

            newTab.Text = "Magical Effects";
            met.Dock = DockStyle.Fill;

            topTabMenu.TabPages.Add(newTab);
            newTab.Controls.Add(met);
            topTabMenu.SelectTab(newTab);
        }

        private void nameGeneratorToolStripMenuItem_Click(object sender, EventArgs e) {
            NameGenerator ng = new NameGenerator();
            ng.Show();
        }

        private void atlasToolStripMenuItem_Click(object sender, EventArgs e) {
            AtlasLoader atlasLoader = new AtlasLoader();
            var result = atlasLoader.ShowDialog();
            if(result == DialogResult.OK) {
                if(atlasLoader.resultTag != null) {
                    AtlasTag tag = atlasLoader.resultTag;

                    foreach (TabPage tp in topTabMenu.TabPages) {
                        foreach (Control c in tp.Controls) {
                            if (c is Atlas) {
                                if (((Atlas)c).MapID == tag.netID) {
                                    topTabMenu.SelectTab(tp);
                                    return;
                                }
                            }
                        }
                    }

                    TabPage newTab = new TabPage();
                    Atlas atlas = new Atlas(tag.netID);

                    newTab.Text = tag.name;
                    atlas.Dock = DockStyle.Fill;

                    topTabMenu.TabPages.Add(newTab);
                    newTab.Controls.Add(atlas);
                    topTabMenu.SelectTab(newTab);
                    
                    atlas.AddMapFromURL(tag.imgSrc);
                    atlas.SetMapScale(tag.scale);
                }
            }
        }

        private void signInToolStripMenuItem_Click(object sender, EventArgs e) {
            if(UserManager.currentUser.userID > 0) {
                UserManager.signOut();
            } else {
                if (signinMenu == null) signinMenu = new SignInMenu();
                if (signinMenu.IsDisposed) signinMenu = new SignInMenu();
                signinMenu.Visible = true;
                signinMenu.Focus();
            }
        }

        private void newCharacterSheetToolStripMenuItem_Click(object sender, EventArgs e) {
            TabPage newTab = new TabPage();
            CharacterSheet cs = new CharacterSheet();

            newTab.Text = "Character Sheet";
            cs.Dock = DockStyle.Fill;
            cs.charaDat.creator = UserManager.currentUser.userID;
            cs.charaDat.useWhitelist = true;
            if(UserManager.currentUser.userID > 0) {
                cs.charaDat.whitelistString = "~" + UserManager.currentUser.userID;
            }
            cs.editable = true;
            

            topTabMenu.TabPages.Add(newTab);
            newTab.Controls.Add(cs);
            topTabMenu.SelectTab(newTab);
        }

        private void loadCharacterSheetToolStripMenuItem_Click(object sender, EventArgs e) {
            bool classic = (sender == loadClassicCharacterSheetMenuItem);
            CharacterLoader loader = new CharacterLoader(classic ? CharacterLoader.CLASSIC_TABLE : CharacterLoader.NEW_TABLE);
            var result = loader.ShowDialog();
            if(result == DialogResult.OK) {
                CharacterTag tag = loader.resultTag;
                if(tag.netID >= 0) {
                    List<Dictionary<string, object>> d;
                    if(classic) {
                        d = SQLManager.runImmediateQuery("vc_chars", CharacterData.classicFieldsList, "`char_id` = " + tag.netID + " LIMIT 1;");
                    } else {
                        d = SQLManager.runImmediateQuery("vc_character_sheets_2", CharacterData.fieldsList, "`id` = " + tag.netID + " LIMIT 1;");
                    }
                    if(d.Count > 0) {
                        SQLParser parser = new SQLParser();
                        CharacterData data = parser.parseCharacterData(d[0], classic);
                        int permission;
                        if(data.useWhitelist) {
                            permission = sys.WhitelistHandler.getCurrentUserPermissionLevel(data.whitelistString);
                        } else {
                            permission = 2;
                        }
                        
                        if(permission > 0) {
                            TabPage newTab = new TabPage();
                            CharacterSheet cs = new CharacterSheet(data);

                            if (permission == 1) cs.editable = false;
                            if (permission == 2) cs.editable = true;
                            newTab.Text = tag.name;
                            cs.Dock = DockStyle.Fill;
                            topTabMenu.TabPages.Add(newTab);
                            newTab.Controls.Add(cs);
                            topTabMenu.SelectTab(newTab);
                        } else {
                            MessageBox.Show("You do not have permission to view this character sheet");
                        }
                    }
                }
            }
        }

        private void generateCharacterToolStripMenuItem_Click(object sender, EventArgs e) {
            TabPage newTab = new TabPage();
            CharacterGenerationMenu cgm = new CharacterGenerationMenu();
            newTab.Text = "Character Generator";
            cgm.Dock = DockStyle.Fill;

            topTabMenu.TabPages.Add(newTab);
            newTab.Controls.Add(cgm);
            topTabMenu.SelectTab(newTab);
        }

        private void newBestiaryTemplateMenuItem_Click(object sender, EventArgs e) {
            TabPage newTab = new TabPage();
            BestiaryTemplate bt = new BestiaryTemplate();

            newTab.Text = "Bestiary Template";
            bt.Dock = DockStyle.Fill;

            topTabMenu.TabPages.Add(newTab);
            newTab.Controls.Add(bt);
            topTabMenu.SelectTab(newTab);
        }

        private void saveCurrentSheetToolStripMenuItem_Click(object sender, EventArgs e) {
            if(topTabMenu.SelectedTab == null) {
                return;
            }
            Control activeControl = null;
            foreach (Control c in topTabMenu.SelectedTab.Controls) {
               if(c is CharacterSheet) {
                    activeControl = c;
                    break;
                }
            }
            if(activeControl != null) {
                if(activeControl is CharacterSheet) {
                    if(((CharacterSheet)activeControl).editable) {
                        ((CharacterSheet)activeControl).suspendSave = false;
                        ((CharacterSheet)activeControl).SaveCharacterData();
                    }
                }
            }
        }

        private void onUserChanged(UserChangeEventArgs e) {
            if(e.tag.userID > 0) {
                userToolStripMenuItem.Text = e.tag.displayName;
                signInToolStripMenuItem.Text = "Sign Out";
            } else {
                userToolStripMenuItem.Text = "User";
                signInToolStripMenuItem.Text = "Sign In";
            }
        }

        private void topTabMenu_DrawItem(object sender, DrawItemEventArgs e) {
            try {
                var tabPage = this.topTabMenu.TabPages[e.Index];
                var tabRect = this.topTabMenu.GetTabRect(e.Index);
                var closeImage = VirtualCampaign.Properties.Resources.closeIcon;
                e.Graphics.DrawImage(closeImage, (tabRect.Right - closeImage.Width - 5), tabRect.Top + (tabRect.Height - closeImage.Height) / 2);
                string title = tabPage.Text;
                bool trunc = false;
                while(e.Graphics.MeasureString(title, tabPage.Font).Width > tabRect.Width - closeImage.Width - 10) {
                    title = title.Substring(0, title.Length - 1);
                    trunc = true;
                }
                if(trunc) title += "...";
                TextRenderer.DrawText(e.Graphics, title, tabPage.Font, tabRect, tabPage.ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
                e.DrawFocusRectangle();
            } catch(Exception x) {
                throw new Exception(x.Message);
            }
        }

        private void topTabMenu_MouseClick(object sender, MouseEventArgs e) {
            int butDim = VirtualCampaign.Properties.Resources.closeIcon.Width;
            for(int i = 0; i < topTabMenu.TabCount; ++i) {
                Rectangle r = topTabMenu.GetTabRect(i);
                r.Offset(r.Width - butDim - 5, (r.Height - butDim) / 2);
                r.Width = butDim;
                r.Height = butDim;
                if(r.Contains(e.Location)) {
                    for(int n = topTabMenu.TabPages[i].Controls.Count - 1; n >= 0; --n) {
                        if (topTabMenu.TabPages[i].Controls[n] is IDisposable) {
                            topTabMenu.TabPages[i].Controls[n].Dispose();
                        }
                    }
                    
                    topTabMenu.TabPages.Remove(topTabMenu.TabPages[i]);
                    break;
                }
            }
        }

        private void versionHistoryToolStripMenuItem_Click(object sender, EventArgs e) {
            VersionHistory vh = new VersionHistory();
            vh.Visible = true;
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e) {
            //dragObject.Location = new Point(this.PointToScreen(e.Location).X + 5, this.PointToScreen(e.Location).Y + 5);
        }

        private void trashButton_MouseClick(object sender, MouseEventArgs e) {
            if(DragControlHandler.getCarriedData() != null) {
                DragControlHandler.clearCarriedData();
            }
        }

        private void startServerToolStripMenuItem_Click(object sender, EventArgs e) {
            AsynchronousSocketListener asl = new AsynchronousSocketListener();
            asl.StartServerThread();
        }

        private void startClientToolStripMenuItem_Click(object sender, EventArgs e) {
            SimpleMessageBox smb = new SimpleMessageBox();
            if(smb.ShowDialog() == DialogResult.OK) {
                AsynchronousClient ac = new AsynchronousClient();
                ac.message = smb.result;
                ac.RunClient();
            }
        }
    }
}
