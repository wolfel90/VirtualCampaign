namespace VirtualCampaign.controls {
    partial class AbilitySlot {
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
            this.itemMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editAbilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAbilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // itemMenu
            // 
            this.itemMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editAbilityToolStripMenuItem,
            this.removeAbilityToolStripMenuItem});
            this.itemMenu.Name = "itemMenu";
            this.itemMenu.Size = new System.Drawing.Size(155, 48);
            // 
            // editAbilityToolStripMenuItem
            // 
            this.editAbilityToolStripMenuItem.Name = "editAbilityToolStripMenuItem";
            this.editAbilityToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.editAbilityToolStripMenuItem.Text = "Edit Ability...";
            this.editAbilityToolStripMenuItem.Click += new System.EventHandler(this.editAbilityToolStripMenuItem_Click);
            // 
            // removeAbilityToolStripMenuItem
            // 
            this.removeAbilityToolStripMenuItem.Name = "removeAbilityToolStripMenuItem";
            this.removeAbilityToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.removeAbilityToolStripMenuItem.Text = "Remove Ability";
            this.removeAbilityToolStripMenuItem.Click += new System.EventHandler(this.removeAbilityToolStripMenuItem_Click);
            // 
            // AbilitySlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ContextMenuStrip = this.itemMenu;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AbilitySlot";
            this.Size = new System.Drawing.Size(70, 70);
            this.Click += new System.EventHandler(this.AbilitySlot_Click);
            this.MouseEnter += new System.EventHandler(this.AbilitySlot_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.AbilitySlot_MouseLeave);
            this.itemMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip itemMenu;
        private System.Windows.Forms.ToolStripMenuItem editAbilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAbilityToolStripMenuItem;
    }
}
