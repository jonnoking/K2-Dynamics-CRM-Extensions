using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2.Demo.CRM.Functions.ServiceBrokerV2.Data
{
    public static class CRMConfig
    {
        public static List<Property> GetStandardReturnProperties()
        {
            List<Property> CRMConfig = new List<Property>();

            //StandardReturnProperties.AddRange(GetStandardInputProperties());

            Property k2processinstanceid = new Property
            {
                Name = "k2processinstanceid",
                MetaData = new MetaData("K2 Process Instance Id", "K2 Process Instance Id"),
                SoType = SourceCode.SmartObjects.Services.ServiceSDK.Types.SoType.Text,
                Type = "string"
            };
            CRMConfig.Add(k2processinstanceid);

            return CRMConfig;

        }
    }
}
