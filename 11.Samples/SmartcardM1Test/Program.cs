using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartcardM1Test
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ApplicationExit += Application_ApplicationExit;
            Application.ThreadExit += Application_ThreadExit;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static void Application_ThreadExit(object sender, EventArgs e)
        {
            DMT.Smartcard.SmartcardService.Release();
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            DMT.Smartcard.SmartcardService.Release();
        }
    }
}
