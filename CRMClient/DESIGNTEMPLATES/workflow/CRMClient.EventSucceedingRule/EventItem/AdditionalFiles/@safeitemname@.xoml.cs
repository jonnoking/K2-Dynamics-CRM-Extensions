using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.Drawing;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

using SourceCode.SmartObjects.Client;
using SourceCode.Workflow.Common.HostedServers;

using SourceCode.Workflow.Common;
using SourceCode.Workflow.Common.Authentication;
using SourceCode.Workflow.Common.Extenders;
using SourceCode.KO;
//using SourceCode.SharePoint.WebServices;
using System.Xml;
using hostContext = $contexttype$;
namespace $rootnamespace$
{
	public partial class $safeitemname$ : IWorkflowContext<hostContext>
	{
		#region K2 Context

        	private hostContext _k2;

       		#endregion

		#region IWorkflowContext<hostContext> Members

        	public $contexttype$ K2
        	{
            		get
            		{
            		    return _k2;
            		}
            		set
            		{
            		    _k2 = value;
            		}
        	}

        	#endregion

		private void EvaluateRule(object sender, ConditionalEventArgs e)
    	{

            if (K2.EventInstance.ActivityInstanceDestination.ActivityInstance.SingleInstance)
            {
                e.Result = SucceedingRuleHelper.AnyOutcomesEvaluatedSuccessfully(K2);
            }
            else
            {
                e.Result = true;
            }
         
    	}

    	private void SetSucceedingRuleTrue_ExecuteCode(object sender, EventArgs e)
    	{
        		K2.SucceedingRule = true;
    	}

    	private void SetSucceedingRuleFalse_ExecuteCode(object sender, EventArgs e)
    	{
        		K2.SucceedingRule = false;
    	}

        private void UpdateCRMTask_ExecuteCode(object sender, EventArgs e)
        {
            XmlDocument tasks = new XmlDocument();
            tasks.LoadXml(K2.ProcessInstance.XmlFields["CRM Tasks"].Value);
            XmlNode taskidNode = tasks.SelectSingleNode("/CRMTasks/Task[SerialNumber= '" + K2.ProcessInstance.ID + "_" + K2.EventInstance.ActivityInstanceDestination.ID + "']/TaskId");

            if (taskidNode != null)
            {
                try
                {
                    using (SourceCode.Workflow.Common.HostedServers.SmartObjects smartObjects = new SmartObjects(K2.Configuration.SmartObjectServer))
                    {
                        SourceCode.SmartObjects.Client.SmartObject smartObject = smartObjects.GetSmartObject(K2.Configuration.CRMFunctionsSmartObject);

                        SourceCode.SmartObjects.Client.SmartMethod smartMethod = smartObjects.GetSingleMethod(smartObject, "SetStateStatus");

                        smartObject.Properties["Entity"].Value = "task";
                        smartObject.Properties["EntityId"].Value = taskidNode.InnerText;
                        smartObject.Properties["State"].Value = "1";
                        smartObject.Properties["Status"].Value = "5";

                        SourceCode.SmartObjects.Client.SmartObject smo = smartObjects.ExecuteSingleMethod(smartMethod);
                    }

                }
                catch (Exception ex)
                {
                    K2.ProcessInstance.Logger.LogErrorMessage("CRM Client Event - Completing task", ex.Message);
                }
            }
        }

	}
}

