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
    public partial class ServiceSchemaConfigurator : Form
    {

        SchemaObject schemaObject;

        public ServiceSchemaConfigurator()
        {
            InitializeComponent();
            schemaObject = new SchemaObject();
        }

        private void btnAddProperty_Click(object sender, EventArgs e)
        {
            AddPropertyForm frmAddProperty = new AddPropertyForm();
            frmAddProperty.ShowDialog();

            if (frmAddProperty.DialogResult == DialogResult.OK)
            {
                schemaObject.AddProperty(frmAddProperty.txtProeprtyName.Text,frmAddProperty.txtDisplayName.Text,frmAddProperty.txtDescription.Text,frmAddProperty.cmbNativeType.Text,(SoType)Enum.Parse(typeof(SoType),frmAddProperty.cmbSmOType.SelectedItem.ToString()));
                updateGrids();
            }
        }

        private void updateGrids()
        {
            gridMethods.DataSource = null;
            gridProperties.DataSource = null;
            gridProperties.DataSource = schemaObject.SchemaProperties;
            gridMethods.DataSource = schemaObject.SchemaMethods;
            gridMethods.Refresh();
            gridProperties.Refresh();
        }

        private void gridProperties_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            K2.PSUK.ServiceObjectSchema.SchemaObject.SchemaProperty property = schemaObject.SchemaProperties[e.RowIndex];
            AddPropertyForm frmAddProperty = MapPropertyToform(property);

            if (frmAddProperty.ShowDialog() == DialogResult.OK)
            {
                schemaObject.SchemaProperties[e.RowIndex] = MapPropertyFormToSchemaProperty(frmAddProperty);
                updateGrids();
            }
        }

        private AddPropertyForm MapPropertyToform(K2.PSUK.ServiceObjectSchema.SchemaObject.SchemaProperty property)
        {
            AddPropertyForm frmAddProperty = new AddPropertyForm();

            frmAddProperty.txtDescription.Text = property.Description;
            frmAddProperty.txtDisplayName.Text = property.DisplayName;
            frmAddProperty.txtProeprtyName.Text = property.Name;
            string x = property.K2Type.ToString();
            frmAddProperty.cmbSmOType.SelectedItem = x;
            frmAddProperty.cmbNativeType.Text = property.TrueType;

            return frmAddProperty;
        }

        private K2.PSUK.ServiceObjectSchema.SchemaObject.SchemaProperty MapPropertyFormToSchemaProperty(AddPropertyForm frmAddProperty)
        {
            K2.PSUK.ServiceObjectSchema.SchemaObject.SchemaProperty property = new SchemaObject.SchemaProperty();

            property.Description = frmAddProperty.txtDescription.Text;
            property.DisplayName = frmAddProperty.txtDisplayName.Text;
            property.Name = frmAddProperty.txtProeprtyName.Text;
            property.K2Type = (SoType)Enum.Parse(typeof(SoType), frmAddProperty.cmbSmOType.SelectedValue.ToString());
            property.TrueType = frmAddProperty.cmbNativeType.Text;

            return property;
        }

        private void btnAddMethod_Click(object sender, EventArgs e)
        {
            if (schemaObject.SchemaProperties.Count <= 0)
            {
                MessageBox.Show("Add properties before trying to add a method");
            }
            else
            {
                AddMethodForm frmAddMethod = new AddMethodForm(schemaObject);
                if (frmAddMethod.ShowDialog() == DialogResult.OK)
                {
                    schemaObject.SchemaMethods.Add(schemaObject.UpdateMethod(frmAddMethod.schemaMethod, (List<SchemaMethodProperty>)frmAddMethod.gridProperties.DataSource));
                    updateGrids();
                }
            }
        }

        private void gridMethods_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddMethodForm frmAddMethod = new AddMethodForm(schemaObject, schemaObject.SchemaMethods[e.RowIndex]);
            if (frmAddMethod.ShowDialog() == DialogResult.OK)
            {
                schemaObject.SchemaMethods[e.RowIndex] = schemaObject.UpdateMethod(frmAddMethod.schemaMethod, (List<SchemaMethodProperty>)frmAddMethod.gridProperties.DataSource);
                updateGrids();
            }

        }

        private void loadSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogSchema.ShowDialog() == DialogResult.OK)
            {
                schemaObject = SchemaManager.LoadSchemaXMLFile(openFileDialogSchema.FileName);
                updateGrids();
            } 
        }

        private void saveSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialogSchema.ShowDialog() == DialogResult.OK)
            {
                SchemaManager.SaveSchemaXMLFile(schemaObject, saveFileDialogSchema.FileName);
            }
        }

        private void gridMethods_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                gridMethods.CurrentCell = gridMethods.Rows[e.RowIndex].Cells[e.ColumnIndex];
                MethodsContextMenu.Show(MousePosition.X, MousePosition.Y);
            }

        }

        private void MethodsItemDelete_Click(object sender, EventArgs e)
        {
            int Index = gridMethods.CurrentRow.Index;
            gridMethods.DataSource = null;
            schemaObject.SchemaMethods.RemoveAt(Index);
            updateGrids();
        }

        private void PropertiesItemDelete_Click(object sender, EventArgs e)
        {
            int Index = gridProperties.CurrentRow.Index;
            gridProperties.DataSource = null;
            schemaObject.SchemaProperties.RemoveAt(Index);
            updateGrids();
        }

        private void gridProperties_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                gridProperties.CurrentCell = gridProperties.Rows[e.RowIndex].Cells[e.ColumnIndex];
                PropertiesContextMenu.Show(MousePosition.X, MousePosition.Y);
            }
        }

    }
}