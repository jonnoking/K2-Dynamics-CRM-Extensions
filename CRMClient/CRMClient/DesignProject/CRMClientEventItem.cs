//////////////////////////////////////////////////////////////////////////
//																		//
//		This class is your main "interface" from the wizard.			//
//		All the "intelligence" of you wizard occurs here in terms		//
//		of what values/properties are saved when your project is 		//
//		saved and what values are sent through to the Extender			//
//																		//
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;

using SourceCode.Workflow.Authoring;
using SourceCode.Workflow.Design;
using SourceCode.Framework;
using System.IO;
using SourceCode.Configuration;

namespace DesignCRMClient
{
    public class CRMClientEventItem : SourceCode.Workflow.Design.ClientEventItem // SourceCode.Workflow.Authoring.EventItem
       //use this to get viewflow to show participants - throwing case error with eventsucceeding rule
        //SourceCode.Workflow.Design.ClientEventItem 
    {
        public CRMClientEventItem()
            : base()
        {
            base.Extender = new CodeExtender();
        }

        #region Custom Public Properties
        //This is an example of how to set a property using the inherited K2 objects

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

        //private K2Field _CustomServiceURL = null;
        //public K2Field CustomServiceURL
        //{
        //    get
        //    {
        //        if (_CustomServiceURL == null)
        //            this.CustomServiceURL = new K2Field(this);
        //        return _CustomServiceURL;
        //    }
        //    set
        //    {
        //        base.OnNotifyPropertyChanged<K2Field>("CustomServiceURL", ref _CustomServiceURL, value);
        //    }
        //}
        private K2Field _ProcessName = null;
        public K2Field ProcessName
        {
            get
            {
                if (_ProcessName == null)
                    this.ProcessName = new K2Field(this);
                return _ProcessName;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("ProcessName", ref _ProcessName, value);
            }
        }
        private K2Field _ActivityName = null;
        public K2Field ActivityName
        {
            get
            {
                if (_ActivityName == null)
                    this.ActivityName = new K2Field(this);
                return _ActivityName;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("ActivityName", ref _ActivityName, value);
            }
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
        private K2Field _CRMEntityForm = null;
        public K2Field CRMEntityForm
        {
            get
            {
                if (_CRMEntityForm == null)
                    this.CRMEntityForm = new K2Field(this);
                return _CRMEntityForm;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("CRMEntityForm", ref _CRMEntityForm, value);
            }
        }


        private K2Field _CRMFormURL = null;
        public K2Field CRMFormURL
        {
            get
            {
                if (_CRMFormURL == null)
                    this.CRMFormURL = new K2Field(this);
                return _CRMFormURL;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("CRMFormURL", ref _CRMFormURL, value);
            }
        }
        private K2Field _CRMCustomSNParameter = null;
        public K2Field CRMCustomSNParameter
        {
            get
            {
                if (_CRMCustomSNParameter == null)
                    this.CRMCustomSNParameter = new K2Field(this);
                return _CRMCustomSNParameter;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("CRMCustomSNParameter", ref _CRMCustomSNParameter, value);
            }
        }

        private string _InternetPlatform = string.Empty;
        public string InternetPlatform
        {
            get
            {
                return _InternetPlatform;
            }
            set
            {
                base.OnNotifyPropertyChanged<string>("InternetPlatform", ref _InternetPlatform, value);
            }
        }

        private bool _InsertSN = false;
        public bool InsertSN
        {
            get
            {
                return _InsertSN;
            }
            set
            {
                base.OnNotifyPropertyChanged<bool>("InsertSN", ref _InsertSN, value);
            }
        }

        private bool _CreateTasks = false;
        public bool CreateTasks
        {
            get
            {
                return _CreateTasks;
            }
            set
            {
                base.OnNotifyPropertyChanged<bool>("CreateTasks", ref _CreateTasks, value);
            }
        }

        private K2Field _TaskCategory = null;
        public K2Field TaskCategory
        {
            get
            {
                if (_TaskCategory == null)
                    this.TaskCategory = new K2Field(this);
                return _TaskCategory;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("TaskCategory", ref _TaskCategory, value);
            }
        }

        private K2Field _TaskDescription = null;
        public K2Field TaskDescription
        {
            get
            {
                if (_TaskDescription == null)
                    this.TaskDescription = new K2Field(this);
                return _TaskDescription;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("TaskDescription", ref _TaskDescription, value);
            }
        }

        private K2Field _TaskDueDate = null;
        public K2Field TaskDueDate
        {
            get
            {
                if (_TaskDueDate == null)
                    this.TaskDueDate = new K2Field(this);
                return _TaskDueDate;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("TaskDueDate", ref _TaskDueDate, value);
            }
        }

        private K2Field _TaskDuration = null;
        public K2Field TaskDuration
        {
            get
            {
                if (_TaskDuration == null)
                    this.TaskDuration = new K2Field(this);
                return _TaskDuration;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("TaskDuration", ref _TaskDuration, value);
            }
        }

        private K2Field _TaskOwnerFQN = null;
        public K2Field TaskOwnerFQN
        {
            get
            {
                if (_TaskOwnerFQN == null)
                    this.TaskOwnerFQN = new K2Field(this);
                return _TaskOwnerFQN;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("TaskOwnerFQN", ref _TaskOwnerFQN, value);
            }
        }

        private K2Field _TaskOwner = null;
        public K2Field TaskOwner
        {
            get
            {
                if (_TaskOwner == null)
                    this.TaskOwner = new K2Field(this);
                return _TaskOwner;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("TaskOwner", ref _TaskOwner, value);
            }
        }

        private K2Field _TaskOwnerId = null;
        public K2Field TaskOwnerId
        {
            get
            {
                if (_TaskOwnerId == null)
                    this.TaskOwnerId = new K2Field(this);
                return _TaskOwnerId;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("TaskOwnerId", ref _TaskOwnerId, value);
            }
        }

        private K2Field _TaskPriority = null;
        public K2Field TaskPriority
        {
            get
            {
                if (_TaskPriority == null)
                    this.TaskPriority = new K2Field(this);
                return _TaskPriority;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("TaskPriority", ref _TaskPriority, value);
            }
        }

        private K2Field _TaskRegarding = null;
        public K2Field TaskRegarding
        {
            get
            {
                if (_TaskRegarding == null)
                    this.TaskRegarding = new K2Field(this);
                return _TaskRegarding;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("TaskRegarding", ref _TaskRegarding, value);
            }
        }

        private K2Field _TaskRegardingId = null;
        public K2Field TaskRegardingId
        {
            get
            {
                if (_TaskRegardingId == null)
                    this.TaskRegardingId = new K2Field(this);
                return _TaskRegardingId;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("TaskRegardingId", ref _TaskRegardingId, value);
            }
        }

        private K2Field _TaskState = null;
        public K2Field TaskState
        {
            get
            {
                if (_TaskState == null)
                    this.TaskState = new K2Field(this);
                return _TaskState;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("TaskState", ref _TaskState, value);
            }
        }

        private K2Field _TaskStatus = null;
        public K2Field TaskStatus
        {
            get
            {
                if (_TaskStatus == null)
                    this.TaskStatus = new K2Field(this);
                return _TaskStatus;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("TaskState", ref _TaskStatus, value);
            }
        }

        private K2Field _TaskSubcategory = null;
        public K2Field TaskSubcategory
        {
            get
            {
                if (_TaskSubcategory == null)
                    this.TaskSubcategory = new K2Field(this);
                return _TaskSubcategory;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("TaskSubcategory", ref _TaskSubcategory, value);
            }
        }

        private K2Field _TaskSubject = null;
        public K2Field TaskSubject
        {
            get
            {
                if (_TaskSubject == null)
                    this.TaskSubject = new K2Field(this);
                return _TaskSubject;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("TaskSubject", ref _TaskSubject, value);
            }
        }

        private K2Field _SN = null;
        public K2Field SN
        {
            get
            {
                if (_SN == null)
                    this.SN = new K2Field(this);
                return _SN;
            }
            set
            {
                base.OnNotifyPropertyChanged<K2Field>("SN", ref _SN, value);
            }
        }

        //private string _SN = null;
        //public string SN
        //{
        //    get
        //    {
        //        return _SN;
        //    }
        //    set
        //    {
        //        base.OnNotifyPropertyChanged<string>("SN", ref _SN, value);
        //    }
        //}


        #endregion


        #region Override Methods
        protected override void Dispose(bool disposing)
        {
            if (base.IsDisposed)
            {
                return;
            }
            _ActivityName = null;
            _ProcessName = null;
            _SmartObjectServer = null;
            _CRMFunctionsSmartObject = null;
            _ProcessName = null;
            _ActivityName = null;
            _CRMEntityId = null;
            _CRMEntityType = null;
            _CRMEntityForm = null;
            _CRMFormURL = null;
            _CRMOrganisation = null;
            _CRMServerURL = null;
            _CRMCustomSNParameter = null;   
            _InternetPlatform = null;
            _SN = null;
            _TaskCategory = null;
            _TaskDescription = null;
            _TaskDueDate = null;
            _TaskDuration = null;
            _TaskOwner = null;
            _TaskOwnerFQN = null;
            _TaskOwnerId = null;
            _TaskPriority = null;
            _TaskRegarding = null;
            _TaskRegardingId = null;
            _TaskState = null;
            _TaskStatus = null;
            _TaskSubcategory = null;
            _TaskSubject = null;
            //ALWAYS call the base.Dispose(disposing); method last
            base.Dispose(disposing);
        }
        //When your wizard page is loaded, this override determines what values exist already and loads those
        //values into the wizard for display. I.e. if you have a value of "123" that was previously entered into a textbox
        //on your wizard, this override will load it into memory and on you Wizard page's "OnActivate", this value
        //can be retrieved and displayed in your text box.
        protected override void OnLoad(ISerializationInfo content)
        {
            base.OnLoad(content);

            if (content.HasProperty("InternetPlatform"))
            {
                _InternetPlatform = content.GetPropertyAsString("InternetPlatform");
            }
            if (content.HasProperty("InsertSN"))
            {
                _InsertSN = content.GetPropertyAsBoolean("InsertSN");
            }
            if (content.HasProperty("CreateTasks"))
            {
                _CreateTasks = content.GetPropertyAsBoolean("CreateTasks");
            }

            if (content.HasProperty("SN"))
            {
                this.SN = (K2Field)content.GetPropertyAsPersistableObject("SN");
            }

            if (content.HasProperty("ActivityName"))
            {
                this.ActivityName = (K2Field)content.GetPropertyAsPersistableObject("ActivityName");
            }
            if (content.HasProperty("ProcessName"))
            {
                this.ProcessName = (K2Field)content.GetPropertyAsPersistableObject("ProcessName");
            }
            if (content.HasProperty("SmartObjectServer"))
            {
                this.SmartObjectServer = (K2Field)content.GetPropertyAsPersistableObject("SmartObjectServer");
            }
            if (content.HasProperty("CRMFunctionsSmartObject"))
            {
                this.CRMFunctionsSmartObject = (K2Field)content.GetPropertyAsPersistableObject("CRMFunctionsSmartObject");
            }
            //if (content.HasProperty("CustomServiceURL"))
            //{
            //    this.CustomServiceURL = (K2Field)content.GetPropertyAsPersistableObject("CustomServiceURL");
            //}
            if (content.HasProperty("CRMServerURL"))
            {
                this.CRMServerURL = (K2Field)content.GetPropertyAsPersistableObject("CRMServerURL");
            }
            if (content.HasProperty("CRMOrganisation"))
            {
                this.CRMOrganisation = (K2Field)content.GetPropertyAsPersistableObject("CRMOrganisation");
            }
            if (content.HasProperty("CRMFormURL"))
            {
                this.CRMFormURL = (K2Field)content.GetPropertyAsPersistableObject("CRMFormURL");
            }
            if (content.HasProperty("CRMEntityId"))
            {
                this.CRMEntityId = (K2Field)content.GetPropertyAsPersistableObject("CRMEntityId");
            }
            if (content.HasProperty("CRMEntityType"))
            {
                this.CRMEntityType = (K2Field)content.GetPropertyAsPersistableObject("CRMEntityType");
            }
            if (content.HasProperty("CRMEntityForm"))
            {
                this.CRMEntityForm = (K2Field)content.GetPropertyAsPersistableObject("CRMEntityForm");
            }
            if (content.HasProperty("CRMCustomSNParameter"))
            {
                this.CRMCustomSNParameter = (K2Field)content.GetPropertyAsPersistableObject("CRMCustomSNParameter");
            }
            if (content.HasProperty("TaskCategory"))
            {
                this.TaskCategory = (K2Field)content.GetPropertyAsPersistableObject("TaskCategory");
            }
            if (content.HasProperty("TaskDescription"))
            {
                this.TaskDescription = (K2Field)content.GetPropertyAsPersistableObject("TaskDescription");
            }
            if (content.HasProperty("TaskDueDate"))
            {
                this.TaskDueDate = (K2Field)content.GetPropertyAsPersistableObject("TaskDueDate");
            }
            if (content.HasProperty("TaskDuration"))
            {
                this.TaskDuration = (K2Field)content.GetPropertyAsPersistableObject("TaskDuration");
            }
            if (content.HasProperty("TaskOwnerFQN"))
            {
                this.TaskOwnerFQN = (K2Field)content.GetPropertyAsPersistableObject("TaskOwnerFQN");
            }
            if (content.HasProperty("TaskOwner"))
            {
                this.TaskOwner = (K2Field)content.GetPropertyAsPersistableObject("TaskOwner");
            }
            if (content.HasProperty("TaskOwnerId"))
            {
                this.TaskOwnerId = (K2Field)content.GetPropertyAsPersistableObject("TaskOwnerId");
            }
            if (content.HasProperty("TaskPriority"))
            {
                this.TaskPriority = (K2Field)content.GetPropertyAsPersistableObject("TaskPriority");
            }
            if (content.HasProperty("TaskRegarding"))
            {
                this.TaskRegarding = (K2Field)content.GetPropertyAsPersistableObject("TaskRegarding");
            }
            if (content.HasProperty("TaskRegardingId"))
            {
                this.TaskRegardingId = (K2Field)content.GetPropertyAsPersistableObject("TaskRegardingId");
            }
            if (content.HasProperty("TaskState"))
            {
                this.TaskState = (K2Field)content.GetPropertyAsPersistableObject("TaskState");
            }
            if (content.HasProperty("TaskStatus"))
            {
                this.TaskStatus = (K2Field)content.GetPropertyAsPersistableObject("TaskStatus");
            }
            if (content.HasProperty("TaskSubcategory"))
            {
                this.TaskSubcategory = (K2Field)content.GetPropertyAsPersistableObject("TaskSubcategory");
            }
            if (content.HasProperty("TaskSubject"))
            {
                this.TaskSubject = (K2Field)content.GetPropertyAsPersistableObject("TaskSubject");
            }

            this.EnsureThatRequiredProcessFieldsExists();
        }

        //When your project is saved, this override determines which properties you have defined
        //gets saved and can ba accessed once the project is reopened
        protected override void OnSave(ISerializationInfo content)
        {
            base.OnSave(content);

            if (!string.IsNullOrEmpty(InternetPlatform))
            {
                content.SetProperty("InternetPlatform", InternetPlatform);
            }
            content.SetProperty("InsertSN", InsertSN);
            content.SetProperty("CreateTasks", CreateTasks);

            if (!K2Field.IsNullOrEmpty(SN))
            {
                content.SetProperty("SN", SN);
            }

            if (!K2Field.IsNullOrEmpty(ActivityName))
            {
                content.SetProperty("ActivityName", ActivityName);
            }
            if (!K2Field.IsNullOrEmpty(ProcessName))
            {
                content.SetProperty("ProcessName", ProcessName);
            }
            if (!K2Field.IsNullOrEmpty(SmartObjectServer))
            {
                content.SetProperty("SmartObjectServer", SmartObjectServer);
            }
            if (!K2Field.IsNullOrEmpty(CRMFunctionsSmartObject))
            {
                content.SetProperty("CRMFunctionsSmartObject", CRMFunctionsSmartObject);
            }
            //if (!K2Field.IsNullOrEmpty(CustomServiceURL))
            //{
            //    content.SetProperty("CustomServiceURL", CustomServiceURL);
            //}
            if (!K2Field.IsNullOrEmpty(CRMServerURL))
            {
                content.SetProperty("CRMServerURL", CRMServerURL);
            }
            if (!K2Field.IsNullOrEmpty(CRMOrganisation))
            {
                content.SetProperty("CRMOrganisation", CRMOrganisation);
            }
            if (!K2Field.IsNullOrEmpty(CRMFormURL))
            {
                content.SetProperty("CRMFormURL", CRMFormURL);
            }
            if (!K2Field.IsNullOrEmpty(CRMEntityId))
            {
                content.SetProperty("CRMEntityId", CRMEntityId);
            }
            if (!K2Field.IsNullOrEmpty(CRMEntityType))
            {
                content.SetProperty("CRMEntityType", CRMEntityType);
            }
            if (!K2Field.IsNullOrEmpty(CRMEntityForm))
            {
                content.SetProperty("CRMEntityForm", CRMEntityForm);
            }
            if (!K2Field.IsNullOrEmpty(CRMCustomSNParameter))
            {
                content.SetProperty("CRMCustomSNParameter", CRMCustomSNParameter);
            }
            if (!K2Field.IsNullOrEmpty(TaskCategory))
            {
                content.SetProperty("TaskCategory", TaskCategory);
            }
            if (!K2Field.IsNullOrEmpty(TaskDescription))
            {
                content.SetProperty("TaskDescription", TaskDescription);
            }
            if (!K2Field.IsNullOrEmpty(TaskDueDate))
            {
                content.SetProperty("TaskDueDate", TaskDueDate);
            }
            if (!K2Field.IsNullOrEmpty(TaskDuration))
            {
                content.SetProperty("TaskDuration", TaskDuration);
            }
            if (!K2Field.IsNullOrEmpty(TaskOwnerFQN))
            {
                content.SetProperty("TaskOwnerFQN", TaskOwnerFQN);
            }
            if (!K2Field.IsNullOrEmpty(TaskOwner))
            {
                content.SetProperty("TaskOwner", TaskOwner);
            }
            if (!K2Field.IsNullOrEmpty(TaskOwnerId))
            {
                content.SetProperty("TaskOwnerId", TaskOwnerId);
            }
            if (!K2Field.IsNullOrEmpty(TaskPriority))
            {
                content.SetProperty("TaskPriority", TaskPriority);
            }
            if (!K2Field.IsNullOrEmpty(TaskRegarding))
            {
                content.SetProperty("TaskRegarding", TaskRegarding);
            }
            if (!K2Field.IsNullOrEmpty(TaskRegardingId))
            {
                content.SetProperty("TaskRegardingId", TaskRegardingId);
            }
            if (!K2Field.IsNullOrEmpty(TaskState))
            {
                content.SetProperty("TaskState", TaskState);
            }
            if (!K2Field.IsNullOrEmpty(TaskStatus))
            {
                content.SetProperty("TaskStatus", TaskStatus);
            }
            if (!K2Field.IsNullOrEmpty(TaskSubcategory))
            {
                content.SetProperty("TaskSubcategory", TaskSubcategory);
            }
            if (!K2Field.IsNullOrEmpty(TaskSubject))
            {
                content.SetProperty("TaskSubject", TaskSubject);
            }
            
            this.EnsureThatRequiredProcessFieldsExists();
        }

        protected override string[] References
        {
            get
            {
                List<string> list = new List<string>();
                list.Add(Path.Combine(ConfigurationManager.ConfigurationFolder, "SourceCode.HostClientAPI.dll"));
                list.Add(Path.Combine(ConfigurationManager.ConfigurationFolder, "SourceCode.SmartObjects.Client.dll"));

                return list.ToArray();
            }
        }


        protected override void PrepareCodeFiles()
        {
            try
            {
                base.PrepareCodeFiles();

                if (base.Extender.Process != null)
                {
                    if (base.Extender.CodeFiles.Count == 0)
                    {
                        //These directories with their respective XOML and XOML.CS files must be copied to the
                        //K2 bin\DesignTemplates\CSharp folder...
                        CodeFileResolver resolver = new CodeFileResolver(base.Extender, @"CRMClient.CRMClientEventItem\EventItem");
                        CodeFile[] files = resolver.GetCodeFilesFromDisk();
                        foreach (CodeFile file in files)
                            base.Extender.CodeFiles.Add(file);
                        resolver = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // for copy'n'paste
        public override T Clone<T>()
        {
            DesignCRMClient.CRMClientEventItem item = base.Clone<DesignCRMClient.CRMClientEventItem>();

            // not sure how to close non K2Fields???
            if (!string.IsNullOrEmpty(_InternetPlatform))
            {
                item.InternetPlatform = _InternetPlatform.Clone().ToString();
            }            
            item.InsertSN = _InsertSN; 
            item.CreateTasks = _CreateTasks;

            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_SN))
            {
                item.SN = _SN.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_ActivityName))
            {
                item.ActivityName = _ActivityName.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_ProcessName))
            {
                item.ProcessName = _ProcessName.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_SmartObjectServer))
            {
                item.SmartObjectServer = _SmartObjectServer.Clone<K2Field>();
            }
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
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_CRMFormURL))
            {
                item.CRMFormURL = _CRMFormURL.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_CRMEntityId))
            {
                item.CRMEntityId = _CRMEntityId.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_CRMEntityType))
            {
                item.CRMEntityType = _CRMEntityType.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_CRMEntityForm))
            {
                item.CRMEntityForm = _CRMEntityForm.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_CRMCustomSNParameter))
            {
                item.CRMCustomSNParameter = _CRMCustomSNParameter.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_TaskCategory))
            {
                item.TaskCategory = _TaskCategory.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_TaskDescription))
            {
                item.TaskDescription = _TaskDescription.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_TaskDueDate))
            {
                item.TaskDueDate = _TaskDueDate.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_TaskDuration))
            {
                item.TaskDuration = _TaskDuration.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_TaskOwnerFQN))
            {
                item.TaskOwnerFQN = _TaskOwnerFQN.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_TaskOwner))
            {
                item.TaskOwner = _TaskOwner.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_TaskOwnerId))
            {
                item.TaskOwnerId = _TaskOwnerId.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_TaskPriority))
            {
                item.TaskPriority = _TaskPriority.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_TaskRegarding))
            {
                item.TaskRegarding = _TaskRegarding.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_TaskRegardingId))
            {
                item.TaskRegardingId = _TaskRegardingId.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_TaskState))
            {
                item.TaskState = _TaskState.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_TaskStatus))
            {
                item.TaskStatus = _TaskStatus.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_TaskSubcategory))
            {
                item.TaskSubcategory = _TaskSubcategory.Clone<K2Field>();
            }
            if (!SourceCode.Workflow.Authoring.K2Field.IsNullOrEmpty(_TaskSubject))
            {
                item.TaskSubject = _TaskSubject.Clone<K2Field>();
            }

            return item as T;
        }


        protected override void PrepareConfigurationForBuild()
        {
            try
            {
                base.PrepareConfigurationForBuild();

                if (base.Extender != null)
                {
                    base.PrepareConfigurationForBuild();

                    base.Extender.Configuration["InternetPlatform"] = this.CreateConfigSettingField<string>(new K2Field(new ValueTypePart(this.InternetPlatform.ToString())));
                    base.Extender.Configuration["InsertSN"] = this.CreateConfigSettingField<string>(new K2Field(new ValueTypePart(this.InsertSN.ToString())));
                    base.Extender.Configuration["CreateTasks"] = this.CreateConfigSettingField<string>(new K2Field(new ValueTypePart(this.CreateTasks.ToString())));
                    // serial number is accessible in code behind
//                    base.Extender.Configuration["SN"] = this.SN;
                    base.Extender.Configuration["SmartObjectServer"] = this.CreateConfigSettingField<string>(this.SmartObjectServer);
                    base.Extender.Configuration["CRMFunctionsSmartObject"] = this.CreateConfigSettingField<string>(this.CRMFunctionsSmartObject);
                    //base.Extender.Configuration["CustomServiceURL"] = this.CreateConfigSettingField<string>(this.CustomServiceURL);

                    base.Extender.Configuration["CRMServerURL"] = this.CreateConfigSettingField<string>(this.CRMServerURL);
                    base.Extender.Configuration["CRMOrganisation"] = this.CreateConfigSettingField<string>(this.CRMOrganisation);
                    base.Extender.Configuration["CRMFormURL"] = this.CreateConfigSettingField<string>(this.CRMFormURL);
                    base.Extender.Configuration["CRMEntityId"] = this.CreateConfigSettingField<string>(this.CRMEntityId);
                    base.Extender.Configuration["CRMEntityType"] = this.CreateConfigSettingField<string>(this.CRMEntityType);
                    base.Extender.Configuration["CRMEntityForm"] = this.CreateConfigSettingField<string>(this.CRMEntityForm);
                    base.Extender.Configuration["CRMCustomSNParameter"] = this.CreateConfigSettingField<string>(this.CRMCustomSNParameter);
                    base.Extender.Configuration["TaskCategory"] = this.CreateConfigSettingField<string>(this.TaskCategory);
                    base.Extender.Configuration["TaskDescription"] = this.CreateConfigSettingField<string>(this.TaskDescription);
                    base.Extender.Configuration["TaskDueDate"] = this.CreateConfigSettingField<string>(this.TaskDueDate);
                    base.Extender.Configuration["TaskDuration"] = this.CreateConfigSettingField<string>(this.TaskDuration);
                    base.Extender.Configuration["CRMServerURL"] = this.CreateConfigSettingField<string>(this.CRMServerURL);
                    base.Extender.Configuration["TaskOwnerFQN"] = this.CreateConfigSettingField<string>(this.TaskOwnerFQN);
                    base.Extender.Configuration["TaskOwner"] = this.CreateConfigSettingField<string>(new K2Field(new ValueTypePart("systemuser")));                         //this.TaskOwner;
                    base.Extender.Configuration["TaskOwnerId"] = this.CreateConfigSettingField<string>(this.TaskOwnerId);
                    base.Extender.Configuration["TaskPriority"] = this.CreateConfigSettingField<string>(this.TaskPriority);
                    base.Extender.Configuration["TaskRegarding"] = this.CreateConfigSettingField<string>(this.CRMEntityType);     //this.TaskRegarding;
                    base.Extender.Configuration["TaskRegardingId"] = this.CreateConfigSettingField<string>(this.CRMEntityId);        //this.TaskRegardingId;
                    base.Extender.Configuration["TaskState"] = this.CreateConfigSettingField<string>(new K2Field(new ValueTypePart("0")));             //this.TaskState;
                    base.Extender.Configuration["TaskStatus"] = this.CreateConfigSettingField<string>(new K2Field(new ValueTypePart("3")));        //this.TaskStatus;
                    base.Extender.Configuration["TaskSubcategory"] = this.CreateConfigSettingField<string>(this.TaskSubcategory);
                    base.Extender.Configuration["TaskSubject"] = this.CreateConfigSettingField<string>(this.TaskSubject);
                    //base.Extender.Configuration["Folio"] = new WorkflowContextFieldPart(WorkflowContextProperty.ProcessFolio.ToString());
                    base.Extender.Configuration["ProcessName"] = this.CreateConfigSettingField<string>(this.ProcessName);
                    //base.Extender.Configuration["ProcessOriginatorFQN"] = new K2Field(new WorkflowContextFieldPart(WorkflowContextProperty.ProcessOriginatorFQN.ToString()));
                    base.Extender.Configuration["ActivityName"] = this.CreateConfigSettingField<string>(this.ActivityName);
                    //base.Extender.Configuration["EventInstanceName"] = new K2Field(new WorkflowContextFieldPart(WorkflowContextProperty.EventInstanceName.ToString()));

                    this.EnsureThatRequiredProcessFieldsExists();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override PlaceHolderCollection GetPlaceHolders()
        {
            PlaceHolderCollection items = base.GetPlaceHolders();

            return items;
        }

        protected override void SetPlaceHolders(PlaceHolderCollection items)
        {
            //base.SetPlaceHolders(items);

            //this.Cc = items["CC"].Value;
        }
        #endregion

        #region Methods
        private void EnsureThatRequiredProcessFieldsExists()
        {
            this.CreateProcessDataField("Entity Id", SourceCode.Workflow.Authoring.DataTypes.String);
            this.CreateProcessDataField("Entity Name", SourceCode.Workflow.Authoring.DataTypes.String);

            this.CreateProcessXmlField("CRM Context", DesignCRMClient.Resources.CRMContext, null);
            this.CreateProcessXmlField("CRM Tasks", DesignCRMClient.Resources.CRMTasks, "<CRMTasks></CRMTasks>");
        }

        private void CreateProcessDataField(string name, SourceCode.Workflow.Authoring.DataTypes dataType)
        {
            if (this.Process == null)
            {
                return;
            }

            if (!this.Process.DataFields.Contains(name))
            {
                SourceCode.Workflow.Authoring.DataField df = new SourceCode.Workflow.Authoring.DataField(name, dataType);
                df.Scope = SourceCode.Workflow.Authoring.FieldScope.Process;
                df.OnDemand = true;
                df.Hidden = false;
                df.Log = false;
                df.Audit = false;
                df.Category = "CRM Client Event";
                this.Process.DataFields.Add(df);
            }
        }

        private void CreateProcessXmlField(string name, string schema, string value)
        {
            if (this.Process == null)
            {
                return;
            }

            if (!this.Process.XmlFields.Contains(name))
            {
                SourceCode.Workflow.Authoring.XmlField xmlf = new SourceCode.Workflow.Authoring.XmlField(name);
                xmlf.Scope = SourceCode.Workflow.Authoring.FieldScope.Process;
                xmlf.OnDemand = false;
                xmlf.Hidden = false;
                xmlf.Log = false;
                xmlf.Audit = false;
                xmlf.Category = "CRM Client Event";
                if (!string.IsNullOrEmpty(schema))
                {
                    xmlf.SchemaURI = schema;
                }
                else
                {
                    xmlf.SchemaURI = this.CreateProcessXmlFieldSchemaString(name);
                }
                if (!string.IsNullOrEmpty(value))
                {
                    xmlf.Value = value;
                }
                this.Process.XmlFields.Add(xmlf);
            }
        }

        private string CreateProcessXmlFieldSchemaString(string name)
        {
            System.Xml.Schema.XmlSchema xmlSchema = new System.Xml.Schema.XmlSchema();
            System.Xml.Schema.XmlSchemaComplexType complexType = new System.Xml.Schema.XmlSchemaComplexType();
            System.Xml.Schema.XmlSchemaSequence sequence = new System.Xml.Schema.XmlSchemaSequence();
            complexType.Particle = sequence;
            System.Xml.Schema.XmlSchemaElement rootElement = new System.Xml.Schema.XmlSchemaElement();
            rootElement.SchemaType = complexType;
            rootElement.Name = name;
            xmlSchema.Items.Add(rootElement);
            System.IO.StringWriter stream = new System.IO.StringWriter();
            xmlSchema.Write(stream);
            string outString = stream.ToString();
            stream.Close();
            stream.Dispose();
            return outString;
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
        #endregion

    }
}
