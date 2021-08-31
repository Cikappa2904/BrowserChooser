using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
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

                RegistryKey FileName = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\shell\\open\\command");
                string filePath = (string)FileName.GetValue("");
                File.Delete(filePath);
                Registry.ClassesRoot.DeleteSubKeyTree("BrowserChooserURL");
                Registry.LocalMachine.DeleteSubKeyTree("Software\\Clients\\StartMenuInternet\\BrowserChooser");
                RegistryKey RegApps = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\RegisteredApplications", true);
                RegApps.DeleteValue("Browser Chooser");
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
