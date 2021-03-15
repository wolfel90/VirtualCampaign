namespace VirtualCampaign.window {
    partial class ItemImageEditor {
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
            this.itemImage = new VirtualCampaign.controls.ItemImage();
            this.bgColorLabel = new System.Windows.Forms.Label();
            this.iconColorLabel = new System.Windows.Forms.Label();
            this.bgRSlider = new System.Windows.Forms.TrackBar();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.bgRLabel = new System.Windows.Forms.Label();
            this.bgRValueLabel = new System.Windows.Forms.Label();
            this.bgGValueLabel = new System.Windows.Forms.Label();
            this.bgGLabel = new System.Windows.Forms.Label();
            this.bgGSlider = new System.Windows.Forms.TrackBar();
            this.bgBValueLabel = new System.Windows.Forms.Label();
            this.bgBLabel = new System.Windows.Forms.Label();
            this.bgBSlider = new System.Windows.Forms.TrackBar();
            this.iconBValueLabel = new System.Windows.Forms.Label();
            this.iconBLabel = new System.Windows.Forms.Label();
            this.iconBSlider = new System.Windows.Forms.TrackBar();
            this.iconGValueLabel = new System.Windows.Forms.Label();
            this.iconGLabel = new System.Windows.Forms.Label();
            this.iconGSlider = new System.Windows.Forms.TrackBar();
            this.iconRValueLabel = new System.Windows.Forms.Label();
            this.iconRLabel = new System.Windows.Forms.Label();
            this.iconRSlider = new System.Windows.Forms.TrackBar();
            this.imageOptionsPanel = new System.Windows.Forms.Panel();
            this.iconOptionsPanel = new System.Windows.Forms.Panel();
            this.bgOptionsPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.bgRSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bgGSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bgBSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconBSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconGSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconRSlider)).BeginInit();
            this.imageOptionsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // itemImage
            // 
            this.itemImage.bgColor = System.Drawing.Color.Black;
            this.itemImage.bgSrc = "back01";
            this.itemImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.itemImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.itemImage.iconColor = System.Drawing.Color.Black;
            this.itemImage.iconSrc = "";
            this.itemImage.Location = new System.Drawing.Point(335, 7);
            this.itemImage.Margin = new System.Windows.Forms.Padding(0);
            this.itemImage.Name = "itemImage";
            this.itemImage.Size = new System.Drawing.Size(66, 66);
            this.itemImage.TabIndex = 0;
            this.itemImage.Click += new System.EventHandler(this.itemImage_Click);
            // 
            // bgColorLabel
            // 
            this.bgColorLabel.Location = new System.Drawing.Point(14, 85);
            this.bgColorLabel.Name = "bgColorLabel";
            this.bgColorLabel.Size = new System.Drawing.Size(332, 20);
            this.bgColorLabel.TabIndex = 1;
            this.bgColorLabel.Text = "BG Color";
            this.bgColorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iconColorLabel
            // 
            this.iconColorLabel.Location = new System.Drawing.Point(386, 85);
            this.iconColorLabel.Name = "iconColorLabel";
            this.iconColorLabel.Size = new System.Drawing.Size(335, 20);
            this.iconColorLabel.TabIndex = 2;
            this.iconColorLabel.Text = "Icon Color";
            this.iconColorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bgRSlider
            // 
            this.bgRSlider.AutoSize = false;
            this.bgRSlider.LargeChange = 25;
            this.bgRSlider.Location = new System.Drawing.Point(41, 108);
            this.bgRSlider.Maximum = 255;
            this.bgRSlider.Name = "bgRSlider";
            this.bgRSlider.Size = new System.Drawing.Size(255, 36);
            this.bgRSlider.SmallChange = 5;
            this.bgRSlider.TabIndex = 3;
            this.bgRSlider.TickFrequency = 10;
            this.bgRSlider.Scroll += new System.EventHandler(this.bgRSlider_Scroll);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(646, 256);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(565, 256);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // bgRLabel
            // 
            this.bgRLabel.Location = new System.Drawing.Point(11, 112);
            this.bgRLabel.Name = "bgRLabel";
            this.bgRLabel.Size = new System.Drawing.Size(24, 20);
            this.bgRLabel.TabIndex = 6;
            this.bgRLabel.Text = "R";
            this.bgRLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bgRValueLabel
            // 
            this.bgRValueLabel.Location = new System.Drawing.Point(302, 112);
            this.bgRValueLabel.Name = "bgRValueLabel";
            this.bgRValueLabel.Size = new System.Drawing.Size(44, 20);
            this.bgRValueLabel.TabIndex = 7;
            this.bgRValueLabel.Text = "R";
            this.bgRValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bgGValueLabel
            // 
            this.bgGValueLabel.Location = new System.Drawing.Point(302, 154);
            this.bgGValueLabel.Name = "bgGValueLabel";
            this.bgGValueLabel.Size = new System.Drawing.Size(44, 20);
            this.bgGValueLabel.TabIndex = 10;
            this.bgGValueLabel.Text = "R";
            this.bgGValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bgGLabel
            // 
            this.bgGLabel.Location = new System.Drawing.Point(11, 154);
            this.bgGLabel.Name = "bgGLabel";
            this.bgGLabel.Size = new System.Drawing.Size(24, 20);
            this.bgGLabel.TabIndex = 9;
            this.bgGLabel.Text = "G";
            this.bgGLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bgGSlider
            // 
            this.bgGSlider.AutoSize = false;
            this.bgGSlider.LargeChange = 25;
            this.bgGSlider.Location = new System.Drawing.Point(41, 150);
            this.bgGSlider.Maximum = 255;
            this.bgGSlider.Name = "bgGSlider";
            this.bgGSlider.Size = new System.Drawing.Size(255, 36);
            this.bgGSlider.SmallChange = 5;
            this.bgGSlider.TabIndex = 8;
            this.bgGSlider.TickFrequency = 10;
            this.bgGSlider.Scroll += new System.EventHandler(this.bgGSlider_Scroll);
            // 
            // bgBValueLabel
            // 
            this.bgBValueLabel.Location = new System.Drawing.Point(302, 196);
            this.bgBValueLabel.Name = "bgBValueLabel";
            this.bgBValueLabel.Size = new System.Drawing.Size(44, 20);
            this.bgBValueLabel.TabIndex = 13;
            this.bgBValueLabel.Text = "R";
            this.bgBValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bgBLabel
            // 
            this.bgBLabel.Location = new System.Drawing.Point(11, 196);
            this.bgBLabel.Name = "bgBLabel";
            this.bgBLabel.Size = new System.Drawing.Size(24, 20);
            this.bgBLabel.TabIndex = 12;
            this.bgBLabel.Text = "B";
            this.bgBLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bgBSlider
            // 
            this.bgBSlider.AutoSize = false;
            this.bgBSlider.LargeChange = 25;
            this.bgBSlider.Location = new System.Drawing.Point(41, 192);
            this.bgBSlider.Maximum = 255;
            this.bgBSlider.Name = "bgBSlider";
            this.bgBSlider.Size = new System.Drawing.Size(255, 36);
            this.bgBSlider.SmallChange = 5;
            this.bgBSlider.TabIndex = 11;
            this.bgBSlider.TickFrequency = 10;
            this.bgBSlider.Scroll += new System.EventHandler(this.bgBSlider_Scroll);
            // 
            // iconBValueLabel
            // 
            this.iconBValueLabel.Location = new System.Drawing.Point(677, 196);
            this.iconBValueLabel.Name = "iconBValueLabel";
            this.iconBValueLabel.Size = new System.Drawing.Size(44, 20);
            this.iconBValueLabel.TabIndex = 22;
            this.iconBValueLabel.Text = "R";
            this.iconBValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iconBLabel
            // 
            this.iconBLabel.Location = new System.Drawing.Point(386, 196);
            this.iconBLabel.Name = "iconBLabel";
            this.iconBLabel.Size = new System.Drawing.Size(24, 20);
            this.iconBLabel.TabIndex = 21;
            this.iconBLabel.Text = "B";
            this.iconBLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iconBSlider
            // 
            this.iconBSlider.AutoSize = false;
            this.iconBSlider.LargeChange = 25;
            this.iconBSlider.Location = new System.Drawing.Point(416, 192);
            this.iconBSlider.Maximum = 255;
            this.iconBSlider.Name = "iconBSlider";
            this.iconBSlider.Size = new System.Drawing.Size(255, 36);
            this.iconBSlider.SmallChange = 5;
            this.iconBSlider.TabIndex = 20;
            this.iconBSlider.TickFrequency = 10;
            this.iconBSlider.Scroll += new System.EventHandler(this.iconBSlider_Scroll);
            // 
            // iconGValueLabel
            // 
            this.iconGValueLabel.Location = new System.Drawing.Point(677, 154);
            this.iconGValueLabel.Name = "iconGValueLabel";
            this.iconGValueLabel.Size = new System.Drawing.Size(44, 20);
            this.iconGValueLabel.TabIndex = 19;
            this.iconGValueLabel.Text = "R";
            this.iconGValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iconGLabel
            // 
            this.iconGLabel.Location = new System.Drawing.Point(386, 154);
            this.iconGLabel.Name = "iconGLabel";
            this.iconGLabel.Size = new System.Drawing.Size(24, 20);
            this.iconGLabel.TabIndex = 18;
            this.iconGLabel.Text = "G";
            this.iconGLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iconGSlider
            // 
            this.iconGSlider.AutoSize = false;
            this.iconGSlider.LargeChange = 25;
            this.iconGSlider.Location = new System.Drawing.Point(416, 150);
            this.iconGSlider.Maximum = 255;
            this.iconGSlider.Name = "iconGSlider";
            this.iconGSlider.Size = new System.Drawing.Size(255, 36);
            this.iconGSlider.SmallChange = 5;
            this.iconGSlider.TabIndex = 17;
            this.iconGSlider.TickFrequency = 10;
            this.iconGSlider.Scroll += new System.EventHandler(this.iconGSlider_Scroll);
            // 
            // iconRValueLabel
            // 
            this.iconRValueLabel.Location = new System.Drawing.Point(677, 112);
            this.iconRValueLabel.Name = "iconRValueLabel";
            this.iconRValueLabel.Size = new System.Drawing.Size(44, 20);
            this.iconRValueLabel.TabIndex = 16;
            this.iconRValueLabel.Text = "R";
            this.iconRValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iconRLabel
            // 
            this.iconRLabel.Location = new System.Drawing.Point(386, 112);
            this.iconRLabel.Name = "iconRLabel";
            this.iconRLabel.Size = new System.Drawing.Size(24, 20);
            this.iconRLabel.TabIndex = 15;
            this.iconRLabel.Text = "R";
            this.iconRLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iconRSlider
            // 
            this.iconRSlider.AutoSize = false;
            this.iconRSlider.LargeChange = 25;
            this.iconRSlider.Location = new System.Drawing.Point(416, 108);
            this.iconRSlider.Maximum = 255;
            this.iconRSlider.Name = "iconRSlider";
            this.iconRSlider.Size = new System.Drawing.Size(255, 36);
            this.iconRSlider.SmallChange = 5;
            this.iconRSlider.TabIndex = 14;
            this.iconRSlider.TickFrequency = 10;
            this.iconRSlider.Scroll += new System.EventHandler(this.iconRSlider_Scroll);
            // 
            // imageOptionsPanel
            // 
            this.imageOptionsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageOptionsPanel.Controls.Add(this.iconOptionsPanel);
            this.imageOptionsPanel.Controls.Add(this.bgOptionsPanel);
            this.imageOptionsPanel.Location = new System.Drawing.Point(15, 75);
            this.imageOptionsPanel.Name = "imageOptionsPanel";
            this.imageOptionsPanel.Size = new System.Drawing.Size(700, 175);
            this.imageOptionsPanel.TabIndex = 23;
            this.imageOptionsPanel.Visible = false;
            // 
            // iconOptionsPanel
            // 
            this.iconOptionsPanel.AutoScroll = true;
            this.iconOptionsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.iconOptionsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.iconOptionsPanel.Location = new System.Drawing.Point(16, 77);
            this.iconOptionsPanel.Name = "iconOptionsPanel";
            this.iconOptionsPanel.Size = new System.Drawing.Size(660, 86);
            this.iconOptionsPanel.TabIndex = 1;
            // 
            // bgOptionsPanel
            // 
            this.bgOptionsPanel.AutoScroll = true;
            this.bgOptionsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bgOptionsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bgOptionsPanel.Location = new System.Drawing.Point(16, 3);
            this.bgOptionsPanel.Name = "bgOptionsPanel";
            this.bgOptionsPanel.Size = new System.Drawing.Size(660, 66);
            this.bgOptionsPanel.TabIndex = 0;
            // 
            // ItemImageEditor
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(728, 291);
            this.Controls.Add(this.imageOptionsPanel);
            this.Controls.Add(this.iconBValueLabel);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.iconBLabel);
            this.Controls.Add(this.iconBSlider);
            this.Controls.Add(this.iconGValueLabel);
            this.Controls.Add(this.iconGLabel);
            this.Controls.Add(this.iconGSlider);
            this.Controls.Add(this.iconRValueLabel);
            this.Controls.Add(this.iconRLabel);
            this.Controls.Add(this.iconRSlider);
            this.Controls.Add(this.bgBValueLabel);
            this.Controls.Add(this.bgBLabel);
            this.Controls.Add(this.bgBSlider);
            this.Controls.Add(this.bgGValueLabel);
            this.Controls.Add(this.bgGLabel);
            this.Controls.Add(this.bgGSlider);
            this.Controls.Add(this.bgRValueLabel);
            this.Controls.Add(this.bgRLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.bgRSlider);
            this.Controls.Add(this.iconColorLabel);
            this.Controls.Add(this.bgColorLabel);
            this.Controls.Add(this.itemImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ItemImageEditor";
            this.Text = "ItemImageEditor";
            this.Click += new System.EventHandler(this.ItemImageEditor_Click);
            ((System.ComponentModel.ISupportInitialize)(this.bgRSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bgGSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bgBSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconBSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconGSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconRSlider)).EndInit();
            this.imageOptionsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private controls.ItemImage itemImage;
        private System.Windows.Forms.Label bgColorLabel;
        private System.Windows.Forms.Label iconColorLabel;
        private System.Windows.Forms.TrackBar bgRSlider;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label bgRLabel;
        private System.Windows.Forms.Label bgRValueLabel;
        private System.Windows.Forms.Label bgGValueLabel;
        private System.Windows.Forms.Label bgGLabel;
        private System.Windows.Forms.TrackBar bgGSlider;
        private System.Windows.Forms.Label bgBValueLabel;
        private System.Windows.Forms.Label bgBLabel;
        private System.Windows.Forms.TrackBar bgBSlider;
        private System.Windows.Forms.Label iconBValueLabel;
        private System.Windows.Forms.Label iconBLabel;
        private System.Windows.Forms.TrackBar iconBSlider;
        private System.Windows.Forms.Label iconGValueLabel;
        private System.Windows.Forms.Label iconGLabel;
        private System.Windows.Forms.TrackBar iconGSlider;
        private System.Windows.Forms.Label iconRValueLabel;
        private System.Windows.Forms.Label iconRLabel;
        private System.Windows.Forms.TrackBar iconRSlider;
        private System.Windows.Forms.Panel imageOptionsPanel;
        private System.Windows.Forms.Panel iconOptionsPanel;
        private System.Windows.Forms.Panel bgOptionsPanel;
    }
}