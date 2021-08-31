using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installer
{
    class InstallerClass
    {
        public static void CreateBrowserChooserFolder (string folderName)
        {
            if (!Directory.Exists(folderName))
            {
                System.IO.Directory.CreateDirectory(folderName);
            }
        }

        public static void CreateUninstallerRegistryKeys (string version, string browserChooserDownloadPath, string uninstallerDownloadPath)
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
    }
}
