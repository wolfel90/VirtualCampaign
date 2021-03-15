namespace VirtualCampaign {
    partial class MainWindow {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.topMenu = new System.Windows.Forms.MenuStrip();
            this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sheetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.characterSheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newCharacterSheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadCharacterSheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.generateCharacterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.legacyCharacterSheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadClassicCharacterSheetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveCurrentSheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.articleBrowserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diceBagMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemBankMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.atlasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nameGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.magicalEffectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topTabMenu = new System.Windows.Forms.TabControl();
            this.versionLabel = new System.Windows.Forms.Label();
            this.trashButton = new System.Windows.Forms.Panel();
            this.bestiaryTemplateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.newBestiaryTemplateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // topMenu
            // 
            this.topMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userToolStripMenuItem,
            this.sheetsToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.serverToolStripMenuItem});
            this.topMenu.Location = new System.Drawing.Point(0, 0);
            this.topMenu.Name = "topMenu";
            this.topMenu.Size = new System.Drawing.Size(944, 24);
            this.topMenu.TabIndex = 1;
            this.topMenu.Text = "menuStrip1";
            // 
            // userToolStripMenuItem
            // 
            this.userToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.signInToolStripMenuItem});
            this.userToolStripMenuItem.Name = "userToolStripMenuItem";
            this.userToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.userToolStripMenuItem.Text = "User";
            // 
            // signInToolStripMenuItem
            // 
            this.signInToolStripMenuItem.Name = "signInToolStripMenuItem";
            this.signInToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.signInToolStripMenuItem.Text = "Sign In";
            this.signInToolStripMenuItem.Click += new System.EventHandler(this.signInToolStripMenuItem_Click);
            // 
            // sheetsToolStripMenuItem
            // 
            this.sheetsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.characterSheetToolStripMenuItem,
            this.legacyCharacterSheetToolStripMenuItem,
            this.toolStripMenuItem2,
            this.bestiaryTemplateMenuItem,
            this.toolStripMenuItem3,
            this.saveCurrentSheetToolStripMenuItem});
            this.sheetsToolStripMenuItem.Name = "sheetsToolStripMenuItem";
            this.sheetsToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.sheetsToolStripMenuItem.Text = "Sheets";
            // 
            // characterSheetToolStripMenuItem
            // 
            this.characterSheetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCharacterSheetToolStripMenuItem,
            this.loadCharacterSheetToolStripMenuItem,
            this.toolStripMenuItem1,
            this.generateCharacterToolStripMenuItem});
            this.characterSheetToolStripMenuItem.Name = "characterSheetToolStripMenuItem";
            this.characterSheetToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.characterSheetToolStripMenuItem.Text = "Character Sheet";
            // 
            // newCharacterSheetToolStripMenuItem
            // 
            this.newCharacterSheetToolStripMenuItem.Name = "newCharacterSheetToolStripMenuItem";
            this.newCharacterSheetToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newCharacterSheetToolStripMenuItem.Text = "New";
            this.newCharacterSheetToolStripMenuItem.Click += new System.EventHandler(this.newCharacterSheetToolStripMenuItem_Click);
            // 
            // loadCharacterSheetToolStripMenuItem
            // 
            this.loadCharacterSheetToolStripMenuItem.Name = "loadCharacterSheetToolStripMenuItem";
            this.loadCharacterSheetToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadCharacterSheetToolStripMenuItem.Text = "Load...";
            this.loadCharacterSheetToolStripMenuItem.Click += new System.EventHandler(this.loadCharacterSheetToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // generateCharacterToolStripMenuItem
            // 
            this.generateCharacterToolStripMenuItem.Name = "generateCharacterToolStripMenuItem";
            this.generateCharacterToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.generateCharacterToolStripMenuItem.Text = "Generate...";
            this.generateCharacterToolStripMenuItem.Click += new System.EventHandler(this.generateCharacterToolStripMenuItem_Click);
            // 
            // legacyCharacterSheetToolStripMenuItem
            // 
            this.legacyCharacterSheetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadClassicCharacterSheetMenuItem});
            this.legacyCharacterSheetToolStripMenuItem.Name = "legacyCharacterSheetToolStripMenuItem";
            this.legacyCharacterSheetToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.legacyCharacterSheetToolStripMenuItem.Text = "Legacy Character Sheet";
            // 
            // loadClassicCharacterSheetMenuItem
            // 
            this.loadClassicCharacterSheetMenuItem.Name = "loadClassicCharacterSheetMenuItem";
            this.loadClassicCharacterSheetMenuItem.Size = new System.Drawing.Size(109, 22);
            this.loadClassicCharacterSheetMenuItem.Text = "Load...";
            this.loadClassicCharacterSheetMenuItem.Click += new System.EventHandler(this.loadCharacterSheetToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(194, 6);
            // 
            // saveCurrentSheetToolStripMenuItem
            // 
            this.saveCurrentSheetToolStripMenuItem.Name = "saveCurrentSheetToolStripMenuItem";
            this.saveCurrentSheetToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.saveCurrentSheetToolStripMenuItem.Text = "Save Current Sheet";
            this.saveCurrentSheetToolStripMenuItem.Click += new System.EventHandler(this.saveCurrentSheetToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.articleBrowserMenuItem,
            this.diceBagMenuItem,
            this.itemBankMenuItem,
            this.atlasToolStripMenuItem,
            this.nameGeneratorToolStripMenuItem,
            this.magicalEffectsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // articleBrowserMenuItem
            // 
            this.articleBrowserMenuItem.Name = "articleBrowserMenuItem";
            this.articleBrowserMenuItem.Size = new System.Drawing.Size(161, 22);
            this.articleBrowserMenuItem.Text = "Article Browser";
            this.articleBrowserMenuItem.Click += new System.EventHandler(this.articleBrowserMenuItem_Click);
            // 
            // diceBagMenuItem
            // 
            this.diceBagMenuItem.Name = "diceBagMenuItem";
            this.diceBagMenuItem.Size = new System.Drawing.Size(161, 22);
            this.diceBagMenuItem.Text = "Dice Bag";
            this.diceBagMenuItem.Click += new System.EventHandler(this.diceBagMenuItem_Click);
            // 
            // itemBankMenuItem
            // 
            this.itemBankMenuItem.Name = "itemBankMenuItem";
            this.itemBankMenuItem.Size = new System.Drawing.Size(161, 22);
            this.itemBankMenuItem.Text = "Item Bank";
            this.itemBankMenuItem.Click += new System.EventHandler(this.itemBankMenuItem_Click);
            // 
            // atlasToolStripMenuItem
            // 
            this.atlasToolStripMenuItem.Name = "atlasToolStripMenuItem";
            this.atlasToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.atlasToolStripMenuItem.Text = "Atlas";
            this.atlasToolStripMenuItem.Click += new System.EventHandler(this.atlasToolStripMenuItem_Click);
            // 
            // nameGeneratorToolStripMenuItem
            // 
            this.nameGeneratorToolStripMenuItem.Name = "nameGeneratorToolStripMenuItem";
            this.nameGeneratorToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.nameGeneratorToolStripMenuItem.Text = "Name Generator";
            this.nameGeneratorToolStripMenuItem.Click += new System.EventHandler(this.nameGeneratorToolStripMenuItem_Click);
            // 
            // magicalEffectsToolStripMenuItem
            // 
            this.magicalEffectsToolStripMenuItem.Name = "magicalEffectsToolStripMenuItem";
            this.magicalEffectsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.magicalEffectsToolStripMenuItem.Text = "Magical Effects";
            this.magicalEffectsToolStripMenuItem.Click += new System.EventHandler(this.magicalEffectsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.versionHistoryToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // versionHistoryToolStripMenuItem
            // 
            this.versionHistoryToolStripMenuItem.Name = "versionHistoryToolStripMenuItem";
            this.versionHistoryToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.versionHistoryToolStripMenuItem.Text = "Version History";
            this.versionHistoryToolStripMenuItem.Click += new System.EventHandler(this.versionHistoryToolStripMenuItem_Click);
            // 
            // serverToolStripMenuItem
            // 
            this.serverToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startServerToolStripMenuItem,
            this.startClientToolStripMenuItem});
            this.serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            this.serverToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.serverToolStripMenuItem.Text = "Server";
            // 
            // startServerToolStripMenuItem
            // 
            this.startServerToolStripMenuItem.Name = "startServerToolStripMenuItem";
            this.startServerToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.startServerToolStripMenuItem.Text = "Start Server";
            this.startServerToolStripMenuItem.Click += new System.EventHandler(this.startServerToolStripMenuItem_Click);
            // 
            // startClientToolStripMenuItem
            // 
            this.startClientToolStripMenuItem.Name = "startClientToolStripMenuItem";
            this.startClientToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.startClientToolStripMenuItem.Text = "Start Client";
            this.startClientToolStripMenuItem.Click += new System.EventHandler(this.startClientToolStripMenuItem_Click);
            // 
            // topTabMenu
            // 
            this.topTabMenu.AllowDrop = true;
            this.topTabMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topTabMenu.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.topTabMenu.HotTrack = true;
            this.topTabMenu.ItemSize = new System.Drawing.Size(120, 18);
            this.topTabMenu.Location = new System.Drawing.Point(0, 24);
            this.topTabMenu.Name = "topTabMenu";
            this.topTabMenu.SelectedIndex = 0;
            this.topTabMenu.Size = new System.Drawing.Size(944, 677);
            this.topTabMenu.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.topTabMenu.TabIndex = 0;
            this.topTabMenu.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.topTabMenu_DrawItem);
            this.topTabMenu.MouseClick += new System.Windows.Forms.MouseEventHandler(this.topTabMenu_MouseClick);
            // 
            // versionLabel
            // 
            this.versionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.versionLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.versionLabel.Location = new System.Drawing.Point(832, 5);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(100, 18);
            this.versionLabel.TabIndex = 2;
            this.versionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // trashButton
            // 
            this.trashButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trashButton.BackgroundImage = global::VirtualCampaign.Properties.Resources.trash;
            this.trashButton.Location = new System.Drawing.Point(798, 2);
            this.trashButton.Margin = new System.Windows.Forms.Padding(0);
            this.trashButton.Name = "trashButton";
            this.trashButton.Size = new System.Drawing.Size(22, 22);
            this.trashButton.TabIndex = 3;
            this.trashButton.Visible = false;
            this.trashButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.trashButton_MouseClick);
            // 
            // bestiaryTemplateMenuItem
            // 
            this.bestiaryTemplateMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newBestiaryTemplateMenuItem});
            this.bestiaryTemplateMenuItem.Name = "bestiaryTemplateMenuItem";
            this.bestiaryTemplateMenuItem.Size = new System.Drawing.Size(197, 22);
            this.bestiaryTemplateMenuItem.Text = "Bestiary Template";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(194, 6);
            // 
            // newBestiaryTemplateMenuItem
            // 
            this.newBestiaryTemplateMenuItem.Name = "newBestiaryTemplateMenuItem";
            this.newBestiaryTemplateMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newBestiaryTemplateMenuItem.Text = "New";
            this.newBestiaryTemplateMenuItem.Click += new System.EventHandler(this.newBestiaryTemplateMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 701);
            this.Controls.Add(this.trashButton);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.topTabMenu);
            this.Controls.Add(this.topMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.topMenu;
            this.Name = "MainWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Virtual Campaign";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseMove);
            this.topMenu.ResumeLayout(false);
            this.topMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip topMenu;
        private System.Windows.Forms.ToolStripMenuItem userToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem articleBrowserMenuItem;
        private System.Windows.Forms.TabControl topTabMenu;
        private System.Windows.Forms.ToolStripMenuItem signInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sheetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem characterSheetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newCharacterSheetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadCharacterSheetToolStripMenuItem;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.ToolStripMenuItem diceBagMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem versionHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem legacyCharacterSheetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadClassicCharacterSheetMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemBankMenuItem;
        private System.Windows.Forms.Panel trashButton;
        private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem atlasToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem generateCharacterToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem saveCurrentSheetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem magicalEffectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nameGeneratorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bestiaryTemplateMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newBestiaryTemplateMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
    }
}

