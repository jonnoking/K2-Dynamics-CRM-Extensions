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

using SourceCode.Workflow.Design;
using SourceCode.Framework;

namespace DesignCRMClient
{
    [SourceCode.Framework.Design.K2ImageAttribute(typeof(PropertyWizardDefinitionImage))]
    public class CRMClientPropertyWizardDefinition : DefaultPropertyWizardDefinition
    {
        public CRMClientPropertyWizardDefinition()
        {

        }

        //This image can be overridden. This image is used when an event has been dropped onto an activity
        //something about image for property value...
        public override K2Image Image
        {
            get
            {
                K2Image k2image = null;
                try
                {
                    k2image = new K2Image(Resources.DesignerIcon);
                }
                catch
                { }
                return k2image;
            }
        }
    }
}
