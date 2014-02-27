using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceCode.Workflow.Authoring;
using SourceCode.Workflow.Design;

namespace K2.Demo.CRM.Test
{
    public static class ProcessAuthoring
    {
        internal static void CreateProcessFile(string path)
        {
            var proc = new SourceCode.Workflow.Design.DefaultProcess();
            proc.SaveAs(path);
        }

        public static DataField EnsureDataField(SourceCode.Workflow.Authoring.Process proc, string DFValue, string DFName)
        {
            //If found first delete..
            if (proc.DataFields.Contains(DFName))
            {
                proc.DataFields.Remove(proc.DataFields[DFName]);
            }

            DataField newDF = new SourceCode.Workflow.Authoring.DataField(DFName, DFValue);
            newDF.Audit = false;
            newDF.Hidden = true;
            newDF.OnDemand = true;
            newDF.Log = false;

            proc.DataFields.Add(newDF);

            return newDF;
        }

        public static void GenerateActivity()
        {
            var createCaseActivity =  WorkflowFactory.CreateActivity<DefaultActivity>("Create Case", WizardNames.DefaultActivity);
            createCaseActivity.MetaData = activityMetaData;
        }
    }
}
