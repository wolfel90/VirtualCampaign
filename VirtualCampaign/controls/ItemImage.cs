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

namespace VirtualCampaign.controls {
    public partial class ItemImage : UserControl {
        private static Font footFont;

        private ImageAttributes iconAtt;
        private ImageAttributes bgAtt;
        private ColorMatrix iconMatrix;
        private ColorMatrix bgMatrix;
        private Color _iconColor;
        private Color _bgColor;
        public Color iconColor { get { return _iconColor; } set { setIconColor(value); } }
        public Color bgColor { get { return _bgColor; } set { setBGColor(value); } }
        private string _iconSrc;
        private string _bgSrc;
        public string iconSrc { get { return _iconSrc; } set { setIconSrc(value); } }
        public string bgSrc { get { return _bgSrc; } set { setBGSrc(value); } }
        private string _footStr;
        public string footStr { get { return _footStr; } set { _footStr = value; Invalidate(); } }

        static ItemImage() {
            footFont = new Font("Verdana", 10f, FontStyle.Bold);
        }

        public ItemImage() {
            InitializeComponent();

            iconAtt = new ImageAttributes();
            bgAtt = new ImageAttributes();

            iconMatrix = new ColorMatrix(new float[][] {
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            });
            bgMatrix = new ColorMatrix(new float[][] {
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            });
            iconSrc = "";
            bgSrc = "back01";
            _iconColor = Color.Black;
            _bgColor = Color.Black;
            _footStr = "";

            iconAtt.SetColorMatrix(iconMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            bgAtt.SetColorMatrix(bgMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        }

        public void setIconColor(int argb) {
            setIconColor(Color.FromArgb(argb));
        }

        public void setIconColor(Color color) {
            iconMatrix = new ColorMatrix(new float[][] {
                new float[] {1 + ((float)color.R / 255f), 0, 0, 0, 0},
                new float[] {0, 1 + ((float)color.G / 255f), 0, 0, 0},
                new float[] {0, 0, 1 + ((float)color.B / 255f), 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            });
            iconAtt.SetColorMatrix(iconMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            _iconColor = color;
        }

        public void setBGColor(int argb) {
            setBGColor(Color.FromArgb(argb));
        }

        public void setBGColor(Color color) {
            bgMatrix = new ColorMatrix(new float[][] {
                new float[] {1 + ((float)color.R / 255f), 0, 0, 0, 0},
                new float[] {0, 1 + ((float)color.G / 255f), 0, 0, 0},
                new float[] {0, 0, 1 + ((float)color.B / 255f), 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            });
            bgAtt.SetColorMatrix(bgMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            _bgColor = color;
        }

        public void setIconSrc(string src) {
            _iconSrc = src;
            Invalidate();
        }

        public void setBGSrc(string src) {
            _bgSrc = src;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            
            if(!string.IsNullOrWhiteSpace(bgSrc)) {
                object bg = Properties.ItemIcons.ResourceManager.GetObject(bgSrc);
                if (bg != null) {
                    g.DrawImage((Bitmap)bg, new Rectangle(0, 0, this.Width - 2, this.Height - 2), 0, 0, ((Bitmap)bg).Width, ((Bitmap)bg).Height, GraphicsUnit.Pixel, bgAtt);
                } else {
                    //g.FillRectangle(new SolidBrush(_bgColor), new Rectangle(0, 0, this.Width, this.Height));
                }
            }
            
            if(!string.IsNullOrWhiteSpace(iconSrc)) {
                object ico = Properties.ItemIcons.ResourceManager.GetObject(iconSrc);
                if (ico != null) {
                    g.DrawImage((Bitmap)ico, new Rectangle(0, 0, this.Width - 2, this.Height - 2), 0, 0, ((Bitmap)ico).Width, ((Bitmap)ico).Width, GraphicsUnit.Pixel, iconAtt);
                }
            }

            if(!string.IsNullOrWhiteSpace(_footStr)) {
                SizeF s = e.Graphics.MeasureString(_footStr, footFont);
                g.DrawString(_footStr, footFont, Brushes.White, new PointF(this.Width - s.Width - 1, this.Height - s.Height));
            }
        }
    }
}
