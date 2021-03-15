namespace VirtualCampaign.controls {
    partial class EquipSlot {
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
            this.itemImage = new VirtualCampaign.controls.ItemImage();
            this.SuspendLayout();
            // 
            // itemImage
            // 
            this.itemImage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.itemImage.bgColor = System.Drawing.Color.Black;
            this.itemImage.bgSrc = "back01";
            this.itemImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemImage.iconColor = System.Drawing.Color.Black;
            this.itemImage.iconSrc = "";
            this.itemImage.Location = new System.Drawing.Point(0, 0);
            this.itemImage.Margin = new System.Windows.Forms.Padding(0);
            this.itemImage.Name = "itemImage";
            this.itemImage.Size = new System.Drawing.Size(66, 66);
            this.itemImage.TabIndex = 0;
            this.itemImage.Click += new System.EventHandler(this.EquipSlot_MouseClick);
            this.itemImage.DoubleClick += new System.EventHandler(this.itemImage_DoubleClick);
            this.itemImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.itemImage_MouseDown);
            this.itemImage.MouseEnter += new System.EventHandler(this.itemImage_MouseEnter);
            this.itemImage.MouseLeave += new System.EventHandler(this.itemImage_MouseLeave);
            // 
            // EquipSlot
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.itemImage);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "EquipSlot";
            this.Size = new System.Drawing.Size(66, 66);
            this.Click += new System.EventHandler(this.EquipSlot_MouseClick);
            this.ResumeLayout(false);

        }

        #endregion

        private ItemImage itemImage;
    }
}
