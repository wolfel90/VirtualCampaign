using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualCampaign.controls;
using VirtualCampaign.data;
using VirtualCampaign.window;

namespace VirtualCampaign.sys {
    public class PageHandler {
        public static TabControl TopTabMenu;

        public static void RequestArticleBrowser() {
            RequestArticleBrowser(-1);
            TopTabMenu = null;
        }

        public static void RequestArticleBrowser(long pageID) {
            Console.Out.WriteLine("Requesting Article ID: " + pageID);
            if(TopTabMenu != null) {
                foreach (TabPage tp in TopTabMenu.TabPages) {
                    foreach (Control c in tp.Controls) {
                        if (c is ArticleBrowser) {
                            TopTabMenu.SelectTab(tp);

                            if(pageID > 0) ((ArticleBrowser)c).displayArticle(pageID);
                            return;
                        }
                    }
                }

                TabPage newTab = new TabPage();
                ArticleBrowser ab = new ArticleBrowser();

                newTab.Text = "Article Browser";
                ab.Dock = DockStyle.Fill;

                TopTabMenu.TabPages.Add(newTab);
                newTab.Controls.Add(ab);
                TopTabMenu.SelectTab(newTab);

                if (pageID > 0) ab.displayArticle(pageID);
            }
        }

        public static void RequestAtlasLoader() {
            RequestAtlasLoader(null);
        }

        public static void RequestAtlasLoader(Article markArticle) {
            if(TopTabMenu != null) {
                AtlasLoader al = new AtlasLoader();
                var result = al.ShowDialog();
                if (result == DialogResult.OK) {
                    if (al.resultTag != null) {
                        AtlasTag tag = al.resultTag;
                        
                        foreach (TabPage tp in TopTabMenu.TabPages) {
                            foreach (Control c in tp.Controls) {
                                if (c is Atlas) {
                                    if(((Atlas)c).MapID == tag.netID) {
                                        TopTabMenu.SelectTab(tp);
                                        if(markArticle != null) {
                                            ((Atlas)c).QueueArticle(markArticle);
                                        }
                                        return;
                                    }
                                }
                            }
                        }
                        TabPage newTab = new TabPage();
                        Atlas atlas = new Atlas(tag.netID);

                        newTab.Text = tag.name;
                        atlas.Dock = DockStyle.Fill;

                        TopTabMenu.TabPages.Add(newTab);
                        newTab.Controls.Add(atlas);
                        TopTabMenu.SelectTab(newTab);

                        atlas.AddMapFromURL(tag.imgSrc);
                        atlas.SetMapScale(tag.scale);

                        if(markArticle != null) {
                            atlas.QueueArticle(markArticle);
                        }
                    }
                }
            }
        }

        public static CharacterSheet RequestCharacterSheet(CharacterData charaData) {
            if(TopTabMenu != null) {
                CharacterSheet cs = new CharacterSheet();
                TabPage newTab = new TabPage();
                if (charaData != null) cs.charaDat = charaData;
                newTab.Text = "Character Sheet";
                cs.Dock = DockStyle.Fill;

                TopTabMenu.TabPages.Add(newTab);
                newTab.Controls.Add(cs);
                TopTabMenu.SelectTab(newTab);
                return cs;
            }
            return null;
        }
    }
}
