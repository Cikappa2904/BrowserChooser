using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.Win32;

namespace Installer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Selecting where the file will be stored
            string downloader = " https://raw.githubusercontent.com/Cikappa2904/BrowserChooser/main/BrowserChooser/bin/Debug/netcoreapp3.1/BrowserChooser.exe" + " -o \"" + textBox1.Text + "\\BrowserChooser.exe" + "\"";
            
            //Downloading the file
            Process downloadProcess = new Process();
            downloadProcess.StartInfo.FileName = "curl";
            downloadProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; //Hides the cmd window
            downloadProcess.StartInfo.Arguments = downloader;
            downloadProcess.Start();

            string filePath = "\"" + textBox1.Text + "\\BrowserChooser.exe" + "\" %1";

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
            string filePath2 = textBox1.Text + "\\BrowserChooser.exe" + ",0";
            StartMenuInternet.SetValue("ApplicationIcon", filePath2);
            StartMenuInternet.SetValue("ApplicationName", "Browser Chooser");
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
            string filePath3 = textBox1.Text + "\\BrowserChooser.exe";
            StartMenuInternet5.SetValue("", filePath3);
            StartMenuInternet5.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Program Files";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                textBox1.Text = dialog.FileName;
            }
        }
    }
}
