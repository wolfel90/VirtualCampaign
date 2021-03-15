namespace VirtualCampaign.window {
    partial class ArticleBrowser {
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
            this.articleTree = new System.Windows.Forms.TreeView();
            this.contentTextEditor = new System.Windows.Forms.RichTextBox();
            this.editButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.newButton = new System.Windows.Forms.Button();
            this.newButtonMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newArticleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newAbilityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newSchoolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTalentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTraitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.atlasPinButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.creditLabel = new System.Windows.Forms.Label();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.contentTextBox = new System.Windows.Forms.WebBrowser();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchLabel = new System.Windows.Forms.Label();
            this.toolPanel = new System.Windows.Forms.Panel();
            this.detailsPanel = new System.Windows.Forms.Panel();
            this.propertiesLabel = new System.Windows.Forms.Label();
            this.propertyGridView = new System.Windows.Forms.DataGridView();
            this.propertyNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyValueCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.whitelistButton = new System.Windows.Forms.Button();
            this.catsLabel = new System.Windows.Forms.Label();
            this.catsList = new System.Windows.Forms.CheckedListBox();
            this.titlePanel = new System.Windows.Forms.Panel();
            this.forwardButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.titleField = new System.Windows.Forms.TextBox();
            this.newButtonMenuStrip.SuspendLayout();
            this.tablePanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.toolPanel.SuspendLayout();
            this.detailsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridView)).BeginInit();
            this.titlePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // articleTree
            // 
            this.articleTree.Location = new System.Drawing.Point(3, 3);
            this.articleTree.Name = "articleTree";
            this.articleTree.Size = new System.Drawing.Size(239, 133);
            this.articleTree.TabIndex = 0;
            this.articleTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.articleTree_AfterSelect);
            // 
            // contentTextEditor
            // 
            this.contentTextEditor.AcceptsTab = true;
            this.contentTextEditor.BackColor = System.Drawing.SystemColors.Window;
            this.contentTextEditor.Location = new System.Drawing.Point(545, 8);
            this.contentTextEditor.Name = "contentTextEditor";
            this.contentTextEditor.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.contentTextEditor.Size = new System.Drawing.Size(14, 21);
            this.contentTextEditor.TabIndex = 1;
            this.contentTextEditor.Text = "";
            this.contentTextEditor.Visible = false;
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(84, 6);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(75, 23);
            this.editButton.TabIndex = 2;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(3, 6);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // newButton
            // 
            this.newButton.ContextMenuStrip = this.newButtonMenuStrip;
            this.newButton.Location = new System.Drawing.Point(3, 6);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(75, 23);
            this.newButton.TabIndex = 4;
            this.newButton.Text = "New";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // newButtonMenuStrip
            // 
            this.newButtonMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newArticleMenuItem,
            this.newAbilityMenuItem,
            this.newSchoolMenuItem,
            this.newTalentMenuItem,
            this.newTraitMenuItem});
            this.newButtonMenuStrip.Name = "newButtonMenuStrip";
            this.newButtonMenuStrip.Size = new System.Drawing.Size(111, 114);
            // 
            // newArticleMenuItem
            // 
            this.newArticleMenuItem.Name = "newArticleMenuItem";
            this.newArticleMenuItem.Size = new System.Drawing.Size(110, 22);
            this.newArticleMenuItem.Text = "Article";
            this.newArticleMenuItem.Click += new System.EventHandler(this.newArticleMenuItem_Click);
            // 
            // newAbilityMenuItem
            // 
            this.newAbilityMenuItem.Name = "newAbilityMenuItem";
            this.newAbilityMenuItem.Size = new System.Drawing.Size(110, 22);
            this.newAbilityMenuItem.Text = "Ability";
            this.newAbilityMenuItem.Click += new System.EventHandler(this.newAbilityMenuItem_Click);
            // 
            // newSchoolMenuItem
            // 
            this.newSchoolMenuItem.Name = "newSchoolMenuItem";
            this.newSchoolMenuItem.Size = new System.Drawing.Size(110, 22);
            this.newSchoolMenuItem.Text = "School";
            this.newSchoolMenuItem.Click += new System.EventHandler(this.newSchoolMenuItem_Click);
            // 
            // newTalentMenuItem
            // 
            this.newTalentMenuItem.Name = "newTalentMenuItem";
            this.newTalentMenuItem.Size = new System.Drawing.Size(110, 22);
            this.newTalentMenuItem.Text = "Talent";
            this.newTalentMenuItem.Click += new System.EventHandler(this.newTalentMenuItem_Click);
            // 
            // newTraitMenuItem
            // 
            this.newTraitMenuItem.Name = "newTraitMenuItem";
            this.newTraitMenuItem.Size = new System.Drawing.Size(110, 22);
            this.newTraitMenuItem.Text = "Trait";
            this.newTraitMenuItem.Click += new System.EventHandler(this.newTraitMenuItem_Click);
            // 
            // tablePanel
            // 
            this.tablePanel.AutoSize = true;
            this.tablePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tablePanel.ColumnCount = 2;
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 251F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanel.Controls.Add(this.buttonPanel, 1, 2);
            this.tablePanel.Controls.Add(this.contentPanel, 1, 1);
            this.tablePanel.Controls.Add(this.searchPanel, 0, 0);
            this.tablePanel.Controls.Add(this.toolPanel, 0, 1);
            this.tablePanel.Controls.Add(this.titlePanel, 1, 0);
            this.tablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tablePanel.Location = new System.Drawing.Point(0, 0);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.RowCount = 3;
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tablePanel.Size = new System.Drawing.Size(780, 500);
            this.tablePanel.TabIndex = 6;
            // 
            // buttonPanel
            // 
            this.buttonPanel.BackColor = System.Drawing.SystemColors.Menu;
            this.buttonPanel.Controls.Add(this.atlasPinButton);
            this.buttonPanel.Controls.Add(this.deleteButton);
            this.buttonPanel.Controls.Add(this.cancelButton);
            this.buttonPanel.Controls.Add(this.creditLabel);
            this.buttonPanel.Controls.Add(this.saveButton);
            this.buttonPanel.Controls.Add(this.contentTextEditor);
            this.buttonPanel.Controls.Add(this.editButton);
            this.buttonPanel.Controls.Add(this.newButton);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPanel.Location = new System.Drawing.Point(254, 459);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(523, 38);
            this.buttonPanel.TabIndex = 2;
            // 
            // atlasPinButton
            // 
            this.atlasPinButton.BackgroundImage = global::VirtualCampaign.Properties.Resources.crosshairs;
            this.atlasPinButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.atlasPinButton.Location = new System.Drawing.Point(246, 6);
            this.atlasPinButton.Name = "atlasPinButton";
            this.atlasPinButton.Size = new System.Drawing.Size(23, 23);
            this.atlasPinButton.TabIndex = 8;
            this.atlasPinButton.UseVisualStyleBackColor = true;
            this.atlasPinButton.Visible = false;
            this.atlasPinButton.Click += new System.EventHandler(this.atlasPinButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(165, 6);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 7;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Visible = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(84, 6);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // creditLabel
            // 
            this.creditLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.creditLabel.AutoSize = true;
            this.creditLabel.Location = new System.Drawing.Point(194, 8);
            this.creditLabel.Name = "creditLabel";
            this.creditLabel.Size = new System.Drawing.Size(0, 13);
            this.creditLabel.TabIndex = 5;
            // 
            // contentPanel
            // 
            this.contentPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.contentPanel.Controls.Add(this.contentTextBox);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(251, 36);
            this.contentPanel.Margin = new System.Windows.Forms.Padding(0);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(529, 420);
            this.contentPanel.TabIndex = 4;
            // 
            // contentTextBox
            // 
            this.contentTextBox.AllowWebBrowserDrop = false;
            this.contentTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentTextBox.Location = new System.Drawing.Point(0, 0);
            this.contentTextBox.MinimumSize = new System.Drawing.Size(20, 20);
            this.contentTextBox.Name = "contentTextBox";
            this.contentTextBox.Size = new System.Drawing.Size(529, 420);
            this.contentTextBox.TabIndex = 6;
            this.contentTextBox.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.contentTextBox_DocumentCompleted);
            this.contentTextBox.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.contentTextBox_Navigated);
            this.contentTextBox.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.contentTextBox_Navigating);
            // 
            // searchPanel
            // 
            this.searchPanel.Controls.Add(this.searchButton);
            this.searchPanel.Controls.Add(this.searchTextBox);
            this.searchPanel.Controls.Add(this.searchLabel);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchPanel.Location = new System.Drawing.Point(3, 3);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(245, 30);
            this.searchPanel.TabIndex = 5;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(218, 3);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(21, 23);
            this.searchButton.TabIndex = 7;
            this.searchButton.Text = ">";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(56, 5);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(163, 20);
            this.searchTextBox.TabIndex = 6;
            this.searchTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.searchTextBox_KeyPress);
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.Location = new System.Drawing.Point(9, 8);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(41, 13);
            this.searchLabel.TabIndex = 5;
            this.searchLabel.Text = "Search";
            // 
            // toolPanel
            // 
            this.toolPanel.Controls.Add(this.detailsPanel);
            this.toolPanel.Controls.Add(this.articleTree);
            this.toolPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolPanel.Location = new System.Drawing.Point(3, 39);
            this.toolPanel.Name = "toolPanel";
            this.tablePanel.SetRowSpan(this.toolPanel, 2);
            this.toolPanel.Size = new System.Drawing.Size(245, 458);
            this.toolPanel.TabIndex = 6;
            // 
            // detailsPanel
            // 
            this.detailsPanel.Controls.Add(this.propertiesLabel);
            this.detailsPanel.Controls.Add(this.propertyGridView);
            this.detailsPanel.Controls.Add(this.whitelistButton);
            this.detailsPanel.Controls.Add(this.catsLabel);
            this.detailsPanel.Controls.Add(this.catsList);
            this.detailsPanel.Location = new System.Drawing.Point(3, 142);
            this.detailsPanel.Name = "detailsPanel";
            this.detailsPanel.Size = new System.Drawing.Size(239, 313);
            this.detailsPanel.TabIndex = 1;
            // 
            // propertiesLabel
            // 
            this.propertiesLabel.Location = new System.Drawing.Point(12, 163);
            this.propertiesLabel.Name = "propertiesLabel";
            this.propertiesLabel.Size = new System.Drawing.Size(224, 13);
            this.propertiesLabel.TabIndex = 4;
            this.propertiesLabel.Text = "Advanced Properties";
            this.propertiesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // propertyGridView
            // 
            this.propertyGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.propertyGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.propertyGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.propertyNameCol,
            this.propertyValueCol});
            this.propertyGridView.Location = new System.Drawing.Point(9, 179);
            this.propertyGridView.Name = "propertyGridView";
            this.propertyGridView.RowHeadersVisible = false;
            this.propertyGridView.Size = new System.Drawing.Size(227, 120);
            this.propertyGridView.TabIndex = 3;
            // 
            // propertyNameCol
            // 
            this.propertyNameCol.HeaderText = "Property";
            this.propertyNameCol.Name = "propertyNameCol";
            // 
            // propertyValueCol
            // 
            this.propertyValueCol.HeaderText = "Value";
            this.propertyValueCol.Name = "propertyValueCol";
            // 
            // whitelistButton
            // 
            this.whitelistButton.Location = new System.Drawing.Point(86, 125);
            this.whitelistButton.Name = "whitelistButton";
            this.whitelistButton.Size = new System.Drawing.Size(75, 23);
            this.whitelistButton.TabIndex = 2;
            this.whitelistButton.Text = "Whitelist...";
            this.whitelistButton.UseVisualStyleBackColor = true;
            this.whitelistButton.Click += new System.EventHandler(this.whitelistButton_Click);
            // 
            // catsLabel
            // 
            this.catsLabel.Location = new System.Drawing.Point(9, 9);
            this.catsLabel.Name = "catsLabel";
            this.catsLabel.Size = new System.Drawing.Size(227, 13);
            this.catsLabel.TabIndex = 1;
            this.catsLabel.Text = "Categories";
            this.catsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // catsList
            // 
            this.catsList.FormattingEnabled = true;
            this.catsList.Location = new System.Drawing.Point(9, 25);
            this.catsList.Name = "catsList";
            this.catsList.ScrollAlwaysVisible = true;
            this.catsList.Size = new System.Drawing.Size(227, 94);
            this.catsList.TabIndex = 0;
            // 
            // titlePanel
            // 
            this.titlePanel.Controls.Add(this.forwardButton);
            this.titlePanel.Controls.Add(this.backButton);
            this.titlePanel.Controls.Add(this.titleField);
            this.titlePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titlePanel.Location = new System.Drawing.Point(254, 3);
            this.titlePanel.Name = "titlePanel";
            this.titlePanel.Size = new System.Drawing.Size(523, 30);
            this.titlePanel.TabIndex = 7;
            // 
            // forwardButton
            // 
            this.forwardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.forwardButton.Enabled = false;
            this.forwardButton.Location = new System.Drawing.Point(494, 4);
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(25, 23);
            this.forwardButton.TabIndex = 5;
            this.forwardButton.Text = ">";
            this.forwardButton.UseVisualStyleBackColor = true;
            this.forwardButton.Click += new System.EventHandler(this.forwardButton_Click);
            // 
            // backButton
            // 
            this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.backButton.Enabled = false;
            this.backButton.Location = new System.Drawing.Point(467, 4);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(25, 23);
            this.backButton.TabIndex = 4;
            this.backButton.Text = "<";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // titleField
            // 
            this.titleField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleField.BackColor = System.Drawing.SystemColors.Window;
            this.titleField.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleField.Location = new System.Drawing.Point(3, 1);
            this.titleField.Name = "titleField";
            this.titleField.Size = new System.Drawing.Size(457, 27);
            this.titleField.TabIndex = 3;
            this.titleField.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ArticleBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tablePanel);
            this.Name = "ArticleBrowser";
            this.Size = new System.Drawing.Size(780, 500);
            this.newButtonMenuStrip.ResumeLayout(false);
            this.tablePanel.ResumeLayout(false);
            this.buttonPanel.ResumeLayout(false);
            this.buttonPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.toolPanel.ResumeLayout(false);
            this.detailsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridView)).EndInit();
            this.titlePanel.ResumeLayout(false);
            this.titlePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView articleTree;
        private System.Windows.Forms.RichTextBox contentTextEditor;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.TableLayoutPanel tablePanel;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.TextBox titleField;
        private System.Windows.Forms.Label creditLabel;
        private System.Windows.Forms.WebBrowser contentTextBox;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.Panel toolPanel;
        private System.Windows.Forms.Panel detailsPanel;
        private System.Windows.Forms.Button whitelistButton;
        private System.Windows.Forms.Label catsLabel;
        private System.Windows.Forms.CheckedListBox catsList;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel titlePanel;
        private System.Windows.Forms.Button forwardButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.DataGridView propertyGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyNameCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyValueCol;
        private System.Windows.Forms.Label propertiesLabel;
        private System.Windows.Forms.ContextMenuStrip newButtonMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem newArticleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newAbilityMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newTalentMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newTraitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newSchoolMenuItem;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button atlasPinButton;
    }
}
