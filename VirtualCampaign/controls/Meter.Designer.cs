namespace VirtualCampaign.controls {
    partial class Meter {
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
            this.curSpinner = new System.Windows.Forms.NumericUpDown();
            this.maxSpinner = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.curSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxSpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // curSpinner
            // 
            this.curSpinner.Dock = System.Windows.Forms.DockStyle.Left;
            this.curSpinner.Location = new System.Drawing.Point(0, 0);
            this.curSpinner.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.curSpinner.Name = "curSpinner";
            this.curSpinner.Size = new System.Drawing.Size(74, 20);
            this.curSpinner.TabIndex = 0;
            this.curSpinner.Visible = false;
            this.curSpinner.ValueChanged += new System.EventHandler(this.curSpinner_ValueChanged);
            // 
            // maxSpinner
            // 
            this.maxSpinner.Dock = System.Windows.Forms.DockStyle.Right;
            this.maxSpinner.Location = new System.Drawing.Point(74, 0);
            this.maxSpinner.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.maxSpinner.Name = "maxSpinner";
            this.maxSpinner.Size = new System.Drawing.Size(74, 20);
            this.maxSpinner.TabIndex = 1;
            this.maxSpinner.Visible = false;
            this.maxSpinner.ValueChanged += new System.EventHandler(this.maxSpinner_ValueChanged);
            // 
            // Meter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.maxSpinner);
            this.Controls.Add(this.curSpinner);
            this.Name = "Meter";
            this.Size = new System.Drawing.Size(148, 22);
            this.Load += new System.EventHandler(this.Meter_Load);
            this.SizeChanged += new System.EventHandler(this.Meter_SizeChanged);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Meter_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.curSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxSpinner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown curSpinner;
        private System.Windows.Forms.NumericUpDown maxSpinner;
    }
}
