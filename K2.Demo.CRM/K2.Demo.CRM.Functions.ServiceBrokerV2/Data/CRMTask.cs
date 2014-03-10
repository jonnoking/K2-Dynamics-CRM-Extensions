using SourceCode.SmartObjects.Services.ServiceSDK;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace K2.Demo.CRM.Functions.ServiceBrokerV2.Data
{
    public class CRMTask
    {
        private ServiceAssemblyBase serviceBroker = null;

        public CRMTask(ServiceAssemblyBase serviceBroker)
        {
            // Set local serviceBroker variable.
            this.serviceBroker = serviceBroker;
        }

        #region Describe

        public void Create()
        {
            List<Property> CRMTaskProps = GetCRMTaskProperties();

            ServiceObject CRMTaskServiceObject = new ServiceObject();
            CRMTaskServiceObject.Name = "crmtask";
            CRMTaskServiceObject.MetaData.DisplayName = "CRM Task";

            // JJK: not sure I need this
            CRMTaskServiceObject.MetaData.ServiceProperties.Add("objecttype", "task");

            CRMTaskServiceObject.Active = true;

            foreach (Property prop in CRMTaskProps)
            {
                CRMTaskServiceObject.Properties.Add(prop);
            }            

            serviceBroker.Service.ServiceObjects.Add(CRMTaskServiceObject);

        }


        private List<Property> GetCRMTaskProperties()
        {
            List<Property> ContainerProperties = new List<Property>();

            Property category = new Property
            {
                Name = "category",
                MetaData = new MetaData("Category", "Category"),
                SoType = SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType.Text,
                Type = "string"
            };
            ContainerProperties.Add(category);

            Property description = new Property
            {
                Name = "description",
                MetaData = new MetaData("Description", "Description"),
                SoType = SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType.Memo,
                Type = "string"
            };
            ContainerProperties.Add(description);

            Property duedate = new Property
            {
                Name = "duedate",
                MetaData = new MetaData("Due Date", "Due Date"),
                SoType = SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType.DateTime,
                Type = "datetime"
            };
            ContainerProperties.Add(duedate);

            Property duration = new Property
            {
                Name = "duration",
                MetaData = new MetaData("Duration", "Duration"),
                SoType = SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType.Number,
                Type = "int"
            };
            ContainerProperties.Add(duration);

            Property ownerfqn = new Property
            {
                Name = "ownerfqn",
                MetaData = new MetaData("Owner FQN", "Owner FQN"),
                SoType = SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType.Text,
                Type = "string"
            };
            ContainerProperties.Add(ownerfqn);

            Property owner = new Property
            {
                Name = "owner",
                MetaData = new MetaData("Owner", "Owner"),
                SoType = SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType.Text,
                Type = "string"
            };
            ContainerProperties.Add(owner);

            Property ownerid = new Property
            {
                Name = "ownerid",
                MetaData = new MetaData("Owner Id", "Owner Id"),
                SoType = SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType.Text,
                Type = "string"
            };
            ContainerProperties.Add(ownerid);

            Property priority = new Property
            {
                Name = "priority",
                MetaData = new MetaData("Priority", "Priority"),
                SoType = SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType.Number,
                Type = "int"
            };
            ContainerProperties.Add(priority);

            Property regarding = new Property
            {
                Name = "regarding",
                MetaData = new MetaData("Regarding", "Regarding"),
                SoType = SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType.Text,
                Type = "string"
            };
            ContainerProperties.Add(regarding);

            Property regardingid = new Property
            {
                Name = "regardingid",
                MetaData = new MetaData("Regarding Id", "Regarding Id"),
                SoType = SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType.Text,
                Type = "string"
            };
            ContainerProperties.Add(regardingid);


            Property state = new Property
            {
                Name = "state",
                MetaData = new MetaData("State", "State"),
                SoType = SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType.Number,
                Type = "int"
            };
            ContainerProperties.Add(state);

            Property status = new Property
            {
                Name = "status",
                MetaData = new MetaData("Status", "Status"),
                SoType = SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType.Number,
                Type = "int"
            };
            ContainerProperties.Add(status);

            Property subcategory = new Property
            {
                Name = "subcategory",
                MetaData = new MetaData("Subcategory", "Subcategory"),
                SoType = SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType.Text,
                Type = "string"
            };
            ContainerProperties.Add(subcategory);

            Property subject = new Property
            {
                Name = "subject",
                MetaData = new MetaData("Subject", "Subject"),
                SoType = SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType.Memo,
                Type = "string"
            };
            ContainerProperties.Add(subject);

            Property taskid = new Property
            {
                Name = "taskid",
                MetaData = new MetaData("Task Id", "Task Id"),
                SoType = SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType.Text,
                Type = "string"
            };
            ContainerProperties.Add(taskid);

            Property k2serialnumber = new Property
            {
                Name = "k2serialnumber",
                MetaData = new MetaData("K2 Serial Number", "K2 Serial Number"),
                SoType = SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType.Text,
                Type = "string"
            };
            ContainerProperties.Add(k2serialnumber);

            Property k2processname = new Property
            {
                Name = "k2processname",
                MetaData = new MetaData("K2 Process Name", "K2 Process Name"),
                SoType = SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType.Text,
                Type = "string"
            };
            ContainerProperties.Add(k2processname);

            Property k2activityname = new Property
            {
                Name = "k2activityname",
                MetaData = new MetaData("K2 Activity Name", "K2 Activity Name"),
                SoType = SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType.Text,
                Type = "string"
            };
            ContainerProperties.Add(k2activityname);

            Property k2processinstanceid = new Property
            {
                Name = "k2processinstanceid",
                MetaData = new MetaData("K2 Process Instance Id", "K2 Process Instance Id"),
                SoType = SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType.Text,
                Type = "string"
            };
            ContainerProperties.Add(k2processinstanceid);

            ContainerProperties.AddRange(CRMFunctionsStandard.GetStandardReturnProperties());

            return ContainerProperties;
        }

        private Method CreateCreateTask(List<Property> CRMTaskProps)
        {
            Method CreateTask = new Method();
            CreateTask.Name = "CreateTask";
            CreateTask.MetaData.DisplayName = "Create Task";
            CreateTask.Type = MethodType.Create;

            CreateTask.InputProperties.Add(CRMTaskProps.Where(p => p.Name == "category").First());
            CreateTask.Validation.RequiredProperties.Add(CRMTaskProps.Where(p => p.Name == "category").First());
            
            CreateTask.InputProperties.Add(CRMTaskProps.Where(p => p.Name == "description").First());
            CreateTask.InputProperties.Add(CRMTaskProps.Where(p => p.Name == "duedate").First());
            CreateTask.Validation.RequiredProperties.Add(CRMTaskProps.Where(p => p.Name == "duedate").First());
            CreateTask.InputProperties.Add(CRMTaskProps.Where(p => p.Name == "duration").First());
            CreateTask.Validation.RequiredProperties.Add(CRMTaskProps.Where(p => p.Name == "duration").First());
            CreateTask.InputProperties.Add(CRMTaskProps.Where(p => p.Name == "ownerfqn").First());
            CreateTask.InputProperties.Add(CRMTaskProps.Where(p => p.Name == "owner").First());
            CreateTask.InputProperties.Add(CRMTaskProps.Where(p => p.Name == "ownerid").First());
            CreateTask.InputProperties.Add(CRMTaskProps.Where(p => p.Name == "priority").First());
            CreateTask.Validation.RequiredProperties.Add(CRMTaskProps.Where(p => p.Name == "priority").First());
            CreateTask.InputProperties.Add(CRMTaskProps.Where(p => p.Name == "regarding").First());
            CreateTask.InputProperties.Add(CRMTaskProps.Where(p => p.Name == "regardingid").First());
            CreateTask.InputProperties.Add(CRMTaskProps.Where(p => p.Name == "state").First());
            CreateTask.Validation.RequiredProperties.Add(CRMTaskProps.Where(p => p.Name == "state").First());

            CreateTask.InputProperties.Add(CRMTaskProps.Where(p => p.Name == "status").First());
            CreateTask.Validation.RequiredProperties.Add(CRMTaskProps.Where(p => p.Name == "status").First());

            CreateTask.InputProperties.Add(CRMTaskProps.Where(p => p.Name == "subcategory").First());
            CreateTask.Validation.RequiredProperties.Add(CRMTaskProps.Where(p => p.Name == "subcategory").First());

            CreateTask.InputProperties.Add(CRMTaskProps.Where(p => p.Name == "subject").First());
            CreateTask.Validation.RequiredProperties.Add(CRMTaskProps.Where(p => p.Name == "subject").First());

            CreateTask.InputProperties.Add(CRMTaskProps.Where(p => p.Name == "k2serialnumber").First());
            CreateTask.InputProperties.Add(CRMTaskProps.Where(p => p.Name == "k2processname").First());
            CreateTask.InputProperties.Add(CRMTaskProps.Where(p => p.Name == "k2activityname").First());
            CreateTask.InputProperties.Add(CRMTaskProps.Where(p => p.Name == "k2processinstancenid").First());
            
            foreach (Property prop in CRMTaskProps)
            {
                CreateTask.ReturnProperties.Add(prop);
            }
            return CreateTask;
        }

        #endregion Describe


        #region Execute

        public void ExecuteCreateTask(Property[] inputs, RequiredProperties required, Property[] returns, MethodType methodType, ServiceObject serviceObject)
        {
            serviceObject.Properties.InitResultTable();

            Functions.CRMFunctions CRMFunctions = new Functions.CRMFunctions(Utilities.FunctionsUtils.GetCRMConfig(serviceBroker.Service.ServiceConfiguration));

            Functions.CRMTask CRMTaskInput = new Functions.CRMTask();
            Functions.CRMTask CRMTaskResult = null;

            try
            {
                //required
                CRMTaskInput.Category = inputs.Where(p => p.Name.Equals("category", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
                
                CRMTaskInput.Description = inputs.Where(p => p.Name.Equals("description", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
                
                //required
                CRMTaskInput.DueDate = DateTime.Parse(inputs.Where(p => p.Name.Equals("duedate", StringComparison.OrdinalIgnoreCase)).First().Value.ToString());

                //required
                CRMTaskInput.Duration = int.Parse(inputs.Where(p => p.Name.Equals("duration", StringComparison.OrdinalIgnoreCase)).First().Value.ToString());
                
                CRMTaskInput.OwnerFQN = inputs.Where(p => p.Name.Equals("ownerfqn", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
                
                CRMTaskInput.Owner = inputs.Where(p => p.Name.Equals("owner", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
                
                CRMTaskInput.OwnerId = inputs.Where(p => p.Name.Equals("ownerid", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
                
                //required
                CRMTaskInput.Priority = int.Parse(inputs.Where(p => p.Name.Equals("priority", StringComparison.OrdinalIgnoreCase)).First().Value.ToString());
                
                CRMTaskInput.Regarding = inputs.Where(p => p.Name.Equals("regarding", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
                CRMTaskInput.RegardingId = inputs.Where(p => p.Name.Equals("regardingid", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
                
                //required
                CRMTaskInput.State = int.Parse(inputs.Where(p => p.Name.Equals("state", StringComparison.OrdinalIgnoreCase)).First().Value.ToString());
                
                //required
                CRMTaskInput.Status = int.Parse(inputs.Where(p => p.Name.Equals("status", StringComparison.OrdinalIgnoreCase)).First().Value.ToString());
                
                //required
                CRMTaskInput.Subcategory = inputs.Where(p => p.Name.Equals("subcategory", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
                
                //required
                CRMTaskInput.Subject = inputs.Where(p => p.Name.Equals("subject", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
                
                CRMTaskInput.K2SerialNumber = inputs.Where(p => p.Name.Equals("k2serialnumber", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
                CRMTaskInput.K2ProcessName = inputs.Where(p => p.Name.Equals("k2processname", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
                CRMTaskInput.K2ActivityName = inputs.Where(p => p.Name.Equals("k2activityname", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();

                int procInstId;
                if (int.TryParse(inputs.Where(p => p.Name.Equals("k2processinstanceid", StringComparison.OrdinalIgnoreCase)).First().Value.ToString(), out procInstId))
                {
                    CRMTaskInput.K2ProcessInstanceId = procInstId;
                }
                
                CRMTaskResult = CRMFunctions.CRMCreateTask(CRMTaskInput);

                List<string> OProps = new List<string>();
                OProps.Where(p => p == "").First();


                if (CRMTaskResult != null)
                {
                    PropertyInfo[] PropInfoCRMTask = CRMTaskResult.GetType().GetProperties(BindingFlags.Public);

                    //foreach(PropertyInfo prop in CRMTaskResult.GetType().GetProperties(BindingFlags.Public))
                    foreach(Property prop in returns)
                    {
                        if(PropInfoCRMTask.Where(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)).First() != null)
                        {
                            prop.Value = Utilities.ReflectionUtils.GetPropValue(CRMTaskResult, PropInfoCRMTask.Where(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)).First().Name);
                        }

                        PropertyInfo aa = PropInfoCRMTask.Where(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)).First();

                        
                       

                        



                        //if (returns.Where(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)).First() != null)
                        //{
                        //    returns.Where(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)).First().Value = Utilities.ReflectionUtils.GetPropValue(CRMTaskResult, prop.Name);
                        //}

                    }
                    returns.Where(p => p.Name.Equals("responsestatus")).First().Value = ResponseStatus.Success;
                }
                else
                {
                    throw new Exception("CRMTaskResult is null.");
                }

            }
            catch (Exception ex)
            {
                returns.Where(p => p.Name.Equals("responsestatus")).First().Value = ResponseStatus.Error;
                returns.Where(p => p.Name.Equals("responsestatusdescription")).First().Value = ex.Message;
            }

            serviceObject.Properties.BindPropertiesToResultTable();
        }

        #endregion Execute

    }
}
