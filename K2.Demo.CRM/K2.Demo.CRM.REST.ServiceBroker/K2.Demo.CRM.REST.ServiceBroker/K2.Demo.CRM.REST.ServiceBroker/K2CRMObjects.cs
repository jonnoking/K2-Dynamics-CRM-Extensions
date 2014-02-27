using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace K2.Demo.CRM.REST.ServiceBroker
{

    public class K2CRMConfig
    {
        public string RESTUrl { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public NetworkCredential Credentials { get; set; }
        public CredentialCache CredentialCache { get; set; }

    }

    public class RESTResponse
    {
        public string Content { get; set; }
        public Object Data { get; set; }
        public string ErrorException { get; set; }
        public string ErrorMessage { get; set; }
        public string StatusCode { get; set; }
        public string StatusDescription { get; set; }
    }

    public class CRMEntityOwnership
    {
        public string Assignee { get; set; }
        public string AssigneeId { get; set; }
        public string Target { get; set; }
        public string TargetId { get; set; }
    }

    public class CRMFetchXML
    {
        public string FetchXML { get; set; }
        public List<CRMState> Entities { get; set; }
    }

    public class CRMBulkActionTask
    {
        public string FetchXML { get; set; }
        public int FromState { get; set; }
        public int FromStatus { get; set; }
        public int ToState { get; set; }
        public int ToStatus { get; set; }
        public List<CRMState> Entities { get; set; }
    }

    public class CRMUser
    {
        public string UserFQN { get; set; }
        public string UserId { get; set; }
        public string DisplayName { get; set; }

    }

    public class CRMState
    {
        public string Entity { get; set; }
        public string EntityId { get; set; }
        public int State { get; set; }
        public int Status { get; set; }
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

    }
    public class CRMWorkflow
    {
        public string WorkflowId { get; set; }
        public string EntityId { get; set; }
        public string WorkflowName { get; set; }
    }

}
