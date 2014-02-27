using System;
using System.Collections.Generic;
using System.Text;

using SourceCode.SmartObjects.Services.ServiceSDK;

namespace K2.Demo.CRM.REST.ServiceBroker
{
    public class DynamicServiceObject : ServiceAssemblyBase
    {
        public DynamicServiceObjectHelper DSOHelper;

        public DynamicServiceObject()
        {
            DSOHelper = new DynamicServiceObjectHelper(this);
        }

        public override string GetConfigSection()
        {
            //This section is used to specify any parameters that may appear in the
            //config section e.g
            //Service.ServiceConfiguration.Add("ExcelServices URL", true, "");
            Service.ServiceConfiguration.Add("PropertiesConfigurationFile", true, "Not Implemented");
            Service.ServiceConfiguration.Add("RESTServiceURL", false, "");
            Service.ServiceConfiguration.Add("CRMURL", false, "");
            Service.ServiceConfiguration.Add("CRMOrganization", false, "");
            Service.ServiceConfiguration.ServiceAuthentication.Impersonate = true;


            return base.GetConfigSection();
        }

        public override string DescribeSchema()
        {
            //Now call the descrobe schema method. This simply put creates an xml file that represents the following:
            DSOHelper.DescribeSchema();
            return base.DescribeSchema();
        }

        public override void Execute()
        {

            //Here we execute the method that was called.
            //the result will either be a single set of data or a collection of data in the case of 
            //a list. The method called is reponsible for setting the return data.
            DSOHelper.Execute();
        }

        public override void Extend()
        {
            throw new Exception("The method or operation is not implemented.");
        }

    }
}
