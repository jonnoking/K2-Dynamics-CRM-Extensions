//////////////////////////////////////////////////////////////////////////
//																		//
//																		//
//																		//
//																		//
//																		//
//																		//
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;

using SourceCode.Workflow.Authoring;
using SourceCode.Workflow.Design;
using SourceCode.Framework;

namespace DesignCRMClient
{
    [Serializable]
    [SourceCode.Framework.Design.K2ImageAttribute(typeof(EventImage))]
    public class CRMClientEvent : SourceCode.Workflow.Design.ClientEvent
        //SourceCode.Workflow.Authoring.Event
    {
        public CRMClientEvent()
            : base()
        {
            //This is to set the type of the base class and what Event Class it inherits from
            base.Type = EventTypes.Client;
            //This is to instantiate the Event Item Class
            base.EventItem = new CRMClientEventItem();

        }

        public new CRMClientEventItem EventItem
        {
            get { return base.EventItem as CRMClientEventItem; }
            set { base.EventItem = value; }
        }

        public override K2Image Image
        {
            get
            {
                K2Image k2image = null;
                try
                {
                    k2image = new K2Image(Resources.DesignerIcon);
                    //this is an example of how to use a XAML image as a K2 Image
                    //k2image = new K2Image(System.Text.UnicodeEncoding.Unicode.GetString(DesignCRMClient.Resources.XAMLFile));
                }
                catch
                { }
                return k2image;
            }
        }
    }
}
