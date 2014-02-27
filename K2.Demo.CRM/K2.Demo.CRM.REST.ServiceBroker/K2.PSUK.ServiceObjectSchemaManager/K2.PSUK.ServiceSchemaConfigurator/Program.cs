using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace K2.PSUK.ServiceObjectSchemaConfigurator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ServiceSchemaConfigurator());
        }
    }
}