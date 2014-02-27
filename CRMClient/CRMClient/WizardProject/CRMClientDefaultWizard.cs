//////////////////////////////////////////////////////////////////////////
//																		//
//																		//
//																		//
//																		//
//																		//
//																		//
//////////////////////////////////////////////////////////////////////////
using System;
using System.Text;
using System.Xml;
using System.Drawing;

using SourceCode.Framework;
using SourceCode.Framework.Design;
using SourceCode.Workflow.Authoring;
using SourceCode.Workflow.Authoring.Design;
using SourceCode.Workflow.WizardFramework;
using SourceCode.Workflow.Design;
using SourceCode.Configuration;
using SourceCode.Workflow.Wizards;

using DesignCRMClient;

namespace WizardCRMClient
{
    //This GUID value for the RegistrationName is Auto-generated and is required to uniquely identify this wizard
    //This value MUST be reflected in the ConfigurationManager.config file
    [ConfigurationName("WizardCRMClient Event Wizard")]
    [DisplayName("WizardCRMClient.Properties.Resources", "WizardCRMClientDisplayName", typeof(CRMClientDefaultWizard))]
    [Description("WizardCRMClient.Properties.Resources", "WizardCRMClientDescription", typeof(CRMClientDefaultWizard))]
    [ToolboxBitmap(typeof(CRMClientDefaultWizard), "Resources.k2crmicon.bmp")]
    [SourceCode.Framework.Design.WizardToolboxItemAttribute()]
    public class CRMClientDefaultWizard : SourceCode.Workflow.WizardFramework.Wizard
    {
        //private CRMClientEvent _clientEvent = null;
        public CRMClientDefaultWizard() : base()
        {
            base.Definition = new DesignCRMClient.CRMClientWizardDefinition();            
        }

        protected override bool OnCanConfigureInstance(object parent)
        {
            if (parent is SourceCode.Workflow.Authoring.Process || parent is SourceCode.Workflow.Authoring.Activity)
            {
                return true;
            }
            return false;
        }


        //This event is fired when the Wizard is dropped onto an activity/the canvas.
        //public override void InitializeForNewExecution(WizardInitializeArgs e)
        //{
        //    base.InitializeForNewExecution(e);
        //    _clientEvent = new CRMClientEvent();
        //    _clientEvent.WizardDefinition = base.Definition;
        //    //(e.Parent as SourceCode.Workflow.Authoring.Activity).Events.Insert(e.InsertIndex, _CRMClientEventEvent);

        //    WizardHelper.GetEventActivity(e.Parent).Events.Insert(e.InsertIndex, _clientEvent);

        //    // add succeeding rules
        //    this.AddCodeFiles(this._clientEvent);
        //    this.AddCodeFiles(this._clientEvent.Activity);

        //    // add process finish rule
        //    this.AddCodeFiles(this._clientEvent.Activity.Process);
            

        //    base.Pages.Add(new WizardCRMClient.Pages.WelcomePage(_clientEvent));
        //    base.Pages.Add(new WizardCRMClient.Pages.CRMDetailsPage(_clientEvent));
        //    base.Pages.Add(new WizardCRMClient.Pages.CRMClientPage(_clientEvent));
        //    base.Pages.Add(new WizardCRMClient.Pages.CRMTaskPage(_clientEvent));
        //    base.Pages.Add(new SourceCode.Workflow.Wizards.Notification.EventNotificationPage(_clientEvent, "CRM Client Event"));

        //    #region Outcomes Pages
        //    base.Pages.Add(new SourceCode.Workflow.Wizards.Outcome.ActionPropertyPage(_clientEvent));
        //    base.Pages.Add(new SourceCode.Workflow.Wizards.Outcome.OutcomePropertyPage(_clientEvent));
        //    #endregion

        //    #region Destination Rule Pages
        //    WizardHelper.AddDestinationPageInWizardIfNeeded(_clientEvent.Activity, base.Pages);
        //    #endregion

        //    base.Pages.Add(new SourceCode.Workflow.Wizards.DefaultClient.FinishedPage());

        //    // create datafields for Entity Id & Entity Name. These are used by the K2 CRM Plugin and K2 CRM Workflow Activity
        //    if (!_clientEvent.Activity.Process.DataFields.Contains("Entity Id"))
        //    {
        //        _clientEvent.Activity.Process.DataFields.Add(new DataField() { Name = "Entity Id", Hidden = false, Type = DataTypes.String, OnDemand = true, Scope = FieldScope.Process, Log = false, Audit = false });
        //    }
        //    if (!_clientEvent.Activity.Process.DataFields.Contains("Entity Name"))
        //    {
        //        _clientEvent.Activity.Process.DataFields.Add(new DataField() { Name = "Entity Name", Hidden = false, Type = DataTypes.String, OnDemand = true, Scope = FieldScope.Process, Log = false, Audit = false });
        //    }

        //    // create xmlfield for CRM Context. This is used by the K2 CRM Plugin and K2 CRM Workflow Activity
        //    if (!_clientEvent.Activity.Process.XmlFields.Contains("CRM Context"))
        //    {
        //        _clientEvent.Activity.Process.XmlFields.Add(new XmlField() { 
        //            Name = "CRM Context", 
        //            Hidden = false, 
        //            OnDemand = false, 
        //            Log = false, 
        //            Audit = false, 
        //            Category = "Custom Wizards",
        //            Scope = FieldScope.Process, 
        //            SchemaURI = this.GetXMLFieldSchemaString("CRM Context", Resources.CRMContext)
        //        });
        //    }

        //    // create xmlfield for CRM Tasks. This is used by the process to track generated CRM Tasks
        //    if (!_clientEvent.Activity.Process.XmlFields.Contains("CRM Tasks"))
        //    {
        //        _clientEvent.Activity.Process.XmlFields.Add(new XmlField() { 
        //            Name = "CRM Tasks", 
        //            Hidden = false, 
        //            OnDemand = false, 
        //            Log = false, 
        //            Audit = false,
        //            Category = "Custom Wizards",
        //            Scope = FieldScope.Process, Value="<CRMTasks></CRMTasks>",
        //            SchemaURI = this.GetXMLFieldSchemaString("CRM Tasks", Resources.CRMTasks)
        //        });
        //    }

        //    this._clientEvent.EventItem.CRMEntityId.Parts.Add(new DataFieldPart(_clientEvent.Activity.Process.DataFields["Entity Id"]));
        //    this._clientEvent.EventItem.CRMEntityType.Parts.Add(new DataFieldPart(_clientEvent.Activity.Process.DataFields["Entity Name"]));
        //    // set default values for Wizard
        //    this._clientEvent.EventItem.TaskOwnerFQN.Parts.Add(new WorkflowContextFieldPart(WorkflowContextProperty.ActivityInstanceDestUserName));
        //    this._clientEvent.EventItem.ProcessName.Parts.Add(new WorkflowContextFieldPart(WorkflowContextProperty.ProcessName));
        //    this._clientEvent.EventItem.ActivityName.Parts.Add(new WorkflowContextFieldPart(WorkflowContextProperty.ActivityInstanceName));
        //    this._clientEvent.EventItem.TaskCategory.Parts.Add(new WorkflowContextFieldPart(WorkflowContextProperty.ProcessName));
        //    this._clientEvent.EventItem.TaskSubcategory.Parts.Add(new WorkflowContextFieldPart(WorkflowContextProperty.ActivityInstanceName));


        //    // set destination rule to Plan per destination - all at once.
        //    // required to generate a CRM task per destination user.
        //    this._clientEvent.Activity.Type = SourceCode.Workflow.Authoring.ActivityTypes.DestinationInstanceParralel;
        //    if (this._clientEvent.Activity.DestinationRule == null)
        //    {
        //        this._clientEvent.Activity.DestinationRule = new SourceCode.Workflow.Design.SimpleRules.SimpleDestinationRule();
        //    }

        //    if (this._clientEvent.Activity.DestinationRule is SourceCode.Workflow.Design.SimpleRules.SimpleDestinationRule)
        //    {
        //        SourceCode.Workflow.Design.SimpleRules.SimpleDestinationRule simplerule = (SourceCode.Workflow.Design.SimpleRules.SimpleDestinationRule)this._clientEvent.Activity.DestinationRule;
        //        simplerule.ResolveQueuesToUsers = true;

        //        if (simplerule.DestinationSets.Capacity == 0)
        //        {
        //            simplerule.DestinationSets.Add(new SourceCode.Workflow.Design.SimpleRules.DestinationSet("Default"));
        //        }
        //    }

        //}

        private void InitializeWizard(SourceCode.Framework.WizardInitializeArgs e)
        {
            DesignCRMClient.CRMClientEvent _clientEvent = null;

            switch (base.Status)
            {
                case SourceCode.Framework.WizardStatus.New:
                case SourceCode.Framework.WizardStatus.NewDelayed:
                    _clientEvent = new DesignCRMClient.CRMClientEvent();
                    _clientEvent.WizardDefinition = base.Definition;
                    SourceCode.Workflow.Wizards.WizardHelper.GetEventActivity(e.Parent).Events.Insert(e.InsertIndex, _clientEvent);

                    #region set some default values for Event/EventItem
                    _clientEvent.EventItem.CRMEntityId.Parts.Add(new SourceCode.Workflow.Design.DataFieldPart(_clientEvent.Activity.Process.DataFields["Entity Id"]));
                    _clientEvent.EventItem.CRMEntityType.Parts.Add(new SourceCode.Workflow.Design.DataFieldPart(_clientEvent.Activity.Process.DataFields["Entity Name"]));
                    _clientEvent.EventItem.TaskOwnerFQN.Parts.Add(new WorkflowContextFieldPart(WorkflowContextProperty.ActivityInstanceDestUserName));
                    _clientEvent.EventItem.ProcessName.Parts.Add(new WorkflowContextFieldPart(WorkflowContextProperty.ProcessName));
                    _clientEvent.EventItem.ActivityName.Parts.Add(new WorkflowContextFieldPart(WorkflowContextProperty.ActivityInstanceName));
                    _clientEvent.EventItem.TaskCategory.Parts.Add(new WorkflowContextFieldPart(WorkflowContextProperty.ProcessName));
                    _clientEvent.EventItem.TaskSubcategory.Parts.Add(new WorkflowContextFieldPart(WorkflowContextProperty.ActivityInstanceName));

                    #endregion

                    #region Configure SucceedingRule
                    //Configure any SucceedingRules. Ensure that it inherits from the correct OutcomeSucceedingRule object to ensure correct functionality in the Outcomes & Actions 

                    //    // add succeeding rules
                        this.AddCodeFiles(_clientEvent);
                        this.AddCodeFiles(_clientEvent.Activity);

                        // add process finish rule
                        this.AddCodeFiles(_clientEvent.Activity.Process);

                    #endregion

                    #region Configure Destination Rule
                        _clientEvent.Activity.Type = SourceCode.Workflow.Authoring.ActivityTypes.DestinationInstanceParralel;
                        if (_clientEvent.Activity.DestinationRule == null)
                    {
                        _clientEvent.Activity.DestinationRule = new SourceCode.Workflow.Design.SimpleRules.SimpleDestinationRule();
                    }

                        if (_clientEvent.Activity.DestinationRule is SourceCode.Workflow.Design.SimpleRules.SimpleDestinationRule)
                    {
                        SourceCode.Workflow.Design.SimpleRules.SimpleDestinationRule simplerule = (SourceCode.Workflow.Design.SimpleRules.SimpleDestinationRule)_clientEvent.Activity.DestinationRule;
                        simplerule.ResolveQueuesToUsers = true;

                        if (simplerule.DestinationSets.Capacity == 0)
                        {
                            simplerule.DestinationSets.Add(new SourceCode.Workflow.Design.SimpleRules.DestinationSet("Default"));
                        }
                    }
                    #endregion

                    if (base.Status == SourceCode.Framework.WizardStatus.NewDelayed)
                    {
                        return;
                    }

                    break;
                case SourceCode.Framework.WizardStatus.Delayed:
                case SourceCode.Framework.WizardStatus.Executed:
                    if (e.Parent is DesignCRMClient.CRMClientEvent)
                    {
                        _clientEvent = (DesignCRMClient.CRMClientEvent)e.Parent;
                    }
                    break;
            }
            base.Pages.Add(new WizardCRMClient.Pages.WelcomePage(_clientEvent));
            base.Pages.Add(new WizardCRMClient.Pages.CRMDetailsPage(_clientEvent));
            base.Pages.Add(new WizardCRMClient.Pages.CRMClientPage(_clientEvent));
            base.Pages.Add(new WizardCRMClient.Pages.CRMTaskPage(_clientEvent));

            #region Notification Pages
            base.Pages.Add(new SourceCode.Workflow.Wizards.Notification.EventNotificationPage(_clientEvent, "CRM Client Event"));
            #endregion

            #region Outcomes Pages
            base.Pages.Add(new SourceCode.Workflow.Wizards.Outcome.ActionPropertyPage(_clientEvent));
            base.Pages.Add(new SourceCode.Workflow.Wizards.Outcome.OutcomePropertyPage(_clientEvent));
            #endregion

            #region Destination Rule Pages
            SourceCode.Workflow.Wizards.WizardHelper.AddDestinationPageInWizardIfNeeded(_clientEvent.Activity, base.Pages);
            #endregion

            base.Pages.Add(new WizardCRMClient.Pages.FinishPage(_clientEvent));
        }
       

        public override void InitializeForNewExecution(SourceCode.Framework.WizardInitializeArgs e)
        {
            base.InitializeForNewExecution(e);
            this.InitializeWizard(e);
        }

        public override void InitializeForNewDelayedExecution(SourceCode.Framework.WizardInitializeArgs e)
        {
            base.InitializeForNewDelayedExecution(e);
            this.InitializeWizard(e);
        }

        public override void InitializeForDelayedExecution(SourceCode.Framework.WizardInitializeArgs e)
        {
            base.InitializeForDelayedExecution(e);
            this.InitializeWizard(e);
        }

        public override void InitializeForReExecution(SourceCode.Framework.WizardInitializeArgs e)
        {
            base.InitializeForReExecution(e);
            this.InitializeWizard(e);
        }

        protected override bool OnFinish(SourceCode.Framework.IWizardHostService host)
        {
            bool retVal = base.OnFinish(host);
            SourceCode.Workflow.WizardFramework.WizardPage firstPageWithError = SourceCode.Workflow.Wizards.WizardHelper.GetErrorPage(this.Host, this.Pages);
            if (firstPageWithError != null)
            {
                if (host.CurrentWizard.CurrentPage != firstPageWithError)
                {
                    host.CurrentWizard.CurrentPage = firstPageWithError;
                }
                retVal = false;
            }
            return retVal;
        }



        private void AddCodeFiles(Event eventItem)
        {
            CRMClientEventSucceedingRule succeedingRule = new CRMClientEventSucceedingRule();
            if (eventItem.SucceedingRule != null)
            {
                if (eventItem.SucceedingRule is CRMClientEventSucceedingRule)
                {
                    succeedingRule = (CRMClientEventSucceedingRule)eventItem.SucceedingRule;
                }
                else
                {
                    succeedingRule = new CRMClientEventSucceedingRule();
                }
            }
            else
            {
                succeedingRule = new CRMClientEventSucceedingRule();
            }
            if (eventItem.SucceedingRule != succeedingRule)
            {
                eventItem.SucceedingRule = succeedingRule;
            }
        }

        private void AddCodeFiles(Activity activity)
        {
            CRMClientActivitySucceedingRule succeedingRule = new CRMClientActivitySucceedingRule();
            if (activity.SucceedingRule != null)
            {
                if (activity.SucceedingRule is CRMClientActivitySucceedingRule)
                {
                    succeedingRule = (CRMClientActivitySucceedingRule)activity.SucceedingRule;
                }
                else
                {
                    succeedingRule = new CRMClientActivitySucceedingRule();
                }
            }
            else
            {
                succeedingRule = new CRMClientActivitySucceedingRule();
            }
            if (activity.SucceedingRule != succeedingRule)
            {
                activity.SucceedingRule = succeedingRule;
            }
        }

        private void AddCodeFiles(Process process)
        {
            CRMClientProcessFinishRule finishRule = new CRMClientProcessFinishRule();
            if (process.FinishRule != null)
            {
                if (process.FinishRule is CRMClientProcessFinishRule)
                {
                    finishRule = (CRMClientProcessFinishRule)process.FinishRule;
                }
                else
                {
                    finishRule = new CRMClientProcessFinishRule();
                }
            }
            else
            {
                finishRule = new CRMClientProcessFinishRule();
            }
            if (process.FinishRule != finishRule)
            {
                process.FinishRule = finishRule;
            }
        }

        //private string GetXMLFieldSchemaString(string xmlfieldname, string resourcexsdname)
        //{
        //    System.Xml.Schema.XmlSchema xmlSchema = new System.Xml.Schema.XmlSchema();
        //    System.Xml.Schema.XmlSchemaComplexType complexType = new System.Xml.Schema.XmlSchemaComplexType();
        //    System.Xml.Schema.XmlSchemaSequence sequence = new System.Xml.Schema.XmlSchemaSequence();
        //    complexType.Particle = sequence;
        //    System.Xml.Schema.XmlSchemaElement rootElement = new System.Xml.Schema.XmlSchemaElement();
        //    rootElement.SchemaType = complexType;
        //    rootElement.Name = xmlfieldname;
        //    xmlSchema.Items.Add(rootElement);
        //    StringBuilder sb = new StringBuilder(resourcexsdname);
        //    System.IO.StringWriter stream = new System.IO.StringWriter(sb);
        //    xmlSchema.Write(stream);
        //    string outstring = stream.ToString();
        //    stream.Close();
        //    stream.Dispose();
        //    return outstring;
        //}

    }
}
