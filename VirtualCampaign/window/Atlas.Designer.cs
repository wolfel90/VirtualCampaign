namespace VirtualCampaign.window {
    partial class Atlas {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.AtlasContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newMarkerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveMarkerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMarkerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defineMarkerBoundsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearMarkerBoundaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AtlasContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // AtlasContextMenu
            // 
            this.AtlasContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMarkerToolStripMenuItem,
            this.moveMarkerToolStripMenuItem,
            this.deleteMarkerToolStripMenuItem,
            this.defineMarkerBoundsToolStripMenuItem,
            this.clearMarkerBoundaryToolStripMenuItem});
            this.AtlasContextMenu.Name = "AtlasContextMenu";
            this.AtlasContextMenu.Size = new System.Drawing.Size(196, 114);
            // 
            // newMarkerToolStripMenuItem
            // 
            this.newMarkerToolStripMenuItem.Name = "newMarkerToolStripMenuItem";
            this.newMarkerToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.newMarkerToolStripMenuItem.Text = "Create New Marker";
            this.newMarkerToolStripMenuItem.Click += new System.EventHandler(this.newMarkerToolStripMenuItem_Click);
            // 
            // moveMarkerToolStripMenuItem
            // 
            this.moveMarkerToolStripMenuItem.Name = "moveMarkerToolStripMenuItem";
            this.moveMarkerToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.moveMarkerToolStripMenuItem.Text = "Move Marker";
            this.moveMarkerToolStripMenuItem.Click += new System.EventHandler(this.moveMarkerToolStripMenuItem_Click);
            // 
            // deleteMarkerToolStripMenuItem
            // 
            this.deleteMarkerToolStripMenuItem.Name = "deleteMarkerToolStripMenuItem";
            this.deleteMarkerToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.deleteMarkerToolStripMenuItem.Text = "Delete Marker";
            this.deleteMarkerToolStripMenuItem.Click += new System.EventHandler(this.deleteMarkerToolStripMenuItem_Click);
            // 
            // defineMarkerBoundsToolStripMenuItem
            // 
            this.defineMarkerBoundsToolStripMenuItem.Name = "defineMarkerBoundsToolStripMenuItem";
            this.defineMarkerBoundsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.defineMarkerBoundsToolStripMenuItem.Text = "Draw Marker Boundary";
            this.defineMarkerBoundsToolStripMenuItem.Click += new System.EventHandler(this.defineMarkerBoundsToolStripMenuItem_Click);
            // 
            // clearMarkerBoundaryToolStripMenuItem
            // 
            this.clearMarkerBoundaryToolStripMenuItem.Name = "clearMarkerBoundaryToolStripMenuItem";
            this.clearMarkerBoundaryToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.clearMarkerBoundaryToolStripMenuItem.Text = "Clear Marker Boundary";
            this.clearMarkerBoundaryToolStripMenuItem.Click += new System.EventHandler(this.clearMarkerBoundaryToolStripMenuItem_Click);
            // 
            // Atlas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ContextMenuStrip = this.AtlasContextMenu;
            this.DoubleBuffered = true;
            this.Name = "Atlas";
            this.Size = new System.Drawing.Size(780, 500);
            this.Load += new System.EventHandler(this.Atlas_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Atlas_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Atlas_KeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Atlas_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Atlas_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Atlas_MouseDown);
            this.MouseEnter += new System.EventHandler(this.Atlas_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.Atlas_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Atlas_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Atlas_MouseUp);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Atlas_MouseWheel);
            this.AtlasContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip AtlasContextMenu;
        private System.Windows.Forms.ToolStripMenuItem newMarkerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveMarkerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteMarkerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defineMarkerBoundsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearMarkerBoundaryToolStripMenuItem;
    }
}
