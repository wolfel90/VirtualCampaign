namespace VirtualCampaign.controls {
    partial class InventoryList {
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
            this.tablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.itemListPanel = new System.Windows.Forms.Panel();
            this.toolPanel = new System.Windows.Forms.Panel();
            this.addPanel = new System.Windows.Forms.Panel();
            this.addButton = new System.Windows.Forms.Button();
            this.tablePanel.SuspendLayout();
            this.addPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tablePanel
            // 
            this.tablePanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tablePanel.ColumnCount = 1;
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanel.Controls.Add(this.itemListPanel, 0, 1);
            this.tablePanel.Controls.Add(this.toolPanel, 0, 0);
            this.tablePanel.Controls.Add(this.addPanel, 0, 2);
            this.tablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel.Location = new System.Drawing.Point(0, 0);
            this.tablePanel.Margin = new System.Windows.Forms.Padding(0);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.RowCount = 3;
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tablePanel.Size = new System.Drawing.Size(500, 500);
            this.tablePanel.TabIndex = 0;
            // 
            // itemListPanel
            // 
            this.itemListPanel.AutoScroll = true;
            this.itemListPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.itemListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemListPanel.Location = new System.Drawing.Point(4, 43);
            this.itemListPanel.Margin = new System.Windows.Forms.Padding(1);
            this.itemListPanel.Name = "itemListPanel";
            this.itemListPanel.Size = new System.Drawing.Size(492, 425);
            this.itemListPanel.TabIndex = 1;
            this.itemListPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.itemListPanel_MouseClick);
            // 
            // toolPanel
            // 
            this.toolPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.toolPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolPanel.Location = new System.Drawing.Point(4, 4);
            this.toolPanel.Margin = new System.Windows.Forms.Padding(1);
            this.toolPanel.Name = "toolPanel";
            this.toolPanel.Size = new System.Drawing.Size(492, 34);
            this.toolPanel.TabIndex = 2;
            // 
            // addPanel
            // 
            this.addPanel.Controls.Add(this.addButton);
            this.addPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addPanel.Location = new System.Drawing.Point(3, 472);
            this.addPanel.Margin = new System.Windows.Forms.Padding(0);
            this.addPanel.Name = "addPanel";
            this.addPanel.Size = new System.Drawing.Size(494, 25);
            this.addPanel.TabIndex = 3;
            // 
            // addButton
            // 
            this.addButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.addButton.Location = new System.Drawing.Point(1, 0);
            this.addButton.Margin = new System.Windows.Forms.Padding(0);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(492, 25);
            this.addButton.TabIndex = 0;
            this.addButton.Text = "+";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // InventoryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tablePanel);
            this.Name = "InventoryList";
            this.Size = new System.Drawing.Size(500, 500);
            this.tablePanel.ResumeLayout(false);
            this.addPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tablePanel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Panel itemListPanel;
        private System.Windows.Forms.Panel toolPanel;
        private System.Windows.Forms.Panel addPanel;
    }
}
