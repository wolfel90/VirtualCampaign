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
    public partial class DeleteMarkerDialog : Form {
        public static int CANCEL = 0, DELETE_MARK = 1, DELETE_MARK_AND_ARTICLE = 2;
        public int Result;

        public DeleteMarkerDialog() {
            InitializeComponent();
            Result = CANCEL;
        }

        private void deleteMarkerButton_Click(object sender, EventArgs e) {
            DialogResult = (DialogResult)DELETE_MARK;
            this.Close();
        }

        private void deleteAllButton_Click(object sender, EventArgs e) {
            DialogResult = (DialogResult)DELETE_MARK_AND_ARTICLE;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            DialogResult = (DialogResult)CANCEL;
            this.Close();
        }
    }
}
