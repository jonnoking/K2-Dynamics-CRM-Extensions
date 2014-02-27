//////////////////////////////////////////////////////////////////////////
//																		//
//																		//
//																		//
//																		//
//																		//
//																		//
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using SourceCode.Framework;
using SourceCode.Workflow.Authoring;
using SourceCode.Workflow.Authoring.Design;
using SourceCode.Workflow.WizardFramework;
using SourceCode.Workflow.Design;
using SourceCode.Configuration;

using DesignCRMClient;

namespace WizardCRMClient
{
    //This property name is directly associated with the name attribute in the ConfigurationManager.config file
    [ConfigurationName("WizardCRMClient Property Wizard")]
    [DisplayName("WizardCRMClient.Properties.Resources", "WizardCRMClientPropertyDisplayName", typeof(CRMClientPropertyWizard))]
    [Description("WizardCRMClient.Properties.Resources", "WizardCRMClientPropertyDescription", typeof(CRMClientPropertyWizard))]  
    public class CRMClientPropertyWizard : PropertyWizard
    {
        private CRMClientEvent _clientEvent = null;

        public CRMClientPropertyWizard()
        {
            base.Definition = new DesignCRMClient.CRMClientPropertyWizardDefinition();
        }

        public override void Initialize(WizardInitializeArgs e)
        {
            base.Initialize(e);
            _clientEvent = (CRMClientEvent)e.Parent;

            // Add some pages for this wizard
            base.Pages.Add(new WizardCRMClient.Pages.CRMDetailsPage(_clientEvent));
            base.Pages.Add(new WizardCRMClient.Pages.CRMClientPage(_clientEvent));
            base.Pages.Add(new WizardCRMClient.Pages.CRMTaskPage(_clientEvent));
            
        }

        protected override bool OnCanConfigureInstance(object parent)
        {
            //Can only be dropped on a Activity
            //Change to Process if it can be dropped anywhere on canvas area
            if (parent is Activity)
                return true;
            return false;
        }
    }
}
