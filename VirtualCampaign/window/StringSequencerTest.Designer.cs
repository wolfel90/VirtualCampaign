namespace VirtualCampaign.window {
    partial class StringSequencerTest {
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
            this.inBox = new System.Windows.Forms.TextBox();
            this.outBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // inBox
            // 
            this.inBox.Location = new System.Drawing.Point(12, 12);
            this.inBox.Name = "inBox";
            this.inBox.Size = new System.Drawing.Size(378, 20);
            this.inBox.TabIndex = 0;
            this.inBox.TextChanged += new System.EventHandler(this.inBox_TextChanged);
            // 
            // outBox
            // 
            this.outBox.Location = new System.Drawing.Point(12, 38);
            this.outBox.Multiline = true;
            this.outBox.Name = "outBox";
            this.outBox.ReadOnly = true;
            this.outBox.Size = new System.Drawing.Size(378, 234);
            this.outBox.TabIndex = 1;
            // 
            // StringSequencerTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 284);
            this.Controls.Add(this.outBox);
            this.Controls.Add(this.inBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StringSequencerTest";
            this.Text = "StringSequencerTest";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inBox;
        private System.Windows.Forms.TextBox outBox;
    }
}