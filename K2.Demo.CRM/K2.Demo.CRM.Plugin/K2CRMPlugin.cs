using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Microsoft.Xrm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using K2.Demo.CRM.Plugin.Properties;
using System.Xml;
using SourceCode.Workflow.Client;

namespace K2.Demo.CRM.Plugin
{
    public class K2CRMPlugin : IPlugin
    {
        public string K2Server = string.Empty;
        public string K2HostServerConnectionString = string.Empty;
        public string K2WorkflowServerConnectionString = string.Empty;
        public string K2ProcessName = string.Empty;
        public string K2StartOption = string.Empty;
        public string K2EntityInfo = string.Empty;
        public string K2EntityToStartFor = string.Empty;
        public string K2StartProcess = string.Empty;
        public string K2Folio = string.Empty;
        private Guid? EntityID = null;
        private string CRMEntityName = string.Empty;

        public void Execute(IServiceProvider serviceProvider)
        {
            //Create the context
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            SourceCode.Workflow.Client.Connection conn = null;
            SourceCode.Workflow.Client.ProcessInstance procInst = null;
            string K2EntityIdDataField = string.Empty;
            string K2EntityNameDataField = string.Empty;
            string K2ContextXMLDataField = string.Empty;
            Entity currentEntity = new Entity();

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                // Obtain the target business entity from the input parameters.
                currentEntity = (Entity)context.InputParameters["Target"];
            }

            if (context.OutputParameters.Contains("id"))
            {
                EntityID = new Guid(context.OutputParameters["id"].ToString());
            }
            else if (currentEntity.Id != null)
            {
                EntityID = currentEntity.Id;
            }

            // Get K2 Associations
            #region K2 Associations

            string K2AssociationsFetchXML = Resources.K2AssociationsFetchXML.Replace("[startoption]", context.MessageName);
            K2AssociationsFetchXML = K2AssociationsFetchXML.Replace("[entityname]", currentEntity.LogicalName.ToLower());
            EntityCollection k2associations = service.RetrieveMultiple(new FetchExpression(K2AssociationsFetchXML));
            
            #endregion

            // if an K2 Association is found then get K2 Settings and start K2 process for each association
            if (k2associations.Entities.Count > 0)
            {
                // Get K2 Settings
                #region K2 Settings
                //Get Connection Settings

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

                bool StartProcess = false;

                // start process for each association returned
                foreach (Entity entity in k2associations.Entities)
                {
                    StartProcess = MustStartForMessage(entity["k2_startoption"].ToString(), context.MessageName);
                    if (StartProcess)
                    {
                        if (entity.Contains("k2_processfullname"))
                        {
                            K2ProcessName = entity["k2_processfullname"].ToString();
                        }
                        if (entity.Contains("k2_entityiddatafield"))
                        {
                            K2EntityIdDataField = entity["k2_entityiddatafield"].ToString();
                        }
                        if (entity.Contains("k2_entitynamedatafield"))
                        {
                            K2EntityNameDataField = entity["k2_entitynamedatafield"].ToString();
                        }
                        if (entity.Contains("k2_contextxmldatafield"))
                        {
                            K2ContextXMLDataField = entity["k2_contextxmldatafield"].ToString();
                        }


                        #region Create XML Context
                        XmlDocument EntityDoc = null;
                        ColumnSet allColumns = new ColumnSet(true);

                        //Entity currentEntity = service.Retrieve(CRMEntityName, EntityID, allColumns);

                        Entity originatorUserEntity = service.Retrieve("systemuser", context.InitiatingUserId, allColumns);

                        XmlDocument inputDataDoc = new XmlDocument();

                        //Create instantiation data for Entity
                        EntityDoc = new XmlDocument();
                        XmlElement EntElement = EntityDoc.CreateElement("CRMContext");

                        //Create Item element
                        XmlElement xmlItem = EntityDoc.CreateElement("Context");

                        //Create Name Element
                        XmlElement xmlName = EntityDoc.CreateElement("EntityId");
                        xmlName.InnerText = EntityID.HasValue ? EntityID.Value.ToString() : "";
                        xmlItem.AppendChild(xmlName);

                        xmlName = EntityDoc.CreateElement("EntityType");
                        xmlName.InnerText = currentEntity.LogicalName;
                        xmlItem.AppendChild(xmlName);

                        XmlElement xmlOrgName = EntityDoc.CreateElement("Organization");
                        xmlOrgName.InnerText = context.OrganizationName;
                        xmlItem.AppendChild(xmlOrgName);

                        xmlName = EntityDoc.CreateElement("CRMUserId");
                        xmlName.InnerText = context.InitiatingUserId.ToString();
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

                        EntElement = null;
                        xmlName = null;

                        #endregion Create XML Context

                        #region Start K2 Process

                        conn = new SourceCode.Workflow.Client.Connection();

                        try
                        {
                            //Open connection to K2 Server
                            ConnectionSetup connectSetup = new ConnectionSetup();
                            connectSetup.ConnectionString = K2WorkflowServerConnectionString;

                            conn.Open(connectSetup);

                            // impersonate as originators if domainname retrieved from CRM systemuser
                            if (originatorUserEntity != null && originatorUserEntity.Contains("domainname"))
                            {
                                if (!string.IsNullOrEmpty(originatorUserEntity["domainname"].ToString()))
                                {
                                    conn.ImpersonateUser(originatorUserEntity["domainname"].ToString());
                                }
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


                        #endregion  Start K2 Process

                    }
                }
            }
        }

        private Boolean MustStartForMessage(string K2StartOption, string MessageName)
        {
            string[] StartOptions = K2StartOption.Split('|');
            for (int i = 0; i < StartOptions.Length; i++)
            {
                if (StartOptions[i].Trim().ToLower() == MessageName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
