using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using SourceCode.Services.DataContracts;
using System.ServiceModel.Syndication;
using K2.Demo.CRM.Functions;
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IK2CRMService" in both code and config file together.
[ServiceContract]
public interface IK2CRMService
{

    [WebInvoke(
        Method = "POST",
        UriTemplate = "CRMChangeOwner",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
    [OperationContract]
    [FaultContract(typeof(Failure))]
    [ServiceKnownType(typeof(Atom10FeedFormatter))]
    CRMEntityOwnership CRMChangeOwner(CRMEntityOwnership crmEntityOwnership);

    [WebInvoke(
        Method = "POST",
        UriTemplate = "CRMSetStateStatus",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
    [OperationContract]
    [FaultContract(typeof(Failure))]
    [ServiceKnownType(typeof(Atom10FeedFormatter))]
    CRMState CRMSetStateStatus(CRMState crmState);

    [WebInvoke(
        Method = "POST",
        UriTemplate = "CRMGetEntities",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
    [OperationContract]
    [FaultContract(typeof(Failure))]
    [ServiceKnownType(typeof(Atom10FeedFormatter))]
    CRMFetchXML CRMGetEntities(CRMFetchXML crmFetchXML);

    [WebInvoke(
        Method = "POST",
        UriTemplate = "CRMBulkActionTasks",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
    [OperationContract]
    [FaultContract(typeof(Failure))]
    [ServiceKnownType(typeof(Atom10FeedFormatter))]
    CRMBulkActionTask CRMBulkActionTasks(CRMBulkActionTask crmBulkActionTask);

    [WebInvoke(
        Method = "POST",
        UriTemplate = "CRMCreateTask",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
    [OperationContract]
    [FaultContract(typeof(Failure))]
    [ServiceKnownType(typeof(Atom10FeedFormatter))]
    CRMTask CRMCreateTask(CRMTask crmTask);

    [WebInvoke(
    Method = "POST",
    UriTemplate = "CRMGetUser",
    RequestFormat = WebMessageFormat.Json,
    ResponseFormat = WebMessageFormat.Json,
    BodyStyle = WebMessageBodyStyle.Bare)]
    [OperationContract]
    [FaultContract(typeof(Failure))]
    [ServiceKnownType(typeof(Atom10FeedFormatter))]
    CRMUser CRMGetUser(CRMUser crmUser);

    [WebInvoke(
    Method = "POST",
    UriTemplate = "CRMStartWorkflow",
    RequestFormat = WebMessageFormat.Json,
    ResponseFormat = WebMessageFormat.Json,
    BodyStyle = WebMessageBodyStyle.Bare)]
    [OperationContract]
    [FaultContract(typeof(Failure))]
    [ServiceKnownType(typeof(Atom10FeedFormatter))]
    CRMWorkflow CRMStartWorkflow(CRMWorkflow crmWF);

    [WebInvoke(
    Method = "POST",
    UriTemplate = "CRMStartWorkflowByID",
    RequestFormat = WebMessageFormat.Json,
    ResponseFormat = WebMessageFormat.Json,
    BodyStyle = WebMessageBodyStyle.Bare)]
    [OperationContract]
    [FaultContract(typeof(Failure))]
    [ServiceKnownType(typeof(Atom10FeedFormatter))]
    CRMWorkflow CRMStartWorkflowByID(CRMWorkflow crmWF);

    [WebInvoke(
    Method = "POST",
    UriTemplate = "CRMGetEntityMetadata",
    RequestFormat = WebMessageFormat.Json,
    ResponseFormat = WebMessageFormat.Json,
    BodyStyle = WebMessageBodyStyle.Bare)]
    [OperationContract]
    [FaultContract(typeof(Failure))]
    [ServiceKnownType(typeof(Atom10FeedFormatter))]
    CRMEntityMetadata CRMGetEntityMetadata(CRMEntityMetadata crmEntityMetadata);

    [WebInvoke(
    Method = "POST",
    UriTemplate = "CRMRetrieveMultiple",
    RequestFormat = WebMessageFormat.Json,
    ResponseFormat = WebMessageFormat.Json,
    BodyStyle = WebMessageBodyStyle.Bare)]
    [OperationContract]
    [FaultContract(typeof(Failure))]
    [ServiceKnownType(typeof(Atom10FeedFormatter))]
    CRMRetrieveMultiple CRMRetrieveMultiple(CRMRetrieveMultiple crmRetrieveMultiple);


    [WebInvoke(
    Method = "POST",
    UriTemplate = "CRMGetAllEntities",
    RequestFormat = WebMessageFormat.Json,
    ResponseFormat = WebMessageFormat.Json,
    BodyStyle = WebMessageBodyStyle.Bare)]
    [OperationContract]
    [FaultContract(typeof(Failure))]
    [ServiceKnownType(typeof(Atom10FeedFormatter))]
    CRMEntityList CRMGetAllEntities(CRMEntityList EntityList);

    [WebInvoke(
    Method = "POST",
    UriTemplate = "CRMGetPicklist",
    RequestFormat = WebMessageFormat.Json,
    ResponseFormat = WebMessageFormat.Json,
    BodyStyle = WebMessageBodyStyle.Bare)]
    [OperationContract]
    [FaultContract(typeof(Failure))]
    [ServiceKnownType(typeof(Atom10FeedFormatter))]
    CRMPicklist CRMGetPicklist(CRMPicklist picklist);

    [WebInvoke(
    Method = "POST",
    UriTemplate = "CRMGetStateStatus",
    RequestFormat = WebMessageFormat.Json,
    ResponseFormat = WebMessageFormat.Json,
    BodyStyle = WebMessageBodyStyle.Bare)]
    [OperationContract]
    [FaultContract(typeof(Failure))]
    [ServiceKnownType(typeof(Atom10FeedFormatter))]
    CRMPicklist CRMGetStateStatus(CRMPicklist picklist);


}
