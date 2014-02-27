using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

using SourceCode.Framework;
using SourceCode.Framework.Design;
using SourceCode.Workflow.Design;
using SourceCode.Workflow.Authoring;
using SourceCode.Workflow.WizardFramework;
using SourceCode.Workflow.WizardFramework.Controls;
using SourceCode.Workflow.VisualDesigners.WizardHost;

using DesignCRMClient;

namespace WizardCRMClient.Pages
{
    public partial class FinishPage : WizardPage
    {
        public FinishPage(CRMClientEvent theEvent)
            : base(theEvent)
        {
            InitializeComponent();

            //Example of how to dynamically add a K2TextBox to a XAML page.
            //View XAML markup code for example of controls: namespace to register K2TextBox controls
            //I.e. <controls:K2TextBox Grid.Column="0" Grid.Row="0" Name="myK2TextBox" TabIndex="0" IsRequired="True"></controls:K2TextBox>

            //this.Background = new ImageBrush(SourceCode.Workflow.WizardFramework.Controls.CommonMethods.CreateBitmapFromSource(image file in Resource file, System.Drawing.Imaging.ImageFormat.Png));
        }


        //This event is called when the Page is loaded.
        protected override bool OnActivate()
        {
            //This is an example of how values will be loaded if this Wizard has been run previously 
            //and data was saved in the Studio Environment.

            //Replace the DesignEventName with your Designer Projects' Event class value.
            //if ((base.DataObject as CRMClientEvent).EventItem.MyK2Field1 != null)
            //{
            //    myK2TextBox.K2Field = (base.DataObject as CRMClientEvent).EventItem.MyK2Field1;
            //}
            //if (!string.IsNullOrEmpty((base.DataObject as CRMClientEvent).EventItem.MyString1))
            //{
            //	myNormalTextBox.Text = (base.DataObject as CRMClientEvent).EventItem.MyString1;
            //}

            return true;
        }

        //This event gets called when the page gets unloaded
        protected override bool OnDeactivate()
        {
            //This is an example of how data will be saved when the wizard page is unloaded.
            //The OnValidate override will be called first to validate if all required information was entered.
            //If OnValidate returns false, this override will not be initialized.

            //Replace the DesignEventName with your Designer Projects' Event class value.
            //(base.DataObject as CRMClientEvent).EventItem.MyK2Field1 = myK2TextBox.K2Field;
            //(base.DataObject as CRMClientEvent).EventItem.MyString1 = myNormalTextBox.Text;

            return true;
        }

        //This event gets called when the Next/Finished button is clicked to 
        //validate that all required information has been entered into relevant areas
        protected override bool OnValidate()
        {
            //This is an example of how data will be validated before the wizard page is unloaded.

            //if (myK2TextBox.IsEmpty == true)
            //{
            //    //Show K2 Error message
            //    ShowK2Error(myK2TextBox, "Please supply a value here.");
            //    //return false so that OnDeactive() override is not initialized.
            //    return false;
            //}
            //else
            //{
            //    HideK2Error(myK2TextBox);
            //}
            //if (myNormalTextBox.Text == string.Empty)
            //{
            //    ShowK2Error(myNormalTextBox, "Please supply a value here.");
            //    return false;
            //}
            //else
            //{
            //    HideK2Error(myNormalTextBox);
            //}


            //When all validations have been passed, return true so that OnDeactivate() override is initialized.
            return true;
        }
    }
}
