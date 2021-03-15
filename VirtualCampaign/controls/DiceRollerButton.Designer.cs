namespace VirtualCampaign.controls {
    partial class DiceRollerButton {
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
            components = new System.ComponentModel.Container();
            this.SuspendLayout();

            this.BackColor = System.Drawing.Color.Transparent;
            this.Image = global::VirtualCampaign.Properties.Resources.rollIcon;
            this.Location = new System.Drawing.Point(10, 7);
            this.Name = "rollButton";
            this.Size = new System.Drawing.Size(34, 27);
            this.TabIndex = 0;
            this.UseVisualStyleBackColor = false;
            this.Visible = false;
            this.Click += new System.EventHandler(this.rollButton_Click);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rollButton_MouseDown);
            this.Text = "";
            this.Size = new System.Drawing.Size(34, 27);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
