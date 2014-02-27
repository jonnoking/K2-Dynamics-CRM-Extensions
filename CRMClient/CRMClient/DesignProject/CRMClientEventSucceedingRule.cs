using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using SourceCode.Configuration;
using SourceCode.Workflow.Authoring;
using SourceCode.Workflow.Design;
using SourceCode.Framework;

namespace DesignCRMClient
{
    public class CRMClientEventSucceedingRule : SourceCode.Workflow.Design.Outcome.Base.OutcomeEventSucceedingRule
        //SourceCode.Workflow.Design.Outcome.DefaultOutcomeEventSucceedingRule
        //SourceCode.Workflow.Design.DefaultEventSucceedingRule
        
    {
        public CRMClientEventSucceedingRule() : base()
        {
            base.Extender = new WindowsWorkflowExtender();
        }

        protected override void OnLoad(ISerializationInfo content)
        {
            base.OnLoad(content);

            if (content.HasProperty("CRMServerURL"))
            {
                this.CRMServerURL = (K2Field)content.GetPropertyAsPersistableObject("CRMServerURL");
            }
            if (content.HasProperty("CRMOrganisation"))
            {
                this.CRMOrganisation = (K2Field)content.GetPropertyAsPersistableObject("CRMOrganisation");
            }
            if (content.HasProperty("CRMEntityId"))
            {
                this.CRMEntityId = (K2Field)content.GetPropertyAsPersistableObject("CRMEntityId");
            }
            if (content.HasProperty("CRMEntityType"))
            {
                this.CRMEntityType = (K2Field)content.GetPropertyAsPersistableObject("CRMEntityType");
            }
            if (content.HasProperty("CustomServiceURL"))
            {
                this.CustomServiceURL = (K2Field)content.GetPropertyAsPersistableObject("CustomServiceURL");
            }
            if (content.HasProperty("SmartObjectServer"))
            {
                this.SmartObjectServer = (K2Field)content.GetPropertyAsPersistableObject("SmartObjectServer");
            }
            if (content.HasProperty("CRMFunctionsSmartObject"))
            {
                this.CRMFunctionsSmartObject = (K2Field)content.GetPropertyAsPersistableObject("CRMFunctionsSmartObject");
            }
        }

        protected override void OnSave(ISerializationInfo content)
        {
            base.OnSave(content);

            if (!K2Field.IsNullOrEmpty(CRMServerURL))
            {
                content.SetProperty("CRMServerURL", CRMServerURL);
            }
            if (!K2Field.IsNullOrEmpty(CRMOrganisation))
            {
                content.SetProperty("CRMOrganisation", CRMOrganisation);
            }
            if (!K2Field.IsNullOrEmpty(CRMEntityId))
            {
                content.SetProperty("CRMEntityId", CRMEntityId);
            }
            if (!K2Field.IsNullOrEmpty(CRMEntityType))
            {
                content.SetProperty("CRMEntityType", CRMEntityType);
            }
            if (!K2Field.IsNullOrEmpty(CustomServiceURL))
            {
                content.SetProperty("CustomServiceURL", CustomServiceURL);
            }
            if (!K2Field.IsNullOrEmpty(SmartObjectServer))
            {
                content.SetProperty("SmartObjectServer", SmartObjectServer);
            }
            if (!K2Field.IsNullOrEmpty(CRMFunctionsSmartObject))
            {
                content.SetProperty("CRMFunctionsSmartObject", CRMFunctionsSmartObject);
            }

        }

        public override T Clone<T>()
        {
            DesignCRMClient.CRMClientEventSucceedingRule item = base.Clone<DesignCRMClient.CRMClientEventSucceedingRule>();

            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_SmartObjectServer))
            {
                item.SmartObjectServer = _SmartObjectServer.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_CRMFunctionsSmartObject))
            {
                item.CRMFunctionsSmartObject = _CRMFunctionsSmartObject.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_CRMServerURL))
            {
                item.CRMServerURL = _CRMServerURL.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_CRMOrganisation))
            {
                item.CRMOrganisation = _CRMOrganisation.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_CRMEntityId))
            {
                item.CRMEntityId = _CRMEntityId.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_CRMEntityType))
            {
                item.CRMEntityType = _CRMEntityType.Clone<K2Field>();
            }

            return item as T;
        }


        protected override void PrepareCodeFiles()
        {
            if (base.Extender != null)
            {
                CodeFileResolver resolver = new CodeFileResolver(base.Extender, @"CRMClient.EventSucceedingRule\EventItem");
                foreach (CodeFile file in resolver.GetCodeFilesFromDisk())
                {
                    if (!base.Extender.CodeFiles.Contains(file.FileName))
                    {
                        base.Extender.CodeFiles.Add(file);
                    }
                }
                resolver = null;
            }
            base.PrepareCodeFiles();
        }

        protected override void PrepareConfigurationForBuild()
        {
            base.PrepareConfigurationForBuild();

            base.Extender.Configuration["CRMEntityId"] = this.CreateConfigSettingField<string>(this.CRMEntityId);
            base.Extender.Configuration["CRMEntityType"] = this.CreateConfigSettingField<string>(this.CRMEntityType);
            base.Extender.Configuration["CRMOrganisation"] = this.CreateConfigSettingField<string>(this.CRMOrganisation);
            base.Extender.Configuration["CRMServerURL"] = this.CreateConfigSettingField<string>(this.CRMServerURL);
            //base.Extender.Configuration["CustomServiceURL"] = this.CustomServiceURL;
            base.Extender.Configuration["SmartObjectServer"] = this.CreateConfigSettingField<string>(this.SmartObjectServer);
            base.Extender.Configuration["CRMFunctionsSmartObject"] = this.CreateConfigSettingField<string>(this.CRMFunctionsSmartObject);
        }

        // Properties
        protected override string[] References
        {
            get
            {
                List<string> list = new List<string>();
                list.Add(Path.Combine(ConfigurationManager.ConfigurationFolder, "SourceCode.HostClientAPI.dll"));
                list.Add(Path.Combine(ConfigurationManager.ConfigurationFolder, "SourceCode.SmartObjects.Client.dll"));
                list.Add(Path.Combine(ConfigurationManager.ConfigurationFolder, "SourceCode.Workflow.Authoring.dll"));
                return list.ToArray();
            }
        }

        public SourceCode.Workflow.Authoring.K2Field CreateConfigSettingField<T>(SourceCode.Workflow.Authoring.K2Field k2field)
        {
            SourceCode.Workflow.Authoring.K2Field newField = new SourceCode.Workflow.Authoring.K2Field();
            if ((k2field != null))
            {
                newField.Parts.AddRange(k2field.Parts.ToArray());
            }
            if ((newField.Parts.Count == 0))
            {
                SourceCode.Workflow.Design.ValueTypePart val = new SourceCode.Workflow.Design.ValueTypePart();
                val.Value = default(T);
                newField.Parts.Add(val);
            }
            newField.ValueType = typeof(T);
            for (System.Collections.IEnumerator it1 = newField.Parts.GetEnumerator(); it1.MoveNext(); )
            {
                SourceCode.Workflow.Authoring.K2FieldPart part = ((SourceCode.Workflow.Authoring.K2FieldPart)(it1.Current));
                part.ValueType = typeof(T);
            }
            return newField;
        }


        private K2Field _CRMServerURL = null;
        public K2Field CRMServerURL
        {
            get
            {
                if (_CRMServerURL == null)
                    this.CRMServerURL = new K2Field(this);
                return _CRMServerURL;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("CRMServerURL", ref _CRMServerURL, value);
            }
        }
        private K2Field _CRMOrganisation = null;
        public K2Field CRMOrganisation
        {
            get
            {
                if (_CRMOrganisation == null)
                    this.CRMOrganisation = new K2Field(this);
                return _CRMOrganisation;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("CRMOrganisation", ref _CRMOrganisation, value);
            }
        }
        private K2Field _CRMEntityId = null;
        public K2Field CRMEntityId
        {
            get
            {
                if (_CRMEntityId == null)
                    this.CRMEntityId = new K2Field(this);
                return _CRMEntityId;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("CRMEntityId", ref _CRMEntityId, value);
            }
        }
        private K2Field _CRMEntityType = null;
        public K2Field CRMEntityType
        {
            get
            {
                if (_CRMEntityType == null)
                    this.CRMEntityType = new K2Field(this);
                return _CRMEntityType;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("CRMEntityType", ref _CRMEntityType, value);
            }
        }
        private K2Field _CustomServiceURL = null;
        public K2Field CustomServiceURL
        {
            get
            {
                if (_CustomServiceURL == null)
                    this.CustomServiceURL = new K2Field(this);
                return _CustomServiceURL;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("CustomServiceURL", ref _CustomServiceURL, value);
            }
        }
        private K2Field _SmartObjectServer = null;
        public K2Field SmartObjectServer
        {
            get
            {
                if (_SmartObjectServer == null)
                    this.SmartObjectServer = new K2Field(this);
                return _SmartObjectServer;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("SmartObjectServer", ref _SmartObjectServer, value);
            }
        }
        private K2Field _CRMFunctionsSmartObject = null;
        public K2Field CRMFunctionsSmartObject
        {
            get
            {
                if (_CRMFunctionsSmartObject == null)
                    this.CRMFunctionsSmartObject = new K2Field(this);
                return _CRMFunctionsSmartObject;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("CRMFunctionsSmartObject", ref _CRMFunctionsSmartObject, value);
            }
        }
    }
}
