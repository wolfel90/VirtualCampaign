using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualCampaign.controls {
    public class AtlasElement : IDisposable {
        public const int TRACK_CORNER = 0, TRACK_CENTER = 1;
        private RectangleF _Bounds;
        private PointF _MapPoint;
        private Control _ElementControl;
        public Control ElementControl { get { return _ElementControl; } set { SetElementControl(value); } }
        public RectangleF Bounds { get { return _Bounds; } set { _Bounds = value; } }
        public PointF Location { get { return _Bounds.Location; } set { _Bounds.X = value.X; _Bounds.Y = value.Y; } }
        public PointF MapPoint { get { return _MapPoint; } set { _MapPoint = value; } }
        public float Z { get; set; }
        public SizeF Size { get { return _Bounds.Size; } set { _Bounds.Width = value.Width; _Bounds.Height = value.Height; } }
        public float Left { get { return _Bounds.X; } }
        public float Right { get { return _Bounds.X + _Bounds.Width; } }
        public float Top { get { return _Bounds.Y; } }
        public float Bottom { get { return _Bounds.Y + _Bounds.Height; } }
        public float Width { get { return _Bounds.Width; } }
        public float Height { get { return _Bounds.Height; } }
        public long NetID;
        public PointF Center { get { return new PointF(_Bounds.X + (_Bounds.Width / 2), _Bounds.Y + (_Bounds.Height / 2)); } }
        public bool Scales;
        public int TrackMode;
        public bool Visible;
        public long creator;
        public bool useWhitelist { get; set; }
        public string whitelistString { get; set; }

        public AtlasElement() {
            ElementControl = null;
            Bounds = new RectangleF(0, 0, 0, 0);
            MapPoint = new PointF();
            Z = 0;
            Scales = true;
            TrackMode = TRACK_CENTER;
            NetID = -1;
            Visible = true;
            creator = -1;
            useWhitelist = false;
            whitelistString = "";
        }

        public void SetElementControl(Control control) {
            _ElementControl = control;
        }

        public virtual void PaintElement(Graphics g, Rectangle r) {
            SolidBrush b = new SolidBrush(Color.White);
            g.FillRectangle(b, r);
        }

        public virtual void Dispose() {
            _ElementControl?.Dispose();
        }
    }
}
