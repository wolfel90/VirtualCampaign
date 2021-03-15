using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace VirtualCampaign.data {
    public class MipMap : IDisposable {
        public Bitmap FullScale;
        public Bitmap HalfScale;
        public Bitmap QuarterScale;
        public Bitmap EighthScale;

        public MipMap(Bitmap img) {
            FullScale = img;
            HalfScale = GenerateResizedImage(img, img.Width / 2, img.Height / 2);
            QuarterScale = GenerateResizedImage(img, img.Width / 4, img.Height / 4);
            EighthScale = GenerateResizedImage(img, img.Width / 8, img.Height / 8);
        }

        public Bitmap GetScaledBitmap(Rectangle size) {
            if(size.Width >= FullScale.Width) {
                return FullScale;
            } else if(size.Width >= HalfScale.Width) {
                return HalfScale;
            } else if(size.Width >= QuarterScale.Width) {
                return QuarterScale;
            } else {
                return EighthScale;
            }
        }

        private Bitmap GenerateResizedImage(Bitmap original, int width, int height) {
            Rectangle r = new Rectangle(0, 0, width, height);
            Bitmap ResultBMP = new Bitmap(width, height);

            ResultBMP.SetResolution(original.HorizontalResolution, original.VerticalResolution);

            using(Graphics g = Graphics.FromImage(ResultBMP)) {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using(var wrapMode = new ImageAttributes()) {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    g.DrawImage(original, r, 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return ResultBMP;
        }

        public void Dispose() {
            FullScale?.Dispose();
            HalfScale?.Dispose();
            QuarterScale?.Dispose();
            EighthScale?.Dispose();
        }
    }
}
