using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace K2.Demo.CRM.Functions.ServiceBroker
{
    class K2CRMHelper
    {
        public RestResponse<CRMTask> CreateTask(CRMTask crmTask, K2CRMConfig config)
        {
            var client = new RestClient(config.RESTUrl);

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Credentials = config.CredentialCache;
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.Resource = "K2CRM/CRMCreateTask";

            request.AddBody(crmTask);

            RestResponse<CRMTask> response = client.Execute<CRMTask>(request);

            return response;
        }

        public RestResponse<CRMEntityOwnership> ChangeOwner(CRMEntityOwnership crmEntityOwnership, K2CRMConfig config)
        {
            var client = new RestClient(config.RESTUrl);

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Credentials = config.CredentialCache;
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.Resource = "K2CRM/CRMChangeOwner";

            request.AddBody(crmEntityOwnership);

            RestResponse<CRMEntityOwnership> response = client.Execute<CRMEntityOwnership>(request);

            return response;
        }


        public RestResponse<CRMState> SetStateStatus(CRMState crmState, K2CRMConfig config)
        {
            var client = new RestClient(config.RESTUrl);

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Credentials = config.CredentialCache;
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.Resource = "K2CRM/CRMSetStateStatus";

            request.AddBody(crmState);

            RestResponse<CRMState> response = client.Execute<CRMState>(request);

            return response;
        }

        public RestResponse<CRMFetchXML> GetEntities(CRMFetchXML crmFetch, K2CRMConfig config)
        {
            var client = new RestClient(config.RESTUrl);

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Credentials = config.CredentialCache;
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.Resource = "K2CRM/CRMGetEntities";

            request.AddBody(crmFetch);

            RestResponse<CRMFetchXML> response = client.Execute<CRMFetchXML>(request);

            return response;
        }

        public RestResponse<CRMBulkActionTask> BulkActionTasks(CRMBulkActionTask crmBulk, K2CRMConfig config)
        {
            var client = new RestClient(config.RESTUrl);

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Credentials = config.CredentialCache;
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.Resource = "K2CRM/CRMBulkActionTasks";

            request.AddBody(crmBulk);

            RestResponse<CRMBulkActionTask> response = client.Execute<CRMBulkActionTask>(request);

            return response;
        }

        public RestResponse<CRMUser> GetCRMUser(CRMUser crmUser, K2CRMConfig config)
        {
            var client = new RestClient(config.RESTUrl);

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Credentials = config.CredentialCache;
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.Resource = "K2CRM/CRMGetUser";

            request.AddBody(crmUser);

            RestResponse<CRMUser> response = client.Execute<CRMUser>(request);

            return response;
        }

        public RestResponse<CRMWorkflow> StartWorkflow(CRMWorkflow crmWF, K2CRMConfig config)
        {
            var client = new RestClient(config.RESTUrl);

            var request = new RestRequest();
            request.Method = Method.POST;
            request.Credentials = config.CredentialCache;
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.Resource = "K2CRM/CRMStartWorkflow";

            request.AddBody(crmWF);

            RestResponse<CRMWorkflow> response = client.Execute<CRMWorkflow>(request);

            return response;
        }
    }
}
