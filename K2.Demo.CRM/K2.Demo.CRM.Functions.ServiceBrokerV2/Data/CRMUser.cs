using SourceCode.SmartObjects.Services.ServiceSDK;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2.Demo.CRM.Functions.ServiceBrokerV2.Data
{
    public class CRMUser
    {
        private ServiceAssemblyBase serviceBroker = null;

        public CRMUser(ServiceAssemblyBase serviceBroker)
        {
            // Set local serviceBroker variable.
            this.serviceBroker = serviceBroker;
        }

        #region Describe

        public void Create()
        {
            List<Property> CRMUserProps = GetCRMUserProperties();

            ServiceObject CRMUserServiceObject = new ServiceObject();
            CRMUserServiceObject.Name = "crmuser";
            CRMUserServiceObject.MetaData.DisplayName = "CRM User";

            // JJK: not sure I need this
            CRMUserServiceObject.MetaData.ServiceProperties.Add("objecttype", "systemuser");

            CRMUserServiceObject.Active = true;

            foreach (Property prop in CRMUserProps)
            {
                CRMUserServiceObject.Properties.Add(prop);
            }            

            serviceBroker.Service.ServiceObjects.Add(CRMUserServiceObject);

        }


        private List<Property> GetCRMUserProperties()
        {
            List<Property> ContainerProperties = new List<Property>();

            Property userfqn = new Property
            {
                Name = "userfqn",
                MetaData = new MetaData("UserFQN", "UserFQN"),
                SoType = SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType.Text,
                Type = "string"
            };
            ContainerProperties.Add(userfqn);

            Property userid = new Property
            {
                Name = "userid",
                MetaData = new MetaData("User Id", "User Id"),
                SoType = SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType.Text,
                Type = "string"
            };
            ContainerProperties.Add(userid);

            ContainerProperties.AddRange(CRMFunctionsStandard.GetStandardReturnProperties());

            return ContainerProperties;
        }

        private Method CreateGetCRMUser(List<Property> CRMTaskProps)
        {
            Method CreateTask = new Method();
            CreateTask.Name = "GetCRMUser";
            CreateTask.MetaData.DisplayName = "Get CRM User";
            CreateTask.Type = MethodType.Read;

            CreateTask.InputProperties.Add(CRMTaskProps.Where(p => p.Name == "userfqn").First());
            CreateTask.Validation.RequiredProperties.Add(CRMTaskProps.Where(p => p.Name == "userfqn").First());
                        
            foreach (Property prop in CRMTaskProps)
            {
                CreateTask.ReturnProperties.Add(prop);
            }
            return CreateTask;
        }

        #endregion Describe


        #region Execute


        public void ExecuteGetUser(Property[] inputs, RequiredProperties required, Property[] returns, MethodType methodType, ServiceObject serviceObject)
        {
            serviceObject.Properties.InitResultTable();

            Functions.CRMFunctions CRMFunctions = new Functions.CRMFunctions(Utilities.FunctionsUtils.GetCRMConfig(serviceBroker.Service.ServiceConfiguration));

            Functions.CRMUser CRMUserInput = new Functions.CRMUser();
            Functions.CRMUser CRMUserResult = null;
            
            try
            {
                CRMUserInput.UserFQN = inputs.Where(p => p.Name.Equals("userfqn", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();

                CRMUserResult = CRMFunctions.CRMGetUser(CRMUserInput);

                if (CRMUserResult != null)
                {
                    returns.Where(p => p.Name.Equals("userfqn")).First().Value = CRMUserInput.UserFQN;
                    returns.Where(p => p.Name.Equals("userid")).First().Value = CRMUserResult.UserId;
                    returns.Where(p => p.Name.Equals("responsestatus")).First().Value = ResponseStatus.Success;
                }
                else
                {
                    throw new Exception("CRMUserResult is null.");
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
