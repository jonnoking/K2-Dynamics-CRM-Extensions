using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using SourceCode.Workflow.Client;
using K2.Demo.CRM.Workflow.Activity.Properties;
using System.Xml;

namespace K2.Demo.CRM.Workflow.Activity
{
    public class K2CRMWorkflowActivity : CodeActivity
    {
        public string K2Server = string.Empty;
        public string K2HostServerConnectionString = string.Empty;
        public string K2WorkflowServerConnectionString = string.Empty;
        public string K2ProcessName = string.Empty;
        public string K2StartOption = string.Empty;
        public string K2EntityInfo = "";
        public string K2EntityToStartFor = string.Empty;
        public string K2StartProcess = string.Empty;
        public string K2Folio = string.Empty;
        private Guid EntityID;
        private string CRMEntityName = string.Empty;
        
        protected override void Execute(CodeActivityContext executionContext)
        {
            SourceCode.Workflow.Client.Connection conn = null;
            SourceCode.Workflow.Client.ProcessInstance procInst = null;
            XmlDocument EntityDoc = null;
            string K2EntityIdDataField = string.Empty;
            string K2EntityNameDataField = string.Empty;
            string K2ContextXMLDataField = string.Empty;


            //Create the tracing service
            ITracingService tracingService = executionContext.GetExtension<ITracingService>();

            //Create the context
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
                        
            // get K2 configuration context
            // custom entity that stores core information for each entity and process

            EntityID = context.PrimaryEntityId;
            CRMEntityName = context.PrimaryEntityName.ToLower();
            

            //Get Processes to Start from the K2 Assosiation entity in CRM
            // Workflow Activity gets these values from the WF configuration
            // Plugin retrieves these values from the K2 Associations entity instance

            K2ProcessName = ProcessFullName.Get<string>(executionContext);
            K2EntityIdDataField = EntityIdDataField.Get<string>(executionContext);
            K2EntityNameDataField = EntityNameDataField.Get<string>(executionContext);
            K2ContextXMLDataField = ContextXMLDataField.Get<string>(executionContext);

            // Get K2 Settings
            #region K2 Settings
            //Get Connection Settings

            // all users need read access to the K2 Settings and K2 Associations Entity
            // need to find a way to give all users access by default
            // TODO: revert to system user to read K2settings

            EntityCollection k2settings = service.RetrieveMultiple(new FetchExpression(Resources.K2SettingsFetchXML));

            foreach (Entity setting in k2settings.Entities)
            {
                string name = setting["k2_name"].ToString();
                string value = setting["k2_value"].ToString();

                switch (name)
                {
                    case "WorkflowServerConnectionString":
                        K2WorkflowServerConnectionString = value;
                        break;
                    case "HostServerConnectionString":
                        K2HostServerConnectionString = value;
                        break;
                    case "K2ServerName":
                        K2Server = value;
                        break;
                }
            }
            #endregion K2 Settings

            #region Create XML Context
            
            ColumnSet allColumns = new ColumnSet(true);
            
            Entity currentEntity = service.Retrieve(CRMEntityName, EntityID, allColumns);

            Entity originatorUserEntity = service.Retrieve("systemuser", context.UserId, allColumns);

            XmlDocument inputDataDoc = new XmlDocument();
            
            //Create instantiation data for Entity
            EntityDoc = new XmlDocument();
            XmlElement EntElement = EntityDoc.CreateElement("CRMContext");

            //Create Item element
            XmlElement xmlItem = EntityDoc.CreateElement("Context");

            //Create Name Element
            XmlElement xmlName = EntityDoc.CreateElement("EntityId");
            xmlName.InnerText = EntityID.ToString();
            xmlItem.AppendChild(xmlName);

            xmlName = EntityDoc.CreateElement("EntityType");
            xmlName.InnerText = context.PrimaryEntityName;
            xmlItem.AppendChild(xmlName);

            XmlElement xmlOrgName = EntityDoc.CreateElement("Organization");
            xmlOrgName.InnerText = context.OrganizationName;
            xmlItem.AppendChild(xmlOrgName);

            xmlName = EntityDoc.CreateElement("CRMUserId");
            xmlName.InnerText = context.UserId.ToString();
            xmlItem.AppendChild(xmlName);

            xmlName = EntityDoc.CreateElement("UserFQN");
            xmlName.InnerText = originatorUserEntity["domainname"] != null ? originatorUserEntity["domainname"].ToString() : "";
            xmlItem.AppendChild(xmlName);

            xmlName = EntityDoc.CreateElement("UserDisplayName");
            xmlName.InnerText = originatorUserEntity["fullname"] != null ? originatorUserEntity["fullname"].ToString() : "";
            xmlItem.AppendChild(xmlName);

            //Add Item to main doc
            EntElement.AppendChild(xmlItem);

            EntityDoc.AppendChild(EntElement);

            //Release node objects
            EntElement = null;
            xmlName = null;

            #endregion Create XML Context

            conn = new Connection();
            //procInst = new ProcessInstance();

            try
            {
                ConnectionSetup connectSetup = new ConnectionSetup();
                connectSetup.ConnectionString = K2WorkflowServerConnectionString;

                conn.Open(connectSetup);

                if (originatorUserEntity != null && originatorUserEntity["domainname"] != null)
                { 
                    conn.ImpersonateUser(originatorUserEntity["domainname"].ToString());
                }

                //Create new process instance
                procInst = conn.CreateProcessInstance(K2ProcessName);

                //Set CRM context field value
                if (!string.IsNullOrEmpty(K2ContextXMLDataField))
                {
                    try
                    {
                        procInst.XmlFields[K2ContextXMLDataField].Value = EntityDoc.OuterXml.ToString();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.EventLog.WriteEntry("SourceCode.Logging.Extension.EventLogExtension", "K2 CRM Plugin - Entity Name - " + context.PrimaryEntityName.ToString() + " - " + "Error writing to XMLField " + K2ContextXMLDataField + " :::: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                    }
                }
                if (!string.IsNullOrEmpty(K2EntityIdDataField))
                {
                    try
                    {
                        procInst.DataFields[K2EntityIdDataField].Value = context.PrimaryEntityId;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.EventLog.WriteEntry("SourceCode.Logging.Extension.EventLogExtension", "K2 CRM Plugin - Entity Name - " + context.PrimaryEntityName.ToString() + " - " + "Error writing to DataField " + K2EntityIdDataField + " :::: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                    }

                }
                if (!string.IsNullOrEmpty(K2EntityNameDataField))
                {
                    try
                    {
                        procInst.DataFields[K2EntityNameDataField].Value = context.PrimaryEntityName.ToLower();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.EventLog.WriteEntry("SourceCode.Logging.Extension.EventLogExtension", "K2 CRM Plugin - Entity Name - " + context.PrimaryEntityName.ToString() + " - " + "Error writing to DataField " + K2EntityNameDataField + " :::: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                    }
                }
                // start the K2 process
                conn.StartProcessInstance(procInst);

                try
                {
                    System.Diagnostics.EventLog.WriteEntry("SourceCode.Logging.Extension.EventLogExtension", "Process Started: " + K2ProcessName, System.Diagnostics.EventLogEntryType.Information);
                }
                catch { }

            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("SourceCode.Logging.Extension.EventLogExtension", "Entity Name - " + context.PrimaryEntityName.ToString() + " - " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                throw;
            }
            finally
            {
                if (service != null)
                    service = null;

                if (conn != null)
                    conn.Dispose();
                    conn = null;

                if (procInst != null)
                    procInst = null;
                EntityDoc = null;
            }
        }

        [Input("Process Full Name")]
        [Default("")]
        public InArgument<string> ProcessFullName { get; set; }

        [Input("Entity Id Data Field")]
        [Default("")]
        public InArgument<string> EntityIdDataField { get; set; }

        [Input("Entity Name Data Field")]
        [Default("")]
        public InArgument<string> EntityNameDataField { get; set; }

        [Input("Context XML Data Field")]
        [Default("")]
        public InArgument<string> ContextXMLDataField { get; set; }

    }
}
