using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualCampaign.controls;

namespace VirtualCampaign.window {
    public partial class ItemImageEditor : Form {
        private Dictionary<PictureBox, string> imgLookup;
        public ItemImage resultImage { get { return itemImage; } }

        public ItemImageEditor() : this(null) {}

        public ItemImageEditor(ItemImage source) {
            InitializeComponent();
            imgLookup = new Dictionary<PictureBox, string>();

            using(StringReader reader = new StringReader(VirtualCampaign.Properties.ItemIcons.bg_options)) {
                string line;
                int mX = 0, mY = 0;
                while((line = reader.ReadLine()) != null) {
                    object img = VirtualCampaign.Properties.ItemIcons.ResourceManager.GetObject(line);
                    if (img != null) {
                        PictureBox box = new PictureBox();
                        box.Image = (Bitmap)img;
                        bgOptionsPanel.Controls.Add(box);
                        imgLookup.Add(box, line);
                        box.Bounds = new Rectangle(mX * 64, mY * 64, 64, 64);
                        box.Click += BGOption_Click;
                        mX++;
                    }
                }
            }

            using (StringReader reader = new StringReader(VirtualCampaign.Properties.ItemIcons.icon_options)) {
                string line;
                int mX = 0, mY = 0;
                while ((line = reader.ReadLine()) != null) {
                    object img = VirtualCampaign.Properties.ItemIcons.ResourceManager.GetObject(line);
                    if (img != null) {
                        PictureBox box = new PictureBox();
                        box.Image = (Bitmap)img;
                        iconOptionsPanel.Controls.Add(box);
                        imgLookup.Add(box, line);
                        box.Bounds = new Rectangle(mX * 64, mY * 64, 64, 64);
                        box.Click += IconOption_Click;
                        mX++;
                    }
                }

            }

            bgOptionsPanel.VerticalScroll.LargeChange = 10;
            iconOptionsPanel.VerticalScroll.SmallChange = 1;

            bgRSlider.Value = source.bgColor.R;
            bgGSlider.Value = source.bgColor.G;
            bgBSlider.Value = source.bgColor.B;
            bgRValueLabel.Text = source.bgColor.R.ToString();
            bgGValueLabel.Text = source.bgColor.G.ToString();
            bgBValueLabel.Text = source.bgColor.B.ToString();

            iconRSlider.Value = source.iconColor.R;
            iconGSlider.Value = source.iconColor.G;
            iconBSlider.Value = source.iconColor.B;
            iconRValueLabel.Text = source.iconColor.R.ToString();
            iconGValueLabel.Text = source.iconColor.G.ToString();
            iconBValueLabel.Text = source.iconColor.B.ToString();

            if (source != null) {
                itemImage.bgSrc = source.bgSrc;
                itemImage.bgColor = source.bgColor;
                itemImage.iconSrc = source.iconSrc;
                itemImage.iconColor = source.iconColor;
            } else {
                itemImage.bgSrc = "back01";
                itemImage.iconSrc = "cuirass01";
            }
        }

        private void okButton_Click(object sender, EventArgs e) {
            if(imageOptionsPanel.Visible) {
                imageOptionsPanel.Visible = false;
            } else {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void bgRSlider_Scroll(object sender, EventArgs e) {
            bgRValueLabel.Text = bgRSlider.Value.ToString();
            itemImage.bgColor = Color.FromArgb(bgRSlider.Value, bgGSlider.Value, bgBSlider.Value);
            itemImage.Invalidate();
        }

        private void bgGSlider_Scroll(object sender, EventArgs e) {
            bgGValueLabel.Text = bgGSlider.Value.ToString();
            itemImage.bgColor = Color.FromArgb(bgRSlider.Value, bgGSlider.Value, bgBSlider.Value);
            itemImage.Invalidate();
        }

        private void bgBSlider_Scroll(object sender, EventArgs e) {
            bgBValueLabel.Text = bgBSlider.Value.ToString();
            itemImage.bgColor = Color.FromArgb(bgRSlider.Value, bgGSlider.Value, bgBSlider.Value);
            itemImage.Invalidate();
        }

        private void iconRSlider_Scroll(object sender, EventArgs e) {
            iconRValueLabel.Text = iconRSlider.Value.ToString();
            itemImage.iconColor = Color.FromArgb(iconRSlider.Value, iconGSlider.Value, iconBSlider.Value);
            itemImage.Invalidate();
        }

        private void iconGSlider_Scroll(object sender, EventArgs e) {
            iconGValueLabel.Text = iconGSlider.Value.ToString();
            itemImage.iconColor = Color.FromArgb(iconRSlider.Value, iconGSlider.Value, iconBSlider.Value);
            itemImage.Invalidate();
        }

        private void iconBSlider_Scroll(object sender, EventArgs e) {
            iconBValueLabel.Text = iconBSlider.Value.ToString();
            itemImage.iconColor = Color.FromArgb(iconRSlider.Value, iconGSlider.Value, iconBSlider.Value);
            itemImage.Invalidate();
        }

        private void itemImage_Click(object sender, EventArgs e) {
            if(imageOptionsPanel.Visible) {
                imageOptionsPanel.Visible = false;
            } else {
                imageOptionsPanel.Bounds = new Rectangle(15, 75, 700, 175);
                imageOptionsPanel.BringToFront();
                imageOptionsPanel.Visible = true;
            }
        }

        private void ItemImageEditor_Click(object sender, EventArgs e) {
            imageOptionsPanel.Visible = false;
        }

        private void BGOption_Click(object sender, EventArgs e) {
            if(sender is PictureBox) {
                if(imgLookup.ContainsKey((PictureBox)sender)) {
                    itemImage.bgSrc = (imgLookup[(PictureBox)sender]);
                }
            }
        }

        private void IconOption_Click(object sender, EventArgs e) {
            if (sender is PictureBox) {
                if (imgLookup.ContainsKey((PictureBox)sender)) {
                    itemImage.iconSrc = (imgLookup[(PictureBox)sender]);
                }
            }
        }
    }
}
