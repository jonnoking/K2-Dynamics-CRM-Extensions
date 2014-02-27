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
    public partial class CRMTaskPage : WizardPage
    {
        public CRMTaskPage(CRMClientEvent theEvent)
            : base(theEvent)
        {
            InitializeComponent();
            InitializeK2TextBox();
                       
        }

        private void InitializeK2TextBox()
        {
            if (base.DataObject != null)
            {
                k2txtCategory.PluginContext = base.DataObject;
                //k2txtCRMOrganisation.PluginContext = base.DataObject;
                //k2txtCRMServer.PluginContext = base.DataObject;
                k2txtDescription.PluginContext = base.DataObject;
                k2txtDueDate.PluginContext = base.DataObject;
                k2txtSubcategory.PluginContext = base.DataObject;
                k2txtSubject.PluginContext = base.DataObject;
            }
            k2txtCategory.AcceptsTab = false;
            //k2txtCRMOrganisation.AcceptsTab = false;
            //k2txtCRMServer.AcceptsTab = false;
            k2txtDescription.AcceptsTab = false;
            k2txtDueDate.AcceptsTab = false;
            k2txtSubcategory.AcceptsTab = false;
            k2txtSubject.AcceptsTab = false;

            #region Position Context Button
            k2txtCategory.ContextBrowserButton = btnContext_k2txtCategory;
            //k2txtCRMOrganisation.ContextBrowserButton = btnContext_k2txtCRMOrganisation;
            //k2txtCRMServer.ContextBrowserButton = btnContext_k2txtCRMServer;
            k2txtDescription.ContextBrowserButton = btnContext_k2txtDescription;
            k2txtDueDate.ContextBrowserButton = btnContext_k2txtDueDate;
            k2txtSubcategory.ContextBrowserButton = btnContext_k2txtSubcategory;
            k2txtSubject.ContextBrowserButton = btnContext_k2txtSubject;
            #endregion

            //k2txtEventName.FieldPartFilter = new Type[] { typeof(ValueTypePart) };
            //k2txtEventName.OverrideInfoMessage = true;
        }

        //This event is called when the Page is loaded.
        protected override bool OnActivate()
        {
            this.k2txtCategory.K2Field = this.EventItem.TaskCategory;
            this.k2txtDescription.K2Field = this.EventItem.TaskDescription;
            this.k2txtDueDate.K2Field = this.EventItem.TaskDueDate;
            this.k2txtSubcategory.K2Field = this.EventItem.TaskSubcategory;
            this.k2txtSubject.K2Field = this.EventItem.TaskSubject;

            return true;
        }

        //This event gets called when the page gets unloaded
        protected override bool OnDeactivate()
        {
            this.EventItem.TaskCategory = this.k2txtCategory.K2Field;
            this.EventItem.TaskDescription = this.k2txtDescription.K2Field;
            this.EventItem.TaskDueDate = this.k2txtDueDate.K2Field;
            this.EventItem.TaskSubcategory = this.k2txtSubcategory.K2Field;
            this.EventItem.TaskSubject = this.k2txtSubject.K2Field;

            return true;
        }

        //This event gets called when the Next/Finished button is clicked to 
        //validate that all required information has been entered into relevant areas
        protected override bool OnValidate()
        {
            if (k2txtSubject.IsEmpty == true)
            {
                ShowK2Error(k2txtSubject, "Please supply a Subject for the task.");
                return false;
            }
            else
            {
                HideK2Error(k2txtSubject);
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

    }
}
