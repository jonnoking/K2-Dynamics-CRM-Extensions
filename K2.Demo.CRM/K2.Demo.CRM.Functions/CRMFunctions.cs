using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Crm.Sdk.Messages;
using System.ServiceModel.Description;
using System.Configuration;
using System.Reflection;
using System.Net;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace K2.Demo.CRM.Functions
{
    public class CRMFunctions
    {
        public CRMFunctions(string crmServiceURL)
        {
            CRMOrganizationServiceURL = crmServiceURL;
        }

        public CRMFunctions(string crmurl, string crmorganization)
        {
            CRMURL = AddTrailingSlash(crmurl);
            CRMOrganization = crmorganization;
            CRMOrganizationServiceURL = CRMURL + CRMOrganization + "/XRMServices/2011/Organization.svc";
            CRMOrganizationDataServiceURL = CRMURL + CRMOrganization + "/XRMServices/2011/OrganizationData.svc";
            CRMDiscoveryServiceURL = CRMURL + "/XRMServices/2011/Discovery.svc";
        }

        public CRMFunctions(CRMConfig config)
        {
            CRMURL = AddTrailingSlash(config.CRMURL);
            CRMOrganization = config.CRMOrganization;
            CRMOrganizationServiceURL = string.IsNullOrEmpty(config.CRMOrganizationServiceURL) ? CRMURL + CRMOrganization + "/XRMServices/2011/Organization.svc" : config.CRMOrganizationServiceURL;
            CRMOrganizationDataServiceURL = string.IsNullOrEmpty(config.CRMOrganizationDataServiceURL) ? CRMURL + CRMOrganization + "/XRMServices/2011/OrganizationData.svc" : config.CRMOrganizationDataServiceURL;
            CRMDiscoveryServiceURL = string.IsNullOrEmpty(config.CRMDiscoveryServiceURL) ?  CRMURL + "/XRMServices/2011/Discovery.svc" : config.CRMDiscoveryServiceURL;
            CRMUsername = config.CRMUsername;
            CRMPassword = config.CRMPassword;
            CRMDomain = config.CRMDomain;
        }

        public string CRMOrganizationServiceURL { get; set; }
        public string CRMOrganizationDataServiceURL { get; set; }
        public string CRMDiscoveryServiceURL { get; set; }
        public string CRMURL { get; set; }
        public string CRMOrganization { get; set; }
        public string CRMUsername { get; set; }
        public string CRMPassword { get; set; }
        public string CRMDomain { get; set; }


        public CRMEntityOwnership CRMChangeOwner(CRMEntityOwnership crmEntityOwnership)
        {
            OrganizationServiceProxy _serviceProxy;
            IOrganizationService _service;
            //res = "";

            using (_serviceProxy = GetCRMConnection())
            {
                // This statement is required to enable early-bound type support.
                _serviceProxy.ServiceConfiguration.CurrentServiceEndpoint.Behaviors.Add(new ProxyTypesBehavior());

                _service = (IOrganizationService)_serviceProxy;

                Microsoft.Crm.Sdk.Messages.AssignRequest req = new Microsoft.Crm.Sdk.Messages.AssignRequest();
                req.Assignee = new EntityReference(crmEntityOwnership.Assignee, new Guid(crmEntityOwnership.AssigneeId));
                req.Target = new EntityReference(crmEntityOwnership.Target, new Guid(crmEntityOwnership.TargetId));

                try
                {
                    Microsoft.Crm.Sdk.Messages.AssignResponse resp = (Microsoft.Crm.Sdk.Messages.AssignResponse)_service.Execute(req);
                    //res = resp.ResponseName;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return crmEntityOwnership;
        }

        public CRMState CRMSetStateStatus(CRMState crmState)
        {
            OrganizationServiceProxy _serviceProxy;
            IOrganizationService _service;
            string res = "";

            using (_serviceProxy = GetCRMConnection())
            {
                // This statement is required to enable early-bound type support.
                _serviceProxy.ServiceConfiguration.CurrentServiceEndpoint.Behaviors.Add(new ProxyTypesBehavior());

                _service = (IOrganizationService)_serviceProxy;

                Microsoft.Crm.Sdk.Messages.SetStateRequest ssr = new Microsoft.Crm.Sdk.Messages.SetStateRequest();
                ssr.State = new OptionSetValue(crmState.State);
                ssr.Status = new OptionSetValue(crmState.Status);
                ssr.EntityMoniker = new EntityReference(crmState.Entity, new Guid(crmState.EntityId));

                try
                {
                    Microsoft.Crm.Sdk.Messages.SetStateResponse resp = (Microsoft.Crm.Sdk.Messages.SetStateResponse)_service.Execute(ssr);
                    res = resp.ResponseName;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return crmState;
        }

        public CRMFetchXML CRMGetEntities(CRMFetchXML crmFetchXML)
        {
            OrganizationServiceProxy _serviceProxy;
            IOrganizationService _service;
            string res = "";
            List<CRMState> returnEntities = new List<CRMState>();

            using (_serviceProxy = GetCRMConnection())
            {
                // This statement is required to enable early-bound type support.
                _serviceProxy.ServiceConfiguration.CurrentServiceEndpoint.Behaviors.Add(new ProxyTypesBehavior());

                _service = (IOrganizationService)_serviceProxy;

                try
                {
                    EntityCollection result = _serviceProxy.RetrieveMultiple(new FetchExpression(crmFetchXML.FetchXML));

                    if (result.Entities.Count > 0)
                    {
                        foreach (var entity in result.Entities)
                        {
                            CRMState cEntity = new CRMState();
                            cEntity.Entity = entity.LogicalName;
                            cEntity.EntityId = entity.Id.ToString();
                            returnEntities.Add(cEntity);
                        }
                    }
                    crmFetchXML.Entities = returnEntities;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return crmFetchXML;
        }

        public CRMBulkActionTask CRMBulkActionTasks(CRMBulkActionTask crmBulkActionTask)
        {
            OrganizationServiceProxy _serviceProxy;
            IOrganizationService _service;
            string res = "";
            List<CRMState> returnEntities = new List<CRMState>();

            using (_serviceProxy = GetCRMConnection())
            {
                // This statement is required to enable early-bound type support.
                _serviceProxy.ServiceConfiguration.CurrentServiceEndpoint.Behaviors.Add(new ProxyTypesBehavior());

                _service = (IOrganizationService)_serviceProxy;

                try
                {
                    EntityCollection result = _serviceProxy.RetrieveMultiple(new FetchExpression(crmBulkActionTask.FetchXML));

                    if (result.Entities.Count > 0)
                    {
                        foreach (var entity in result.Entities)
                        {
                            CRMState cs = new CRMState();
                            cs.Entity = entity.LogicalName;
                            cs.EntityId = entity.Id.ToString();
                            cs.State = crmBulkActionTask.ToState;
                            cs.Status = crmBulkActionTask.ToStatus;
                            returnEntities.Add(cs);
                            CRMSetStateStatus(cs);
                        }
                        crmBulkActionTask.Entities = returnEntities;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return crmBulkActionTask;
        }

        public CRMTask CRMCreateTask(CRMTask crmTask)
        {
            OrganizationServiceProxy _serviceProxy;
            IOrganizationService _service;
            string res = "";
            List<CRMState> returnEntities = new List<CRMState>();

            using (_serviceProxy = GetCRMConnection())
            {
                // This statement is required to enable early-bound type support.
                _serviceProxy.ServiceConfiguration.CurrentServiceEndpoint.Behaviors.Add(new ProxyTypesBehavior());

                _service = (IOrganizationService)_serviceProxy;

                try
                {
                    Entity task = new Entity();
                    task.LogicalName = "task";
                    task["subject"] = crmTask.Subject;
                    task["description"] = crmTask.Description;
                    task["category"] = crmTask.Category;
                    task["subcategory"] = crmTask.Subcategory;
                    if (!string.IsNullOrEmpty(crmTask.RegardingId.Trim()) && !string.IsNullOrEmpty(crmTask.Regarding.Trim()))
                    {
                        task["regardingobjectid"] = new EntityReference(crmTask.Regarding.ToLower(), new Guid(crmTask.RegardingId));
                    }

                    // assumes that the K2 CRM solution has been deployed
                    task["k2_k2serialnumber"] = crmTask.K2SerialNumber;
                    task["k2_k2activityname"] = crmTask.K2ActivityName;
                    task["k2_k2processname"] = crmTask.K2ProcessName;
                    task["k2_k2processinstanceid"] = crmTask.K2ProcessInstanceId;
                    
                    task["scheduleddurationminutes"] = crmTask.Duration;
                    task["scheduledend"] = crmTask.DueDate;
                    task["prioritycode"] = new OptionSetValue(crmTask.Priority);
                    task["statecode"] = new OptionSetValue(crmTask.State);
                    task["statuscode"] = new OptionSetValue(crmTask.Status);

                    Guid newtask = _service.Create(task);

                    crmTask.Id = newtask.ToString();

                    if (string.IsNullOrEmpty(crmTask.OwnerId.Trim()) && !string.IsNullOrEmpty(crmTask.OwnerFQN.Trim()))
                    {
                        CRMUser cu = new CRMUser();
                        cu.UserFQN = crmTask.OwnerFQN;
                        cu = CRMGetUser(cu);
                        crmTask.OwnerId = cu.UserId;
                    }

                    if (!string.IsNullOrEmpty(crmTask.OwnerId.Trim()))
                    {
                        CRMEntityOwnership eo = new CRMEntityOwnership();
                        eo.Target = "task";
                        eo.TargetId = newtask.ToString();
                        eo.Assignee = crmTask.Owner.ToLower();
                        eo.AssigneeId = crmTask.OwnerId;
                        CRMChangeOwner(eo);
                    }

                    crmTask.Id = newtask.ToString();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return crmTask;
        }

        public CRMUser CRMGetUser(CRMUser crmUser)
        {
            CRMState returnEntities = new CRMState();
            CRMUser ret = new CRMUser();

            string fetchxml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                          <entity name='systemuser'>
                            <attribute name='fullname' />
                            <attribute name='businessunitid' />
                            <attribute name='title' />
                            <attribute name='address1_telephone1' />
                            <attribute name='systemuserid' />
                            <order attribute='fullname' descending='false' />
                            <filter type='and'>
                              <condition attribute='domainname' operator='eq' value='"
                                   + crmUser.UserFQN + "' />"
                                + "</filter>"
                              + "</entity>"
                            + "</fetch>";

            CRMFetchXML fx = new CRMFetchXML();
            fx.FetchXML = fetchxml;
            List<CRMState> user = CRMGetEntities(fx).Entities;

            if (user.Count > 0)
            {
                crmUser.UserId = user[0].EntityId;
            }
            return crmUser;
        }

        public CRMWorkflow CRMStartWorkflow(CRMWorkflow crmWF)
        {
            string fetchxml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>"
          + "<entity name='workflow'>"
            + "<attribute name='workflowid' />"
            + "<attribute name='workflowidunique' />"
            + "<attribute name='name' />"
            + "<attribute name='category' />"
            + "<attribute name='primaryentity' />"
            + "<attribute name='statecode' />"
            + "<attribute name='createdon' />"
            + "<attribute name='ownerid' />"
            + "<attribute name='owningbusinessunit' />"
            + "<attribute name='type' />"
            + "<attribute name='parentworkflowid' />"
            + "<order attribute='name' descending='false' />"
            + "<filter type='and'>"
              + "<condition attribute='statecode' operator='eq' value='1' />"
              + "<condition attribute='name' operator='eq' value='" + crmWF.WorkflowName + "' />"
              + " <condition attribute='parentworkflowid' operator='null' />"
            + "</filter>"
          + "</entity>"
        + "</fetch>";

            CRMFetchXML fx = new CRMFetchXML();
            fx.FetchXML = fetchxml;
            List<CRMState> wf = CRMGetEntities(fx).Entities;

            if (wf.Count > 0)
            {
                crmWF.WorkflowId = wf[0].EntityId;
                CRMStartWorkflowByID(crmWF);
            }

            return crmWF;
        }

        public CRMWorkflow CRMStartWorkflowByID(CRMWorkflow crmWF)
        {
            OrganizationServiceProxy _serviceProxy;
            //IOrganizationService _service;

            using (_serviceProxy = GetCRMConnection())
            {
                try
                {
                    // Create an ExecuteWorkflow request.
                    ExecuteWorkflowRequest request = new ExecuteWorkflowRequest()
                    {
                        WorkflowId = new Guid(crmWF.WorkflowId),
                        EntityId = new Guid(crmWF.EntityId)
                    };

                    // Execute the workflow.
                    ExecuteWorkflowResponse response = (ExecuteWorkflowResponse)_serviceProxy.Execute(request);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return crmWF;
        }

        public CRMEntityMetadata CRMGetEntityMetadata(CRMEntityMetadata entitytype)
        {
            OrganizationServiceProxy _serviceProxy;

            Microsoft.Xrm.Sdk.Metadata.EntityFilters filter = entitytype.IncludeAttributes ? Microsoft.Xrm.Sdk.Metadata.EntityFilters.Attributes : Microsoft.Xrm.Sdk.Metadata.EntityFilters.Entity;

            using (_serviceProxy = GetCRMConnection())
            {
                try
                {
                    RetrieveEntityRequest retrieveEntityRequest = new RetrieveEntityRequest
                        {
                            EntityFilters = filter,
                            LogicalName = entitytype.LogicalName.ToLower()
                        };

                    RetrieveEntityResponse retrieveEntityResponse = (RetrieveEntityResponse)_serviceProxy.Execute(retrieveEntityRequest);
                    EntityMetadata entityMetadata = retrieveEntityResponse.EntityMetadata;

                    entitytype.DisplayName = entityMetadata.DisplayName.UserLocalizedLabel != null ? entityMetadata.DisplayName.UserLocalizedLabel.Label.ToString() : entityMetadata.LogicalName;
                    entitytype.ObjectTypeCode = entityMetadata.ObjectTypeCode.Value;
                    entitytype.LogicalName = entityMetadata.LogicalName;
                    entitytype.PrimaryIdAttribute = entityMetadata.PrimaryIdAttribute;
                    entitytype.PrimaryNameAttribute = entityMetadata.PrimaryNameAttribute;
                    entitytype.IsCustomEntity = entityMetadata.IsCustomEntity.Value;
                    entitytype.Attributes = new List<CRMAttribute>();

                    List<CRMAttribute> Attributes = new List<CRMAttribute>();
                    if (entitytype.IncludeAttributes)
                    {
                        foreach (AttributeMetadata metadata in entityMetadata.Attributes)
                        {
                            CRMAttribute attrib = new CRMAttribute();
                            attrib.AttributeType = metadata.AttributeType.HasValue ? metadata.AttributeType.Value.ToString() : "";
                            attrib.Description = metadata.Description.UserLocalizedLabel != null ? metadata.Description.UserLocalizedLabel.Label : "";
                            attrib.DisplayName = metadata.DisplayName.UserLocalizedLabel != null ? metadata.DisplayName.UserLocalizedLabel.Label : metadata.LogicalName;
                            attrib.IsPrimaryId = metadata.IsPrimaryId.HasValue ? metadata.IsPrimaryId.Value : false;
                            attrib.IsPrimaryName = metadata.IsPrimaryName.HasValue ? metadata.IsPrimaryName.Value : false;
                            attrib.IsValidForCreate = metadata.IsValidForCreate.HasValue ? metadata.IsValidForCreate.Value : false;
                            attrib.IsValidForRead = metadata.IsValidForRead.HasValue ? metadata.IsValidForRead.Value : false;
                            attrib.IsValidForUpdate = metadata.IsValidForUpdate.HasValue ? metadata.IsValidForUpdate.Value : false;
                            attrib.LogicalName = metadata.LogicalName;
                            attrib.RequiredLevel = metadata.RequiredLevel.Value.ToString();
                            attrib.SchemaName = metadata.SchemaName;
                            Attributes.Add(attrib);
                        }
                        entitytype.Attributes = Attributes;
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return entitytype;
        }

        public ConditionOperator GetConditionOperator(string opt)
        {
            switch(opt.ToLower())
            {
                case "equal":
                    return ConditionOperator.Equal;
            }
            return ConditionOperator.Equal;
        }

        public CRMRetrieveMultiple CRMRetrieveMultiple(CRMRetrieveMultiple retrieveMultiple)
        {
            List<CRMRetrieveMultipleReturn> results = new List<CRMRetrieveMultipleReturn>();

            OrganizationServiceProxy _serviceProxy;
            using (_serviceProxy = GetCRMConnection())
            {
                QueryExpression query;
                if (!string.IsNullOrEmpty(retrieveMultiple.ConditionAttributeName.Trim()))
                {
                    //Create Query Expression.
                    query = new QueryExpression()
                    {
                        EntityName = retrieveMultiple.LinkFromEntityName.ToLower(),
                        ColumnSet = new ColumnSet(true),
                        LinkEntities = 
                    {
                        new LinkEntity
                        {
                            LinkFromEntityName = retrieveMultiple.LinkFromEntityName.ToLower(),
                            LinkFromAttributeName = retrieveMultiple.LinkFromAttributeName,
                            LinkToEntityName = retrieveMultiple.LinkToEntityName,
                            LinkToAttributeName = retrieveMultiple.LinkToAttributeName,
                            LinkCriteria = new FilterExpression
                            {
                                FilterOperator = LogicalOperator.And,
                                Conditions = 
                                {
                                    new ConditionExpression
                                    {
                                        AttributeName = retrieveMultiple.ConditionAttributeName,
                                        Operator = GetConditionOperator(retrieveMultiple.ConditionOperator),
                                        Values = { retrieveMultiple.ConditionValue }
                                    }
                                }
                            }
                        }
                    }
                    };
                }
                else
                {
                    //Create Query Expression.
                    query = new QueryExpression()
                    {
                        EntityName = retrieveMultiple.LinkFromEntityName.ToLower(),
                        ColumnSet = new ColumnSet(true),
                        LinkEntities = 
                    {
                        new LinkEntity
                        {
                            LinkFromEntityName = retrieveMultiple.LinkFromEntityName.ToLower(),
                            LinkFromAttributeName = retrieveMultiple.LinkFromAttributeName,
                            LinkToEntityName = retrieveMultiple.LinkToEntityName,
                            LinkToAttributeName = retrieveMultiple.LinkToAttributeName,
                        }
                    }
                    };
                }
                EntityCollection ec = _serviceProxy.RetrieveMultiple(query);

                foreach (Entity e in ec.Entities)
                {
                    CRMRetrieveMultipleReturn res = new CRMRetrieveMultipleReturn();
                    res.EntityId = e.Id.ToString();

                    string[] Columns = retrieveMultiple.ReturnAttributes.Split('|');
                    for (int i = 0; i < Columns.Length; i++)
                    {
                        if (e.Attributes.Contains(Columns[i].Trim()))
                        {
                            switch (i.ToString())
                            {
                                case "0":
                                    res.ReturnAttribute0 = CRMTypeMapping(e.Attributes[Columns[i].Trim()].ToString());
                                    break;
                                case "1":
                                    res.ReturnAttribute1 = CRMTypeMapping(e.Attributes[Columns[i].Trim()].ToString());
                                    break;
                                case "2":
                                    res.ReturnAttribute2 = CRMTypeMapping(e.Attributes[Columns[i].Trim()].ToString());
                                    break;
                                case "3":
                                    res.ReturnAttribute3 = CRMTypeMapping(e.Attributes[Columns[i].Trim()].ToString());
                                    break;
                                case "4":
                                    res.ReturnAttribute4 = CRMTypeMapping(e.Attributes[Columns[i].Trim()].ToString());
                                    break;
                                case "5":
                                    res.ReturnAttribute5 = CRMTypeMapping(e.Attributes[Columns[i].Trim()].ToString());
                                    break;
                                case "6":
                                    res.ReturnAttribute6 = CRMTypeMapping(e.Attributes[Columns[i].Trim()].ToString());
                                    break;
                                case "7":
                                    res.ReturnAttribute7 = CRMTypeMapping(e.Attributes[Columns[i].Trim()].ToString());
                                    break;
                                case "8":
                                    res.ReturnAttribute8 = CRMTypeMapping(e.Attributes[Columns[i].Trim()].ToString());
                                    break;
                                case "9":
                                    res.ReturnAttribute9 = CRMTypeMapping(e.Attributes[Columns[i].Trim()].ToString());
                                    break;
                            }
                        }
                    }
                    results.Add(res);
                }
                retrieveMultiple.Results = results;
            }
            return retrieveMultiple;
        }

        public CRMEntityList CRMGetAllEntities(CRMEntityList EntityList)
        {
            OrganizationServiceProxy _serviceProxy;
            List<CRMEntityMetadata> Results = new List<CRMEntityMetadata>();

            using (_serviceProxy = GetCRMConnection())
            {
                try
                {
                    RetrieveAllEntitiesRequest retrieveAllEntitiesRequest = new RetrieveAllEntitiesRequest
                    {
                        EntityFilters = EntityFilters.Entity,
                    };

                    RetrieveAllEntitiesResponse retrieveAllEntitiesResponse = (RetrieveAllEntitiesResponse)_serviceProxy.Execute(retrieveAllEntitiesRequest);
                    
                    foreach (EntityMetadata Metadata in retrieveAllEntitiesResponse.EntityMetadata)
                    {
                        try
                        {
                            CRMEntityMetadata Entity = new CRMEntityMetadata();
                            Entity.DisplayName = Metadata.DisplayName.UserLocalizedLabel != null ? Metadata.DisplayName.UserLocalizedLabel.Label.ToString() : Metadata.LogicalName;
                            Entity.ObjectTypeCode = Metadata.ObjectTypeCode.HasValue ? Metadata.ObjectTypeCode.Value : 0;
                            Entity.LogicalName = Metadata.LogicalName;
                            Entity.PrimaryIdAttribute = Metadata.PrimaryIdAttribute;
                            Entity.PrimaryNameAttribute = Metadata.PrimaryNameAttribute;
                            Entity.IsCustomEntity = Metadata.IsCustomEntity.HasValue ? Metadata.IsCustomEntity.Value : false;
                            Entity.Attributes = new List<CRMAttribute>();
                            Results.Add(Entity);
                        }
                        catch (Exception e)
                        {
                            throw;
                        }
                    }
                    //EntityList.Entities = new List<CRMEntityMetadata>();
                    EntityList.Entities = Results;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return EntityList;
        }

        public CRMPicklist CRMGetPicklist(CRMPicklist picklist)
        {
            OrganizationServiceProxy _serviceProxy;

            using (_serviceProxy = GetCRMConnection())
            {
                try
                {
                    RetrieveAttributeRequest retrieveAttributeRequest = new RetrieveAttributeRequest
                    {
                        EntityLogicalName = picklist.EntityLogicalName,
                        LogicalName = picklist.AttributeLogicalName
                    };
                    RetrieveAttributeResponse retrieveAttributeResponse = (RetrieveAttributeResponse)_serviceProxy.Execute(retrieveAttributeRequest);

                    PicklistAttributeMetadata pick = (PicklistAttributeMetadata)retrieveAttributeResponse.AttributeMetadata;

                    StateAttributeMetadata a = new StateAttributeMetadata();
                    

                    List<CRMPicklistOption> options = new List<CRMPicklistOption>();
                    foreach (OptionMetadata o in pick.OptionSet.Options)
                    {
                        CRMPicklistOption option = new CRMPicklistOption();
                        option.PicklistValue = o.Value.HasValue ? o.Value.Value : 0;
                        option.PicklistLabel = o.Label.UserLocalizedLabel.Label;
                        options.Add(option);
                    }
                    picklist.Picklist = options;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return picklist;
        }

        public CRMPicklist CRMGetStateStatus(CRMPicklist picklist)
        {
            OrganizationServiceProxy _serviceProxy;

            using (_serviceProxy = GetCRMConnection())
            {
                try
                {
                    RetrieveAttributeRequest retrieveAttributeStateRequest = new RetrieveAttributeRequest
                    {
                        EntityLogicalName = picklist.EntityLogicalName,
                        LogicalName = "statecode",
                    };
                    RetrieveAttributeResponse retrieveAttributeStateResponse = (RetrieveAttributeResponse)_serviceProxy.Execute(retrieveAttributeStateRequest);

                    RetrieveAttributeRequest retrieveAttributeStatusRequest = new RetrieveAttributeRequest
                    {
                        EntityLogicalName = picklist.EntityLogicalName,
                        LogicalName = "statuscode"
                    };
                    RetrieveAttributeResponse retrieveAttributeStatusResponse = (RetrieveAttributeResponse)_serviceProxy.Execute(retrieveAttributeStatusRequest);

                    StateAttributeMetadata state = (StateAttributeMetadata)retrieveAttributeStateResponse.AttributeMetadata;
                    StatusAttributeMetadata status = (StatusAttributeMetadata)retrieveAttributeStatusResponse.AttributeMetadata;


                    List<CRMPicklistOption> options = new List<CRMPicklistOption>();
                    foreach (StatusOptionMetadata o in status.OptionSet.Options)
                    {
                        OptionMetadata s = state.OptionSet.Options.Where(p => p.Value.Value == o.State.Value).First();

                        CRMPicklistOption option = new CRMPicklistOption();
                        option.PicklistValue = o.Value.HasValue ? o.Value.Value : 0;
                        option.PicklistLabel = o.Label.UserLocalizedLabel.Label;
                        option.PicklistParentLabel = s.Label.UserLocalizedLabel.Label.ToString();
                        option.PicklistParentValue = s.Value.HasValue ? s.Value.Value : 0; 
                        options.Add(option);
                    }
                    picklist.Picklist = options;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return picklist;
        }

        #region Helpers
        private OrganizationServiceProxy GetCRMConnection()
        {
            Uri crmOrg = new Uri(this.CRMOrganizationServiceURL);

            if (!string.IsNullOrEmpty(this.CRMUsername))
            {
                
                ClientCredentials creds = new ClientCredentials();
                //creds.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
                creds.Windows.ClientCredential = new NetworkCredential(this.CRMUsername, this.CRMPassword, this.CRMDomain);
                return new OrganizationServiceProxy(crmOrg, null, creds, null);
            }
            else
            {
                return new OrganizationServiceProxy(crmOrg, null, null, null);
            }           
        }

        private DiscoveryServiceProxy GetDiscoveryConnection()
        {
            Uri discovery = new Uri(this.CRMDiscoveryServiceURL);
            return new DiscoveryServiceProxy(discovery, null, null, null);
        }

        private bool CheckTaskField(string fieldname)
        {
            string requesturl = "http://crm.denallix.com/Denallix/xrmservices/2011/OrganizationData.svc/TaskSet?$select=" + fieldname;
            HttpWebRequest request = WebRequest.Create(requesturl) as HttpWebRequest;
            try
            {
                // Get response  
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode.ToString() != "200")
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private string AddTrailingSlash(string url)
        {
            if (url.LastIndexOf(@"/") != url.Length - 1)
            {
                return url + "/";
            }
            return url;
        }

        private string CRMTypeMapping(object value)
        {
            // to do: all other CRM types
            switch (value.GetType().ToString())
            {
                case "System.String":
                    return value.ToString();
                case "Microsoft.Xrm.Sdk.EntityReference":
                    return ((EntityReference)value).Id.ToString();
                //break;
                default:
                    return value.GetType().ToString();
            }
        }

        #endregion Helpers
    }
}
