using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

using SourceCode.Framework;
using SourceCode.Framework.Design;
using SourceCode.Workflow.Design;
using SourceCode.Workflow.Authoring;
using SourceCode.Workflow.WizardFramework;
using SourceCode.Workflow.WizardFramework.Controls;
using SourceCode.Workflow.VisualDesigners.WizardHost;

using DesignCRMClient;

namespace WizardCRMClient.Pages
{
    public partial class CRMClientPage : WizardPage
    {
        public CRMClientPage(CRMClientEvent theEvent)
            : base(theEvent)
        {
            InitializeComponent();
            InitializeK2TextBox();

            //Example of how to dynamically add a K2TextBox to a XAML page.
            //View XAML markup code for example of controls: namespace to register K2TextBox controls
            //I.e. <controls:K2TextBox Grid.Column="0" Grid.Row="0" Name="myK2TextBox" TabIndex="0" IsRequired="True"></controls:K2TextBox>

            //this.Background = new ImageBrush(SourceCode.Workflow.WizardFramework.Controls.CommonMethods.CreateBitmapFromSource(image file in Resource file, System.Drawing.Imaging.ImageFormat.Png));
        }

        private void InitializeK2TextBox()
        {
            if (base.DataObject != null)
            {
                k2txtCRMFormURL.PluginContext = base.DataObject;
                k2txtEntityForm.PluginContext = base.DataObject;
                k2txtCRMCustomSNParameter.PluginContext = base.DataObject;
            }

            k2txtCRMFormURL.AcceptsTab = false;
            k2txtCRMCustomSNParameter.AcceptsTab = false;
            k2txtEntityForm.AcceptsTab = false;

            #region Position Context Button
            k2txtCRMFormURL.ContextBrowserButton = btnContext_k2txtCRMFormURL;
            k2txtEntityForm.ContextBrowserButton = btnContext_k2txtEntityForm;
            k2txtCRMCustomSNParameter.ContextBrowserButton = btnContext_k2txtCRMCustomSNParameter;
            #endregion

        }


        //This event is called when the Page is loaded.
        protected override bool OnActivate()
        {
            
            // load CRM Entities


            this.k2txtCRMFormURL.K2Field = this.EventItem.CRMFormURL;
            this.k2txtEntityForm.K2Field = this.EventItem.CRMEntityForm;
            this.k2txtCRMCustomSNParameter.K2Field = this.EventItem.CRMCustomSNParameter;

            //this.chkCRMClientPage_AddTask.IsChecked = this.EventItem.CreateTasks;
            //this.chkCRMClientPage_InsertSN.IsChecked = this.EventItem.InsertSN;

            return true;
        }

        //This event gets called when the page gets unloaded
        protected override bool OnDeactivate()
        {
            this.EventItem.CRMEntityForm = this.k2txtEntityForm.K2Field;
            this.EventItem.CRMFormURL = this.k2txtCRMFormURL.K2Field;
            this.EventItem.InternetPlatform = ClientEventItem.WorklistPlatform.ASP.ToString();
            this.EventItem.CRMCustomSNParameter = this.k2txtCRMCustomSNParameter.K2Field;

            //this.WizardDefinition.DesignTimeSPSite = this._designTimeSPSite;
            //this.WizardDefinition.DesignTimeSPTaskList = this._designTimeSPTaskList;
            //this.WizardDefinition.DesignTimeContentType = this._designTimeContentType;

            return true;
        }

        //This event gets called when the Next/Finished button is clicked to 
        //validate that all required information has been entered into relevant areas
        protected override bool OnValidate()
        {
            return true;
        }

        public CRMClientEvent Event
        {
            get { return (base.DataObject as CRMClientEvent); }
        }

        public CRMClientEventItem EventItem
        {
            get { return (base.DataObject as CRMClientEvent).EventItem; }
        }

        public CRMClientWizardDefinition WizardDefinition
        {
            get { return (this.Event.WizardDefinition as CRMClientWizardDefinition); }
        }
        public CRMClientEventSucceedingRule SucceedingRule
        {
            get { return (CRMClientEventSucceedingRule)(base.DataObject as CRMClientEvent).SucceedingRule; }
        }

    }
}
