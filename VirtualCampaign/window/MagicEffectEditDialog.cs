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

namespace VirtualCampaign.window {
    public partial class MagicEffectEditDialog : Form {
        public MagicEffect startEffect, resultEffect;
        private bool ready;

        public MagicEffectEditDialog() : this(null) { }

        public MagicEffectEditDialog(MagicEffect effect) {
            InitializeComponent();
            ready = false;

            startEffect = effect;

            if(effect != null) {
                if (effect.netID > 0) idValueLabel.Text = effect.netID.ToString();
                nameField.Text = effect.title;
                schoolField.Text = effect.School;
                raritySpinner.Value = effect.Rarity;
                prefixField.Text = effect.Prefix;
                suffixField.Text = effect.Suffix;
                modsField.Text = effect.GenerateModsString();
                effectField.Text = effect.Effect;
                switch(effect.Type) {
                    case MagicEffect.EffectType.Continuous_Effect:
                        typeCombo.SelectedItem = "Continuous (Generic)";
                        break;
                    case MagicEffect.EffectType.Triggered_Armor_Effect:
                        typeCombo.SelectedItem = "Triggered (Armor)";
                        break;
                    case MagicEffect.EffectType.Continuous_Armor_Effect:
                        typeCombo.SelectedItem = "Continuous (Armor)";
                        break;
                    case MagicEffect.EffectType.Triggered_Weapon_Effect:
                        typeCombo.SelectedItem = "Triggered (Weapon)";
                        break;
                    case MagicEffect.EffectType.Continuous_Weapon_Effect:
                        typeCombo.SelectedItem = "Continuous (Weapon)";
                        break;
                    case MagicEffect.EffectType.Triggered_Effect:
                    default:
                        typeCombo.SelectedItem = "Triggered (Generic)";
                        break;
                }
                switch(effect.Quality) {
                    case MagicEffect.EffectQuality.Negative:
                        qualityCombo.SelectedItem = "Negative";
                        break;
                    case MagicEffect.EffectQuality.Positive:
                        qualityCombo.SelectedItem = "Positive";
                        break;
                    case MagicEffect.EffectQuality.Neutral:
                    default:
                        qualityCombo.SelectedItem = "Neutral";
                        break;
                }
            } else {
                idValueLabel.Text = "Custom";
                typeCombo.SelectedItem = "Triggered (Generic)";
                qualityCombo.SelectedItem = "Neutral";
            }
            resultEffect = null;
            ready = true;
        }

        private void okButton_Click(object sender, EventArgs e) {
            resultEffect = new MagicEffect();
            resultEffect.title = nameField.Text;
            resultEffect.School = schoolField.Text;
            resultEffect.Rarity = (int)raritySpinner.Value;
            resultEffect.Prefix = prefixField.Text;
            resultEffect.Suffix = suffixField.Text;
            resultEffect.SetModifiers(modsField.Text);
            resultEffect.Effect = effectField.Text;
            switch(typeCombo.SelectedItem.ToString()) {
                case "Triggered (Generic)":
                    resultEffect.Type = MagicEffect.EffectType.Triggered_Effect;
                    break;
                case "Continuous (Generic)":
                    resultEffect.Type = MagicEffect.EffectType.Continuous_Effect;
                    break;
                case "Triggered (Armor)":
                    resultEffect.Type = MagicEffect.EffectType.Triggered_Armor_Effect;
                    break;
                case "Continuous (Armor)":
                    resultEffect.Type = MagicEffect.EffectType.Continuous_Armor_Effect;
                    break;
                case "Triggered (Weapon)":
                    resultEffect.Type = MagicEffect.EffectType.Triggered_Weapon_Effect;
                    break;
                case "Continuous (Weapon)":
                    resultEffect.Type = MagicEffect.EffectType.Continuous_Weapon_Effect;
                    break;
            }
            switch(qualityCombo.SelectedItem.ToString()) {
                case "Neutral":
                    resultEffect.Quality = MagicEffect.EffectQuality.Neutral;
                    break;
                case "Negative":
                    resultEffect.Quality = MagicEffect.EffectQuality.Negative;
                    break;
                case "Positive":
                    resultEffect.Quality = MagicEffect.EffectQuality.Positive;
                    break;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void value_Changed(object sender, EventArgs e) {
            if(ready && idValueLabel.Text != "Custom") idValueLabel.Text = "Custom";
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            resultEffect = startEffect;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
