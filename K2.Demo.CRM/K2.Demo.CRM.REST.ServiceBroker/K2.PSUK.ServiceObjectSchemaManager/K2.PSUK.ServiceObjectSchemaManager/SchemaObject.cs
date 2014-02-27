using System;
using System.Collections.Generic;
using System.Text;

using SourceCode.SmartObjects.Services.ServiceSDK.Types;

namespace K2.PSUK.ServiceObjectSchema
{
    [Serializable]
    public class SchemaObject
    {
        private string serviceInstanceName;
        public string ServiceInstanceName
        {
            get { return serviceInstanceName; }
            set { serviceInstanceName = value; }
        }

        private string serviceInstanceDisplayName;
        public string ServiceInstanceDisplayName
        {
            get { return serviceInstanceDisplayName; }
            set { serviceInstanceDisplayName = value; }
        }

        private string serviceInstanceDescription;
        public string ServiceInstanceDescription
        {
            get { return serviceInstanceDescription; }
            set { serviceInstanceDescription = value; }
        }

        private List<SchemaProperty> schemaProperties = new List<SchemaProperty>();
        public List<SchemaProperty> SchemaProperties
        {
            get { return schemaProperties; }
            set { schemaProperties = value; }
        }

        public void AddProperty(string name,string displayName,string description, string trueType,SoType k2Type)
        {
            SchemaProperty prop = new SchemaProperty();
            prop.Name=name;
            prop.DisplayName = displayName;
            prop.Description = description;
            prop.TrueType = trueType;
            prop.K2Type = k2Type;
            SchemaProperties.Add(prop);
        }

        private List<SchemaMethod> schemaMethods = new List<SchemaMethod>();
        public List<SchemaMethod> SchemaMethods
        {
            get { return schemaMethods; }
            set { schemaMethods = value; }
        }

        public void AddMethod(string name, string displayName, string description, MethodType k2Type, List<string> inputProps, List<string> requiredProps, List<string> returnProps)
        {
            SchemaMethod meth = new SchemaMethod();
            meth.Name = name;
            meth.DisplayName = displayName;
            meth.K2Type = k2Type;
            meth.Description = description;
            meth.InputProperties = inputProps;
            meth.RequiredProperties = requiredProps;
            meth.ReturnProperties = returnProps;
            SchemaMethods.Add(meth);
        }

        public List<SchemaMethodProperty> GetMethodProperties(SchemaObject.SchemaMethod schemaMethod)
        {
            List<SchemaMethodProperty> properties = new List<SchemaMethodProperty>();
            foreach (SchemaProperty p in schemaProperties)
            {
                SchemaMethodProperty methodProps = new SchemaMethodProperty();
                methodProps.Property = p.Name;
                
                //Loop through inputs for method
                foreach (string property in schemaMethod.InputProperties)
                    if (property == p.Name)
                        methodProps.Input = true;

                //loop thorugh requireds for method
                foreach (string property in schemaMethod.RequiredProperties)
                    if (property == p.Name)
                        methodProps.Required = true;

                //loop through returns for method
                foreach (string property in schemaMethod.ReturnProperties)
                    if (property == p.Name)
                        methodProps.Return = true;

                properties.Add(methodProps);
            }
            return properties;
        }

        public SchemaMethod UpdateMethod(SchemaMethod schemaMethod, List<SchemaMethodProperty> properties)
        {
            schemaMethod.InputProperties = new List<string>();
            schemaMethod.ReturnProperties = new List<string>();
            schemaMethod.RequiredProperties = new List<string>();

            foreach(SchemaMethodProperty p in properties)
            {
                if (p.Input)
                    schemaMethod.InputProperties.Add(p.Property);

                if (p.Required)
                    schemaMethod.RequiredProperties.Add(p.Property);

                if (p.Return)
                    schemaMethod.ReturnProperties.Add(p.Property);
            }
            return schemaMethod;
        }

        public class SchemaProperty
        {
            private string name;
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            private string displayName;
            public string DisplayName
            {
                get { return displayName; }
                set { displayName = value; }
            }

            private string description;
            public string Description
            {
                get { return description; }
                set { description = value; }
            }


            private string trueType;
            public string TrueType
            {
                get { return trueType; }
                set { trueType = value; }
            }

            private SoType k2Type;
            public SoType K2Type
            {
                get { return k2Type; }
                set { k2Type = value; }
            }
        }

        public class SchemaMethod
        {

            public SchemaMethod()
            {
                inputProperties = new List<string>();
                returnProperties = new List<string>();
                requiredProperties = new List<string>();
            }

            private string name;
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            private string displayName;
            public string DisplayName
            {
                get { return displayName; }
                set { displayName = value; }
            }

            private string description;
            public string Description
            {
                get { return description; }
                set { description = value; }
            }

            private MethodType k2Type;
            public MethodType K2Type
            {
                get { return k2Type; }
                set { k2Type = value; }
            }

            private List<string> inputProperties;
            public List<string> InputProperties
            {
                get { return inputProperties; }
                set { inputProperties = value; }
            }

            private List<string> requiredProperties;
            public List<string> RequiredProperties
            {
                get { return requiredProperties; }
                set { requiredProperties = value; }
            }

            private List<string> returnProperties;
            public List<string> ReturnProperties
            {
                get { return returnProperties; }
                set { returnProperties = value; }
            }
        }
    }

    public class SchemaMethodProperty
    {
        string _Property;
        public string Property
        {
            get { return _Property; }
            set { _Property = value; }
        }

        bool _Input;
        public bool Input
        {
            get { return _Input; }
            set { _Input = value; }
        }
        bool _Return;
        public bool Return
        {
            get { return _Return; }
            set { _Return = value; }
        }

        bool _Required;
        public bool Required
        {
            get { return _Required; }
            set { _Required = value; }
        }
    }

}
