using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualCampaign.controls {
    public class SystemTreeNode : TreeNode {
        public SystemTreeNode(string text) : base(text) {
            NodeFont = new Font("Microsoft Sans Serif", 8, FontStyle.Italic);
        }
    }
}
