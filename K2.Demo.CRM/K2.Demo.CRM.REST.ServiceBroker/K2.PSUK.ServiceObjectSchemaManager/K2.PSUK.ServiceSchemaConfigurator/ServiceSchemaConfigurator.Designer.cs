namespace K2.PSUK.ServiceObjectSchemaConfigurator
{
    partial class ServiceSchemaConfigurator
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddProperty = new System.Windows.Forms.Button();
            this.gridProperties = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.displayNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trueTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.k2TypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schemaPropertyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.openFileDialogSchema = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogSchema = new System.Windows.Forms.SaveFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.gridMethods = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.displayNameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.k2TypeDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schemaMethodBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnAddMethod = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSchemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSchemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importPropertiesListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MethodsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MethodsItemDelte = new System.Windows.Forms.ToolStripMenuItem();
            this.PropertiesContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PropertiesItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schemaPropertyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMethods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schemaMethodBindingSource)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.MethodsContextMenu.SuspendLayout();
            this.PropertiesContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Properties";
            // 
            // btnAddProperty
            // 
            this.btnAddProperty.Location = new System.Drawing.Point(12, 331);
            this.btnAddProperty.Name = "btnAddProperty";
            this.btnAddProperty.Size = new System.Drawing.Size(78, 23);
            this.btnAddProperty.TabIndex = 2;
            this.btnAddProperty.Text = "Add Property";
            this.btnAddProperty.UseVisualStyleBackColor = true;
            this.btnAddProperty.Click += new System.EventHandler(this.btnAddProperty_Click);
            // 
            // gridProperties
            // 
            this.gridProperties.AllowUserToAddRows = false;
            this.gridProperties.AllowUserToDeleteRows = false;
            this.gridProperties.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProperties.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProperties.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.displayNameDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.trueTypeDataGridViewTextBoxColumn,
            this.k2TypeDataGridViewTextBoxColumn});
            this.gridProperties.DataSource = this.schemaPropertyBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridProperties.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridProperties.Location = new System.Drawing.Point(12, 52);
            this.gridProperties.Name = "gridProperties";
            this.gridProperties.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProperties.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridProperties.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.gridProperties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridProperties.Size = new System.Drawing.Size(1044, 273);
            this.gridProperties.TabIndex = 3;
            this.gridProperties.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridProperties_CellMouseUp);
            this.gridProperties.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridProperties_CellDoubleClick);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // displayNameDataGridViewTextBoxColumn
            // 
            this.displayNameDataGridViewTextBoxColumn.DataPropertyName = "DisplayName";
            this.displayNameDataGridViewTextBoxColumn.HeaderText = "DisplayName";
            this.displayNameDataGridViewTextBoxColumn.Name = "displayNameDataGridViewTextBoxColumn";
            this.displayNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // trueTypeDataGridViewTextBoxColumn
            // 
            this.trueTypeDataGridViewTextBoxColumn.DataPropertyName = "TrueType";
            this.trueTypeDataGridViewTextBoxColumn.HeaderText = "TrueType";
            this.trueTypeDataGridViewTextBoxColumn.Name = "trueTypeDataGridViewTextBoxColumn";
            this.trueTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // k2TypeDataGridViewTextBoxColumn
            // 
            this.k2TypeDataGridViewTextBoxColumn.DataPropertyName = "K2Type";
            this.k2TypeDataGridViewTextBoxColumn.HeaderText = "K2Type";
            this.k2TypeDataGridViewTextBoxColumn.Name = "k2TypeDataGridViewTextBoxColumn";
            this.k2TypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // schemaPropertyBindingSource
            // 
            this.schemaPropertyBindingSource.DataSource = typeof(K2.PSUK.ServiceObjectSchema.SchemaObject.SchemaProperty);
            // 
            // openFileDialogSchema
            // 
            this.openFileDialogSchema.FileName = "openFileDialog1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 363);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Methods";
            // 
            // gridMethods
            // 
            this.gridMethods.AllowUserToAddRows = false;
            this.gridMethods.AllowUserToDeleteRows = false;
            this.gridMethods.AutoGenerateColumns = false;
            this.gridMethods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMethods.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn1,
            this.displayNameDataGridViewTextBoxColumn1,
            this.descriptionDataGridViewTextBoxColumn1,
            this.k2TypeDataGridViewTextBoxColumn1});
            this.gridMethods.DataSource = this.schemaMethodBindingSource;
            this.gridMethods.Location = new System.Drawing.Point(12, 380);
            this.gridMethods.Name = "gridMethods";
            this.gridMethods.ReadOnly = true;
            this.gridMethods.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridMethods.Size = new System.Drawing.Size(1041, 278);
            this.gridMethods.TabIndex = 7;
            this.gridMethods.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridMethods_CellMouseUp);
            this.gridMethods.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridMethods_CellDoubleClick);
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            this.nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn1.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            this.nameDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // displayNameDataGridViewTextBoxColumn1
            // 
            this.displayNameDataGridViewTextBoxColumn1.DataPropertyName = "DisplayName";
            this.displayNameDataGridViewTextBoxColumn1.HeaderText = "DisplayName";
            this.displayNameDataGridViewTextBoxColumn1.Name = "displayNameDataGridViewTextBoxColumn1";
            this.displayNameDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // descriptionDataGridViewTextBoxColumn1
            // 
            this.descriptionDataGridViewTextBoxColumn1.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn1.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn1.Name = "descriptionDataGridViewTextBoxColumn1";
            this.descriptionDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // k2TypeDataGridViewTextBoxColumn1
            // 
            this.k2TypeDataGridViewTextBoxColumn1.DataPropertyName = "K2Type";
            this.k2TypeDataGridViewTextBoxColumn1.HeaderText = "K2Type";
            this.k2TypeDataGridViewTextBoxColumn1.Name = "k2TypeDataGridViewTextBoxColumn1";
            this.k2TypeDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // schemaMethodBindingSource
            // 
            this.schemaMethodBindingSource.DataSource = typeof(K2.PSUK.ServiceObjectSchema.SchemaObject.SchemaMethod);
            // 
            // btnAddMethod
            // 
            this.btnAddMethod.Location = new System.Drawing.Point(12, 664);
            this.btnAddMethod.Name = "btnAddMethod";
            this.btnAddMethod.Size = new System.Drawing.Size(75, 23);
            this.btnAddMethod.TabIndex = 8;
            this.btnAddMethod.Text = "Add Method";
            this.btnAddMethod.UseVisualStyleBackColor = true;
            this.btnAddMethod.Click += new System.EventHandler(this.btnAddMethod_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(1084, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSchemaToolStripMenuItem,
            this.saveSchemaToolStripMenuItem,
            this.importPropertiesListToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(35, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // loadSchemaToolStripMenuItem
            // 
            this.loadSchemaToolStripMenuItem.Name = "loadSchemaToolStripMenuItem";
            this.loadSchemaToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.loadSchemaToolStripMenuItem.Text = "Load Schema";
            this.loadSchemaToolStripMenuItem.Click += new System.EventHandler(this.loadSchemaToolStripMenuItem_Click);
            // 
            // saveSchemaToolStripMenuItem
            // 
            this.saveSchemaToolStripMenuItem.Name = "saveSchemaToolStripMenuItem";
            this.saveSchemaToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.saveSchemaToolStripMenuItem.Text = "Save Schema";
            this.saveSchemaToolStripMenuItem.Click += new System.EventHandler(this.saveSchemaToolStripMenuItem_Click);
            // 
            // importPropertiesListToolStripMenuItem
            // 
            this.importPropertiesListToolStripMenuItem.Name = "importPropertiesListToolStripMenuItem";
            this.importPropertiesListToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.importPropertiesListToolStripMenuItem.Text = "Import Properties List";
            // 
            // MethodsContextMenu
            // 
            this.MethodsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MethodsItemDelte});
            this.MethodsContextMenu.Name = "MethodsContextMenu";
            this.MethodsContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.MethodsContextMenu.Size = new System.Drawing.Size(117, 26);
            // 
            // MethodsItemDelte
            // 
            this.MethodsItemDelte.Name = "MethodsItemDelte";
            this.MethodsItemDelte.Size = new System.Drawing.Size(116, 22);
            this.MethodsItemDelte.Text = "Delete";
            this.MethodsItemDelte.Click += new System.EventHandler(this.MethodsItemDelete_Click);
            // 
            // PropertiesContextMenu
            // 
            this.PropertiesContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PropertiesItemDelete});
            this.PropertiesContextMenu.Name = "PropertiesContextMenu";
            this.PropertiesContextMenu.Size = new System.Drawing.Size(117, 26);
            // 
            // PropertiesItemDelete
            // 
            this.PropertiesItemDelete.Name = "PropertiesItemDelete";
            this.PropertiesItemDelete.Size = new System.Drawing.Size(116, 22);
            this.PropertiesItemDelete.Text = "Delete";
            this.PropertiesItemDelete.Click += new System.EventHandler(this.PropertiesItemDelete_Click);
            // 
            // ServiceSchemaConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 715);
            this.Controls.Add(this.btnAddMethod);
            this.Controls.Add(this.gridMethods);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gridProperties);
            this.Controls.Add(this.btnAddProperty);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ServiceSchemaConfigurator";
            this.Text = "Service Schema Configurator";
            ((System.ComponentModel.ISupportInitialize)(this.gridProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schemaPropertyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMethods)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schemaMethodBindingSource)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.MethodsContextMenu.ResumeLayout(false);
            this.PropertiesContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddProperty;
        private System.Windows.Forms.DataGridView gridProperties;
        private System.Windows.Forms.OpenFileDialog openFileDialogSchema;
        private System.Windows.Forms.SaveFileDialog saveFileDialogSchema;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView gridMethods;
        private System.Windows.Forms.Button btnAddMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn displayNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trueTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn k2TypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource schemaPropertyBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn displayNameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn k2TypeDataGridViewTextBoxColumn1;
        private System.Windows.Forms.BindingSource schemaMethodBindingSource;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadSchemaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSchemaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importPropertiesListToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip MethodsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem MethodsItemDelte;
        private System.Windows.Forms.ContextMenuStrip PropertiesContextMenu;
        private System.Windows.Forms.ToolStripMenuItem PropertiesItemDelete;
    }
}

