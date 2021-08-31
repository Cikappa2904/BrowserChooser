using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Installer
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]


        
        static void Main(string[] args)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            

            if (args.Length > 0 && args[0] == "/s")
            {

                string version = "0.3.0.1";
                string installPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Browser Chooser";
                string downloadPath = installPath + "\\BrowserChooser.exe";
                string downloadPath2 = installPath + "\\uninstaller.exe";

                Uri downloadLink = new System.Uri("https://raw.githubusercontent.com/Cikappa2904/BrowserChooser/main/BrowserChooser/bin/Release/netcoreapp3.1/publish/BrowserChooser.exe");
                Uri downloadLink2 = new System.Uri("https://raw.githubusercontent.com/Cikappa2904/BrowserChooser/main/Uninstaller/bin/Release/Uninstaller.exe");


                if (!Directory.Exists(installPath))
                {
                    System.IO.Directory.CreateDirectory(installPath);
                }

                string filePath = "\"" + installPath + "\\BrowserChooser.exe" + "\" %1";

                RegistryKey uninstall = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Browser Chooser");
                uninstall.SetValue("DisplayIcon", downloadPath);
                uninstall.SetValue("DisplayName", "Browser Chooser");
                uninstall.SetValue("DisplayVersion", version);
                uninstall.SetValue("UninstallString", downloadPath2);
                string silentUninstall = downloadPath + " /s";
                uninstall.SetValue("QuietUninstallString", downloadPath2);
                uninstall.SetValue("Publisher", "Cikappa2904");


                
                RegistryKey URL = Registry.ClassesRoot.CreateSubKey(@"BrowserChooserURL");
                URL.SetValue("", "Browser Chooser");
                URL.SetValue("FriendlyTypeName", "Browser Chooser");
                URL.SetValue("URL Protocol", "");
                URL.Close();
                

                
                RegistryKey Shell = Registry.ClassesRoot.CreateSubKey(@"BrowserChooserURL\shell\open\command");
                Shell.SetValue("", filePath);
                Shell.Close();
                

                
                RegistryKey RegApps = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\RegisteredApplications", true);
                RegApps.SetValue("Browser Chooser", "Software\\Clients\\StartMenuInternet\\BrowserChooser\\Capabilities");
                RegApps.Close();
               

                
                RegistryKey StartMenuInternet = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\Capabilities");
                StartMenuInternet.SetValue("ApplicationDescription", "Choose what browser you want to use every time");
                string filePath2 = installPath + "\\BrowserChooser.exe" + ",0";
                StartMenuInternet.SetValue("ApplicationIcon", filePath2);
                StartMenuInternet.SetValue("ApplicationName", "Browser Chooser");
                StartMenuInternet.Close();
               

                
                RegistryKey StartMenuInternet2 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\Capabilities\\Startmenu");
                StartMenuInternet2.SetValue("StartMenuInternet", "Browser Chooser");
                StartMenuInternet2.Close();
                

                
                RegistryKey StartMenuInternet3 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\Capabilities\\URLAssociations");
                StartMenuInternet3.SetValue("http", "BrowserChooserURL");
                StartMenuInternet3.SetValue("https", "BrowserChooserURL");
                StartMenuInternet3.Close();
                

                
                RegistryKey StartMenuInternet4 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\DefaultIcon");
                StartMenuInternet4.SetValue("", filePath2);
                StartMenuInternet4.Close();
                

                
                RegistryKey StartMenuInternet5 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\shell\\open\\command");
                string filePath3 = installPath + "\\BrowserChooser.exe";
                StartMenuInternet5.SetValue("", filePath3);
                StartMenuInternet5.Close();
                

                WebClient myWebClient = new WebClient();
                myWebClient.DownloadFile(downloadLink, downloadPath); //Downloading BrowserChooser.exe
                myWebClient.DownloadFile(downloadLink2, downloadPath2); //Downloading uninstaller.exe

                
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }

            
        }

        

    }
}
