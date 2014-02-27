using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using K2.PSUK.ServiceObjectSchema;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;

namespace K2.PSUK.ServiceObjectSchemaConfigurator
{
    public partial class AddMethodForm : Form
    {

        internal SchemaObject schemaObject;
        internal SchemaObject.SchemaMethod schemaMethod;

        public AddMethodForm(SchemaObject schemaObject)
        {
            InitializeComponent();
            this.schemaObject = schemaObject;
            schemaMethod = new SchemaObject.SchemaMethod();

            PopulateListOfMethodTypes();
            PopulateListOfProperties();
        }

        private void PopulateListOfMethodTypes()
        {
            cmbType.DataSource = Enum.GetNames(typeof(MethodType));
        }

        public AddMethodForm(SchemaObject schemaObject, SchemaObject.SchemaMethod schemaMethod)
        {
            InitializeComponent();
            this.schemaObject = schemaObject;
            this.schemaMethod = schemaMethod;
            PopulateListOfProperties();
            PopulateListOfMethodTypes();
            txtName.Text = schemaMethod.Name;
            txtDescription.Text = schemaMethod.Description;
            txtDisplayName.Text = schemaMethod.DisplayName;
            cmbType.SelectedItem = schemaMethod.K2Type.ToString();
        }


        private void PopulateListOfProperties()
        {
           gridProperties.DataSource = schemaObject.GetMethodProperties(schemaMethod);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            schemaMethod.Name = txtName.Text;
            schemaMethod.Description = txtDescription.Text;
            schemaMethod.DisplayName = txtDisplayName.Text;
            schemaMethod.K2Type = (MethodType)Enum.Parse(typeof(MethodType),cmbType.SelectedValue.ToString());

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void gridProperties_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ((DataGridViewCheckBoxCell)gridProperties.Rows[e.RowIndex].Cells[e.ColumnIndex]).Value = !(bool)((DataGridViewCheckBoxCell)gridProperties.Rows[e.RowIndex].Cells[e.ColumnIndex]).Value;
        }
    }
}