using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Net;
using K2.Demo.CRM.Wizard.Functions.Properties;
namespace K2.Demo.CRM.Wizard.Functions
{
    public class WizardFunctions
    {
        public WizardFunctions()
        {
            
        }

        //public string RESTServiceURL { get; set; }


        public RestResponse<CRMTask> CreateTask(CRMTask task, K2CRMConfig config)
        {
            var client = new RestClient(config.RESTUrl);

            var request = new RestRequest();
            request.Method = Method.POST;

            if (config.CredentialCache != null)
            {
                request.Credentials = config.CredentialCache;
            }
            if (config.Credentials != null)
            {
                request.Credentials = config.Credentials;
            }

            request.RequestFormat = RestSharp.DataFormat.Json;
            request.Resource = "K2CRM/CRMCreateTask";

            request.AddBody(task);
            RestResponse<CRMTask> response = null; 
            if (config.Async)
            {
                client.ExecuteAsync<CRMTask>(request, null);
            }
            else
            {
                response = client.Execute<CRMTask>(request);
            }
            return response;
        }

        public RestResponse<CRMEntityOwnership> ChangeOwner(CRMEntityOwnership crmEntityOwnership, K2CRMConfig config)
        {
            var client = new RestClient(config.RESTUrl);

            var request = new RestRequest();
            request.Method = Method.POST;
            if (config.CredentialCache != null)
            {
                request.Credentials = config.CredentialCache;
            }
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
            if (config.CredentialCache != null)
            {
                request.Credentials = config.CredentialCache;
            }
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
            if (config.CredentialCache != null)
            {
                request.Credentials = config.CredentialCache;
            }
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
            if (config.CredentialCache != null)
            {
                request.Credentials = config.CredentialCache;
            }
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.Resource = "K2CRM/CRMBulkActionTasks";

            request.AddBody(crmBulk);

            RestResponse<CRMBulkActionTask> response = client.Execute<CRMBulkActionTask>(request);

            return response;
        }

        public RestResponse<CRMBulkActionTask> BulkActionTasksSetCriteria(CRMBulkActionTask crmBulk, K2CRMConfig config, CRMTask crmTask)
        {
            string K2TasksFetchXML = Resources.K2CRMTaskCleanUpFetchXML.Replace("[entityid]", crmTask.RegardingId).Replace("[entityname]", crmTask.Regarding).Replace("[activityname]", crmTask.K2ActivityName).Replace("[processname]", crmTask.K2ProcessName).Replace("[statuscode]", crmBulk.FromStatus.ToString()).Replace("[processinstanceid]", crmTask.K2ProcessInstanceId.ToString());
            crmBulk.FetchXML = K2TasksFetchXML;

            return BulkActionTasks(crmBulk, config);
        }

        public RestResponse<CRMUser> GetCRMUser(CRMUser crmUser, K2CRMConfig config)
        {
            var client = new RestClient(config.RESTUrl);

            var request = new RestRequest();
            request.Method = Method.POST;
            if (config.CredentialCache != null)
            {
                request.Credentials = config.CredentialCache;
            }
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
            if (config.CredentialCache != null)
            {
                request.Credentials = config.CredentialCache;
            }
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.Resource = "K2CRM/CRMStartWorkflow";

            request.AddBody(crmWF);

            RestResponse<CRMWorkflow> response = client.Execute<CRMWorkflow>(request);

            return response;
        }

        public RestResponse<CRMEntityMetadata> GetEntityMetadata(CRMEntityMetadata entitymetadata, K2CRMConfig config)
        {
            var client = new RestClient(config.RESTUrl);

            var request = new RestRequest();
            request.Method = Method.POST;
            if (config.CredentialCache != null)
            {
                request.Credentials = config.CredentialCache;
            }
            if (config.Credentials != null)
            {
                request.Credentials = config.Credentials;
            }


            request.RequestFormat = RestSharp.DataFormat.Json;
            request.Resource = "K2CRM/CRMGetEntityMetadata";

            request.AddBody(entitymetadata);

            RestResponse<CRMEntityMetadata> response = client.Execute<CRMEntityMetadata>(request);

            return response;
        }

        public RestResponse<CRMRetrieveMultiple> RetrieveMultiple(CRMRetrieveMultiple retrieveMultiple, K2CRMConfig config)
        {
            var client = new RestClient(config.RESTUrl);

            var request = new RestRequest();
            request.Method = Method.POST;
            if (config.CredentialCache != null)
            {
                request.Credentials = config.CredentialCache;
            }
            if (config.Credentials != null)
            {
                request.Credentials = config.Credentials;
            }


            request.RequestFormat = RestSharp.DataFormat.Json;
            request.Resource = "K2CRM/CRMRetrieveMultiple";

            request.AddBody(retrieveMultiple);

            RestResponse<CRMRetrieveMultiple> response = client.Execute<CRMRetrieveMultiple>(request);

            return response;
        }

        public RestResponse<CRMEntityList> GetAllEntities(CRMEntityList entityList, K2CRMConfig config)
        {
            var client = new RestClient(config.RESTUrl);

            var request = new RestRequest();
            request.Method = Method.POST;
            if (config.CredentialCache != null)
            {
                request.Credentials = config.CredentialCache;
            }
            if (config.Credentials != null)
            {
                request.Credentials = config.Credentials;
            }

            request.RequestFormat = RestSharp.DataFormat.Json;
            request.Resource = "K2CRM/CRMGetAllEntities";

            request.AddBody(entityList);

            RestResponse<CRMEntityList> response = client.Execute<CRMEntityList>(request);

            return response;
        }

        public RestResponse<CRMPicklist> GetPicklist(CRMPicklist picklist, K2CRMConfig config)
        {
            var client = new RestClient(config.RESTUrl);

            var request = new RestRequest();
            request.Method = Method.POST;
            if (config.CredentialCache != null)
            {
                request.Credentials = config.CredentialCache;
            }
            if (config.Credentials != null)
            {
                request.Credentials = config.Credentials;
            }

            request.RequestFormat = RestSharp.DataFormat.Json;
            request.Resource = "K2CRM/CRMGetPicklist";

            request.AddBody(picklist);

            RestResponse<CRMPicklist> response = client.Execute<CRMPicklist>(request);

            return response;
        }

        public RestResponse<CRMPicklist> CRMGetStateStatus(CRMPicklist picklist, K2CRMConfig config)
        {
            var client = new RestClient(config.RESTUrl);

            var request = new RestRequest();
            request.Method = Method.POST;
            if (config.CredentialCache != null)
            {
                request.Credentials = config.CredentialCache;
            }
            if (config.Credentials != null)
            {
                request.Credentials = config.Credentials;
            }

            request.RequestFormat = RestSharp.DataFormat.Json;
            request.Resource = "K2CRM/CRMGetStateStatus";

            request.AddBody(picklist);

            RestResponse<CRMPicklist> response = client.Execute<CRMPicklist>(request);

            return response;
        }


        private K2CRMConfig GetK2CRMConfig(string User, string Password, string RESTUrl)
        {
            K2CRMConfig config = new K2CRMConfig();
            config.RESTUrl = RESTUrl;
            if (!string.IsNullOrEmpty(User))
            {
                string[] domainuser = User.Split('\\');
                config.User = domainuser[1];
                config.Domain = domainuser[0];
                config.Password = Password;
                config.Credentials = new NetworkCredential(domainuser[1], Password, domainuser[0]);
                config.CredentialCache = new CredentialCache();
                config.CredentialCache.Add(new Uri(RESTUrl), "NTLM", config.Credentials);
            }
            return config;
        }


    }
}
