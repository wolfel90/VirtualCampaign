namespace VirtualCampaign.controls {
    partial class CompoundedStatField {
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
            this.valueLabel = new System.Windows.Forms.Label();
            this.valueSpinner = new System.Windows.Forms.NumericUpDown();
            this.confirmButton = new System.Windows.Forms.Button();
            this.editorPane = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.valueSpinner)).BeginInit();
            this.editorPane.SuspendLayout();
            this.SuspendLayout();
            // 
            // valueLabel
            // 
            this.valueLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.valueLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valueLabel.Location = new System.Drawing.Point(0, 0);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(200, 25);
            this.valueLabel.TabIndex = 0;
            this.valueLabel.Text = "0";
            this.valueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.valueLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.valueLabel_MouseClick);
            // 
            // valueSpinner
            // 
            this.valueSpinner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valueSpinner.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueSpinner.Location = new System.Drawing.Point(3, 3);
            this.valueSpinner.Name = "valueSpinner";
            this.valueSpinner.Size = new System.Drawing.Size(169, 20);
            this.valueSpinner.TabIndex = 1;
            this.valueSpinner.ValueChanged += new System.EventHandler(this.valueSpinner_ValueChanged);
            // 
            // confirmButton
            // 
            this.confirmButton.Image = global::VirtualCampaign.Properties.Resources.green_check;
            this.confirmButton.Location = new System.Drawing.Point(177, 3);
            this.confirmButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 0);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(21, 20);
            this.confirmButton.TabIndex = 2;
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // editorPane
            // 
            this.editorPane.ColumnCount = 2;
            this.editorPane.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.editorPane.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.editorPane.Controls.Add(this.valueSpinner, 0, 0);
            this.editorPane.Controls.Add(this.confirmButton, 1, 0);
            this.editorPane.Location = new System.Drawing.Point(2, 0);
            this.editorPane.Name = "editorPane";
            this.editorPane.RowCount = 1;
            this.editorPane.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.editorPane.Size = new System.Drawing.Size(200, 25);
            this.editorPane.TabIndex = 4;
            this.editorPane.Visible = false;
            // 
            // CompoundedStatField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.editorPane);
            this.Controls.Add(this.valueLabel);
            this.Name = "CompoundedStatField";
            this.Size = new System.Drawing.Size(200, 25);
            this.SizeChanged += new System.EventHandler(this.CompoundedStatField_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.valueSpinner)).EndInit();
            this.editorPane.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.NumericUpDown valueSpinner;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.TableLayoutPanel editorPane;
    }
}
