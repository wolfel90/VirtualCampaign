namespace VirtualCampaign.window {
    partial class ItemBank {
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
            this.tablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.itemStoreBar = new VirtualCampaign.controls.ItemHotBar();
            this.builderPanel = new System.Windows.Forms.Panel();
            this.arrowLabel = new System.Windows.Forms.Label();
            this.componentsLabel = new System.Windows.Forms.Label();
            this.resultItemLabel = new System.Windows.Forms.Label();
            this.baseItemLabel = new System.Windows.Forms.Label();
            this.componentsHotBar = new VirtualCampaign.controls.ItemHotBar();
            this.resultItemSlot = new VirtualCampaign.controls.EquipSlot();
            this.baseItemSlot = new VirtualCampaign.controls.EquipSlot();
            this.detailsTextField = new System.Windows.Forms.TextBox();
            this.itemList = new VirtualCampaign.controls.CategorizedItemList();
            this.panel1 = new System.Windows.Forms.Panel();
            this.onUseComboBox = new System.Windows.Forms.ComboBox();
            this.onEquipComboBox = new System.Windows.Forms.ComboBox();
            this.onUseLabel = new System.Windows.Forms.Label();
            this.onEquipLabel = new System.Windows.Forms.Label();
            this.effectPanel = new System.Windows.Forms.Panel();
            this.resultItemPanel = new System.Windows.Forms.Panel();
            this.baseItemPanel = new System.Windows.Forms.Panel();
            this.tablePanel.SuspendLayout();
            this.builderPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.effectPanel.SuspendLayout();
            this.resultItemPanel.SuspendLayout();
            this.baseItemPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tablePanel
            // 
            this.tablePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tablePanel.ColumnCount = 2;
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.5F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.5F));
            this.tablePanel.Controls.Add(this.itemStoreBar, 0, 2);
            this.tablePanel.Controls.Add(this.builderPanel, 1, 0);
            this.tablePanel.Controls.Add(this.detailsTextField, 1, 1);
            this.tablePanel.Controls.Add(this.itemList, 0, 0);
            this.tablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel.Location = new System.Drawing.Point(0, 0);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.RowCount = 3;
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tablePanel.Size = new System.Drawing.Size(800, 450);
            this.tablePanel.TabIndex = 0;
            // 
            // itemStoreBar
            // 
            this.itemStoreBar.adaptableTooltip = true;
            this.tablePanel.SetColumnSpan(this.itemStoreBar, 2);
            this.itemStoreBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemStoreBar.DropMode = 0;
            this.itemStoreBar.GrabMode = 0;
            this.itemStoreBar.Location = new System.Drawing.Point(3, 414);
            this.itemStoreBar.Name = "itemStoreBar";
            this.itemStoreBar.Size = new System.Drawing.Size(794, 33);
            this.itemStoreBar.TabIndex = 2;
            // 
            // builderPanel
            // 
            this.builderPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.builderPanel.Controls.Add(this.baseItemPanel);
            this.builderPanel.Controls.Add(this.resultItemPanel);
            this.builderPanel.Controls.Add(this.effectPanel);
            this.builderPanel.Controls.Add(this.panel1);
            this.builderPanel.Controls.Add(this.arrowLabel);
            this.builderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.builderPanel.Location = new System.Drawing.Point(391, 3);
            this.builderPanel.Name = "builderPanel";
            this.builderPanel.Size = new System.Drawing.Size(406, 232);
            this.builderPanel.TabIndex = 3;
            // 
            // arrowLabel
            // 
            this.arrowLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.arrowLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arrowLabel.Location = new System.Drawing.Point(169, 150);
            this.arrowLabel.Name = "arrowLabel";
            this.arrowLabel.Size = new System.Drawing.Size(60, 42);
            this.arrowLabel.TabIndex = 6;
            this.arrowLabel.Text = "→";
            this.arrowLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // componentsLabel
            // 
            this.componentsLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.componentsLabel.Location = new System.Drawing.Point(0, 0);
            this.componentsLabel.Name = "componentsLabel";
            this.componentsLabel.Size = new System.Drawing.Size(406, 24);
            this.componentsLabel.TabIndex = 5;
            this.componentsLabel.Text = "Components";
            this.componentsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // resultItemLabel
            // 
            this.resultItemLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.resultItemLabel.Location = new System.Drawing.Point(11, 6);
            this.resultItemLabel.Name = "resultItemLabel";
            this.resultItemLabel.Size = new System.Drawing.Size(70, 13);
            this.resultItemLabel.TabIndex = 4;
            this.resultItemLabel.Text = "Result Item";
            this.resultItemLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // baseItemLabel
            // 
            this.baseItemLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.baseItemLabel.Location = new System.Drawing.Point(9, 7);
            this.baseItemLabel.Name = "baseItemLabel";
            this.baseItemLabel.Size = new System.Drawing.Size(70, 13);
            this.baseItemLabel.TabIndex = 1;
            this.baseItemLabel.Text = "Base Item";
            this.baseItemLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // componentsHotBar
            // 
            this.componentsHotBar.adaptableTooltip = true;
            this.componentsHotBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.componentsHotBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.componentsHotBar.DropMode = 0;
            this.componentsHotBar.GrabMode = 0;
            this.componentsHotBar.Location = new System.Drawing.Point(0, 27);
            this.componentsHotBar.Name = "componentsHotBar";
            this.componentsHotBar.Size = new System.Drawing.Size(406, 28);
            this.componentsHotBar.TabIndex = 3;
            // 
            // resultItemSlot
            // 
            this.resultItemSlot.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.resultItemSlot.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.resultItemSlot.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.resultItemSlot.DropMode = 2;
            this.resultItemSlot.editable = true;
            this.resultItemSlot.emptyIconSrc = null;
            this.resultItemSlot.GrabMode = 2;
            this.resultItemSlot.itemData = null;
            this.resultItemSlot.Location = new System.Drawing.Point(11, 21);
            this.resultItemSlot.Margin = new System.Windows.Forms.Padding(0);
            this.resultItemSlot.Name = "resultItemSlot";
            this.resultItemSlot.Size = new System.Drawing.Size(70, 70);
            this.resultItemSlot.TabIndex = 1;
            this.resultItemSlot.MouseClick += new System.Windows.Forms.MouseEventHandler(this.resultItemSlot_MouseClick);
            // 
            // baseItemSlot
            // 
            this.baseItemSlot.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.baseItemSlot.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.baseItemSlot.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.baseItemSlot.DropMode = 2;
            this.baseItemSlot.editable = true;
            this.baseItemSlot.emptyIconSrc = null;
            this.baseItemSlot.GrabMode = 2;
            this.baseItemSlot.itemData = null;
            this.baseItemSlot.Location = new System.Drawing.Point(9, 22);
            this.baseItemSlot.Margin = new System.Windows.Forms.Padding(0);
            this.baseItemSlot.Name = "baseItemSlot";
            this.baseItemSlot.Size = new System.Drawing.Size(70, 70);
            this.baseItemSlot.TabIndex = 0;
            this.baseItemSlot.MouseClick += new System.Windows.Forms.MouseEventHandler(this.baseItemSlot_MouseClick);
            // 
            // detailsTextField
            // 
            this.detailsTextField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailsTextField.ForeColor = System.Drawing.Color.Black;
            this.detailsTextField.Location = new System.Drawing.Point(391, 241);
            this.detailsTextField.Multiline = true;
            this.detailsTextField.Name = "detailsTextField";
            this.detailsTextField.ReadOnly = true;
            this.detailsTextField.Size = new System.Drawing.Size(406, 167);
            this.detailsTextField.TabIndex = 4;
            // 
            // itemList
            // 
            this.itemList.AutoScroll = true;
            this.itemList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.itemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemList.Location = new System.Drawing.Point(3, 3);
            this.itemList.Name = "itemList";
            this.tablePanel.SetRowSpan(this.itemList, 2);
            this.itemList.Size = new System.Drawing.Size(382, 405);
            this.itemList.TabIndex = 5;
            this.itemList.SizeChanged += new System.EventHandler(this.itemList_SizeChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.componentsHotBar);
            this.panel1.Controls.Add(this.componentsLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(406, 55);
            this.panel1.TabIndex = 7;
            // 
            // onUseComboBox
            // 
            this.onUseComboBox.FormattingEnabled = true;
            this.onUseComboBox.Items.AddRange(new object[] {
            "<None>"});
            this.onUseComboBox.Location = new System.Drawing.Point(65, 7);
            this.onUseComboBox.Name = "onUseComboBox";
            this.onUseComboBox.Size = new System.Drawing.Size(121, 21);
            this.onUseComboBox.TabIndex = 8;
            this.onUseComboBox.Text = "<None>";
            this.onUseComboBox.SelectedIndexChanged += new System.EventHandler(this.onUseComboBox_SelectedIndexChanged);
            // 
            // onEquipComboBox
            // 
            this.onEquipComboBox.FormattingEnabled = true;
            this.onEquipComboBox.Items.AddRange(new object[] {
            "<None>"});
            this.onEquipComboBox.Location = new System.Drawing.Point(282, 6);
            this.onEquipComboBox.Name = "onEquipComboBox";
            this.onEquipComboBox.Size = new System.Drawing.Size(121, 21);
            this.onEquipComboBox.TabIndex = 9;
            this.onEquipComboBox.Text = "<None>";
            this.onEquipComboBox.SelectedIndexChanged += new System.EventHandler(this.onEquipComboBox_SelectedIndexChanged);
            // 
            // onUseLabel
            // 
            this.onUseLabel.Location = new System.Drawing.Point(10, 11);
            this.onUseLabel.Name = "onUseLabel";
            this.onUseLabel.Size = new System.Drawing.Size(49, 13);
            this.onUseLabel.TabIndex = 10;
            this.onUseLabel.Text = "On Use";
            this.onUseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // onEquipLabel
            // 
            this.onEquipLabel.Location = new System.Drawing.Point(218, 10);
            this.onEquipLabel.Name = "onEquipLabel";
            this.onEquipLabel.Size = new System.Drawing.Size(58, 13);
            this.onEquipLabel.TabIndex = 11;
            this.onEquipLabel.Text = "Equipped";
            this.onEquipLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // effectPanel
            // 
            this.effectPanel.Controls.Add(this.onUseComboBox);
            this.effectPanel.Controls.Add(this.onEquipLabel);
            this.effectPanel.Controls.Add(this.onUseLabel);
            this.effectPanel.Controls.Add(this.onEquipComboBox);
            this.effectPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.effectPanel.Location = new System.Drawing.Point(0, 55);
            this.effectPanel.Name = "effectPanel";
            this.effectPanel.Size = new System.Drawing.Size(406, 35);
            this.effectPanel.TabIndex = 12;
            // 
            // resultItemPanel
            // 
            this.resultItemPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resultItemPanel.BackColor = System.Drawing.SystemColors.Control;
            this.resultItemPanel.Controls.Add(this.resultItemSlot);
            this.resultItemPanel.Controls.Add(this.resultItemLabel);
            this.resultItemPanel.Location = new System.Drawing.Point(299, 123);
            this.resultItemPanel.Name = "resultItemPanel";
            this.resultItemPanel.Size = new System.Drawing.Size(90, 100);
            this.resultItemPanel.TabIndex = 13;
            // 
            // baseItemPanel
            // 
            this.baseItemPanel.Controls.Add(this.baseItemLabel);
            this.baseItemPanel.Controls.Add(this.baseItemSlot);
            this.baseItemPanel.Location = new System.Drawing.Point(13, 123);
            this.baseItemPanel.Name = "baseItemPanel";
            this.baseItemPanel.Size = new System.Drawing.Size(90, 100);
            this.baseItemPanel.TabIndex = 14;
            // 
            // ItemBank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tablePanel);
            this.Name = "ItemBank";
            this.Size = new System.Drawing.Size(800, 450);
            this.tablePanel.ResumeLayout(false);
            this.tablePanel.PerformLayout();
            this.builderPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.effectPanel.ResumeLayout(false);
            this.resultItemPanel.ResumeLayout(false);
            this.baseItemPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tablePanel;
        private controls.ItemHotBar itemStoreBar;
        private System.Windows.Forms.Panel builderPanel;
        private controls.EquipSlot resultItemSlot;
        private controls.EquipSlot baseItemSlot;
        private System.Windows.Forms.TextBox detailsTextField;
        private controls.ItemHotBar componentsHotBar;
        private System.Windows.Forms.Label arrowLabel;
        private System.Windows.Forms.Label componentsLabel;
        private System.Windows.Forms.Label resultItemLabel;
        private System.Windows.Forms.Label baseItemLabel;
        private controls.CategorizedItemList itemList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox onEquipComboBox;
        private System.Windows.Forms.ComboBox onUseComboBox;
        private System.Windows.Forms.Label onUseLabel;
        private System.Windows.Forms.Label onEquipLabel;
        private System.Windows.Forms.Panel effectPanel;
        private System.Windows.Forms.Panel baseItemPanel;
        private System.Windows.Forms.Panel resultItemPanel;
    }
}