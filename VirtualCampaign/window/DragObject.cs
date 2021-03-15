using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualCampaign.window {
    public partial class DragObject : Form {
        private Control visualControl;

        public DragObject() {
            InitializeComponent();
            Size = new Size(1, 1);
            visualControl = null;
        }

        public void setVisualControl(Control c) {
            if(visualControl != null) {
                Controls.Remove(visualControl);
                visualControl.Dispose();
            }
            visualControl = c;
            if(visualControl != null) {
                Controls.Add(visualControl);
                Size = new Size(visualControl.Size.Width, visualControl.Size.Height);
                Console.Out.WriteLine(Size);
            }
        }
    }
}
