using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualCampaign.controls {
    public partial class DiceRollerContextMenu : ContextMenuStrip {
        private static Random rand;
        public int baseline { get; set; }
        public string rollSource { get; set; }

        static DiceRollerContextMenu() {
            rand = new Random(DateTime.Now.Millisecond);
        }

        public DiceRollerContextMenu() {
            InitializeComponent();
            baseline = 0;
            rollSource = "";
        }

        private void rollNormallyToolStripMenuItem_Click(object sender, EventArgs e) {
            int total = 0;
            string output = "(";
            for (int i = 0; i < 3; ++i) {
                if (i > 0) output += " + ";
                int ra = rand.Next(1, 7);
                total += ra;

                output += ra.ToString();
            }
            total += baseline;
            output += ") " + (baseline < 0 ? "- " : "+ ") + (Math.Abs(baseline)) + (String.IsNullOrWhiteSpace(rollSource) ? "" : " " + rollSource) + " = " + total;
            MessageBox.Show(output);
        }

        private void rollWithAdvantageToolStripMenuItem_Click(object sender, EventArgs e) {
            int total = 0;
            string output = "(";
            for (int i = 0; i < 4; ++i) {
                if (i > 0) output += " + ";
                int ra = rand.Next(1, 7);
                total += ra;

                output += ra.ToString();
            }
            total += baseline;
            output += ") " + (baseline < 0 ? "- " : "+ ") + (Math.Abs(baseline)) + (String.IsNullOrWhiteSpace(rollSource) ? "" : " " + rollSource) + " = " + total;
            MessageBox.Show(output);
        }

        private void rollWithDisadvantageToolStripMenuItem_Click(object sender, EventArgs e) {
            int total = 0;
            string output = "(";
            for (int i = 0; i < 2; ++i) {
                if (i > 0) output += " + ";
                int ra = rand.Next(1, 7);
                total += ra;

                output += ra.ToString();
            }
            total += baseline;
            output += ") " + (baseline < 0 ? "- " : "+ ") + (Math.Abs(baseline)) + (String.IsNullOrWhiteSpace(rollSource) ? "" : " " + rollSource) + " = " + total;
            MessageBox.Show(output);
        }

        private void rollWithHalfAdvantageToolStripMenuItem_Click(object sender, EventArgs e) {
            int total = 0;
            List<int> rolls = new List<int>();
            string output = "(";
            for (int i = 0; i < 4; ++i) {
                int ra = rand.Next(1, 7);
                rolls.Add(ra);
            }
            int lowest = 0;
            for (int i = 1; i < rolls.Count; ++i) {
                if (rolls[i] < rolls[lowest]) {
                    lowest = i;
                }
            }
            rolls.RemoveAt(lowest);
            for (int i = 0; i < rolls.Count; ++i) {
                if (i > 0) output += " + ";
                total += rolls[i];
                output += rolls[i].ToString();
            }
            total += baseline;
            output += ") " + (baseline < 0 ? "- " : "+ ") + (Math.Abs(baseline)) + (String.IsNullOrWhiteSpace(rollSource) ? "" : " " + rollSource) + " = " + total;
            MessageBox.Show(output);
        }

        private void rollWithHalfDisadvantageToolStripMenuItem_Click(object sender, EventArgs e) {
            int total = 0;
            List<int> rolls = new List<int>();
            string output = "(";
            for (int i = 0; i < 4; ++i) {
                int ra = rand.Next(1, 7);
                rolls.Add(ra);
            }
            int highest = 0;
            for (int i = 1; i < rolls.Count; ++i) {
                if (rolls[i] > rolls[highest]) {
                    highest = i;
                }
            }
            rolls.RemoveAt(highest);
            for (int i = 0; i < rolls.Count; ++i) {
                if (i > 0) output += " + ";
                total += rolls[i];
                output += rolls[i].ToString();
            }
            total += baseline;
            output += ") " + (baseline < 0 ? "- " : "+ ") + (Math.Abs(baseline)) + (String.IsNullOrWhiteSpace(rollSource) ? "" : " " + rollSource) + " = " + total;
            MessageBox.Show(output);
        }
    }
}
