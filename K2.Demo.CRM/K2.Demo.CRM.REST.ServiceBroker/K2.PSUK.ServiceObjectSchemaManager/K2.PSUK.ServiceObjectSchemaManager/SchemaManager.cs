using System;
using System.Collections.Generic;
using System.Text;
using SourceCode.SmartObjects.Services.ServiceSDK;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;

namespace K2.PSUK.ServiceObjectSchema
{
    public static class SchemaManager
    {

        public static void SaveSchemaXMLFile(SchemaObject schemaObject, string filePath)
        {
            if (filePath.Length == 0)
            {
                filePath = @"ServiceObjectSchema\SchemaObject.xml";
            }
            Serialization.SerializeObject(schemaObject, filePath);
        }

        public static SchemaObject LoadSchemaXMLFile(string filePath)
        {
            if (filePath.Length == 0)
            {
                filePath = @"ServiceObjectSchema\SchemaObject.xml";
            }
            SchemaObject schemaObject = (SchemaObject)Serialization.DeserializeObject(filePath, typeof(SchemaObject));
            return schemaObject;
        }

        public static Property CreateProperty(string name, string displayName, string description, string type, SoType soType)
        {
            Property proprty = new Property();
            proprty.Name = name;
            proprty.MetaData.DisplayName = displayName;
            proprty.MetaData.Description = description;
            proprty.Type = type;
            proprty.SoType = soType;

            return proprty;
        }

        public static Method CreateMethod(string name,string displayName, string description, MethodType type, List<string> inputProps, List<string> requiredProps, List<string> returnProps)
        {
            Method method = new Method();
            method.Name = name;
            method.MetaData.DisplayName = displayName;
            method.Type = type;
            method.MetaData.Description = description;
            foreach (string iProp in inputProps)
            {
                method.InputProperties.Add(iProp);
            }
            foreach (string rProp in requiredProps)
            {
                method.Validation.RequiredProperties.Add(rProp);
            }
            foreach (string reProp in returnProps)
            {
                method.ReturnProperties.Add(reProp);
            }
            return method;
        }

        private static SoType GetSOType(string type)
        {
            SoType soType = SoType.Text;
            switch (type.ToLower())
            {
                case "autoguid" :
                    soType = SoType.AutoGuid;
                    break;
                case "autonumber":
                    soType = SoType.Autonumber;
                    break;
                case "datetime":
                    soType = SoType.DateTime;
                    break;
                case "decimal":
                    soType = SoType.Decimal;
                    break;
                case "default":
                    soType = SoType.Default;
                    break;
                case "file":
                    soType = SoType.File;
                    break;
                case "guid":
                    soType = SoType.Guid;
                    break;
                case "hyperlink":
                    soType = SoType.HyperLink;
                    break;
                case "image":
                    soType = SoType.Image;
                    break;
                case "memo":
                    soType = SoType.Memo;
                    break;
                case "multivalue":
                    soType = SoType.MultiValue;
                    break;
                case "number":
                    soType = SoType.Number;
                    break;
                case "text":
                    soType = SoType.Text;
                    break;
                case "xml":
                    soType = SoType.Xml;
                    break;
                case "yesno":
                    soType = SoType.YesNo;
                    break;
            }
            return soType;
        }

        private static MethodType GetMethodType(string methodType)
        {
            MethodType methType = MethodType.Execute;
            switch (methodType.ToLower())
            {
                case "create":
                    methType = MethodType.Create;
                    break;
                case "delete":
                    methType = MethodType.Delete;
                    break;
                case "execute":
                    methType = MethodType.Execute;
                    break;
                case "list":
                    methType = MethodType.List;
                    break;
                case "read":
                    methType = MethodType.Read;
                    break;
                case "update":
                    methType = MethodType.Update;
                    break;
            }
            return methType;
        }

    }
}
