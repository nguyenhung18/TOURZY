using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TOURZY___Tourism_Management_System
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            fLogin loginForm = new fLogin();
            if (loginForm.ShowDialog() == DialogResult.OK)  
            {
                Application.Run(new User());
            }
           
        }
    }
}
