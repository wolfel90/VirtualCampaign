using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualCampaign.data;
using VirtualCampaign.sys;
using VirtualCampaign.window;

namespace VirtualCampaign.controls {
    public partial class AbilitySlot : UserControl {
        public event EventHandler ValueChanged;
        public event EventHandler AbilityClicked;
        private ImageAttributes imgAtt;
        private ColorMatrix imgMatrix;
        private Ability _ability;
        public Ability ability { get { return _ability; } set { setAbility(value); } }
        public bool refreshing { get; set; }
        public bool adaptableTooltips;

        public AbilitySlot() {
            InitializeComponent();
            refreshing = false;
            adaptableTooltips = false;

            imgAtt = new ImageAttributes();
            imgMatrix = new ColorMatrix(new float[][] {
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            });
            imgAtt.SetColorMatrix(imgMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        }

        public void setAbility(Ability ab) {
            _ability = ab;
            //abilityImage.ImageLocation = "https://s3.amazonaws.com/cdn-origin-etr.akc.org/wp-content/uploads/2017/11/12231413/Labrador-Retriever-MP.jpg";
            Invalidate();
            if (!refreshing) OnChanged(EventArgs.Empty);
        }

        protected virtual void OnChanged(EventArgs e) {
            ValueChanged?.Invoke(this, e);
        }

        private void AbilitySlot_Click(object sender, EventArgs e) {
            AbilityClicked?.Invoke(this, e);
        }

        private void editAbilityToolStripMenuItem_Click(object sender, EventArgs e) {
            using (var abilityLoadMenu = new TTALoader(TTALoader.ABILITY_TYPE, TTALoader.SINGLE_SELECT)) {
                abilityLoadMenu.ReloadList();
                var result = abilityLoadMenu.ShowDialog();
                if (result == DialogResult.OK) {
                    if (abilityLoadMenu.resultAbilities != null) {
                        if (abilityLoadMenu.resultAbilities.Length > 0) {
                            setAbility(abilityLoadMenu.resultAbilities[0]);
                        }
                    }
                }
            }
        }

        private void removeAbilityToolStripMenuItem_Click(object sender, EventArgs e) {
            AbilityGrid p;
            if(this.Parent is AbilityGrid) {
                p = (AbilityGrid)this.Parent;
                p.removeAbilityAt(p.IndexOf(this));
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            if (ability != null) {
                object img;
                if(string.IsNullOrWhiteSpace(ability.imgSrc)) {
                    img = VirtualCampaign.Properties.Resources.abilityDefault;
                } else {
                    img = Properties.AbilityIcons.ResourceManager.GetObject(ability.imgSrc);
                    if(img == null) {
                        Console.Out.WriteLine(ability.imgSrc);
                        img = VirtualCampaign.Properties.Resources.abilityDefault;
                    }
                }
                if(img != null) {
                    g.DrawImage((Bitmap)img, new Rectangle(0, 0, this.Width - 4, this.Height - 4), 0, 0, ((Bitmap)img).Width, ((Bitmap)img).Height, GraphicsUnit.Pixel, imgAtt);
                }
            }
        }

        private void AbilitySlot_MouseEnter(object sender, EventArgs e) {
            if(adaptableTooltips) {
                if (ability != null) {
                    AdaptableTooltipHandler.CurrentControl = this;
                    AdaptableTooltipHandler.Tooltip.generateTooltip(ability);
                    AdaptableTooltipHandler.Tooltip.Show();
                    AdaptableTooltipHandler.Tooltip.Location = this.PointToScreen(new Point(0 + this.Width + 5, 0));
                }
            }
        }

        private void AbilitySlot_MouseLeave(object sender, EventArgs e) {
            if(AdaptableTooltipHandler.CurrentControl == this) {
                AdaptableTooltipHandler.CurrentControl = null;
                AdaptableTooltipHandler.Tooltip.Hide();
            }
        }
    }
}
