using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualCampaign.sys;

namespace VirtualCampaign.window {
    public partial class NameGenerator : Form {
        private List<string> mNames, fNames, sNames;
        private Random rand;

        private NameGeneration gen;
        
        public NameGenerator() {
            InitializeComponent();
            rand = new Random();
            mNames = new List<string>();
            fNames = new List<string>();
            sNames = new List<string>();
            string line;
            using (System.IO.StringReader reader = new System.IO.StringReader(VirtualCampaign.Properties.Resources.First_Names_M)) {
                while ((line = reader.ReadLine()) != null) {
                    mNames.Add(line);
                }
            }
            using (System.IO.StringReader reader = new System.IO.StringReader(VirtualCampaign.Properties.Resources.First_Names_F)) {
                while ((line = reader.ReadLine()) != null) {
                    fNames.Add(line);
                }
            }
            using (System.IO.StringReader reader = new System.IO.StringReader(VirtualCampaign.Properties.Resources.Last_Names)) {
                while ((line = reader.ReadLine()) != null) {
                    sNames.Add(line);
                }
            }

            gen = new NameGeneration();
            gen.PopulatePhonetics();
            //Console.Out.WriteLine(gen.PerformPhoneticAnalysis());
        }

        private void generateButton_Click(object sender, EventArgs e) {
            List<string> firstNames = new List<string>();
            string characterName = "";

            if(phoneticCheck.Checked) {
                characterName += gen.GenerateName() + " " + gen.GenerateName();
            } else {
                if (genderSelection.SelectedItem.ToString() == "Female" || genderSelection.SelectedItem.ToString() == "Either") {
                    firstNames.AddRange(fNames);
                }
                if (genderSelection.SelectedItem.ToString() == "Male" || genderSelection.SelectedItem.ToString() == "Either") {
                    firstNames.AddRange(mNames);
                }

                if (firstNames.Count > 0) {
                    characterName += firstNames[rand.Next(0, firstNames.Count)];
                }
                characterName += " ";
                if (sNames.Count > 0) {
                    characterName += sNames[rand.Next(0, sNames.Count)];
                }
            }

            nameTextField.AppendText(characterName + Environment.NewLine);
            nameTextField.Height = nameTextField.PreferredHeight;
            nameListPanel.VerticalScroll.Value = nameListPanel.VerticalScroll.Maximum;
        }

        private void clearButton_Click(object sender, EventArgs e) {
            nameTextField.Text = "";
            nameTextField.Height = nameTextField.PreferredHeight;
        }
    }
}
