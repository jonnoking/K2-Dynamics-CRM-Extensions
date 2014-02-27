using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SourceCode.Services;
using SourceCode.Services.ServiceContracts;
using SourceCode.Services.DataContracts;
using SourceCode.Workflow.Client;
using System.Configuration;
using System.ServiceModel.Activation;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Client;
using System.ServiceModel.Description;
using Microsoft.Crm.Sdk.Messages;
using K2.Demo.CRM.Functions;
using System.Net;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "K2DemoREST" in code, svc and config file together.
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class K2CRMService : IK2CRMService
{
    //private string CRMService = System.Configuration.ConfigurationManager.AppSettings["CRMUrl"];

    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public CRMEntityOwnership CRMChangeOwner(CRMEntityOwnership crmEntityOwnership)
    {
        CRMFunctions function = new CRMFunctions(crmEntityOwnership.Config);
        return function.CRMChangeOwner(crmEntityOwnership);
    }

    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public CRMState CRMSetStateStatus(CRMState crmState)
    {
        CRMFunctions function = new CRMFunctions(crmState.Config);
        return function.CRMSetStateStatus(crmState);
    }

    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public CRMFetchXML CRMGetEntities(CRMFetchXML crmFetchXML)
    {
        CRMFunctions function = new CRMFunctions(crmFetchXML.Config);
        return function.CRMGetEntities(crmFetchXML);

    }

    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public CRMBulkActionTask CRMBulkActionTasks(CRMBulkActionTask crmBulkActionTask)
    {
        CRMFunctions function = new CRMFunctions(crmBulkActionTask.Config);
        return function.CRMBulkActionTasks(crmBulkActionTask);
    }

    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public CRMTask CRMCreateTask(CRMTask crmTask)
    {
        CRMFunctions function = new CRMFunctions(crmTask.Config);
        return function.CRMCreateTask(crmTask);
    }

    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public CRMUser CRMGetUser(CRMUser crmUser)
    {
        CRMFunctions function = new CRMFunctions(crmUser.Config);
        return function.CRMGetUser(crmUser);
    }

    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public CRMWorkflow CRMStartWorkflow(CRMWorkflow crmWF)
    {
        CRMFunctions function = new CRMFunctions(crmWF.Config);
        return function.CRMStartWorkflow(crmWF);
    }

    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public CRMWorkflow CRMStartWorkflowByID(CRMWorkflow crmWF)
    {
        CRMFunctions function = new CRMFunctions(crmWF.Config);
        return function.CRMStartWorkflowByID(crmWF);
    }

    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public CRMEntityMetadata CRMGetEntityMetadata(CRMEntityMetadata crmEntityMetadata)
    {
        CRMFunctions function = new CRMFunctions(crmEntityMetadata.Config);
        return function.CRMGetEntityMetadata(crmEntityMetadata);
    }
    
    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public CRMRetrieveMultiple CRMRetrieveMultiple(CRMRetrieveMultiple crmRetrieveMultiple)
    {
        CRMFunctions function = new CRMFunctions(crmRetrieveMultiple.Config);
        return function.CRMRetrieveMultiple(crmRetrieveMultiple);
    }

    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public CRMEntityList CRMGetAllEntities(CRMEntityList EntityList)
    {
        CRMFunctions function = new CRMFunctions(EntityList.Config);
        return function.CRMGetAllEntities(EntityList);
    }

    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public CRMPicklist CRMGetPicklist(CRMPicklist picklist)
    {
        CRMFunctions function = new CRMFunctions(picklist.Config);
        return function.CRMGetPicklist(picklist);
    }

    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public CRMPicklist CRMGetStateStatus(CRMPicklist picklist)
    {
        CRMFunctions function = new CRMFunctions(picklist.Config);
        return function.CRMGetStateStatus(picklist);
    }

    //private bool CheckTaskField(string fieldname)
    //{
    //    string requesturl = "http://crm.denallix.com/Denallix/xrmservices/2011/OrganizationData.svc/TaskSet?$select=" + fieldname;
    //    HttpWebRequest request = WebRequest.Create(requesturl) as HttpWebRequest;
    //    try
    //    {
    //        // Get response  
    //        using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
    //        {
    //            if (response.StatusCode.ToString() != "200")
    //            {
    //                return false;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        return false;
    //    }
    //    return true;
    //}


}
