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
using SourceCode.Workflow.Authoring;
using SourceCode.Framework;

namespace DesignCRMClient
{
    [SourceCode.Framework.Design.K2ImageAttribute(typeof(WizardDefinitionImage))]
    public class CRMClientWizardDefinition : DefaultWizardDefinition
    {
        public CRMClientWizardDefinition()
        {

        }

        //The Image object can be overridden here as well, otherwise the defualt K2 image will be used.
        //something about image on activity bar...
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
