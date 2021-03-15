namespace VirtualCampaign.controls {
    partial class DiceRollerContextMenu {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            this.rollNormallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.rollWithAdvantageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rollWithDisadvantageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.rollWithHalfAdvantageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rollWithHalfDisadvantageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SuspendLayout();

            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rollNormallyToolStripMenuItem,
            this.toolStripMenuItem1,
            this.rollWithAdvantageToolStripMenuItem,
            this.rollWithDisadvantageToolStripMenuItem,
            this.toolStripMenuItem2,
            this.rollWithHalfAdvantageToolStripMenuItem,
            this.rollWithHalfDisadvantageToolStripMenuItem});
            this.Name = "rollOptionsMenu";
            this.Size = new System.Drawing.Size(211, 126);
            // 
            // rollNormallyToolStripMenuItem
            // 
            this.rollNormallyToolStripMenuItem.Name = "rollNormallyToolStripMenuItem";
            this.rollNormallyToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.rollNormallyToolStripMenuItem.Text = "Roll Normally";
            this.rollNormallyToolStripMenuItem.Click += new System.EventHandler(this.rollNormallyToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(207, 6);
            // 
            // rollWithAdvantageToolStripMenuItem
            // 
            this.rollWithAdvantageToolStripMenuItem.Name = "rollWithAdvantageToolStripMenuItem";
            this.rollWithAdvantageToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.rollWithAdvantageToolStripMenuItem.Text = "Roll w/ Advantage";
            this.rollWithAdvantageToolStripMenuItem.Click += new System.EventHandler(this.rollWithAdvantageToolStripMenuItem_Click);
            // 
            // rollWithDisadvantageToolStripMenuItem
            // 
            this.rollWithDisadvantageToolStripMenuItem.Name = "rollWithDisadvantageToolStripMenuItem";
            this.rollWithDisadvantageToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.rollWithDisadvantageToolStripMenuItem.Text = "Roll w/ Disadvantage";
            this.rollWithDisadvantageToolStripMenuItem.Click += new System.EventHandler(this.rollWithDisadvantageToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(207, 6);
            // 
            // rollWithHalfAdvantageToolStripMenuItem
            // 
            this.rollWithHalfAdvantageToolStripMenuItem.Name = "rollWithHalfAdvantageToolStripMenuItem";
            this.rollWithHalfAdvantageToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.rollWithHalfAdvantageToolStripMenuItem.Text = "Roll w/ Half Advantage";
            this.rollWithHalfAdvantageToolStripMenuItem.Click += new System.EventHandler(this.rollWithHalfAdvantageToolStripMenuItem_Click);
            // 
            // rollWithHalfDisadvantageToolStripMenuItem
            // 
            this.rollWithHalfDisadvantageToolStripMenuItem.Name = "rollWithHalfDisadvantageToolStripMenuItem";
            this.rollWithHalfDisadvantageToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.rollWithHalfDisadvantageToolStripMenuItem.Text = "Roll w/ Half Disadvantage";
            this.rollWithHalfDisadvantageToolStripMenuItem.Click += new System.EventHandler(this.rollWithHalfDisadvantageToolStripMenuItem_Click);
            
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem rollNormallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rollWithAdvantageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rollWithDisadvantageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem rollWithHalfAdvantageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rollWithHalfDisadvantageToolStripMenuItem;
    }
}
