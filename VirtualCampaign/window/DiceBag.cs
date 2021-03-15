using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualCampaign.window {
    public partial class DiceBag : Form {
        private static Random rand;

        public DiceBag() {
            InitializeComponent();
            rand = new Random(DateTime.Now.Millisecond);
        }

        private void rollButton_Click(object sender, EventArgs e) {
            string result = "";
            int n;
            long count;
            if (d4Spinner.Value > 0) {
                result += "========== D4 ==========";
                count = 0;
                for(int i = 0; i < d4Spinner.Value; ++i) {
                    if(i % 5 == 0) {
                        result += Environment.NewLine;
                    }
                    n = rand.Next(1, 5);
                    count += n;
                    result += n + "   ";
                }
                if(d4Spinner.Value > 1) {
                    result += Environment.NewLine + Environment.NewLine + "= Total: " + count + " =";
                }
                result += Environment.NewLine + Environment.NewLine;
            }

            if (d6Spinner.Value > 0) {
                result += "========== D6 ==========";
                count = 0;
                for (int i = 0; i < d6Spinner.Value; ++i) {
                    if (i % 5 == 0) {
                        result += Environment.NewLine;
                    }
                    n = rand.Next(1, 7);
                    count += n;
                    result += n + "   ";
                }
                if (d6Spinner.Value > 1) {
                    result += Environment.NewLine + Environment.NewLine + "= Total: " + count + " =";
                }
                result += Environment.NewLine + Environment.NewLine;
            }

            if (d8Spinner.Value > 0) {
                result += "========== D8 ==========";
                count = 0;
                for (int i = 0; i < d8Spinner.Value; ++i) {
                    if (i % 5 == 0) {
                        result += Environment.NewLine;
                    }
                    n = rand.Next(1, 9);
                    count += n;
                    result += n + "   ";
                }
                if (d8Spinner.Value > 1) {
                    result += Environment.NewLine + Environment.NewLine + "= Total: " + count + " =";
                }
                result += Environment.NewLine + Environment.NewLine;
            }

            if (d10Spinner.Value > 0) {
                result += "========== D10 ==========";
                count = 0;
                for (int i = 0; i < d10Spinner.Value; ++i) {
                    if (i % 5 == 0) {
                        result += Environment.NewLine;
                    }
                    n = rand.Next(1, 11);
                    count += n;
                    result += (n < 10 ? " " : "") + n + "  ";
                }
                if (d10Spinner.Value > 1) {
                    result += Environment.NewLine + Environment.NewLine + "= Total: " + count + " =";
                }
                result += Environment.NewLine + Environment.NewLine;
            }

            if (d12Spinner.Value > 0) {
                result += "========== D12 ==========";
                count = 0;
                for (int i = 0; i < d12Spinner.Value; ++i) {
                    if (i % 5 == 0) {
                        result += Environment.NewLine;
                    }
                    n = rand.Next(1, 13);
                    count += n;
                    result += (n < 10 ? " " : "") + n + "  ";
                }
                if (d12Spinner.Value > 1) {
                    result += Environment.NewLine + Environment.NewLine + "= Total: " + count + " =";
                }
                result += Environment.NewLine + Environment.NewLine;
            }

            if (d20Spinner.Value > 0) {
                result += "========== D20 ==========";
                count = 0;
                for (int i = 0; i < d20Spinner.Value; ++i) {
                    if (i % 5 == 0) {
                        result += Environment.NewLine;
                    }
                    n = rand.Next(1, 21);
                    count += n;
                    result += (n < 10 ? " " : "") + n + "  ";
                }
                if (d20Spinner.Value > 1) {
                    result += Environment.NewLine + Environment.NewLine + "= Total: " + count + " =";
                }
                result += Environment.NewLine + Environment.NewLine;
            }

            if (alphaSpinner.Value > 0) {
                result += "========= Alpha =========";
                for (int i = 0; i < alphaSpinner.Value; ++i) {
                    if (i == 0) {
                        result += Environment.NewLine;
                    }
                    result += (char)(65 + rand.Next(0, 26));
                }
                result += Environment.NewLine + Environment.NewLine;
            }

            if (d100Spinner.Value > 0) {
                result += "========== D100 ==========";
                count = 0;
                for (int i = 0; i < d100Spinner.Value; ++i) {
                    if (i % 5 == 0) {
                        result += Environment.NewLine;
                    }
                    n = rand.Next(1, 101);
                    count += n;
                    result += (n < 100 ? (n < 10 ? "  " : " ") : "") + n + "  ";
                }
                if (d100Spinner.Value > 1) {
                    result += Environment.NewLine + Environment.NewLine + "= Total: " + count + " =";
                }
                result += Environment.NewLine + Environment.NewLine;
            }

            if (customSpinner.Value > 0) {
                result += "========= D" + customSelectSpinner.Value + " =========";
                count = 0;
                for (int i = 0; i < customSpinner.Value; ++i) {
                    if (i % 5 == 0) {
                        result += Environment.NewLine;
                    }
                    n = rand.Next(1, (int)customSelectSpinner.Value + 1);
                    count += n;
                    result += (n < 1000 ? (n < 100 ? (n < 10 ? "   " : "  ") : " ") : "") + n + "  ";
                }
                if (customSpinner.Value > 1) {
                    result += Environment.NewLine + Environment.NewLine + "= Total: " + count + " =";
                }
                result += Environment.NewLine + Environment.NewLine;
            }

            resultField.Text = result;
        }

        private void resetButton_Click(object sender, EventArgs e) {
            d4Spinner.Value = 0;
            d6Spinner.Value = 0;
            d8Spinner.Value = 0;
            d10Spinner.Value = 0;
            d12Spinner.Value = 0;
            d20Spinner.Value = 0;
            alphaSpinner.Value = 0;
            d100Spinner.Value = 0;
            customSpinner.Value = 0;
        }
    }
}
