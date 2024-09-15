using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;

namespace Uninstaller
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            if (args.Length > 0 && args[0] == "/s")
            {

                //UninstallClass.DeleteUninstaller();

                UninstallClass.DeleteBrowserChooserURL();
                UninstallClass.DeleteSMIBrowserChooser();
                UninstallClass.DeleteRegAppBrowserChooser();
                UninstallClass.DeleteUninstallRegBrowserChooser();

                string fileName = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\", 0));

                Process.Start(new ProcessStartInfo()
                {
                    Arguments = "/C choice /C Y /N /D Y /T 3 & rmdir /s /q \"" + fileName + "\"",
                    //WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = false,
                    FileName = "cmd.exe"
                });


                Application.Exit();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainWindow());

            }


            
        }
    }
}
