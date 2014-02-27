using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Net;

namespace K2.Demo.CRM.Functions
{
    //public class K2CRMConfig
    //{
    //    public string RESTUrl { get; set; }
    //    public string User { get; set; }
    //    public string Password { get; set; }
    //    public string Domain { get; set; }
    //    public NetworkCredential Credentials { get; set; }
    //    public CredentialCache CredentialCache { get; set; }

    //}

    //public class RESTResponse
    //{
    //    public string Content { get; set; }
    //    public Object Data { get; set; }
    //    public string ErrorException { get; set; }
    //    public string ErrorMessage { get; set; }
    //    public string StatusCode { get; set; }
    //    public string StatusDescription { get; set; }
    //}

    public class CRMConfig
    {
        public string CRMURL { get; set; }
        public string CRMOrganization { get; set; }
        public string CRMOrganizationServiceURL { get; set; }
        public string CRMDiscoveryServiceURL { get; set; }
        public string CRMOrganizationDataServiceURL { get; set; }
        public string CRMUsername { get; set; }
        public string CRMPassword { get; set; }
        public string CRMDomain { get; set; }
    }

    public class CRMEntityMetadata
    {
        public bool IncludeAttributes { get; set; }
        public string DisplayName { get; set; }
        public int ObjectTypeCode { get; set; }
        public string LogicalName { get; set; }
        public string PrimaryIdAttribute { get; set; }
        public string PrimaryNameAttribute { get; set; }
        public bool IsCustomEntity { get; set; }
        public List<CRMAttribute> Attributes { get; set; }
        public CRMConfig Config { get; set; }
    }

    public class CRMEntityOwnership
    {
        public string Assignee { get; set; }
        public string AssigneeId { get; set; }
        public string Target { get; set; }
        public string TargetId { get; set; }
        public CRMConfig Config { get; set; }
    }

    public class CRMFetchXML
    {
        public string FetchXML { get; set; }
        public List<CRMState> Entities { get; set; }
        public CRMConfig Config { get; set; }
    }

    public class CRMBulkActionTask
    {
        public string FetchXML { get; set; }
        public int FromState { get; set; }
        public int FromStatus { get; set; }
        public int ToState { get; set; }
        public int ToStatus { get; set; }
        public List<CRMState> Entities { get; set; }
        public CRMConfig Config { get; set; }
    }

    public class CRMUser
    {
        public string UserFQN { get; set; }
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public CRMConfig Config { get; set; }
    }

    public class CRMState
    {
        public string Entity { get; set; }
        public string EntityId { get; set; }
        public int State { get; set; }
        public int Status { get; set; }
        public CRMConfig Config { get; set; }
    }

    public class CRMTask
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Regarding { get; set; }
        public string RegardingId { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public DateTime DueDate { get; set; }
        public int Duration { get; set; }
        public int State { get; set; }
        public int Status { get; set; }
        public int Priority { get; set; }
        public string Owner { get; set; }
        public string OwnerId { get; set; }
        public string OwnerFQN { get; set; }
        public string K2SerialNumber { get; set; }
        public string K2ActivityName { get; set; }
        public string K2ProcessName { get; set; }
        public int K2ProcessInstanceId { get; set; }
        public CRMConfig Config { get; set; }
    }
    public class CRMWorkflow
    {
        public string WorkflowId { get; set; }
        public string EntityId { get; set; }
        public string WorkflowName { get; set; }
        public CRMConfig Config { get; set; }
    }
    public class CRMRetrieveMultiple
    {
        public string LinkFromEntityName { get; set; }
        public string LinkFromAttributeName { get; set; }
        public string LinkToEntityName { get; set; }
        public string LinkToAttributeName { get; set; }
        public string ConditionAttributeName { get; set; }
        public string ConditionOperator { get; set; }
        public string ConditionValue { get; set; }
        public string ReturnAttributes { get; set; }
        public List<CRMRetrieveMultipleReturn> Results { get; set; }
        public CRMConfig Config { get; set; }
    }

    public class CRMRetrieveMultipleReturn
    {
        public string EntityId { get; set; }
        public string ReturnAttribute0 { get; set; }
        public string ReturnAttribute1 { get; set; }
        public string ReturnAttribute2 { get; set; }
        public string ReturnAttribute3 { get; set; }
        public string ReturnAttribute4 { get; set; }
        public string ReturnAttribute5 { get; set; }
        public string ReturnAttribute6 { get; set; }
        public string ReturnAttribute7 { get; set; }
        public string ReturnAttribute8 { get; set; }
        public string ReturnAttribute9 { get; set; }
    }

    public class CRMSharePointDocumentLocation
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CRMEntityList
    {
        public string Organization { get; set; }
        public string CRMURL { get; set; }
        public List<CRMEntityMetadata> Entities { get; set; }
        public CRMConfig Config { get; set; }
    }

    public class CRMAttribute
    {
        public string LogicalName { get; set; }
        public string SchemaName { get; set; }
        public string DisplayName { get; set; }
        public string AttributeType { get; set; }
        public string Description { get; set; }
        public string RequiredLevel { get; set; }
        public bool IsPrimaryId { get; set; }
        public bool IsPrimaryName { get; set; }
        public bool IsValidForCreate { get; set; }
        public bool IsValidForRead { get; set; }
        public bool IsValidForUpdate { get; set; }
    }

    public class CRMPicklist
    {
        public string EntityLogicalName { get; set; }
        public string AttributeLogicalName { get; set; }
        public List<CRMPicklistOption> Picklist { get; set; }
        public CRMConfig Config { get; set; }
    }

    public class CRMPicklistOption
    {
        public int PicklistValue { get; set; }
        public string PicklistLabel { get; set; }
        public int PicklistParentValue { get; set; }
        public string PicklistParentLabel { get; set; }
    }


}
