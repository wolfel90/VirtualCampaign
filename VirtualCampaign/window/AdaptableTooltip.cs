using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualCampaign.data;
using VirtualCampaign.controls;
using VirtualCampaign.sys;

namespace VirtualCampaign.window {
    public partial class AdaptableTooltip : Form {

        public AdaptableTooltip() {
            InitializeComponent();
            Size = new Size(1, 1);
        }

        public void generateTooltip(Object src) {
            Clear();
            if(src == null) {
                Size = new Size(1, 1);
                return;
            }

            if (src is ItemData) {
                ItemImage iconBox = new ItemImage();
                Label contentBox = new Label();

                iconBox.bgSrc = ((ItemData)src).bgSrc;
                iconBox.bgColor = Color.FromArgb(((ItemData)src).bgColor);
                iconBox.iconSrc = ((ItemData)src).iconSrc;
                iconBox.iconColor = Color.FromArgb(((ItemData)src).iconColor);
                contentBox.Text = ((ItemData)src).getDescriptiveString();

                int height = TextRenderer.MeasureText(contentBox.Text, contentBox.Font, new Size(200, 0), TextFormatFlags.WordBreak).Height;

                iconBox.Bounds = new Rectangle(2, 2, 64, 64);
                contentBox.Bounds = new Rectangle(68, 2, 200, height);

                contentPanel.Controls.Add(iconBox);
                contentPanel.Controls.Add(contentBox);

                this.Size = new Size(270, contentBox.Bottom + 5 > iconBox.Bottom + 2 ? contentBox.Bottom + 5 : iconBox.Bottom + 2);

            } else if (src is EquipSlot) {
                if (((EquipSlot)src).itemData != null) {
                    generateTooltip(((EquipSlot)src).itemData);
                }
            } else if (src is AbilitySlot) {
                if (((AbilitySlot)src).ability != null) {
                    generateTooltip(((AbilitySlot)src).ability);
                }
            } else if (src is Ability) {
                PictureBox iconBox = new PictureBox();
                Label titleBox = new Label();
                Label contentBox = new Label();

                iconBox.Image = (Bitmap)Properties.AbilityIcons.ResourceManager.GetObject(((Ability)src).imgSrc);
                titleBox.Text = ((Ability)src).title;
                contentBox.Text = "Level " + ((Ability)src).level + " " + ((Ability)src).school + " " + ((Ability)src).type + " - " + ((Ability)src).vocation + Environment.NewLine +
                    "Cost: " + ((Ability)src).cost + Environment.NewLine +
                    "Exhaustion: " + ((Ability)src).exhaustion + Environment.NewLine +
                    "Range: " + ((Ability)src).range + Environment.NewLine +
                    "Duration: " + ((Ability)src).duration + Environment.NewLine +
                    "Acc. Check: " + (((Ability)src).accCheck ? "Yes" : "No") + Environment.NewLine +
                    Environment.NewLine + StringFunctions.stripSystemTags(((Ability)src).description);

                int height = TextRenderer.MeasureText(contentBox.Text, contentBox.Font, new Size(200, 0), TextFormatFlags.WordBreak).Height;

                iconBox.Bounds = new Rectangle(2, 2, 64, 64);
                titleBox.Bounds = new Rectangle(68, 2, 200, 20);
                contentBox.Bounds = new Rectangle(68, 24, 200, height);

                contentPanel.Controls.Add(iconBox);
                contentPanel.Controls.Add(titleBox);
                contentPanel.Controls.Add(contentBox);

                this.Size = new Size(270, contentBox.Bottom + 5 > iconBox.Bottom + 2 ? contentBox.Bottom + 5 : iconBox.Bottom + 2);
            } else if (src is TraitPanel) {
                if (((TraitPanel)src).trait != null) {
                    generateTooltip(((TraitPanel)src).trait);
                }
            } else if (src is Trait) {
                Label titleBox = new Label();
                Label contentBox = new Label();
                titleBox.Text = ((Trait)src).title;
                contentBox.Text = ((Trait)src).type + " Trait" + Environment.NewLine 
                    + (((Trait)src).cost > 0 ? ((Trait)src).cost + " Trait Point" + (((Trait)src).cost > 1 ? "s" : "") + Environment.NewLine : "") 
                    + Environment.NewLine + StringFunctions.stripSystemTags(((Trait)src).description);

                int height = TextRenderer.MeasureText(contentBox.Text, contentBox.Font, new Size(200, 0), TextFormatFlags.WordBreak).Height;

                titleBox.Bounds = new Rectangle(2, 2, 200, 20);
                contentBox.Bounds = new Rectangle(2, 24, 200, height);

                contentPanel.Controls.Add(titleBox);
                contentPanel.Controls.Add(contentBox);

                this.Size = new Size(204, contentBox.Bottom + 5);
            } else if(src is TalentPanel) {
                if(((TalentPanel)src).talent != null) {
                    generateTooltip(((TalentPanel)src).talent);
                }
            } else if(src is Talent) {
                Label titleBox = new Label();
                Label contentBox = new Label();

                titleBox.Text = ((Talent)src).title;
                contentBox.Text = ((Talent)src).type + " Talent" + Environment.NewLine + "Rank " + ((Talent)src).rank + 
                    (((Talent)src).rank < Talent.TALENT_MAX ? Environment.NewLine + ((Talent)src).progress.ToString() + "/" + (((Talent)src).rank >= 10 ? "10" : (((Talent)src).rank + 1).ToString()) + " Progress" : "") +
                    Environment.NewLine + Environment.NewLine +
                    StringFunctions.stripSystemTags(((Talent)src).description);

                int height = TextRenderer.MeasureText(contentBox.Text, contentBox.Font, new Size(200, 0), TextFormatFlags.WordBreak).Height;

                titleBox.Bounds = new Rectangle(2, 2, 200, 20);
                contentBox.Bounds = new Rectangle(2, 24, 200, height);

                contentPanel.Controls.Add(titleBox);
                contentPanel.Controls.Add(contentBox);

                this.Size = new Size(204, contentBox.Bottom + 5);
            } else if(src is ProficiencyPanel) {
                Label textBox = new Label();
                textBox.AutoSize = false;

                textBox.Text = ((ProficiencyPanel)src).getBaseline() + " - Base Score";
                foreach(string key in ((ProficiencyPanel)src).GetValueAddends()) {
                    if(key != CompoundedStat.BASELINE) {
                        textBox.Text += Environment.NewLine + 
                            (((ProficiencyPanel)src).getAddend(key) > 0 ? "+" : "") + ((ProficiencyPanel)src).getAddend(key) + " - " + ((ProficiencyPanel)src).getHint(key);
                    }
                }

                
                textBox.Text += Environment.NewLine + Environment.NewLine + ((ProficiencyPanel)src).Modifier.Baseline + " - Base Modifier";
                foreach (string key in ((ProficiencyPanel)src).Modifier.GetAllAddends()) {
                    if (key != CompoundedStat.BASELINE) {
                        textBox.Text += Environment.NewLine +
                            (((ProficiencyPanel)src).Modifier.getAddend(key) > 0 ? "+" : "") + ((ProficiencyPanel)src).Modifier.getAddend(key) + " - " + ((ProficiencyPanel)src).Modifier.getHint(key);
                    }
                }

                int height = TextRenderer.MeasureText(textBox.Text, textBox.Font, new Size(200, 0), TextFormatFlags.WordBreak).Height;

                textBox.Bounds = new Rectangle(2, 2, 200, height);

                contentPanel.Controls.Add(textBox);

                this.Size = new Size(204, textBox.Bottom + 5);
            }
        }

        private void Clear() {
            foreach(Control c in contentPanel.Controls) {
                c.Dispose();
            }
            contentPanel.Controls.Clear();
        }
    }
}