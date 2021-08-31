using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uninstaller
{
    class UninstallClass
    {
        public static void DeleteUninstaller ()
        {
            RegistryKey FileName = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\shell\\open\\command");
            if (FileName != null)
            {
                string filePath = FileName.GetValue("") as string;
                if (File.Exists(filePath)) //Check if file exists
                {
                    File.Delete(filePath);
                }
            }
        }

        public static void DeleteBrowserChooserURL ()
        {
            if (Registry.ClassesRoot.OpenSubKey("BrowserChooserURL") != null) //Check if the Key BrowserChooserURL exists
            {
                Registry.ClassesRoot.DeleteSubKeyTree("BrowserChooserURL");
            }
        }

        public static void DeleteSMIBrowserChooser ()
        {
            if (Registry.LocalMachine.OpenSubKey("Software\\Clients\\StartMenuInternet\\BrowserChooser") != null) //Check if the key BrowserChooser exists
            {
                Registry.LocalMachine.DeleteSubKeyTree("Software\\Clients\\StartMenuInternet\\BrowserChooser");
            }
        }

        public static void DeleteRegAppBrowserChooser ()
        {
            RegistryKey regApps = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\RegisteredApplications", true);
            if (Registry.GetValue(@"HKEY_LOCAL_MACHINE\\SOFTWARE\\RegisteredApplications", "Browser Chooser", null) != null) //Check if the value Browser Chooser exists
            {
                regApps.DeleteValue("Browser Chooser");
            }
        }

        public static void DeleteUninstallRegBrowserChooser ()
        {
            if (Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Browser Chooser") != null)
            {
                Registry.LocalMachine.DeleteSubKeyTree(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Browser Chooser");
            }
        }


    }
}
