using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;
using System.IO;
using VirtualCampaign.data;

namespace VirtualCampaign.controls {
    public class AtlasMap : AtlasElement {
        private string _src;
        public string ImgSrc { get { return _src; } set { SetMapImgSrc(value); } }
        public float scale;
        private MipMap mapImg;

        public AtlasMap() {
            mapImg = null;
            TrackMode = TRACK_CORNER;
            Scales = true;
            scale = 1;
        }

        public void SetMapImgSrc(string src) {
            _src = src;
            using (WebClient client = new WebClient()) {
                byte[] data = client.DownloadData(src);
                using(MemoryStream mem = new MemoryStream(data)) {
                    Bitmap img = (Bitmap)Bitmap.FromStream(mem);
                    mapImg = new MipMap(img);
                    this.Size = new SizeF(img.Width, img.Height);
                }
            }
        }

        public override void PaintElement(Graphics g, Rectangle r) {
            if(mapImg != null) {
                g.DrawImage(mapImg.GetScaledBitmap(r), r);
            } else {
                g.FillRectangle(Brushes.Gray, r);
            }
        }

        public override void Dispose() {
            mapImg?.Dispose();
            base.Dispose();
        }
    }
}
