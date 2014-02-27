using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SourceCode.SmartObjects.Services.ServiceSDK;

namespace K2.PSUK.ServiceObjectSchemaConfigurator
{
    public partial class AddPropertyForm : Form
    {
        public AddPropertyForm()
        {
            InitializeComponent();
            InitialiseForm();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AddPropertyForm_Load(object sender, EventArgs e)
        {
            
        }

        private void PopulateSmOTypes()
        {

            cmbSmOType.DataSource = Enum.GetNames(typeof(SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType));

        }

        private void InitialiseForm()
        {
            PopulateSmOTypes();
        }
    }
}