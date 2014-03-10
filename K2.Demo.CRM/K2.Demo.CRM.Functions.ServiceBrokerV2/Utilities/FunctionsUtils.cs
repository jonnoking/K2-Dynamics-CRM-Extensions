using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2.Demo.CRM.Functions.ServiceBrokerV2.Utilities
{
    public static class FunctionsUtils
    {
        public static Functions.CRMConfig GetCRMConfig(ServiceConfiguration serviceConfig)
        {
            Functions.CRMConfig crmconfig = new Functions.CRMConfig
            {
                CRMURL = serviceConfig["CRMUrl"].ToString(),
                CRMOrganization = serviceConfig["CRMOrganization"].ToString()
            };

            if (!serviceConfig.ServiceAuthentication.Impersonate && !string.IsNullOrEmpty(serviceConfig.ServiceAuthentication.UserName.Trim()) && !string.IsNullOrEmpty(serviceConfig.ServiceAuthentication.Password.Trim()))
            {
                // Static credentials
                if (serviceConfig.ServiceAuthentication.UserName.Contains("\\"))
                {
                    char[] sp = { '\\' };
                    string[] user = serviceConfig.ServiceAuthentication.UserName.Split(sp);
                    crmconfig.CRMDomain = user[0].Trim();
                    crmconfig.CRMUsername = user[1].Trim();
                }
                else
                {
                    crmconfig.CRMUsername = serviceConfig.ServiceAuthentication.UserName.Trim();
                }
                crmconfig.CRMPassword = serviceConfig.ServiceAuthentication.Password;
            }

            return crmconfig;
        }

        public static Functions.CRMFunctions GetCRMFunctions (ServiceConfiguration serviceConfig)
        {
            return new Functions.CRMFunctions(GetCRMConfig(serviceConfig));
        }


    }
}
