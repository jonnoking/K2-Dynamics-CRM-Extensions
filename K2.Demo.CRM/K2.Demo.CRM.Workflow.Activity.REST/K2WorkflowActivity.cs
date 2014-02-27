using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System.Activities;
using System.Net;
using System.IO;
using System.Xml;

namespace K2.Demo.CRM.Workflow.Activity.REST
{
    public class K2WorkflowActivity : CodeActivity
    {

        //public string K2Server = string.Empty;
        //public string K2HostServerConnectionString = string.Empty;
        //public string K2WorkflowServerConnectionString = string.Empty;
        //public string K2ProcessName = string.Empty;
        //public string K2StartOption = string.Empty;
        //public string K2EntityInfo = "";
        //public string K2EntityToStartFor = string.Empty;
        //public string K2StartProcess = string.Empty;
        //public string K2Folio = string.Empty;

        protected override void Execute(CodeActivityContext executionContext)
        {
            Guid EntityID;
            string CRMEntityName = string.Empty;

            string K2ProcessFullName = string.Empty;
            string K2Folio = string.Empty;
            int K2ProcesPriority = 2;
            string K2ServicesBaseUri = string.Empty;
            string K2EntityIdDataField = string.Empty;
            string K2EntityNameDataField = string.Empty;
            string K2ContextXMLDataField = string.Empty;
            string K2OriginatorDataField = string.Empty;

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


            // get inputs from WF activity configuration
            K2ProcessFullName = InputProcessFullName.Get<string>(executionContext);
            K2EntityIdDataField = InputEntityIdDataField.Get<string>(executionContext);
            K2EntityNameDataField = InputEntityNameDataField.Get<string>(executionContext);
            K2Folio = InputFolio.Get<string>(executionContext);
            K2ProcesPriority = InputPriority.Get<int>(executionContext);
            K2OriginatorDataField = InputOriginatorDataField.Get<string>(executionContext);
            K2ServicesBaseUri = InputServiceBaseUri.Get<string>(executionContext);

            ColumnSet allColumns = new ColumnSet(true);

            // get entity WF is running against
            Entity currentEntity = service.Retrieve(CRMEntityName, EntityID, allColumns);

            // get system user entity of user starting WF
            Entity originatorUserEntity = service.Retrieve("systemuser", context.UserId, allColumns);


            string originator = string.Empty;

            string ProcessStartXml = Properties.Resources.K2ServicesStartProcess;

            try
            {

                if (originatorUserEntity != null && originatorUserEntity["domainname"] != null)
                {
                    originator = originatorUserEntity["domainname"].ToString();
                }

                // create process start xml for service -- needs validation & error handling 
                ProcessStartXml = ProcessStartXml.Replace("[FULLNAME]", K2ProcessFullName).Replace("[FOLIO]", K2Folio).Replace("[PRIORITY]", K2ProcesPriority.ToString())
                    .Replace("[ENTITY ID]", K2EntityIdDataField).Replace("[ENTITY ID VALUE]", EntityID.ToString()).Replace("[ENTITY NAME]", K2EntityNameDataField).Replace("[ENTITY NAME VALUE]", CRMEntityName)
                    .Replace("[CRM ORIGINATOR]", K2OriginatorDataField).Replace("[CRM ORIGINATOR VALUE]", originator); ;

                XmlDocument xmlstart = new XmlDocument();
                xmlstart.LoadXml(ProcessStartXml);

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(K2ServicesBaseUri + "REST.svc/Process/Instances/StartInstance?synchronous=false");
                request.UseDefaultCredentials = true;
                request.Method = "POST";
                request.ContentType = "application/xml";

                // hack to get content length, required if not running .net 4
                //using (MemoryStream stream = new MemoryStream())
                //{
                //    using (var writer = new StreamWriter(stream))
                //    {
                //        writer.Write(ProcessStartXml);
                //        request.ContentLength = stream.Length;
                //        writer.Close();
                //    }
                //}

                using (var writer = XmlWriter.Create(request.GetRequestStream()))
                {
                    xmlstart.WriteTo(writer);
                }

                WebResponse response = null;
                Stream responseStream = null;
                StreamReader responseReader = null;

                try
                {
                    request.Timeout = 20000;

                    response = request.GetResponse();

                    // if you want to check the response from the service call
                    //if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK /* 200 */ )
                    //{
                    //    responseStream = response.GetResponseStream();
                    //    responseReader = new StreamReader(responseStream);
                    //    string responseString = responseReader.ReadToEnd();


                    //}
                    //else
                    //{
                    //throw new Exception("Process start failed");
                    //}
                    System.Diagnostics.EventLog.WriteEntry("SourceCode.Logging.Extension.EventLogExtension", string.Format("K2 process {0} started successfully for Entity ID: {1}, Entity Logical Name: {2}.", K2ProcessFullName, EntityID, CRMEntityName), System.Diagnostics.EventLogEntryType.Information);
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("SourceCode.Logging.Extension.EventLogExtension", string.Format("Error starting K2 process {0} for Entity ID: {1}, Entity Logical Name: {2}. Error: {3}", K2ProcessFullName, EntityID, CRMEntityName, ex.Message), System.Diagnostics.EventLogEntryType.Error);
            }

        }

        [Input("Process Full Name")]
        [RequiredArgument]
        [Default("")]
        public InArgument<string> InputProcessFullName { get; set; }

        [Input("Entity Id Data Field")]
        [RequiredArgument]
        [Default("")]
        public InArgument<string> InputEntityIdDataField { get; set; }

        [Input("Entity Name Data Field")]
        [RequiredArgument]
        [Default("")]
        public InArgument<string> InputEntityNameDataField { get; set; }

        [Input("Originator Data Field")]
        [RequiredArgument]
        [Default("")]
        public InArgument<string> InputOriginatorDataField { get; set; }

        [Input("Folio")]
        [RequiredArgument]
        [Default("")]
        public InArgument<string> InputFolio { get; set; }

        [Input("Priority")]
        [Default("")]
        public InArgument<int> InputPriority { get; set; }

        [Input("ServiceBaseUri")]
        [RequiredArgument]
        [Default("http://dlx.denallix.com:81/")]
        public InArgument<string> InputServiceBaseUri { get; set; }
    }
}
