namespace VirtualCampaign.controls {
    partial class ItemSlot {
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
            this.starButton = new System.Windows.Forms.PictureBox();
            this.closeButton = new System.Windows.Forms.PictureBox();
            this.equipButton = new System.Windows.Forms.PictureBox();
            this.infoLabel1 = new System.Windows.Forms.Label();
            this.infoLabel2 = new System.Windows.Forms.Label();
            this.infoLabel3 = new System.Windows.Forms.Label();
            this.expandBar = new System.Windows.Forms.Label();
            this.itemImage = new VirtualCampaign.controls.ItemImage();
            ((System.ComponentModel.ISupportInitialize)(this.starButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipButton)).BeginInit();
            this.SuspendLayout();
            // 
            // starButton
            // 
            this.starButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.starButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.starButton.Image = global::VirtualCampaign.Properties.Resources.item_star_inactive;
            this.starButton.InitialImage = global::VirtualCampaign.Properties.Resources.item_star_inactive;
            this.starButton.Location = new System.Drawing.Point(366, 52);
            this.starButton.Name = "starButton";
            this.starButton.Size = new System.Drawing.Size(32, 25);
            this.starButton.TabIndex = 7;
            this.starButton.TabStop = false;
            this.starButton.Click += new System.EventHandler(this.starButton_Click);
            this.starButton.MouseEnter += new System.EventHandler(this.starButton_MouseEnter);
            this.starButton.MouseLeave += new System.EventHandler(this.starButton_MouseLeave);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.closeButton.Image = global::VirtualCampaign.Properties.Resources.item_delete_active;
            this.closeButton.InitialImage = global::VirtualCampaign.Properties.Resources.item_delete_active;
            this.closeButton.Location = new System.Drawing.Point(366, -1);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(32, 25);
            this.closeButton.TabIndex = 8;
            this.closeButton.TabStop = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            this.closeButton.MouseEnter += new System.EventHandler(this.closeButton_MouseEnter);
            this.closeButton.MouseLeave += new System.EventHandler(this.closeButton_MouseLeave);
            // 
            // equipButton
            // 
            this.equipButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.equipButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.equipButton.Image = global::VirtualCampaign.Properties.Resources.item_equip_inactive;
            this.equipButton.InitialImage = global::VirtualCampaign.Properties.Resources.item_equip_inactive;
            this.equipButton.Location = new System.Drawing.Point(366, 24);
            this.equipButton.Name = "equipButton";
            this.equipButton.Size = new System.Drawing.Size(32, 28);
            this.equipButton.TabIndex = 9;
            this.equipButton.TabStop = false;
            this.equipButton.Click += new System.EventHandler(this.equipButton_Click);
            this.equipButton.MouseEnter += new System.EventHandler(this.equipButton_MouseEnter);
            this.equipButton.MouseLeave += new System.EventHandler(this.equipButton_MouseLeave);
            // 
            // infoLabel1
            // 
            this.infoLabel1.Location = new System.Drawing.Point(95, 8);
            this.infoLabel1.Name = "infoLabel1";
            this.infoLabel1.Size = new System.Drawing.Size(265, 20);
            this.infoLabel1.TabIndex = 10;
            this.infoLabel1.Text = "Title";
            this.infoLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.infoLabel1.Click += new System.EventHandler(this.ItemSlot_Click);
            this.infoLabel1.DoubleClick += new System.EventHandler(this.ItemSlot_Click);
            // 
            // infoLabel2
            // 
            this.infoLabel2.Location = new System.Drawing.Point(95, 29);
            this.infoLabel2.Name = "infoLabel2";
            this.infoLabel2.Size = new System.Drawing.Size(265, 20);
            this.infoLabel2.TabIndex = 12;
            this.infoLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.infoLabel2.Click += new System.EventHandler(this.ItemSlot_Click);
            this.infoLabel2.DoubleClick += new System.EventHandler(this.ItemSlot_Click);
            // 
            // infoLabel3
            // 
            this.infoLabel3.Location = new System.Drawing.Point(92, 52);
            this.infoLabel3.Name = "infoLabel3";
            this.infoLabel3.Size = new System.Drawing.Size(268, 20);
            this.infoLabel3.TabIndex = 13;
            this.infoLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.infoLabel3.Click += new System.EventHandler(this.ItemSlot_Click);
            this.infoLabel3.DoubleClick += new System.EventHandler(this.ItemSlot_Click);
            // 
            // expandBar
            // 
            this.expandBar.AllowDrop = true;
            this.expandBar.Location = new System.Drawing.Point(2, 2);
            this.expandBar.Name = "expandBar";
            this.expandBar.Size = new System.Drawing.Size(21, 75);
            this.expandBar.TabIndex = 14;
            this.expandBar.Text = "+";
            this.expandBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.expandBar.Click += new System.EventHandler(this.expandBar_Click);
            // 
            // itemImage
            // 
            this.itemImage.bgColor = System.Drawing.Color.Black;
            this.itemImage.bgSrc = "back01";
            this.itemImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.itemImage.iconColor = System.Drawing.Color.Black;
            this.itemImage.iconSrc = "";
            this.itemImage.Location = new System.Drawing.Point(26, 7);
            this.itemImage.Margin = new System.Windows.Forms.Padding(0);
            this.itemImage.Name = "itemImage";
            this.itemImage.Size = new System.Drawing.Size(66, 66);
            this.itemImage.TabIndex = 11;
            // 
            // ItemSlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.expandBar);
            this.Controls.Add(this.infoLabel3);
            this.Controls.Add(this.infoLabel2);
            this.Controls.Add(this.itemImage);
            this.Controls.Add(this.infoLabel1);
            this.Controls.Add(this.equipButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.starButton);
            this.Name = "ItemSlot";
            this.Size = new System.Drawing.Size(398, 78);
            this.Click += new System.EventHandler(this.ItemSlot_Click);
            this.DoubleClick += new System.EventHandler(this.ItemSlot_Click);
            ((System.ComponentModel.ISupportInitialize)(this.starButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox starButton;
        private System.Windows.Forms.PictureBox closeButton;
        private System.Windows.Forms.PictureBox equipButton;
        private System.Windows.Forms.Label infoLabel1;
        private ItemImage itemImage;
        private System.Windows.Forms.Label infoLabel2;
        private System.Windows.Forms.Label infoLabel3;
        private System.Windows.Forms.Label expandBar;
    }
}
