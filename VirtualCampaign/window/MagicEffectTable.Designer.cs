namespace VirtualCampaign.window {
    partial class MagicEffectTable {
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
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.layoutTable = new System.Windows.Forms.TableLayoutPanel();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.newButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.schoolColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qualityColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.rarityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prefixColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.suffixColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.effectColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.layoutTable.SuspendLayout();
            this.controlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idColumn,
            this.nameColumn,
            this.typeColumn,
            this.schoolColumn,
            this.qualityColumn,
            this.rarityColumn,
            this.prefixColumn,
            this.suffixColumn,
            this.effectColumn,
            this.modsColumn});
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGrid.Location = new System.Drawing.Point(3, 3);
            this.dataGrid.MultiSelect = false;
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGrid.Size = new System.Drawing.Size(894, 587);
            this.dataGrid.TabIndex = 0;
            this.dataGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellValueChanged);
            this.dataGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGrid_RowsAdded);
            // 
            // layoutTable
            // 
            this.layoutTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.layoutTable.ColumnCount = 1;
            this.layoutTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutTable.Controls.Add(this.dataGrid, 0, 0);
            this.layoutTable.Controls.Add(this.controlPanel, 0, 1);
            this.layoutTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTable.Location = new System.Drawing.Point(0, 0);
            this.layoutTable.Name = "layoutTable";
            this.layoutTable.RowCount = 2;
            this.layoutTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.layoutTable.Size = new System.Drawing.Size(900, 633);
            this.layoutTable.TabIndex = 1;
            // 
            // controlPanel
            // 
            this.controlPanel.Controls.Add(this.newButton);
            this.controlPanel.Controls.Add(this.saveButton);
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlPanel.Location = new System.Drawing.Point(3, 596);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(894, 34);
            this.controlPanel.TabIndex = 1;
            // 
            // newButton
            // 
            this.newButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.newButton.Location = new System.Drawing.Point(683, 5);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(100, 23);
            this.newButton.TabIndex = 1;
            this.newButton.Text = "New Effect";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.saveButton.Location = new System.Drawing.Point(791, 5);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(100, 23);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Save Changes";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // idColumn
            // 
            this.idColumn.HeaderText = "ID";
            this.idColumn.Name = "idColumn";
            this.idColumn.Visible = false;
            // 
            // nameColumn
            // 
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.Name = "nameColumn";
            // 
            // typeColumn
            // 
            this.typeColumn.HeaderText = "Type";
            this.typeColumn.Items.AddRange(new object[] {
            "Triggered (Generic)",
            "Continuous (Generic)",
            "Triggered (Armor)",
            "Continuous (Armor)",
            "Triggered (Weapon)",
            "Continuous (Weapon)"});
            this.typeColumn.Name = "typeColumn";
            this.typeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.typeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // schoolColumn
            // 
            this.schoolColumn.HeaderText = "School";
            this.schoolColumn.Name = "schoolColumn";
            // 
            // qualityColumn
            // 
            this.qualityColumn.HeaderText = "Quality";
            this.qualityColumn.Items.AddRange(new object[] {
            "Neutral",
            "Positive",
            "Negative"});
            this.qualityColumn.Name = "qualityColumn";
            this.qualityColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.qualityColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // rarityColumn
            // 
            this.rarityColumn.HeaderText = "Rarity";
            this.rarityColumn.Name = "rarityColumn";
            this.rarityColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // prefixColumn
            // 
            this.prefixColumn.HeaderText = "Prefix";
            this.prefixColumn.Name = "prefixColumn";
            // 
            // suffixColumn
            // 
            this.suffixColumn.HeaderText = "Suffix";
            this.suffixColumn.Name = "suffixColumn";
            // 
            // effectColumn
            // 
            this.effectColumn.HeaderText = "Effect";
            this.effectColumn.Name = "effectColumn";
            // 
            // modsColumn
            // 
            this.modsColumn.HeaderText = "Mods";
            this.modsColumn.Name = "modsColumn";
            // 
            // MagicEffectTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutTable);
            this.Name = "MagicEffectTable";
            this.Size = new System.Drawing.Size(900, 633);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.layoutTable.ResumeLayout(false);
            this.controlPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.TableLayoutPanel layoutTable;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn typeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn schoolColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn qualityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rarityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn prefixColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn suffixColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn effectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modsColumn;
    }
}
