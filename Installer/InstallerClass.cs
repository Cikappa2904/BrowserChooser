using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Installer
{
    class InstallerClass
    {
        public static void CreateBrowserChooserFolder(string folderName)
        {
            if (!Directory.Exists(folderName))
            {
                System.IO.Directory.CreateDirectory(folderName);
            }
        }

        public static void CreateUninstallerRegistryKeys(string version, string browserChooserDownloadPath, string uninstallerDownloadPath)
        {
            RegistryKey uninstall = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Browser Chooser");
            uninstall.SetValue("DisplayIcon", browserChooserDownloadPath);
            uninstall.SetValue("DisplayName", "Browser Chooser");
            uninstall.SetValue("DisplayVersion", version);
            string silentUninstall = browserChooserDownloadPath + " /s";
            uninstall.SetValue("QuietUninstallString", silentUninstall);
            uninstall.SetValue("UninstallString", uninstallerDownloadPath);
            uninstall.SetValue("Publisher", "Cikappa2904");
        }

        public static void CreateBrowserChooserURL()
        {
            RegistryKey URL = Registry.ClassesRoot.CreateSubKey(@"BrowserChooserURL");
            URL.SetValue("", "Browser Chooser");
            URL.SetValue("FriendlyTypeName", "Browser Chooser");
            URL.SetValue("URL Protocol", "");
            URL.Close();
        }

        public static void ShellBrowserChooserURL(string filePath)
        {
            RegistryKey Shell = Registry.ClassesRoot.CreateSubKey(@"BrowserChooserURL\shell\open\command");
            Shell.SetValue("", filePath);
            Shell.Close();
        }

        public static void RegisteredApplications()
        {
            RegistryKey RegApps = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\RegisteredApplications", true);
            RegApps.SetValue("Browser Chooser", "Software\\Clients\\StartMenuInternet\\BrowserChooser\\Capabilities");
            RegApps.Close();
        }

        public static void AddBrowserChooserToStartMenuInternet(string path)
        {
            RegistryKey StartMenuInternet = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\Capabilities");
            StartMenuInternet.SetValue("ApplicationDescription", "Choose what browser you want to use every time");
            string filePath = path + "\\BrowserChooser.exe" + ",0";
            StartMenuInternet.SetValue("ApplicationIcon", filePath);
            StartMenuInternet.SetValue("ApplicationName", "Browser Chooser");
            StartMenuInternet.Close();
        }

        public static void StartMenuInternetCapabilities()
        {
            RegistryKey StartMenuInternet = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\Capabilities\\Startmenu");
            StartMenuInternet.SetValue("StartMenuInternet", "Browser Chooser");
            StartMenuInternet.Close();
        }

        public static void RegisterBrowserChooserForHTTPAndHTTPS()
        {
            RegistryKey StartMenuInternet = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\Capabilities\\URLAssociations");
            StartMenuInternet.SetValue("http", "BrowserChooserURL");
            StartMenuInternet.SetValue("https", "BrowserChooserURL");
            StartMenuInternet.Close();
        }

        public static void DefaultIcon(string path)
        {
            RegistryKey StartMenuInternet = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\DefaultIcon");
            string filePath = path + "\\BrowserChooser.exe" + ",0";
            StartMenuInternet.SetValue("", filePath);
            StartMenuInternet.Close();
        }
        
        public static void StartMenuInternetShell(string path)
        {
            RegistryKey StartMenuInternet = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\shell\\open\\command");
            string filePath = path + "\\BrowserChooser.exe";
            StartMenuInternet.SetValue("", filePath);
            StartMenuInternet.Close();
        } 


    }

}
