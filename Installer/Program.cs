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
                string browserChooserDownloadPath = installPath + "\\BrowserChooser.exe";
                string uninstallerDownloadPath = installPath + "\\Uninstaller.exe";

                Uri browserChooserDownloadLink = new System.Uri("https://raw.githubusercontent.com/Cikappa2904/BrowserChooser/main/BrowserChooser/bin/Release/netcoreapp3.1/publish/BrowserChooser.exe");
                Uri uninstallerDownloadLink = new System.Uri("https://raw.githubusercontent.com/Cikappa2904/BrowserChooser/main/Uninstaller/bin/Release/Uninstaller.exe");


                InstallerClass.CreateBrowserChooserFolder(installPath);

                string filePath = "\"" + installPath + "\\BrowserChooser.exe" + "\" %1";

                InstallerClass.CreateUninstallerRegistryKeys(version, browserChooserDownloadPath, uninstallerDownloadPath);

                InstallerClass.CreateBrowserChooserURL();

                InstallerClass.ShellBrowserChooserURL(filePath);

                InstallerClass.RegisteredApplications();

                InstallerClass.AddBrowserChooserToStartMenuInternet(installPath);

                InstallerClass.StartMenuInternetCapabilities();

                InstallerClass.RegisterBrowserChooserForHTTPAndHTTPS();

                InstallerClass.DefaultIcon(installPath);

                InstallerClass.StartMenuInternetShell(installPath);

                InstallerClass.Download(browserChooserDownloadLink, browserChooserDownloadPath); //Download BrowserChooser.exe from GitHub

                InstallerClass.Download(uninstallerDownloadLink, uninstallerDownloadPath); //Download Uninstaller.exe from GitHub

                Console.WriteLine("Installation completed!");



                
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
