using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using SourceCode.KO;
using SourceCode.Workflow.Common.Extenders;

using SourceCode.SmartObjects.Client;
using SourceCode.Workflow.Common.HostedServers;

using System.Xml;
using hostContext = $contexttype$;
namespace $rootnamespace$
{
    public partial class $safeitemname$ : ICodeExtender<hostContext>
	{
         #region ICodeExtender<hostContext> Members

        public void Main($contexttype$ K2)
        {
            string strURL = string.Empty;      
            
            string smartObjectName = K2.Configuration.CRMFunctionsSmartObject;
            try
            {
                using (SourceCode.Workflow.Common.HostedServers.SmartObjects smartObjects = new SmartObjects(K2.Configuration.SmartObjectServer))
                {
                    SourceCode.SmartObjects.Client.SmartObject smartObject = smartObjects.GetSmartObject(smartObjectName);

                    SourceCode.SmartObjects.Client.SmartMethod smartMethod = smartObjects.GetSingleMethod(smartObject, "GetEntityMetadata");

                    SourceCode.SmartObjects.Client.SmartProperty logicalEntitName = smartMethod.InputProperties["EntityLogicalName"];
                    logicalEntitName.Value = K2.Configuration.CRMEntityType.ToLower();

                    SourceCode.SmartObjects.Client.SmartObject smo = smartObjects.ExecuteSingleMethod(smartMethod);

                    if (string.IsNullOrEmpty(K2.Configuration.CRMFormURL.Trim()))
                    {
                        string extraqs = string.Empty;

                        if (!string.IsNullOrEmpty(K2.Configuration.CRMEntityForm.Trim()))
                        {
                            extraqs = "&extraqs=formid%3d" + K2.Configuration.CRMEntityForm;
                        }
                        if (!string.IsNullOrEmpty(K2.Configuration.CRMCustomSNParameter.Trim()))
                        {
                            if (string.IsNullOrEmpty(extraqs))
                            {
                                extraqs = "&extraqs=" + K2.Configuration.CRMCustomSNParameter + "%3d" + K2.SerialNumber;
                            }
                            else
                            {
                                extraqs = extraqs + "%26" + K2.Configuration.CRMCustomSNParameter + "%3d" + K2.SerialNumber;
                            }
                        }
                        strURL = CheckCRMURL(K2.Configuration.CRMServerURL) + K2.Configuration.CRMOrganisation + "/main.aspx?etc=" + smo.Properties["EntityObjectTypeCode"].Value + extraqs + "&id=" + K2.Configuration.CRMEntityId + "&pagetype=entityrecord";
                    }
                    else
                    {
                        strURL = K2.Configuration.CRMFormURL;
                    }

                    smartObject.Dispose();
                    smartObject = null;
                    smartMethod.Dispose();
                    smartMethod = null;

                    smartObject = smartObjects.GetSmartObject(smartObjectName);
                    smartMethod = smartObjects.GetSingleMethod(smartObject, "CreateTask");

                    DateTime due = new DateTime();

                    smartObject.Properties["Category"].Value = K2.Configuration.TaskCategory;
                    smartObject.Properties["Description"].Value = K2.Configuration.TaskDescription;
                    smartObject.Properties["DueDate"].Value = DateTime.TryParse(K2.Configuration.TaskDueDate, out due) ? DateTime.Parse(K2.Configuration.TaskDueDate).ToString("yyyy-MM-dd HH:mm:ss") : DateTime.Now.Add(new TimeSpan(3, 0, 0, 0)).ToString("yyyy-MM-dd HH:mm:ss");
                    smartObject.Properties["Duration"].Value = string.IsNullOrEmpty(K2.Configuration.TaskDuration) ? "0" : K2.Configuration.TaskDuration;
                    smartObject.Properties["OwnerFQN"].Value = K2.ActivityInstanceDestination.User.Name.Replace("K2:", "").Replace("k2:", ""); //K2.Configuration.TaskOwnerFQN.Replace("K2:", "").Replace("k2:", "");
                    smartObject.Properties["OwnerId"].Value = K2.Configuration.TaskOwnerId;
                    smartObject.Properties["Owner"].Value = K2.Configuration.TaskOwner.Replace("K2:", "").Replace("k2:", "");
                    smartObject.Properties["Priority"].Value = string.IsNullOrEmpty(K2.Configuration.TaskPriority) ? "1" : K2.Configuration.TaskPriority;
                    smartObject.Properties["Regarding"].Value = K2.Configuration.TaskRegarding;
                    smartObject.Properties["RegardingId"].Value = K2.Configuration.TaskRegardingId;
                    smartObject.Properties["State"].Value = string.IsNullOrEmpty(K2.Configuration.TaskState) ? "0" : K2.Configuration.TaskState;
                    smartObject.Properties["Status"].Value = string.IsNullOrEmpty(K2.Configuration.TaskStatus) ? "3" : K2.Configuration.TaskStatus;
                    smartObject.Properties["Subcategory"].Value = K2.Configuration.TaskSubcategory;
                    smartObject.Properties["Subject"].Value = K2.Configuration.TaskSubject;
                    smartObject.Properties["K2SerialNumber"].Value = K2.SerialNumber;
                    smartObject.Properties["K2ActivityName"].Value = K2.ActivityInstanceDestination.Activity.Name;
                    smartObject.Properties["K2ProcessName"].Value = K2.ProcessInstance.Process.Name;
                    smartObject.Properties["K2ProcessInstanceId"].Value = K2.ProcessInstance.ID.ToString();

                    smo = smartObjects.ExecuteSingleMethod(smartMethod);

                    XmlDocument tasks = new XmlDocument();
                    tasks.LoadXml(K2.ProcessInstance.XmlFields["CRM Tasks"].Value);

                    string newtask = "<Task><TaskId>" + smo.Properties["TaskId"].Value + "</TaskId><SerialNumber>" + K2.SerialNumber + "</SerialNumber><DestinationUser>" + K2.Configuration.TaskOwnerFQN + "</DestinationUser><Process>" + K2.Configuration.ProcessName + "</Process><Activity>" + K2.Configuration.ActivityName + "</Activity></Task>";
                    //string newtask = "<Task><Guid>" + taskresponse.Data.Id + "</Guid><SerialNumber>" + K2.SerialNumber + "</SerialNumber><DestinationUser>" + K2.Configuration.TaskOwnerFQN + "</DestinationUser><Process></Process><Activity></Activity></Task>";

                    tasks.FirstChild.InnerXml += newtask;

                    K2.ProcessInstance.XmlFields["CRM Tasks"].Value = tasks.OuterXml;

                    K2.AddWorklist(K2.Configuration.InternetPlatform, System.Web.HttpUtility.UrlPathEncode(strURL));


                }
            }
            catch (Exception ex)
            {
                K2.ProcessInstance.Logger.LogErrorMessage(K2.Event.Name, ex.Message);
            }
        }
        
        private string CheckCRMURL(string url)
        {
            string[] crmdetails;
            if (url.Contains(";"))
            {
                crmdetails = url.Split(';');
                url = crmdetails[0].ToLower().Replace("url=", "");
            }

            if (url.LastIndexOf(@"/") != url.Length - 1)
            {
                return url + "/";
            }
            return url;
        }

        

        #endregion
    }

}
 
