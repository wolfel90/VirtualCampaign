using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualCampaign.controls {
    public partial class TraitTooltip : ToolTip {
        public static Font titleFont { get; set; }
        public static Font descriptionFont { get; set; }
        public string title { get; set; }
        public string description { get; set; }

        static TraitTooltip() {
            titleFont = new Font("Arial", 8);
            descriptionFont = new Font("Arial", 8);
        }

        public TraitTooltip() {
            InitializeComponent();
            this.OwnerDraw = true;
            this.Popup += new PopupEventHandler(this.OnPopup);
            this.Draw += new DrawToolTipEventHandler(this.OnDraw);
            title = "Trait";
            description = "Description";
        }

        private void OnPopup(object sender, PopupEventArgs e) {
            e.ToolTipSize = new Size(200, 60);
        }

        private void OnDraw(object sender, DrawToolTipEventArgs e) {
            Graphics g = e.Graphics;

            g.FillRectangle(Brushes.Beige, e.Bounds);
            g.DrawRectangle(new Pen(Brushes.Black, 1), new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1));
            g.DrawString(title, titleFont, Brushes.Black, new PointF(e.Bounds.X + 5, e.Bounds.Y + 5));
            g.DrawString(description, descriptionFont, Brushes.Black, new PointF(e.Bounds.X + 5, e.Bounds.Y + 20));
        }
    }
}
