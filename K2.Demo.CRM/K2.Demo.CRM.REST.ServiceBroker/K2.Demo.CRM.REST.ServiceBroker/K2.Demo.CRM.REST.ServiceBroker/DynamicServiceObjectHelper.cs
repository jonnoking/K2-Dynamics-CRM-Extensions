using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Web.Services;
using System.Net;
using K2.PSUK.ServiceObjectSchema;
using SourceCode.SmartObjects.Services.ServiceSDK;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using RestSharp;
using K2.Demo.CRM.Wizard.Functions;
using K2.Demo.CRM.REST.ServiceBroker.Properties;

namespace K2.Demo.CRM.REST.ServiceBroker
{
    public class DynamicServiceObjectHelper
    {
        public ServiceInstance SvcI;
        public ServiceAssemblyBase SvcBase;

        public K2CRMConfig config;
        public CRMConfig crmconfig;

        public DynamicServiceObjectHelper(ServiceAssemblyBase svcBase)
        {
            //when an instance of this class is created set the internal variables to 
            //the base class
            SvcBase = svcBase;
            SvcI = svcBase.Service;

        }

        public void DescribeSchema()
        {
            string schemaPath = Environment.CurrentDirectory + @"\K2.Demo.CRM.REST.ServiceBroker.xml";
            SchemaObject schemaObject = SchemaManager.LoadSchemaXMLFile(schemaPath);

            //Populate the details of the current instance of the service base and create an instance of the service object

            SvcI.Name = schemaObject.ServiceInstanceName;
            SvcI.MetaData.DisplayName = schemaObject.ServiceInstanceDisplayName;
            SvcI.MetaData.Description = schemaObject.ServiceInstanceDescription;

            //Create the service object instance
            ServiceObject SO = new ServiceObject(schemaObject.ServiceInstanceName);
            SO.MetaData.DisplayName = schemaObject.ServiceInstanceDisplayName;
            SO.Active = true;

            //Create the properties for the serviceObject
            //Create a property for the serviceObject
            Property prop = null;

            //Dynamic Properties;
            foreach (SchemaObject.SchemaProperty sProp in schemaObject.SchemaProperties)
            {
                prop = SchemaManager.CreateProperty(sProp.Name,sProp.DisplayName, sProp.Description, sProp.TrueType,sProp.K2Type);
                //Add this property to the service object properties collection
                SO.Properties.Add(prop);
            }

            SourceCode.SmartObjects.Services.ServiceSDK.Objects.Method meth = null;
            foreach (SchemaObject.SchemaMethod sMeth in schemaObject.SchemaMethods)
            {
                meth = SchemaManager.CreateMethod(sMeth.Name,sMeth.DisplayName,sMeth.Description,sMeth.K2Type,sMeth.InputProperties,sMeth.RequiredProperties,sMeth.ReturnProperties);
                SO.Methods.Add(meth);
            }

            SvcI.ServiceObjects.Add(SO);
        }

        public void Execute()
        {
            SvcI = SvcBase.Service;

            config = GetK2CRMConfig(SvcI.ServiceConfiguration.ServiceAuthentication.UserName, SvcI.ServiceConfiguration.ServiceAuthentication.Password, GetConfigPropertyValue("RESTServiceURL"));
            crmconfig = new CRMConfig
            {
                CRMURL = GetConfigPropertyValue("CRMURL"),
                CRMOrganization = GetConfigPropertyValue("CRMOrganization")
            };


            ServiceObject so = SvcI.ServiceObjects[0];
            string methodName = so.Methods[0].Name;
            switch (methodName.ToLower())
            {
                case "changeowner":
                    ChangeOwner(ref so);
                    break;
                case "setstatestatus":
                    SetStateStatus(ref so);
                    break;
                case "getentities":
                    GetEntities(ref so);
                    break;
                case "bulkactiontasks":
                    BulkActionTasks(ref so);
                    break;
                case "createtask":
                    CreateTask(ref so);
                    break;
                case "getcrmuser":
                    GetCRMUser(ref so);
                    break;
                case "startcrmworkflow":
                    StartCRMWorkflow(ref so);
                    break;
                case "getentitymetadata":
                    GetEntityMetadata(ref so);
                    break;
                case "bulkactiontaskssetcriteria":
                    BulkActionTasksSetCriteria(ref so);
                    break;
                case "retrievemultiple":
                    RetrieveMultiple(ref so);
                    break;
                case "getallentities":
                    GetAllEntities(ref so);
                    break;
                case "getentityattributes":
                    GetEntityAttributes(ref so);
                    break;
                case "getpicklist":
                    GetPicklist(ref so);
                    break;
                case "getstatestatuspicklist":
                    GetStateStatusPicklist(ref so);
                    break;
                    
            }
        }

        private void ChangeOwner(ref ServiceObject so)
        {
            SourceCode.SmartObjects.Services.ServiceSDK.Objects.Method meth = so.Methods[0];
            //K2CRMHelper helper = new K2CRMHelper();
            WizardFunctions helper = new WizardFunctions();
            CRMEntityOwnership owner = new CRMEntityOwnership();
            owner.Config = crmconfig;

            try
            {
                owner.Assignee = NotNull(so.Properties["Assignee"].Value);
                owner.AssigneeId = NotNull(so.Properties["AssigneeId"].Value);
                owner.Target = NotNull(so.Properties["Target"].Value);
                owner.TargetId = NotNull(so.Properties["TargetId"].Value);

                RestResponse<CRMEntityOwnership> response = helper.ChangeOwner(owner, config);

                so.Properties.InitResultTable();

                for (int c = 0; c < meth.ReturnProperties.Count; c += 1)
                {
                    Property prop = so.Properties[meth.ReturnProperties[c]];
                    prop = SetChangeOwnerProperties(prop, response);
                }

                so.Properties.BindPropertiesToResultTable();

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        private void SetStateStatus(ref ServiceObject so)
        {
            SourceCode.SmartObjects.Services.ServiceSDK.Objects.Method meth = so.Methods[0];
            WizardFunctions helper = new WizardFunctions();

            CRMState state = new CRMState();
            state.Config = crmconfig;

            try
            {
                state.Entity = NotNull(so.Properties["Entity"].Value);
                state.EntityId = NotNull(so.Properties["EntityId"].Value);
                state.State = int.Parse(NotNull(so.Properties["State"].Value));
                state.Status = int.Parse(NotNull(so.Properties["Status"].Value));

                RestResponse<CRMState> response = helper.SetStateStatus(state, config);

                so.Properties.InitResultTable();

                for (int c = 0; c < meth.ReturnProperties.Count; c += 1)
                {
                    Property prop = so.Properties[meth.ReturnProperties[c]];
                    prop = SetStateStatusProperties(prop, response);
                }

                so.Properties.BindPropertiesToResultTable();

            }
            catch (Exception ex)
            {
                throw;
            }
        }
                
        private void GetEntities(ref ServiceObject so)
        {
            SourceCode.SmartObjects.Services.ServiceSDK.Objects.Method meth = so.Methods[0];
            WizardFunctions helper = new WizardFunctions();

            CRMFetchXML fetch = new CRMFetchXML();
            fetch.Config = crmconfig;

            try
            {
                fetch.FetchXML = NotNull(so.Properties["FetchXML"].Value);

                RestResponse<CRMFetchXML> response = helper.GetEntities(fetch, config);
               
                so.Properties.InitResultTable();

                if (response.Data != null && response.Data.Entities != null)
                {
                    foreach (CRMState state in response.Data.Entities)
                    {
                        for (int c = 0; c < meth.ReturnProperties.Count; c += 1)
                        {
                            Property prop = so.Properties[meth.ReturnProperties[c]];
                            prop = SetGetEntitiesProperties(prop, state);
                            prop = SetGetEntitiesRESTProperties(prop, response);
                        }
                        so.Properties.BindPropertiesToResultTable();
                    }
                }
                else
                {
                    for (int c = 0; c < meth.ReturnProperties.Count; c += 1)
                    {
                        Property prop = so.Properties[meth.ReturnProperties[c]];
                        prop = SetGetEntitiesRESTProperties(prop, response);
                    }
                    so.Properties.BindPropertiesToResultTable();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
                                
        private void BulkActionTasks(ref ServiceObject so)
        {
            SourceCode.SmartObjects.Services.ServiceSDK.Objects.Method meth = so.Methods[0];
            WizardFunctions helper = new WizardFunctions();

            CRMBulkActionTask fetch = new CRMBulkActionTask();
            fetch.Config = crmconfig;

            try
            {
                fetch.FetchXML = NotNull(so.Properties["FetchXML"].Value);
                fetch.FromState = int.Parse(NotNull(so.Properties["FromState"].Value));
                fetch.FromStatus = int.Parse(NotNull(so.Properties["FromStatus"].Value));
                fetch.ToState = int.Parse(NotNull(so.Properties["ToState"].Value));
                fetch.ToStatus = int.Parse(NotNull(so.Properties["ToStatus"].Value));

                RestResponse<CRMBulkActionTask> response = helper.BulkActionTasks(fetch, config);

                so.Properties.InitResultTable();
                
                if (response.Data != null && response.Data.Entities != null)
                {
                    foreach (CRMState state in response.Data.Entities)
                    {
                        for (int c = 0; c < meth.ReturnProperties.Count; c += 1)
                        {
                            Property prop = so.Properties[meth.ReturnProperties[c]];
                            prop = SetGetEntitiesProperties(prop, state);
                            prop = SetBulkActionTasksRESTProperties(prop, response);
                        }
                        so.Properties.BindPropertiesToResultTable();
                    }
                }
                else
                {
                    for (int c = 0; c < meth.ReturnProperties.Count; c += 1)
                    {
                        Property prop = so.Properties[meth.ReturnProperties[c]];
                        prop = SetBulkActionTasksRESTProperties(prop, response);
                    }
                    so.Properties.BindPropertiesToResultTable();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void BulkActionTasksSetCriteria(ref ServiceObject so)
        {
            SourceCode.SmartObjects.Services.ServiceSDK.Objects.Method meth = so.Methods[0];
            WizardFunctions helper = new WizardFunctions();

            CRMBulkActionTask fetch = new CRMBulkActionTask();
            fetch.Config = crmconfig;

            try
            {
                string fetchstring = string.Empty;
                if (!string.IsNullOrEmpty(NotNull(so.Properties["K2ActivityName"])))
                {
                    fetchstring = Resources.K2CRMTaskCleanUpFinishRule.Replace("[entityid]", NotNull(so.Properties["RegardingId"].Value)).Replace("[entityname]", NotNull(so.Properties["Regarding"].Value));
                    fetchstring = fetchstring.Replace("[processname]", NotNull(so.Properties["K2ProcessName"].Value));
                    fetchstring = fetchstring.Replace("[statecode]", NotNull(so.Properties["FromState"].Value));
                    fetchstring = fetchstring.Replace("[processinstanceid]", NotNull(so.Properties["K2ProcessInstanceId"].Value));
                }
                else
                {
                    fetchstring = Resources.K2CRMTaskCleanUpState0FetchXML.Replace("[entityid]", NotNull(so.Properties["RegardingId"].Value)).Replace("[entityname]", NotNull(so.Properties["Regarding"].Value));
                    fetchstring = fetchstring.Replace("[activityname]", NotNull(so.Properties["K2ActivityName"].Value));
                    fetchstring = fetchstring.Replace("[processname]", NotNull(so.Properties["K2ProcessName"].Value));
                    fetchstring = fetchstring.Replace("[statecode]", NotNull(so.Properties["FromState"].Value));
                    fetchstring = fetchstring.Replace("[processinstanceid]", NotNull(so.Properties["K2ProcessInstanceId"].Value));   
                }
                
                fetch.FetchXML = fetchstring;
                fetch.FromState = int.Parse(NotNull(so.Properties["FromState"].Value));
                fetch.FromStatus = int.Parse(NotNull(so.Properties["FromStatus"].Value));
                fetch.ToState = int.Parse(NotNull(so.Properties["ToState"].Value));
                fetch.ToStatus = int.Parse(NotNull(so.Properties["ToStatus"].Value));

                RestResponse<CRMBulkActionTask> response = helper.BulkActionTasks(fetch, config);

                so.Properties.InitResultTable();

                if (response.Data != null && response.Data.Entities != null)
                {
                    foreach (CRMState state in response.Data.Entities)
                    {
                        for (int c = 0; c < meth.ReturnProperties.Count; c += 1)
                        {
                            Property prop = so.Properties[meth.ReturnProperties[c]];
                            prop = SetGetEntitiesProperties(prop, state);
                            prop = SetBulkActionTasksRESTProperties(prop, response);
                        }
                        so.Properties.BindPropertiesToResultTable();
                    }
                }
                else
                {
                    for (int c = 0; c < meth.ReturnProperties.Count; c += 1)
                    {
                        Property prop = so.Properties[meth.ReturnProperties[c]];
                        prop = SetBulkActionTasksRESTProperties(prop, response);
                    }
                    so.Properties.BindPropertiesToResultTable();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
                         
        private void CreateTask(ref ServiceObject so)
        {
            SourceCode.SmartObjects.Services.ServiceSDK.Objects.Method meth = so.Methods[0];
            WizardFunctions helper = new WizardFunctions();

            bool async = false;
            bool.TryParse(NotNull(so.Properties["ExecuteAsync"].Value), out async);
            config.Async = async;
            

            CRMTask task = new CRMTask();
            task.Config = crmconfig;

            task.Category = NotNull(so.Properties["Category"].Value);
            task.Description = NotNull(so.Properties["Description"].Value);
            task.DueDate = DateTime.Parse(NotNull(so.Properties["DueDate"].Value));
            task.Duration = int.Parse(NotNull(so.Properties["Duration"].Value));
            task.OwnerFQN = NotNull(so.Properties["OwnerFQN"].Value);
            task.OwnerId = NotNull(so.Properties["OwnerId"].Value);
            task.Owner = NotNull(so.Properties["Owner"].Value);
            task.Priority = int.Parse(NotNull(so.Properties["Priority"].Value));
            task.Regarding = NotNull(so.Properties["Regarding"].Value);
            task.RegardingId = NotNull(so.Properties["RegardingId"].Value);
            task.State = int.Parse(NotNull(so.Properties["State"].Value));
            task.Status = int.Parse(NotNull(so.Properties["Status"].Value));
            task.Subcategory = NotNull(so.Properties["Subcategory"].Value);
            task.Subject = NotNull(so.Properties["Subject"].Value);
            task.K2SerialNumber = NotNull(so.Properties["K2SerialNumber"].Value);
            task.K2ActivityName = NotNull(so.Properties["K2ActivityName"].Value);
            task.K2ProcessName = NotNull(so.Properties["K2ProcessName"].Value);
            task.K2ProcessInstanceId = int.Parse(NotNull(so.Properties["K2ProcessInstanceId"].Value));

            try
            {
                RestResponse<CRMTask> response = helper.CreateTask(task, config);

                if (response != null)
                {
                    so.Properties.InitResultTable();

                    for (int c = 0; c < meth.ReturnProperties.Count; c += 1)
                    {
                        Property prop = so.Properties[meth.ReturnProperties[c]];
                        prop = SetTaskProperties(prop, response);
                    }
                    so.Properties.BindPropertiesToResultTable();
                }
                {
                    // do something for async call
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        private void GetCRMUser(ref ServiceObject so)
        {
            SourceCode.SmartObjects.Services.ServiceSDK.Objects.Method meth = so.Methods[0];
            WizardFunctions helper = new WizardFunctions();
            CRMUser user = new CRMUser();
            user.Config = crmconfig;

            try
            {
                user.UserFQN = NotNull(so.Properties["UserFQN"].Value);

                RestResponse<CRMUser> response = helper.GetCRMUser(user, config);

                so.Properties.InitResultTable();

                for (int c = 0; c < meth.ReturnProperties.Count; c += 1)
                {
                    Property prop = so.Properties[meth.ReturnProperties[c]];
                    prop = SetCRMUserProperties(prop, response);
                }

                so.Properties.BindPropertiesToResultTable();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void StartCRMWorkflow(ref ServiceObject so)
        {
            SourceCode.SmartObjects.Services.ServiceSDK.Objects.Method meth = so.Methods[0];
            WizardFunctions helper = new WizardFunctions();
            CRMWorkflow crmWF = new CRMWorkflow();
            crmWF.Config = crmconfig;

            try
            {
                crmWF.EntityId = NotNull(so.Properties["EntityId"].Value);
                crmWF.WorkflowName = NotNull(so.Properties["WorkflowName"].Value);

                RestResponse<CRMWorkflow> response = helper.StartWorkflow(crmWF, config);

                so.Properties.InitResultTable();

                for (int c = 0; c < meth.ReturnProperties.Count; c += 1)
                {
                    Property prop = so.Properties[meth.ReturnProperties[c]];
                    prop = SetCRMWorkflowProperties(prop, response);
                }

                so.Properties.BindPropertiesToResultTable();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void GetEntityMetadata(ref ServiceObject so)
        {
            SourceCode.SmartObjects.Services.ServiceSDK.Objects.Method meth = so.Methods[0];
            //K2CRMHelper helper = new K2CRMHelper();
            WizardFunctions helper = new WizardFunctions();
            CRMEntityMetadata entitymetadata = new CRMEntityMetadata();
            entitymetadata.Config = crmconfig;

            try
            {
                entitymetadata.LogicalName = NotNull(so.Properties["EntityLogicalName"].Value);
                entitymetadata.IncludeAttributes = false;
                RestResponse<CRMEntityMetadata> response = helper.GetEntityMetadata(entitymetadata, config);

                so.Properties.InitResultTable();

                for (int c = 0; c < meth.ReturnProperties.Count; c += 1)
                {
                    Property prop = so.Properties[meth.ReturnProperties[c]];
                    prop = SetEntityMetadataProperties(prop, response);
                }

                so.Properties.BindPropertiesToResultTable();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void GetAllEntities(ref ServiceObject so)
        {
            SourceCode.SmartObjects.Services.ServiceSDK.Objects.Method meth = so.Methods[0];
            WizardFunctions helper = new WizardFunctions();
            CRMEntityList entitymetadata = new CRMEntityList();
            entitymetadata.Config = crmconfig;

            try
            {
                RestResponse<CRMEntityList> response = helper.GetAllEntities(entitymetadata, config);

                so.Properties.InitResultTable();
                foreach (CRMEntityMetadata ret in response.Data.Entities)
                {
                    for (int c = 0; c < meth.ReturnProperties.Count; c += 1)
                    {
                        Property prop = so.Properties[meth.ReturnProperties[c]];
                        prop = SetEntityMetadataProperties(prop, ret);
                    }
                    so.Properties.BindPropertiesToResultTable();
                }
            }
            catch (Exception ex)
            {
                throw;
            }                       
        }

        private void GetEntityAttributes(ref ServiceObject so)
        {
            SourceCode.SmartObjects.Services.ServiceSDK.Objects.Method meth = so.Methods[0];
            //K2CRMHelper helper = new K2CRMHelper();
            WizardFunctions helper = new WizardFunctions();
            CRMEntityMetadata entitymetadata = new CRMEntityMetadata();
            entitymetadata.Config = crmconfig;

            try
            {
                entitymetadata.LogicalName = NotNull(so.Properties["EntityLogicalName"].Value);
                entitymetadata.IncludeAttributes = true;
                RestResponse<CRMEntityMetadata> response = helper.GetEntityMetadata(entitymetadata, config);

                so.Properties.InitResultTable();
                foreach (CRMAttribute ret in response.Data.Attributes)
                {
                    for (int c = 0; c < meth.ReturnProperties.Count; c += 1)
                    {
                        Property prop = so.Properties[meth.ReturnProperties[c]];
                        prop = SetGetEntityAttributeProperties(prop, ret);
                    }
                    so.Properties.BindPropertiesToResultTable();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void GetPicklist(ref ServiceObject so)
        {
            SourceCode.SmartObjects.Services.ServiceSDK.Objects.Method meth = so.Methods[0];
            //K2CRMHelper helper = new K2CRMHelper();
            WizardFunctions helper = new WizardFunctions();
            CRMPicklist picklist = new CRMPicklist();
            picklist.Config = crmconfig;

            try
            {
                picklist.EntityLogicalName = NotNull(so.Properties["EntityLogicalName"].Value);
                picklist.AttributeLogicalName = NotNull(so.Properties["AttributeLogicalName"].Value);

                RestResponse<CRMPicklist> response = helper.GetPicklist(picklist, config);

                so.Properties.InitResultTable();
                foreach (CRMPicklistOption ret in response.Data.Picklist)
                {
                    for (int c = 0; c < meth.ReturnProperties.Count; c += 1)
                    {
                        Property prop = so.Properties[meth.ReturnProperties[c]];
                        prop = SetGetPicklistOptionProperties(prop, ret);
                    }
                    so.Properties.BindPropertiesToResultTable();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void GetStateStatusPicklist(ref ServiceObject so)
        {
            SourceCode.SmartObjects.Services.ServiceSDK.Objects.Method meth = so.Methods[0];
            //K2CRMHelper helper = new K2CRMHelper();
            WizardFunctions helper = new WizardFunctions();
            CRMPicklist picklist = new CRMPicklist();
            picklist.Config = crmconfig;

            try
            {
                picklist.EntityLogicalName = NotNull(so.Properties["EntityLogicalName"].Value);

                RestResponse<CRMPicklist> response = helper.CRMGetStateStatus(picklist, config);

                so.Properties.InitResultTable();
                foreach (CRMPicklistOption ret in response.Data.Picklist)
                {
                    for (int c = 0; c < meth.ReturnProperties.Count; c += 1)
                    {
                        Property prop = so.Properties[meth.ReturnProperties[c]];
                        prop = SetGetPicklistOptionProperties(prop, ret);
                    }
                    so.Properties.BindPropertiesToResultTable();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void RetrieveMultiple(ref ServiceObject so)
        {
            SourceCode.SmartObjects.Services.ServiceSDK.Objects.Method meth = so.Methods[0];
            //K2CRMHelper helper = new K2CRMHelper();
            WizardFunctions helper = new WizardFunctions();
            CRMRetrieveMultiple multiple = new CRMRetrieveMultiple();
            multiple.Config = crmconfig;

            try
            {
                multiple.LinkFromEntityName = NotNull(so.Properties["LinkFromEntityName"].Value);
                multiple.LinkFromAttributeName = NotNull(so.Properties["LinkFromAttributeName"].Value);
                multiple.LinkToEntityName = NotNull(so.Properties["LinkToEntityName"].Value);
                multiple.LinkToAttributeName = NotNull(so.Properties["LinkToAttributeName"].Value);
                multiple.ConditionAttributeName = NotNull(so.Properties["ConditionAttributeName"].Value);
                multiple.ConditionOperator = NotNull(so.Properties["ConditionOperator"].Value);
                multiple.ConditionValue = NotNull(so.Properties["ConditionValue"].Value);
                multiple.ReturnAttributes = NotNull(so.Properties["ReturnAttributes"].Value);

                RestResponse<CRMRetrieveMultiple> response = helper.RetrieveMultiple(multiple, config);

                so.Properties.InitResultTable();

                foreach (CRMRetrieveMultipleReturn ret in response.Data.Results)
                { 
                    for (int c = 0; c < meth.ReturnProperties.Count; c += 1)
                    {
                        Property prop = so.Properties[meth.ReturnProperties[c]];
                        prop = SetRetrieveMultipleProperties(prop, response);
                        prop = SetRetrieveMultipleReturnProperties(prop, ret);
                    }
                    so.Properties.BindPropertiesToResultTable();
                }
                

            }
            catch (Exception ex)
            {
                throw;
            }
        }




        private Property SetTaskProperties(Property prop, RestResponse<CRMTask> task)
        {
            if (task.Data != null)
            {
                switch (prop.Name.ToLower())
                {
                    case "taskid":
                        prop.Value = task.Data.Id;
                        break;
                    case "category":
                        prop.Value = task.Data.Category;
                        break;
                    case "description":
                        prop.Value = task.Data.Description;
                        break;
                    case "duedate":
                        prop.Value = task.Data.DueDate;
                        break;
                    case "duration":
                        prop.Value = task.Data.Duration;
                        break;
                    case "ownerfqn":
                        prop.Value = task.Data.OwnerFQN;
                        break;
                    case "owner":
                        prop.Value = task.Data.Owner;
                        break;
                    case "ownerid":
                        prop.Value = task.Data.OwnerId;
                        break;
                    case "priority":
                        prop.Value = task.Data.Priority;
                        break;
                    case "regarding":
                        prop.Value = task.Data.Regarding;
                        break;
                    case "regardingid":
                        prop.Value = task.Data.RegardingId;
                        break;
                    case "state":
                        prop.Value = task.Data.State;
                        break;
                    case "status":
                        prop.Value = task.Data.Status;
                        break;
                    case "subcategory":
                        prop.Value = task.Data.Subcategory;
                        break;
                    case "subject":
                        prop.Value = task.Data.Subject;
                        break;
                    case "k2serialnumber":
                        prop.Value = task.Data.K2SerialNumber;
                        break;
                    case "k2processname":
                        prop.Value = task.Data.K2ProcessName;
                        break;
                    case "k2activityname":
                        prop.Value = task.Data.K2ActivityName;
                        break;
                    case "k2processinstanceid":
                        prop.Value = task.Data.K2ProcessInstanceId;
                        break;
                }
            }

            switch (prop.Name.ToLower())
            {
                case "restcontent":
                    prop.Value = task.Content;
                    break;
                //case "resterrorexception":
                //    prop.Value = NotNull(task.ErrorException.StackTrace);
                //    break;
                case "resterrormessage":
                    prop.Value = task.ErrorMessage;
                    break;
                case "reststatuscode":
                    prop.Value = task.StatusCode.ToString();
                    break;
                case "reststatusdescription":
                    prop.Value = task.StatusDescription;
                    break;
            }

            return prop;
        }

        private Property SetChangeOwnerProperties(Property prop, RestResponse<CRMEntityOwnership> task)
        {
            if (task.Data != null)
            {
                switch (prop.Name.ToLower())
                {
                    case "assignee":
                        prop.Value = task.Data.Assignee;
                        break;
                    case "assigneeid":
                        prop.Value = task.Data.AssigneeId;
                        break;
                    case "target":
                        prop.Value = task.Data.Target;
                        break;
                    case "targetId":
                        prop.Value = task.Data.TargetId;
                        break;
                }
            }
            switch (prop.Name.ToLower())
            {
                case "restcontent":
                    prop.Value = task.Content;
                    break;
                //case "resterrorexception":
                //    prop.Value = NotNull(task.ErrorException.StackTrace);
                //    break;
                case "resterrormessage":
                    prop.Value = task.ErrorMessage;
                    break;
                case "reststatuscode":
                    prop.Value = task.StatusCode.ToString();
                    break;
                case "reststatusdescription":
                    prop.Value = task.StatusDescription;
                    break;
            }

            return prop;
        }

        private Property SetStateStatusProperties(Property prop, RestResponse<CRMState> task)
        {
            if (task.Data != null)
            {
                switch (prop.Name.ToLower())
                {
                    case "entity":
                        prop.Value = task.Data.Entity;
                        break;
                    case "entityid":
                        prop.Value = task.Data.EntityId;
                        break;
                    case "state":
                        prop.Value = task.Data.State;
                        break;
                    case "status":
                        prop.Value = task.Data.Status;
                        break;
                }
            }
            switch (prop.Name.ToLower())
            {
                case "restcontent":
                    prop.Value = task.Content;
                    break;
                //case "resterrorexception":
                //    prop.Value = NotNull(task.ErrorException.StackTrace);;
                //    break;
                case "resterrormessage":
                    prop.Value = task.ErrorMessage;
                    break;
                case "reststatuscode":
                    prop.Value = task.StatusCode.ToString();
                    break;
                case "reststatusdescription":
                    prop.Value = task.StatusDescription;
                    break;
            }
            return prop;
        }

        private Property SetGetEntitiesProperties(Property prop, CRMState task)
        {
            if (task != null)
            {
                switch (prop.Name.ToLower())
                {
                    case "entity":
                        prop.Value = task.Entity;
                        break;
                    case "entityid":
                        prop.Value = task.EntityId;
                        break;
                    case "state":
                        prop.Value = task.State;
                        break;
                    case "status":
                        prop.Value = task.Status;
                        break;
                }
            }
            return prop;
        }

        private Property SetGetEntitiesRESTProperties(Property prop, RestResponse<CRMFetchXML> task)
        {
            if (task.Data != null)
            {
                switch (prop.Name.ToLower())
                {
                    case "fetchxml":
                        prop.Value = task.Data.FetchXML;
                        break;
                }
            }
            
            switch (prop.Name.ToLower())
            {
                case "restcontent":
                    prop.Value = task.Content;
                    break;
                //case "resterrorexception":
                //    prop.Value = NotNull(task.ErrorException.StackTrace);
                //    break;
                case "resterrormessage":
                    prop.Value = NotNull(task.ErrorMessage);
                    break;
                case "reststatuscode":
                    prop.Value = task.StatusCode.ToString();
                    break;
                case "reststatusdescription":
                    prop.Value = task.StatusDescription;
                    break;
            }
            return prop;
        }

        private Property SetBulkActionTasksRESTProperties(Property prop, RestResponse<CRMBulkActionTask> task)
        {
            if (task.Data != null)
            { 
                switch (prop.Name.ToLower())
                {
                    case "fetchxml":
                        prop.Value = task.Data.FetchXML;
                        break;
                    case "fromstate":
                        prop.Value = task.Data.FromState;
                        break;
                    case "fromstatus":
                        prop.Value = task.Data.FromStatus;
                        break;
                    case "tostate":
                        prop.Value = task.Data.ToState;
                        break;
                    case "tostatus":
                        prop.Value = task.Data.ToStatus;
                        break;
                }
            }

            switch (prop.Name.ToLower())
            {
                case "restcontent":
                    prop.Value = task.Content;
                    break;
                //case "resterrorexception":
                //    prop.Value = NotNull(task.ErrorException.StackTrace);
                //    break;
                case "resterrormessage":
                    prop.Value = task.ErrorMessage;
                    break;
                case "reststatuscode":
                    prop.Value = task.StatusCode.ToString();
                    break;
                case "reststatusdescription":
                    prop.Value = task.StatusDescription;
                    break;
            }
            return prop;
        }

        private Property SetCRMUserProperties(Property prop, RestResponse<CRMUser> task)
        {
            if (task.Data != null)
            {
                switch (prop.Name.ToLower())
                {
                    case "userfqn":
                        prop.Value = task.Data.UserFQN;
                        break;
                    case "userid":
                        prop.Value = task.Data.UserId;
                        break;
                }
            }
            
            switch (prop.Name.ToLower())
            {
                case "restcontent":
                    prop.Value = task.Content;
                    break;
                //case "resterrorexception":
                //    prop.Value = NotNull(task.ErrorException.StackTrace);
                //    break;
                case "resterrormessage":
                    prop.Value = task.ErrorMessage;
                    break;
                case "reststatuscode":
                    prop.Value = task.StatusCode.ToString();
                    break;
                case "reststatusdescription":
                    prop.Value = task.StatusDescription;
                    break;
            }
            return prop;
        }

        private Property SetCRMWorkflowProperties(Property prop, RestResponse<CRMWorkflow> task)
        {
            if (task.Data != null)
            {
                switch (prop.Name.ToLower())
                {
                    case "entityid":
                        prop.Value = task.Data.EntityId;
                        break;
                    case "workflowid":
                        prop.Value = task.Data.WorkflowId;
                        break;
                }
            }

            switch (prop.Name.ToLower())
            {
                case "restcontent":
                    prop.Value = task.Content;
                    break;
                //case "resterrorexception":
                //    prop.Value = NotNull(task.ErrorException.StackTrace);
                //    break;
                case "resterrormessage":
                    prop.Value = task.ErrorMessage;
                    break;
                case "reststatuscode":
                    prop.Value = task.StatusCode.ToString();
                    break;
                case "reststatusdescription":
                    prop.Value = task.StatusDescription;
                    break;
            }
            return prop;
        }

        private Property SetEntityMetadataProperties(Property prop, CRMEntityMetadata task)
        {
            if (task != null)
            {
                switch (prop.Name.ToLower())
                {
                    case "entitylogicalname":
                        prop.Value = task.LogicalName;
                        break;
                    case "entitydisplayname":
                        prop.Value = task.DisplayName;
                        break;
                    case "entityobjecttypecode":
                        prop.Value = task.ObjectTypeCode;
                        break;
                    case "entityprimaryidattribute":
                        prop.Value = task.PrimaryIdAttribute;
                        break;
                    case "entityprimarynameattribute":
                        prop.Value = task.PrimaryNameAttribute;
                        break;
                    case "entityiscustomentity":
                        prop.Value = task.IsCustomEntity;
                        break;
                }
            }
            return prop;
        }

        private Property SetEntityMetadataProperties(Property prop, RestResponse<CRMEntityMetadata> task)
        {
            if (task.Data != null)
            {
                switch (prop.Name.ToLower())
                {
                    case "entitylogicalname":
                        prop.Value = task.Data.LogicalName;
                        break;
                    case "entitydisplayname":
                        prop.Value = task.Data.DisplayName;
                        break;
                    case "entityobjecttypecode":
                        prop.Value = task.Data.ObjectTypeCode;
                        break;
                    case "entityprimaryidattribute":
                        prop.Value = task.Data.PrimaryIdAttribute;
                        break;
                    case "entityprimarynameattribute":
                        prop.Value = task.Data.PrimaryNameAttribute;
                        break;
                    case "entityiscustomentity":
                        prop.Value = task.Data.IsCustomEntity;
                        break;
                }
            }

            switch (prop.Name.ToLower())
            {
                case "restcontent":
                    prop.Value = task.Content;
                    break;
                //case "resterrorexception":
                //    prop.Value = NotNull(task.ErrorException.StackTrace);
                //    break;
                case "resterrormessage":
                    prop.Value = task.ErrorMessage;
                    break;
                case "reststatuscode":
                    prop.Value = task.StatusCode.ToString();
                    break;
                case "reststatusdescription":
                    prop.Value = task.StatusDescription;
                    break;
            }
            return prop;
        }

        private Property SetRetrieveMultipleProperties(Property prop, RestResponse<CRMRetrieveMultiple> task)
        {
            if (task.Data != null)
            {
                switch (prop.Name.ToLower())
                {
                    case "linkfromentityname":
                        prop.Value = task.Data.LinkFromEntityName;
                        break;
                    case "linkfromfromattributename":
                        prop.Value = task.Data.LinkFromAttributeName;
                        break;
                    case "linktoattributename":
                        prop.Value = task.Data.LinkToAttributeName;
                        break;
                    case "linktoentityname":
                        prop.Value = task.Data.LinkToEntityName;
                        break;
                    case "conditionattributename":
                        prop.Value = task.Data.ConditionAttributeName;
                        break;
                    case "conditionoperator":
                        prop.Value = task.Data.ConditionOperator;
                        break;
                    case "conditionvalue":
                        prop.Value = task.Data.ConditionValue;
                        break;
                    case "returnattributes":
                        prop.Value = task.Data.ReturnAttributes;
                        break;
                }
            }
            switch (prop.Name.ToLower())
            {
                case "restcontent":
                    prop.Value = task.Content;
                    break;
                case "resterrormessage":
                    prop.Value = task.ErrorMessage;
                    break;
                case "reststatuscode":
                    prop.Value = task.StatusCode.ToString();
                    break;
                case "reststatusdescription":
                    prop.Value = task.StatusDescription;
                    break;
            }

            return prop;
        }

        private Property SetRetrieveMultipleReturnProperties(Property prop, CRMRetrieveMultipleReturn task)
        {
            switch (prop.Name.ToLower())
            {
                case "entityid":
                    prop.Value = task.EntityId;
                    break;
                case "returnattribute0":
                    prop.Value = task.ReturnAttribute0;
                    break;
                case "returnattribute1":
                    prop.Value = task.ReturnAttribute1;
                    break;
                case "returnattribute2":
                    prop.Value = task.ReturnAttribute2;
                    break;
                case "returnattribute3":
                    prop.Value = task.ReturnAttribute3;
                    break;
                case "returnattribute4":
                    prop.Value = task.ReturnAttribute4;
                    break;
                case "returnattribute5":
                    prop.Value = task.ReturnAttribute5;
                    break;
                case "returnattribute6":
                    prop.Value = task.ReturnAttribute6;
                    break;
                case "returnattribute7":
                    prop.Value = task.ReturnAttribute7;
                    break;
                case "returnattribute8":
                    prop.Value = task.ReturnAttribute8;
                    break;
                case "returnattribute9":
                    prop.Value = task.ReturnAttribute9;
                    break;
            }

            return prop;
        }


        private Property SetGetEntityAttributeProperties(Property prop, CRMAttribute task)
        {
            switch (prop.Name.ToLower())
            {
                case "attributelogicalname":
                    prop.Value = task.LogicalName;
                    break;
                case "attributeschemaname":
                    prop.Value = task.SchemaName;
                    break;
                case "attributedisplayname":
                    prop.Value = task.DisplayName;
                    break;
                case "attributetype":
                    prop.Value = task.AttributeType;
                    break;
                case "attributedescription":
                    prop.Value = task.Description;
                    break;
                case "attributerequiredlevel":
                    prop.Value = task.RequiredLevel;
                    break;
                case "attributeisvalidforcreate":
                    prop.Value = task.IsValidForCreate;
                    break;
                case "attributeisvalidforread":
                    prop.Value = task.IsValidForRead;
                    break;
                case "attributeisvalidforupdate":
                    prop.Value = task.IsValidForUpdate;
                    break;
            }

            return prop;
        }

        private Property SetGetPicklistOptionProperties(Property prop, CRMPicklistOption task)
        {
            switch (prop.Name.ToLower())
            {
                case "picklistlabel":
                    prop.Value = task.PicklistLabel;
                    break;
                case "picklistvalue":
                    prop.Value = task.PicklistValue;
                    break;
                case "picklistparentlabel":
                    prop.Value = task.PicklistParentLabel;
                    break;
                case "picklistparentvalue":
                    prop.Value = task.PicklistParentValue;
                    break;
            }

            return prop;
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

        
        private string GetConfigPropertyValue(string propertyName)
        {
            string configValue = "";

            if (SvcBase.Service.ServiceConfiguration.Contains(propertyName) == true)
            {
                if (SvcBase.Service.ServiceConfiguration[propertyName] != null)
                {
                    configValue = SvcBase.Service.ServiceConfiguration[propertyName].ToString();
                }
            }
            return configValue;
        }

        public string NotNull(object x)
        {
            if (x != null)
            {
                return x.ToString();
            }
            else
            {
                return "";
            }
        }

    }
}
