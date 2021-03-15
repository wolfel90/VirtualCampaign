namespace VirtualCampaign.controls {
    partial class AtlasInfoPanel {
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
            this.editButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.editPanel = new System.Windows.Forms.Panel();
            this.iconOptionsPanel = new System.Windows.Forms.Panel();
            this.bgOptionsPanel = new System.Windows.Forms.Panel();
            this.iconPictureBox = new System.Windows.Forms.PictureBox();
            this.bgPictureBox = new System.Windows.Forms.PictureBox();
            this.contentBox = new System.Windows.Forms.WebBrowser();
            this.tablePanel.SuspendLayout();
            this.editPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bgPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tablePanel
            // 
            this.tablePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tablePanel.ColumnCount = 2;
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanel.Controls.Add(this.editButton, 0, 2);
            this.tablePanel.Controls.Add(this.closeButton, 1, 2);
            this.tablePanel.Controls.Add(this.titleLabel, 0, 0);
            this.tablePanel.Controls.Add(this.editPanel, 0, 1);
            this.tablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel.Location = new System.Drawing.Point(0, 0);
            this.tablePanel.Margin = new System.Windows.Forms.Padding(0);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.RowCount = 3;
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tablePanel.Size = new System.Drawing.Size(346, 176);
            this.tablePanel.TabIndex = 1;
            // 
            // editButton
            // 
            this.editButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editButton.Location = new System.Drawing.Point(2, 153);
            this.editButton.Margin = new System.Windows.Forms.Padding(2);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(169, 21);
            this.editButton.TabIndex = 1;
            this.editButton.Text = "Edit Marker";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.closeButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.closeButton.Location = new System.Drawing.Point(175, 153);
            this.closeButton.Margin = new System.Windows.Forms.Padding(2);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(169, 21);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tablePanel.SetColumnSpan(this.titleLabel, 2);
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(2, 2);
            this.titleLabel.Margin = new System.Windows.Forms.Padding(2);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(342, 26);
            this.titleLabel.TabIndex = 3;
            this.titleLabel.Text = "Title";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.titleLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titleLabel_MouseDown);
            this.titleLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.titleLabel_MouseMove);
            this.titleLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.titleLabel_MouseUp);
            // 
            // editPanel
            // 
            this.tablePanel.SetColumnSpan(this.editPanel, 2);
            this.editPanel.Controls.Add(this.iconOptionsPanel);
            this.editPanel.Controls.Add(this.bgOptionsPanel);
            this.editPanel.Controls.Add(this.iconPictureBox);
            this.editPanel.Controls.Add(this.bgPictureBox);
            this.editPanel.Controls.Add(this.contentBox);
            this.editPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editPanel.Location = new System.Drawing.Point(3, 33);
            this.editPanel.Name = "editPanel";
            this.editPanel.Size = new System.Drawing.Size(340, 115);
            this.editPanel.TabIndex = 4;
            // 
            // iconOptionsPanel
            // 
            this.iconOptionsPanel.AutoScroll = true;
            this.iconOptionsPanel.Location = new System.Drawing.Point(44, 58);
            this.iconOptionsPanel.Name = "iconOptionsPanel";
            this.iconOptionsPanel.Size = new System.Drawing.Size(293, 50);
            this.iconOptionsPanel.TabIndex = 6;
            // 
            // bgOptionsPanel
            // 
            this.bgOptionsPanel.AutoScroll = true;
            this.bgOptionsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bgOptionsPanel.Location = new System.Drawing.Point(44, 3);
            this.bgOptionsPanel.Name = "bgOptionsPanel";
            this.bgOptionsPanel.Size = new System.Drawing.Size(293, 50);
            this.bgOptionsPanel.TabIndex = 5;
            // 
            // iconPictureBox
            // 
            this.iconPictureBox.Location = new System.Drawing.Point(9, 71);
            this.iconPictureBox.Name = "iconPictureBox";
            this.iconPictureBox.Size = new System.Drawing.Size(24, 24);
            this.iconPictureBox.TabIndex = 4;
            this.iconPictureBox.TabStop = false;
            // 
            // bgPictureBox
            // 
            this.bgPictureBox.Location = new System.Drawing.Point(9, 16);
            this.bgPictureBox.Name = "bgPictureBox";
            this.bgPictureBox.Size = new System.Drawing.Size(24, 24);
            this.bgPictureBox.TabIndex = 3;
            this.bgPictureBox.TabStop = false;
            // 
            // contentBox
            // 
            this.contentBox.Location = new System.Drawing.Point(256, 88);
            this.contentBox.MinimumSize = new System.Drawing.Size(20, 20);
            this.contentBox.Name = "contentBox";
            this.contentBox.Size = new System.Drawing.Size(69, 20);
            this.contentBox.TabIndex = 2;
            this.contentBox.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.contentBox_DocumentCompleted);
            // 
            // AtlasInfoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.tablePanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AtlasInfoPanel";
            this.Size = new System.Drawing.Size(346, 176);
            this.tablePanel.ResumeLayout(false);
            this.tablePanel.PerformLayout();
            this.editPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bgPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tablePanel;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.WebBrowser contentBox;
        private System.Windows.Forms.Panel editPanel;
        private System.Windows.Forms.Panel bgOptionsPanel;
        private System.Windows.Forms.PictureBox iconPictureBox;
        private System.Windows.Forms.PictureBox bgPictureBox;
        private System.Windows.Forms.Panel iconOptionsPanel;
    }
}
