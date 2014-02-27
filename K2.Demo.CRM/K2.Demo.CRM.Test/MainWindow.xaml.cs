using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
using K2.Demo.CRM.Functions;
using System.Collections.Specialized;
using System.IO;
using RestSharp;
using SourceCode.Workflow.Authoring;
using SourceCode.Workflow.Design;
using K2.Demo.CRM.Test.Authoring;
using SourceCode.Framework;
using SourceCode.Workflow.Design.SimpleRules;
using SourceCode.Workflow.Design.Outcome;
using SourceCode.Workflow.Design.SmartObjects;
using SourceCode.SmartObjects.Client;

namespace K2.Demo.CRM.Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            K2.Demo.CRM.Functions.CRMConfig crmconfig = new K2.Demo.CRM.Functions.CRMConfig
            {
                CRMURL = "http://crm.denallix.com",
                CRMOrganization = "Denallix"
            };
             functions = new Functions.CRMFunctions(crmconfig);

        }

        K2.Demo.CRM.Functions.CRMFunctions functions;

        private void btnAddDocLocation_Click(object sender, RoutedEventArgs e)
        {
            OrganizationServiceProxy _serviceProxy;
            IOrganizationService _service;
            //res = "";

            using (_serviceProxy = GetCRMConnection())
            {
                _serviceProxy.ServiceConfiguration.CurrentServiceEndpoint.Behaviors.Add(new ProxyTypesBehavior());

                _service = (IOrganizationService)_serviceProxy;

                Entity spdl = new Entity("sharepointdocumentlocation");

                spdl.Attributes["name"] = "Demo";
                spdl.Attributes["absoluteurl"] = txtDocumentLocationURL.Text;
                spdl.Attributes["regardingobjectid"] = new EntityReference("account", new Guid(txtEntityId.Text));

                try
                {
                    _service.Create(spdl);
                    //res = resp.ResponseName;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }




        private OrganizationServiceProxy GetCRMConnection()
        {
            string CRMOrganizationServiceURL = "http://crm.denallix.com/Denallix/XRMServices/2011/Organization.svc";

            Uri crmOrg = new Uri(CRMOrganizationServiceURL);
            return new OrganizationServiceProxy(crmOrg, null, null, null);
        }

        private DiscoveryServiceProxy GetDiscoveryConnection()
        {
            string CRMDiscoveryServiceURL = "http://crm.denallix.com/XRMServices/2011/Discovery.svc";
            Uri discovery = new Uri(CRMDiscoveryServiceURL);
            return new DiscoveryServiceProxy(discovery, null, null, null);
        }

        private string AddTrailingSlash(string url)
        {
            if (url.LastIndexOf(@"/") != url.Length - 1)
            {
                return url + "/";
            }
            return url;
        }

        private void btnGetEntities_Click(object sender, RoutedEventArgs e)
        {

            K2.Demo.CRM.Functions.CRMEntityList el = new Functions.CRMEntityList();

            cbEntities.ItemsSource = functions.CRMGetAllEntities(el).Entities;

        }

        private void btnGetAttributes_Click(object sender, RoutedEventArgs e)
        {
            CRMEntityMetadata emd = cbEntities.SelectedItem as CRMEntityMetadata;
            CRMEntityMetadata md = new CRMEntityMetadata();
            md.LogicalName = emd.LogicalName;
            md.IncludeAttributes = true;

            cbAttributes.ItemsSource = functions.CRMGetEntityMetadata(md).Attributes.Where(p => p.AttributeType == "Picklist");

        }

        private void btnGetState_Click(object sender, RoutedEventArgs e)
        {
            CRMEntityMetadata emd = cbEntities.SelectedItem as CRMEntityMetadata;
            CRMAttribute att = cbAttributes.SelectedItem as CRMAttribute;
            
            CRMPicklist pl = new CRMPicklist();
            pl.EntityLogicalName = emd.LogicalName;



            cbState.ItemsSource = functions.CRMGetStateStatus(pl).Picklist;
        }

        List<StateStatus> CRMSS = new List<StateStatus>();

        private void btnGetPicklist_Click(object sender, RoutedEventArgs e)
        {
            CRMEntityMetadata emd = cbEntities.SelectedItem as CRMEntityMetadata;
            CRMAttribute att = cbAttributes.SelectedItem as CRMAttribute;

            CRMPicklist pl = new CRMPicklist();
            pl.AttributeLogicalName = att.LogicalName;
            pl.EntityLogicalName = emd.LogicalName;

            CRMPicklist cp = functions.CRMGetPicklist(pl);

            cbOptions.ItemsSource = cp.Picklist.OrderByDescending(p => p.PicklistParentValue).OrderBy(p => p.PicklistValue);
        }

        private void btnGetRESTEntities_Click(object sender, RoutedEventArgs e)
        {
            Wizard.Functions.K2CRMConfig config = new Wizard.Functions.K2CRMConfig{
                User = "administrator", 
                Domain = "Denallix", 
                Password = "K2pass!"
            };

            Wizard.Functions.CRMEntityList el = new Wizard.Functions.CRMEntityList();

            K2.Demo.CRM.Wizard.Functions.WizardFunctions functions = new Wizard.Functions.WizardFunctions();
            RestSharp.RestResponse<Wizard.Functions.CRMEntityList> a = functions.GetAllEntities(el, config);
            int q = 0;
        }


        // Process Authoring


        const string CRMFunctionsSmartObjectGuid = "676582a1-3fb1-4f01-99db-aa2db7621f58";
        DefaultProcess process = null;
        private void btnGenerateCRMProcess_Click(object sender, RoutedEventArgs e)
        {
            SourceCode.SmartObjects.Client.SmartObjectClientServer smoServer = new SmartObjectClientServer();
            smoServer.CreateConnection();
            smoServer.Connection.Open("Integrated=True;IsPrimaryLogin=True;Authenticate=True;EncryptedPassword=False;Host=localhost;Port=5555");

            SmartObject smoCRM = smoServer.GetSmartObject("Demo_K2_CRM_Functions");
            smoCRM.MethodToExecute = "GetAllEntities";

            SmartObjectList smoEntities = smoServer.ExecuteList(smoCRM);


            // get state status details
            CRMPicklist pl = new CRMPicklist();
//            pl.AttributeLogicalName = att.LogicalName;
            pl.EntityLogicalName = "lead";

            CRMPicklist cp = functions.CRMGetStateStatus(pl);

            int seq = 0;
            foreach (CRMPicklistOption plo in cp.Picklist.OrderBy(p => p.PicklistParentValue).OrderBy(p => p.PicklistValue))
            {
                StateStatus ss = new StateStatus();
                ss.State = plo.PicklistParentValue;
                ss.StateName = plo.PicklistParentLabel;
                ss.Status = plo.PicklistValue;
                ss.StatusName = plo.PicklistLabel;
                ss.Sequence = seq;
                seq++;
                CRMSS.Add(ss);
            }


            bool ActiveOnly = true;


            string nowish = DateTime.Now.ToString("yyyyMMddHHmmss");
            // Create new process
            process = WorkflowFactory.CreateProcess<DefaultProcess>(nowish, WizardNames.DefaultProcess);

            DataField dfEntityId = new DataField("Entity Id", "");
            DataField dfEntityName = new DataField("Entity Name", "");
            process.DataFields.Add(dfEntityId);
            process.DataFields.Add(dfEntityName);


            var dimensions = WorkflowHelpers.GetActivityDimensions(process.StartActivity);
            int x = Convert.ToInt32(Math.Round(dimensions.X + dimensions.Width + 40D));
            int y = Convert.ToInt32(dimensions.Y) + 100;

            DefaultActivity PrevStatAct = null;
            DefaultActivity PrevReviewAct = null;

            PrevStatAct = CreateStartActivity("", dfEntityId, dfEntityName, x, ref y);

            SourceCode.Workflow.Authoring.Line startLine = WorkflowFactory.CreateLine("StartLine"); 
            startLine.StartActivity = process.StartActivity;
            startLine.FinishActivity = PrevStatAct;
            process.Lines.Add(startLine);

            int c = 0;

            foreach (StateStatus ss in CRMSS.OrderBy(p => p.Sequence))
            {
                DefaultActivity act = CreateStatusActivity(ss.StateName + " - " + ss.StatusName, dfEntityId, dfEntityName, x, ref y);
                if (PrevReviewAct != null)
                {
                    PrevReviewAct.FinishLines[0].FinishActivity = act;
                }

                if (c == 0)
                {
                    SourceCode.Workflow.Authoring.Line firstline = WorkflowFactory.CreateLine("Firstline");
                    firstline.StartActivity = PrevStatAct;
                    firstline.FinishActivity = act;
                    process.Lines.Add(firstline);
                }
                c++;

                DefaultActivity act1 = null;
                if (!ActiveOnly || ActiveOnly && ss.State == 0)
                {
                    act1 = CreateCRMActivity(ss.StateName + " - " + ss.StatusName, dfEntityId, dfEntityName, x, ref y);

                    SourceCode.Workflow.Authoring.Line line = WorkflowFactory.CreateLine("Line " + ss.Sequence);
                    line.StartActivity = act;
                    line.FinishActivity = act1;
                    process.Lines.Add(line);

                    if (PrevStatAct != null)
                    {
                        act1.FinishLines[1].FinishActivity = PrevStatAct;
                    }
                }

                if (act1 == null && PrevStatAct.FinishLines.Count == 0)
                {
                    PrevReviewAct = null;
                    SourceCode.Workflow.Authoring.Line updateLine = WorkflowFactory.CreateLine("Update Line " + ss.Sequence);
                    updateLine.StartActivity = PrevStatAct;
                    updateLine.FinishActivity = act;
                    process.Lines.Add(updateLine);
                }


                if (act != null)
                {
                    WorkflowHelpers.AutoPositionLines(act.StartLines);
                    WorkflowHelpers.AutoPositionLines(act.FinishLines);
                }
                if (act1 != null)
                {
                    WorkflowHelpers.AutoPositionLines(act1.StartLines);
                    WorkflowHelpers.AutoPositionLines(act1.FinishLines);
                }

                PrevReviewAct = act1;
                PrevStatAct = act;
            }
            


            process.FinishRule = new DesignCRMClient.CRMClientProcessFinishRule();

            process.SaveAs(txtProcessPath.Text+nowish+".kprx");

            process = null;
        }

        private DefaultActivity CreateStartActivity(string status, DataField dfEntityid, DataField dfEntityName, int x, ref int y)
        {
            DefaultActivity activity = WorkflowFactory.CreateActivity<DefaultActivity>("Set Value", WizardNames.DefaultActivity);
            process.Activities.Add(activity);
            activity.MetaData = "Generated by K2 for Dynamics CRM 2011 Process Generation Tool";
            activity.ExpectedDuration = 5;
            activity.Priority = 2;
            WorkflowHelpers.PositionActivity(activity, x, y);
            y += 100;

            return activity;
        }


        private DefaultActivity CreateStatusActivity(string status, DataField dfEntityid, DataField dfEntityName, int x, ref int y)
        {
            DefaultActivity actUpdate = WorkflowFactory.CreateActivity<DefaultActivity>(status + " Update", WizardNames.DefaultActivity);
            process.Activities.Add(actUpdate);
            actUpdate.MetaData = "Generated by K2 for Dynamics CRM 2011 Process Generation Tool";
            actUpdate.ExpectedDuration = 5;
            actUpdate.Priority = 2;
            WorkflowHelpers.PositionActivity(actUpdate, x, y);
            y += 100;

            // Create Event
            var eventname = "Update Staus - " + status;
            var smoUpdateStatus = WorkflowFactory.CreateEvent<SmartObjectEvent>(eventname, WizardNames.SmartObjectEvent);

            // Configure Event
            var smoInputs = new Dictionary<string, K2Field>();
            smoInputs.Add("EntityId", K2FieldFactory.CreateK2Field(dfEntityid));
            smoInputs.Add("Entity", K2FieldFactory.CreateK2Field(dfEntityName));
            smoInputs.Add("State", K2FieldFactory.CreateK2Field(2));
            smoInputs.Add("Status", K2FieldFactory.CreateK2Field(6));

            var smoOutputs = new Dictionary<string, K2Field>();

            smoUpdateStatus.EventItem.Properties = CreateSmartFormatProperties(
                CRMFunctionsSmartObjectGuid,
                "SmartObject Server",
                "K2_CRM_Functions",
                "K2 CRM Functions",
                "SetStateStatus",
                "Set State Status",
                false,
                smoInputs,
                smoOutputs,
                "Integrated=True;IsPrimaryLogin=True;Authenticate=True;EncryptedPassword=False;Host=localhost;Port=5555");

            actUpdate.Events.Add(smoUpdateStatus);

            return actUpdate;
        }


        private DefaultActivity CreateCRMActivity(string ActName, DataField dfEntityid, DataField dfEntityName, int x, ref int y)
        {

            DefaultActivity actCRMReview = WorkflowFactory.CreateActivity<DefaultActivity>(ActName + " Review", WizardNames.DefaultActivity);
            process.Activities.Add(actCRMReview);
            actCRMReview.MetaData = "Generated by K2 for Dynamics CRM 2011 Process Generation Tool";
            actCRMReview.ExpectedDuration = 1440;
            actCRMReview.Priority = 2;
            actCRMReview.Slots = 1;
            actCRMReview.Type = ActivityTypes.DestinationInstanceParralel;
            WorkflowHelpers.PositionActivity(actCRMReview, x, y);
            y += 100;

            DesignCRMClient.CRMClientEvent crmClientEvent = WorkflowFactory.CreateEvent<DesignCRMClient.CRMClientEvent>(ActName + " Review", WizardNames.CRMClientEvent);
            
            crmClientEvent.EventItem.CRMCustomSNParameter.Parts.Add("k2_sn");
            crmClientEvent.EventItem.CRMEntityId.Parts.Add(dfEntityid);
            crmClientEvent.EventItem.CRMEntityType.Parts.Add(dfEntityName);
            crmClientEvent.EventItem.CRMFunctionsSmartObject.Parts.Add("K2_CRM_Functions");
            crmClientEvent.EventItem.CRMOrganisation.Parts.Add("Denallix");

            // Add some destination users
            SimpleDestinationRule destinationRule = new SimpleDestinationRule();
            DestinationSet defaultDestinationSet = new DestinationSet();
            Destination destination1 = new Destination();
            destination1.Type = DestinationTypes.User;

            SourceCode.Workflow.Design.WorkflowContextFieldPart fpProcOrigName = new WorkflowContextFieldPart();
            fpProcOrigName.WfProps = "ProcessOriginatorName";
            fpProcOrigName.WorkflowContextProperty = WorkflowContextProperty.ProcessOriginatorName;

            destination1.Value = new K2Field(fpProcOrigName);

            defaultDestinationSet.Destinations.Add(destination1);
            destinationRule.DestinationSets.Add(defaultDestinationSet);
            actCRMReview.DestinationRule = destinationRule;
            destinationRule.ResolveQueuesToUsers = true;

            crmClientEvent.EventItem.TaskSubject.Parts.Add(new WorkflowContextFieldPart(WorkflowContextProperty.ProcessFolio));
            crmClientEvent.EventItem.TaskSubject.Parts.Add(" - ");
            crmClientEvent.EventItem.TaskSubject.Parts.Add(new WorkflowContextFieldPart(WorkflowContextProperty.ActivityInstanceName));

            crmClientEvent.EventItem.TaskDescription.Parts.Add(new WorkflowContextFieldPart(WorkflowContextProperty.ProcessFolio));
            crmClientEvent.EventItem.TaskDescription.Parts.Add(" - ");
            crmClientEvent.EventItem.TaskDescription.Parts.Add(new WorkflowContextFieldPart(WorkflowContextProperty.ActivityInstanceName));

            crmClientEvent.EventItem.TaskOwnerFQN.Parts.Add(new WorkflowContextFieldPart(WorkflowContextProperty.ActivityInstanceDestUserName));
            crmClientEvent.EventItem.ProcessName.Parts.Add(new WorkflowContextFieldPart(WorkflowContextProperty.ProcessName));
            crmClientEvent.EventItem.ActivityName.Parts.Add(new WorkflowContextFieldPart(WorkflowContextProperty.ActivityInstanceName));
            crmClientEvent.EventItem.TaskCategory.Parts.Add(new WorkflowContextFieldPart(WorkflowContextProperty.ProcessName));
            crmClientEvent.EventItem.TaskSubcategory.Parts.Add(new WorkflowContextFieldPart(WorkflowContextProperty.ActivityInstanceName));


            actCRMReview.SucceedingRule = new DesignCRMClient.CRMClientActivitySucceedingRule();
            crmClientEvent.SucceedingRule = new DesignCRMClient.CRMClientEventSucceedingRule();

            actCRMReview.Events.Add(crmClientEvent);

            // Add two actions for the client event
            EventAction approveAction = WorkflowFactory.CreateK2Object<EventAction>("Approve");
            approveAction.ActionItem = new DefaultOutcomeAction();
            EventAction declineAction = WorkflowFactory.CreateK2Object<EventAction>("Decline");
            declineAction.ActionItem = new DefaultOutcomeAction();
            crmClientEvent.Actions.Add(approveAction);
            crmClientEvent.Actions.Add(declineAction);


            // Find the default succeeding rule property wizard definition,
            // and replace it with the default outcome succeeding rule
            //PropertyWizardDefinition propWizDefSimple = WorkflowHelpers.FindOfType<SimpleSucceedingRulePropertyWizardDefinition>(actCRMReview.WizardDefinition.PropertyWizardDefinitions);
            //PropertyWizardDefinition propWizDefOutcome = WorkflowHelpers.FindOfType<OutcomeSucceedingRulePropertyWizardDefinition>(actCRMReview.WizardDefinition.PropertyWizardDefinitions);
            //if (propWizDefSimple != null && propWizDefOutcome == null)
            //{
            //    actCRMReview.WizardDefinition.PropertyWizardDefinitions.Remove(propWizDefSimple);
            //    actCRMReview.WizardDefinition.PropertyWizardDefinitions.Add(
            //        WorkflowFactory.CreatePropertyWizardDefinition(PropertyWizardNames.OutcomeSucceedingRule));
            //}
            
            SourceCode.Workflow.Design.Outcome.Common.GenerateDefaultOutcomesForActions(crmClientEvent);
            SourceCode.Workflow.Design.Outcome.Common.GenerateDefaultLinesForOutcomes(actCRMReview.SucceedingRule as DesignCRMClient.CRMClientActivitySucceedingRule);
            SourceCode.Workflow.Design.Outcome.Common.SyncActivityAndEventSucceedingRule(crmClientEvent);
            return actCRMReview;

        }


        internal static SmartFormatProperties CreateSmartFormatProperties(
            string soGuid, string server, string name, string displayname,
            string methodname, string methoddisplayname,
            bool methodislist,
            System.Collections.Generic.Dictionary<string, K2Field> SOInputs,
            System.Collections.Generic.Dictionary<string, K2Field> SOReturns,
            string connection)
        {

            Guid smoGuid;


            SmartObjectClientServer svr = new SmartObjectClientServer();
            svr.CreateConnection();
            svr.Connection.Open(connection);

            SmartObject so = svr.GetSmartObject(name);
            smoGuid = so.Guid;

            SmartFormatProperties properties = new SmartFormatProperties();
            properties.Locals["Guid"] = new Local("Guid", soGuid);
            //properties.Locals["Server"] = new Local("Server", "Integrated=True;IsPrimaryLogin=True;Authenticate=True;EncryptedPassword=False;Host=Localhost;Port=5555");
            properties.Locals["Server"] = new Local("Server", connection);
            properties.Locals["Name"] = new Local("Name", name);
            properties.Locals["DisplayName"] = new Local("DisplayName", displayname);
            properties.Locals["MethodName"] = new Local("MethodName", methodname);
            properties.Locals["MethodDisplayName"] = new Local("MethodDisplayName", methoddisplayname);
            properties.Locals["IsList"] = new Local("IsList", new K2Field(new K2FieldPart[] { new ValueTypePart(methodislist) }));
            properties.Locals["MethodType"] = new Local("MethodType", "execute");

            foreach (SmartProperty item in so.Properties)
            {
                properties.Properties.Add(item.Name, new Property(item.Name, item.Name, null, item.Type.ToString(), item.IsUnique));
            }

            //Add inputs
            foreach (var item in SOInputs)
            {
                if (so.Methods[methodname].InputProperties.Contains(item.Key))
                {
                    SmartProperty prop = so.Methods[methodname].InputProperties[item.Key];
                    properties.Inputs.Add(prop.Name, new Input(prop.Name, prop.Type.ToString(), item.Value));
                }

                foreach (SmartParameter itemParams in so.Methods[methodname].Parameters)
                {
                    if (itemParams.Name == item.Key)
                    {
                        SmartProperty prop = so.Methods[methodname].Parameters[item.Key];
                        properties.Inputs.Add(prop.Name, new Input(prop.Name, prop.Type.ToString(), item.Value));
                    }
                }

            }


            //Map return properties
            foreach (SmartProperty item in so.Methods[methodname].ReturnProperties)
            {
                bool found = false;

                //Search and see if we have passed a mapped item 
                if (SOReturns != null)
                {
                    foreach (var SetReturnItem in SOReturns)
                    {
                        if (SetReturnItem.Key == item.Name)
                        {
                            SmartProperty prop = so.Methods[methodname].ReturnProperties[SetReturnItem.Key];
                            properties.Returns.Add(prop.Name, new Return(prop.Name, SetReturnItem.Value, prop.Type.ToString()));
                            found = true;
                        }
                    }
                }

                //If no mapped item found set a null value.
                if (found == false)
                {
                    properties.Returns.Add(item.Name, new Return(item.Name, item.Name, item.IsUnique, null, item.Type.ToString()));
                }
            }


            return properties;
        }

        private void btnRestCred_Click(object sender, RoutedEventArgs e)
        {
            string BaseUrl = "http://crm.denallix.com/Denallix/xrmservices/2011/OrganizationData.svc/AccountSet";
            //string Resource = "AccountSet?";
            var client = new RestClient();
            client.BaseUrl = BaseUrl;
            
            CredentialCache credCache = new CredentialCache();

            credCache.GetCredential(new Uri(RemoveTrailingSlash(BaseUrl)), "Negotiate"); 
 //           credCache.GetCredential(new Uri(""), "Negotiate"); 


            //credCache.GetCredential(new Uri(BaseUrl), "NTLM");

            SourceCode.Workflow.Common.Authentication.ADCredentials ad = new SourceCode.Workflow.Common.Authentication.ADCredentials();
            CredentialCache k2Cache = ad.GetCredentials(RemoveTrailingSlash(BaseUrl));

            var request = new RestRequest();
            request.Credentials = credCache;
            //request.Credentials = new NetworkCredential("holly", "K2pass!", "DENALLIX");
//            request.Credentials = request.Credentials.GetCredential(new Uri(RemoveTrailingSlash(BaseUrl)), "NTLM");
            request.Method = RestSharp.Method.GET;
            request.RequestFormat = RestSharp.DataFormat.Xml;
            //request.Resource = Resource;



            var response = client.Execute(request);
        }

        public string RemoveTrailingSlash(string url)
        {
            if (url.EndsWith("/"))
            {
                return url.Remove(url.Length - 1, 1);
            }
            else
            {
                return url;
            }
        }

    }


    public class StateStatus
    {
        public int State { get; set; }
        public int Status { get; set; }
        public string StateName { get; set; }
        public string StatusName { get; set; }
        public int Sequence { get; set; }
    }

}
