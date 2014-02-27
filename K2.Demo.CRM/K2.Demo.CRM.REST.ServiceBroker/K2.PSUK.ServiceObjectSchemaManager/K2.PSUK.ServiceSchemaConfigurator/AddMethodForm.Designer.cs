namespace K2.PSUK.ServiceObjectSchemaConfigurator
{
    partial class AddMethodForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gridProperties = new System.Windows.Forms.DataGridView();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.schemaMethodPropertyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.propertyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inputDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.returnDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.requiredDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schemaMethodPropertyBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Display Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(91, 13);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(202, 20);
            this.txtName.TabIndex = 2;
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Location = new System.Drawing.Point(91, 39);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(202, 20);
            this.txtDisplayName.TabIndex = 3;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(91, 66);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(202, 20);
            this.txtDescription.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Description";
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(91, 93);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(202, 21);
            this.cmbType.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Type";
            // 
            // gridProperties
            // 
            this.gridProperties.AllowUserToAddRows = false;
            this.gridProperties.AllowUserToDeleteRows = false;
            this.gridProperties.AutoGenerateColumns = false;
            this.gridProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProperties.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.propertyDataGridViewTextBoxColumn,
            this.inputDataGridViewCheckBoxColumn,
            this.returnDataGridViewCheckBoxColumn,
            this.requiredDataGridViewCheckBoxColumn});
            this.gridProperties.DataSource = this.schemaMethodPropertyBindingSource;
            this.gridProperties.Location = new System.Drawing.Point(15, 120);
            this.gridProperties.Name = "gridProperties";
            this.gridProperties.ReadOnly = true;
            this.gridProperties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridProperties.Size = new System.Drawing.Size(693, 263);
            this.gridProperties.TabIndex = 8;
            this.gridProperties.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridProperties_CellContentClick);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(551, 390);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(633, 389);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // schemaMethodPropertyBindingSource
            // 
            this.schemaMethodPropertyBindingSource.DataSource = typeof(K2.PSUK.ServiceObjectSchema.SchemaMethodProperty);
            // 
            // propertyDataGridViewTextBoxColumn
            // 
            this.propertyDataGridViewTextBoxColumn.DataPropertyName = "Property";
            this.propertyDataGridViewTextBoxColumn.HeaderText = "Property";
            this.propertyDataGridViewTextBoxColumn.Name = "propertyDataGridViewTextBoxColumn";
            // 
            // inputDataGridViewCheckBoxColumn
            // 
            this.inputDataGridViewCheckBoxColumn.DataPropertyName = "Input";
            this.inputDataGridViewCheckBoxColumn.HeaderText = "Input";
            this.inputDataGridViewCheckBoxColumn.Name = "inputDataGridViewCheckBoxColumn";
            // 
            // returnDataGridViewCheckBoxColumn
            // 
            this.returnDataGridViewCheckBoxColumn.DataPropertyName = "Return";
            this.returnDataGridViewCheckBoxColumn.HeaderText = "Return";
            this.returnDataGridViewCheckBoxColumn.Name = "returnDataGridViewCheckBoxColumn";
            // 
            // requiredDataGridViewCheckBoxColumn
            // 
            this.requiredDataGridViewCheckBoxColumn.DataPropertyName = "Required";
            this.requiredDataGridViewCheckBoxColumn.HeaderText = "Required";
            this.requiredDataGridViewCheckBoxColumn.Name = "requiredDataGridViewCheckBoxColumn";
            // 
            // AddMethodForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 424);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.gridProperties);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtDisplayName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddMethodForm";
            this.Text = "Add Method";
            ((System.ComponentModel.ISupportInitialize)(this.gridProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schemaMethodPropertyBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox txtName;
        internal System.Windows.Forms.TextBox txtDisplayName;
        internal System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.DataGridView gridProperties;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn inputDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn returnDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn requiredDataGridViewCheckBoxColumn;
        private System.Windows.Forms.BindingSource schemaMethodPropertyBindingSource;
    }
}