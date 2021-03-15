using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VirtualCampaign.controls {
    public class GameItemCollectionControl : UserControl {
        public const int NONE = 0, CLONE = 1, MOVE = 2;
        public int GrabMode { get; set; }
        public int DropMode { get; set; }
    }
}
