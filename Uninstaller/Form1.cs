using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Uninstaller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegistryKey FileName = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\shell\\open\\command");
            string filePath = (string)FileName.GetValue("");
            File.Delete(filePath);
            Registry.ClassesRoot.DeleteSubKeyTree("BrowserChooserURL");
            Registry.LocalMachine.DeleteSubKeyTree("Software\\Clients\\StartMenuInternet\\BrowserChooser");
            RegistryKey RegApps = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\RegisteredApplications", true);
            RegApps.DeleteValue("Browser Chooser");
            


        }
    }
}
