namespace VirtualCampaign.controls {
    partial class KeynoteGrid {
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
            this.components = new System.ComponentModel.Container();
            this.keynoteMenuStip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createKeynoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteKeynoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.createBranchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameBranchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteBranchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keynoteMenuStip.SuspendLayout();
            this.SuspendLayout();
            // 
            // keynoteMenuStip
            // 
            this.keynoteMenuStip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createKeynoteToolStripMenuItem,
            this.deleteKeynoteToolStripMenuItem,
            this.toolStripMenuItem1,
            this.createBranchToolStripMenuItem,
            this.renameBranchToolStripMenuItem,
            this.deleteBranchToolStripMenuItem});
            this.keynoteMenuStip.Name = "keynoteMenuStip";
            this.keynoteMenuStip.Size = new System.Drawing.Size(158, 120);
            // 
            // createKeynoteToolStripMenuItem
            // 
            this.createKeynoteToolStripMenuItem.Name = "createKeynoteToolStripMenuItem";
            this.createKeynoteToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.createKeynoteToolStripMenuItem.Text = "Create Keynote";
            this.createKeynoteToolStripMenuItem.Click += new System.EventHandler(this.createKeynoteToolStripMenuItem_Click);
            // 
            // deleteKeynoteToolStripMenuItem
            // 
            this.deleteKeynoteToolStripMenuItem.Name = "deleteKeynoteToolStripMenuItem";
            this.deleteKeynoteToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.deleteKeynoteToolStripMenuItem.Text = "Delete Keynote";
            this.deleteKeynoteToolStripMenuItem.Click += new System.EventHandler(this.deleteKeynoteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(154, 6);
            // 
            // createBranchToolStripMenuItem
            // 
            this.createBranchToolStripMenuItem.Name = "createBranchToolStripMenuItem";
            this.createBranchToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.createBranchToolStripMenuItem.Text = "Create Branch";
            this.createBranchToolStripMenuItem.Click += new System.EventHandler(this.createBranchToolStripMenuItem_Click);
            // 
            // renameBranchToolStripMenuItem
            // 
            this.renameBranchToolStripMenuItem.Name = "renameBranchToolStripMenuItem";
            this.renameBranchToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.renameBranchToolStripMenuItem.Text = "Rename Branch";
            this.renameBranchToolStripMenuItem.Click += new System.EventHandler(this.renameBranchToolStripMenuItem_Click);
            // 
            // deleteBranchToolStripMenuItem
            // 
            this.deleteBranchToolStripMenuItem.Name = "deleteBranchToolStripMenuItem";
            this.deleteBranchToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.deleteBranchToolStripMenuItem.Text = "Delete Branch";
            this.deleteBranchToolStripMenuItem.Click += new System.EventHandler(this.deleteBranchToolStripMenuItem_Click);
            // 
            // KeynotePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContextMenuStrip = this.keynoteMenuStip;
            this.Name = "KeynotePanel";
            this.Size = new System.Drawing.Size(100, 25);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeynotePanel_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.KeynotePanel_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.KeynotePanel_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.KeynotePanel_MouseUp);
            this.keynoteMenuStip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip keynoteMenuStip;
        private System.Windows.Forms.ToolStripMenuItem createBranchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createKeynoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteKeynoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteBranchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameBranchToolStripMenuItem;
    }
}
