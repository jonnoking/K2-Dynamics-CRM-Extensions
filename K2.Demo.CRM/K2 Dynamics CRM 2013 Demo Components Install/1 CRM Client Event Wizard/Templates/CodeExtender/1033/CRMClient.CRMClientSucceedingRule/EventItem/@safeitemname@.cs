using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Xml;

using SourceCode.Workflow.Common.HostedServers;
using SourceCode.SmartObjects.Client;

using SourceCode.KO;
using SourceCode.Workflow.Common;
using SourceCode.Workflow.Common.Authentication;
using SourceCode.Workflow.Common.Extenders;
using hostContext = $contexttype$;
namespace $rootnamespace$
{
    public partial class $safeitemname$ : ICodeExtender<hostContext>
	{
         #region ICodeExtender<hostContext> Members

        public void Main($contexttype$ K2)
        {
            if (SucceedingRuleHelper.AnyOutcomesEvaluatedSuccessfully(K2))
            {
                try
                {
                    using (SourceCode.Workflow.Common.HostedServers.SmartObjects smartObjects = new SmartObjects(K2.Configuration.SmartObjectServer))
                    {
                        SourceCode.SmartObjects.Client.SmartObject smartObject = smartObjects.GetSmartObject(K2.Configuration.CRMFunctionsSmartObject);

                        SourceCode.SmartObjects.Client.SmartMethod smartMethod = smartObjects.GetSingleMethod(smartObject, "BulkActionTasksSetCriteria");

                        smartObject.Properties["FromState"].Value = "0";
                        smartObject.Properties["FromStatus"].Value = "3";
                        smartObject.Properties["ToState"].Value = "2";
                        smartObject.Properties["ToStatus"].Value = "6";
                        smartObject.Properties["Regarding"].Value = K2.Configuration.CRMEntityType;
                        smartObject.Properties["RegardingId"].Value = K2.Configuration.CRMEntityId; ;
                        smartObject.Properties["K2ActivityName"].Value = K2.ActivityInstance.Activity.Name;
                        smartObject.Properties["K2ProcessName"].Value = K2.ProcessInstance.Process.Name;
                        smartObject.Properties["K2ProcessInstanceId"].Value = K2.ProcessInstance.ID.ToString();

                        SourceCode.SmartObjects.Client.SmartObject smo = smartObjects.ExecuteSingleMethod(smartMethod);
                        K2.SucceedingRule = true;
                    }

                }
                catch (Exception ex)
                {
                    K2.ProcessInstance.Logger.LogErrorMessage("CRM Client Event - Completing task", ex.Message);
                }
            }
            else
            {
                K2.SucceedingRule = false;
            }
        }

        #endregion
    }
} 
