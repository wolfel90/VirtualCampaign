using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualCampaign.controls {
    public partial class DiceRollerButton : Button {
        private static Random rand;
        private DiceRollerContextMenu popup = new DiceRollerContextMenu();
        public int baseline { get; set; }

        static DiceRollerButton() {
            rand = new Random(DateTime.Now.Millisecond);
        }

        public DiceRollerButton() {
            InitializeComponent();
            baseline = 0;
        }

        private void rollButton_Click(object sender, EventArgs e) {
            int total = 0;
            string output = "(";
            for (int i = 0; i < 3; ++i) {
                if (i > 0) output += " + ";
                int ra = rand.Next(1, 7);
                total += ra;

                output += ra.ToString();
            }
            total += baseline;
            output += ") + " + baseline + " = " + total;
            MessageBox.Show(output);
        }

        private void rollButton_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                popup.baseline = baseline;
                popup.Show(e.Location);
            }
        }
    }
}
