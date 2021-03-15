using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace VCLauncher {
    public partial class SplashPage : Form {
        public delegate void CallDelegate(string text);
        private bool mouseDown = false;
        private Point lastPos = new Point();
        
        public SplashPage() {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void SplashPage_MouseDown(object sender, MouseEventArgs e) {
            mouseDown = true;
            lastPos = e.Location;
        }

        private void SplashPage_MouseMove(object sender, MouseEventArgs e) {
            if(mouseDown) {
                this.Location = new Point((this.Location.X - lastPos.X) + e.X, (this.Location.Y - lastPos.Y) + e.Y);
                this.Update();
            }
        }

        private void SplashPage_MouseUp(object sender, MouseEventArgs e) {
            mouseDown = false;
        }

        private void SplashPage_Activated(object sender, EventArgs e) {
            LoaderThread lt = new LoaderThread();
            lt.callbackLabel = infoLabel;
            Thread thread = new Thread(new ThreadStart(lt.doVersionCheck));
            thread.Start();
        }
    }
}
