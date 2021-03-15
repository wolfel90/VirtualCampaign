using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualCampaign.sys;

namespace VirtualCampaign.data {
    public class Article : ArticleTag {
        public string content = "";
        
        public Article() {
            netID = -1;
            name = "";
        }

        public Article(ArticleTag tag) {
            bGuide = tag.bGuide;
            bGuide2 = tag.bGuide2;
            bLore = tag.bLore;
            bLore2 = tag.bLore2;
            bAtlas = tag.bAtlas;
            bTimeline = tag.bTimeline;
            bTrait = tag.bTrait;
            bTalent = tag.bTalent;
            bSchool = tag.bSchool;
            bAbility = tag.bAbility;
            bRedirect = tag.bRedirect;
            setCategories(tag.generateCategoryString());
            netID = tag.netID;
            name = tag.name;
            creator = tag.creator;
            creationTime = tag.creationTime;
            editor = tag.editor;
            editTime = tag.editTime;
            useWhitelist = tag.useWhitelist;
            whitelistString = tag.whitelistString;
            properties = tag.properties;
        }
    }
}
