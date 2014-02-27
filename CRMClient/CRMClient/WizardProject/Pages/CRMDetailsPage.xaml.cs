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
using SourceCode.Workflow.Plugins.Crm;
using DesignCRMClient;

namespace WizardCRMClient.Pages
{
    public partial class CRMDetailsPage : WizardPage
    {
        public CRMDetailsPage(CRMClientEvent theEvent)
            : base(theEvent)
        {
            InitializeComponent();
            InitializeK2TextBox();
        }

        private void InitializeK2TextBox()
        {



            if (base.DataObject != null)
            {
                //k2txtCustomServiceURL.PluginContext = base.DataObject;
                k2txtCRMOrganisation.PluginContext = base.DataObject;
                k2txtCRMServer.PluginContext = base.DataObject;
                k2txtSmartObectServer.PluginContext = base.DataObject;
                k2txtCRMFunctionsSmO.PluginContext = base.DataObject;
                k2txtEntityId.PluginContext = base.DataObject;
                k2txtEntityType.PluginContext = base.DataObject;
                
            }
            //k2txtCustomServiceURL.AcceptsTab = false;
            k2txtCRMOrganisation.AcceptsTab = false;
            k2txtCRMServer.AcceptsTab = false;
            k2txtEntityId.AcceptsTab = false;
            k2txtEntityType.AcceptsTab = false;
            k2txtCRMFunctionsSmO.AcceptsTab = false;
            k2txtSmartObectServer.AcceptsTab = false;
            

            #region Position Context Button
            //k2txtCustomServiceURL.ContextBrowserButton = btnContext_k2txtCustomServiceURL;
            k2txtCRMOrganisation.ContextBrowserButton = btnContext_k2txtCRMOrganisation;
            k2txtCRMServer.ContextBrowserButton = btnContext_k2txtCRMServer;
            k2txtEntityId.ContextBrowserButton = btnContext_k2txtEntityId;
            k2txtEntityType.ContextBrowserButton = btnContext_k2txtEntityType;
            k2txtSmartObectServer.ContextBrowserButton = btnContext_k2txtSmartObectServer;
            k2txtCRMFunctionsSmO.ContextBrowserButton = btnContext_k2txtCRMFunctionsSmO;
            
            #endregion


            k2txtEventName.FieldPartFilter = new Type[] { typeof(ValueTypePart) };
            k2txtEventName.OverrideInfoMessage = true;

            // set defaults
            k2txtCRMServer.FieldPartFilter = new System.Type[] { typeof(SourceCode.Workflow.Design.ArtifactLibraryFieldPart) };
            k2txtCRMServer.DefaultTypeToSelect = typeof(SourceCode.Workflow.Plugins.Crm.CrmServerField);
            k2txtCRMServer.GetDefaultField();

            //k2txtCRMOrganisation.FieldPartFilter = new System.Type[] { typeof(SourceCode.Workflow.Design.ArtifactLibraryFieldPart) };
            //k2txtCRMOrganisation.DefaultTypeToSelect = typeof(SourceCode.Workflow.Plugins.Crm.CrmOrganizationPluginField);
            //k2txtCRMOrganisation.GetDefaultField();

            k2txtSmartObectServer.FieldPartFilter = new System.Type[] { typeof(SourceCode.Workflow.Design.ArtifactLibraryFieldPart) };
            k2txtSmartObectServer.DefaultTypeToSelect = typeof(SourceCode.Workflow.Design.EnvironmentSettings.SmartObjectField);
            k2txtSmartObectServer.GetDefaultField();

            //k2txtCRMFunctionsSmO.FieldPartFilter = new System.Type[] { typeof(SourceCode.Workflow.Design.SmartObjectFieldPart), typeof(ValueTypePart) };

        }
        //This event is called when the Page is loaded.
        protected override bool OnActivate()
        {
            this.k2txtEventName.K2Field = new K2Field(new K2FieldPart[] { new ValueTypePart(this.Event.Name) });
            //this.k2txtCustomServiceURL.K2Field = this.EventItem.CustomServiceURL;
            this.k2txtCRMOrganisation.K2Field = this.EventItem.CRMOrganisation;
            this.k2txtCRMServer.K2Field = this.EventItem.CRMServerURL;
            this.k2txtCRMFunctionsSmO.K2Field = this.EventItem.CRMFunctionsSmartObject;
            this.k2txtSmartObectServer.K2Field = this.EventItem.SmartObjectServer;
            this.k2txtEntityId.K2Field = this.EventItem.CRMEntityId;
            this.k2txtEntityType.K2Field = this.EventItem.CRMEntityType;
            
            return true;
        }

        //This event gets called when the page gets unloaded
        protected override bool OnDeactivate()
        {
            this.Event.Name = this.k2txtEventName.K2Field.DesignTimeValue;
            //this.EventItem.CustomServiceURL = this.k2txtCustomServiceURL.K2Field;
            this.EventItem.CRMOrganisation = this.k2txtCRMOrganisation.K2Field;
            this.EventItem.CRMServerURL = this.k2txtCRMServer.K2Field;
            this.EventItem.CRMEntityId = this.k2txtEntityId.K2Field;
            this.EventItem.CRMEntityType = this.k2txtEntityType.K2Field;
            this.EventItem.SmartObjectServer = this.k2txtSmartObectServer.K2Field;
            this.EventItem.CRMFunctionsSmartObject = this.k2txtCRMFunctionsSmO.K2Field;
            

            this.SucceedingRule.CRMOrganisation = this.k2txtCRMOrganisation.K2Field;
            this.SucceedingRule.CRMServerURL = this.k2txtCRMServer.K2Field;
            this.SucceedingRule.CRMEntityId = this.k2txtEntityId.K2Field;
            this.SucceedingRule.CRMEntityType = this.k2txtEntityType.K2Field;
            this.SucceedingRule.SmartObjectServer = this.k2txtSmartObectServer.K2Field;
            this.SucceedingRule.CRMFunctionsSmartObject = this.k2txtCRMFunctionsSmO.K2Field;
            //this.SucceedingRule.CustomServiceURL = this.k2txtCustomServiceURL.K2Field;

            this.ActivitySucceedingRule.CRMOrganisation = this.k2txtCRMOrganisation.K2Field;
            this.ActivitySucceedingRule.CRMServerURL = this.k2txtCRMServer.K2Field;
            this.ActivitySucceedingRule.CRMEntityId = this.k2txtEntityId.K2Field;
            this.ActivitySucceedingRule.CRMEntityType = this.k2txtEntityType.K2Field;
            this.ActivitySucceedingRule.SmartObjectServer = this.k2txtSmartObectServer.K2Field;
            this.ActivitySucceedingRule.CRMFunctionsSmartObject = this.k2txtCRMFunctionsSmO.K2Field;

            this.ProcessFinishRule.CRMOrganisation = this.k2txtCRMOrganisation.K2Field;
            this.ProcessFinishRule.CRMServerURL = this.k2txtCRMServer.K2Field;
            //this.ProcessFinishRule.CRMEntityId = this.k2txtEntityId.K2Field;
            //this.ProcessFinishRule.CRMEntityType = this.k2txtEntityType.K2Field;
            this.ProcessFinishRule.SmartObjectServer = this.k2txtSmartObectServer.K2Field;
            this.ProcessFinishRule.CRMFunctionsSmartObject = this.k2txtCRMFunctionsSmO.K2Field;

            //this.ActivitySucceedingRule.CustomServiceURL = this.k2txtCustomServiceURL.K2Field;
            
            
            return true;
        }

        //This event gets called when the Next/Finished button is clicked to 
        //validate that all required information has been entered into relevant areas
        protected override bool OnValidate()
        {
            if (k2txtEventName.IsEmpty == true)
            {
                //Show K2 Error message
                ShowK2Error(k2txtEventName, "Please supply an Event Name.");
                return false;
            }
            else
            {
                HideK2Error(k2txtEventName);
            }

            if (k2txtSmartObectServer.IsEmpty == true)
            {
                //Show K2 Error message
                ShowK2Error(k2txtSmartObectServer, "Please specify a K2 SmartObject Server.");
                return false;
            }
            else
            {
                HideK2Error(k2txtSmartObectServer);
            }

            if (k2txtCRMFunctionsSmO.IsEmpty == true)
            {
                //Show K2 Error message
                ShowK2Error(k2txtCRMFunctionsSmO, "Please specify a K2 CRM Functions SmartObject.");
                return false;
            }
            else
            {
                HideK2Error(k2txtCRMFunctionsSmO);
            }


            //if (k2txtCustomServiceURL.IsEmpty == true)
            //{
            //    //Show K2 Error message
            //    ShowK2Error(k2txtCustomServiceURL, "Please specify a K2 CRM Services URL.");
            //    return false;
            //}
            //else
            //{
            //    HideK2Error(k2txtCustomServiceURL);
            //}

            if (k2txtCRMServer.IsEmpty == true)
            {
                //Show K2 Error message
                ShowK2Error(k2txtCRMServer, "Please specify a CRM Server URL.");
                return false;
            }
            else
            {
                HideK2Error(k2txtCRMServer);
            }

            if (k2txtCRMOrganisation.IsEmpty == true)
            {
                //Show K2 Error message
                ShowK2Error(k2txtCRMOrganisation, "Please specifiy an Organisation.");
                return false;
            }
            else
            {
                HideK2Error(k2txtCRMOrganisation);
            }

            if (k2txtEntityId.IsEmpty == true)
            {
                //Show K2 Error message
                ShowK2Error(k2txtEntityId, "Please specify the Entity Id");
                return false;
            }
            else
            {
                HideK2Error(k2txtEntityId);
            }

            if (k2txtEntityType.IsEmpty == true)
            {
                //Show K2 Error message
                ShowK2Error(k2txtEntityType, "Please specify the Entity Type (e.g.lead or incident)");
                return false;
            }
            else
            {
                HideK2Error(k2txtEntityType);
            }


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
        public CRMClientActivitySucceedingRule ActivitySucceedingRule
        {
            get { return (CRMClientActivitySucceedingRule)(base.DataObject as CRMClientEvent).Activity.SucceedingRule; }
        }
        public CRMClientProcessFinishRule ProcessFinishRule
        {
            get { return (CRMClientProcessFinishRule)(base.DataObject as CRMClientEvent).Activity.Process.FinishRule; }
        }

    }
}
