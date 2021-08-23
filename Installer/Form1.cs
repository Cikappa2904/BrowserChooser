using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using System.Diagnostics;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.Win32;


namespace Installer
{
    public partial class Form1 : Form
    {
        private ProgressBar progressBar1;
        private TextBox progressText;

        public Form1()
        {
            InitializeComponent();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public string Get_Form1Text()
        {
            return textBox1.Text;
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

   
        private void button1_Click(object sender, EventArgs e)
        {
            string version = "1.0.0.0";

            Form3 form3 = new Form3();
            progressBar1 = form3.progressBar1;
            progressText = form3.textBox1;

            this.Hide();
            form3.Show();

            //Selecting where the file will be stored
            string downloadPath = textBox1.Text + "\\BrowserChooser.exe";
            string downloadPath2 = textBox1.Text + "\\uninstaller.exe";

            Uri downloadLink = new System.Uri("https://raw.githubusercontent.com/Cikappa2904/BrowserChooser/main/BrowserChooser/bin/Release/netcoreapp3.1/publish/BrowserChooser.exe");
            Uri downloadLink2 = new System.Uri("https://raw.githubusercontent.com/Cikappa2904/BrowserChooser/main/Uninstaller/bin/Release/Uninstaller.exe");


            if (!Directory.Exists(textBox1.Text))
            {
                progressText.AppendText("Creating " + textBox1.Text + Environment.NewLine);
                System.IO.Directory.CreateDirectory(textBox1.Text);
            }

            string filePath = "\"" + textBox1.Text + "\\BrowserChooser.exe" + "\" %1";

            progressText.AppendText("- Adding registry keys to HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall" + Environment.NewLine);
            RegistryKey uninstall = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Browser Chooser");
            uninstall.SetValue("DisplayIcon", downloadPath);
            uninstall.SetValue("DisplayName", "Browser Chooser");
            uninstall.SetValue("DisplayVersion", version);
            uninstall.SetValue("UninstallString", downloadPath2);
            uninstall.SetValue("Publisher", "Cikappa2904");


            progressText.AppendText("- Adding registry keys to HKCR" + Environment.NewLine);
            RegistryKey URL = Registry.ClassesRoot.CreateSubKey(@"BrowserChooserURL");
            URL.SetValue("", "Browser Chooser");
            URL.SetValue("FriendlyTypeName", "Browser Chooser");
            URL.SetValue("URL Protocol", "");
            URL.Close();
            progressBar1.Value = 20;

            progressText.AppendText("- Adding registry keys to HKCR\\BrowserChooserURL" + Environment.NewLine);
            RegistryKey Shell = Registry.ClassesRoot.CreateSubKey(@"BrowserChooserURL\shell\open\command");
            Shell.SetValue("", filePath);
            Shell.Close();
            progressBar1.Value = 30;

            progressText.AppendText("- Adding registry keys to HKLM\\SOFTWARE\\RegisteredApplications" + Environment.NewLine);
            RegistryKey RegApps = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\RegisteredApplications", true);
            RegApps.SetValue("Browser Chooser", "Software\\Clients\\StartMenuInternet\\BrowserChooser\\Capabilities");
            RegApps.Close();
            progressBar1.Value = 40;

            progressText.AppendText("- Adding registry keys to HKLM\\SOFTWARE\\Clients\\StartMenuInternet" + Environment.NewLine);
            RegistryKey StartMenuInternet = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\Capabilities");
            StartMenuInternet.SetValue("ApplicationDescription", "Choose what browser you want to use every time");
            string filePath2 = textBox1.Text + "\\BrowserChooser.exe" + ",0";
            StartMenuInternet.SetValue("ApplicationIcon", filePath2);
            StartMenuInternet.SetValue("ApplicationName", "Browser Chooser");
            StartMenuInternet.Close();
            progressBar1.Value = 50;

            progressText.AppendText("- Adding registry keys to HKLM\\SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\Capabilities" + Environment.NewLine);
            RegistryKey StartMenuInternet2 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\Capabilities\\Startmenu");
            StartMenuInternet2.SetValue("StartMenuInternet", "Browser Chooser");
            StartMenuInternet2.Close();
            progressBar1.Value = 60;

            progressText.AppendText("- Adding registry keys to HKLM\\SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\Capabilities" + Environment.NewLine);
            RegistryKey StartMenuInternet3 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\Capabilities\\URLAssociations");
            StartMenuInternet3.SetValue("http", "BrowserChooserURL");
            StartMenuInternet3.SetValue("https", "BrowserChooserURL");
            StartMenuInternet3.Close();
            progressBar1.Value = 70;

            progressText.AppendText("- Adding registry keys to HKLM\\SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser" + Environment.NewLine);
            RegistryKey StartMenuInternet4 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\DefaultIcon");
            StartMenuInternet4.SetValue("", filePath2);
            StartMenuInternet4.Close();
            progressBar1.Value = 80;

            progressText.AppendText("- Adding registry values to HKLM\\SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\shell\\open\\command" + Environment.NewLine);
            RegistryKey StartMenuInternet5 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\shell\\open\\command");
            string filePath3 = textBox1.Text + "\\BrowserChooser.exe";
            StartMenuInternet5.SetValue("", filePath3);
            StartMenuInternet5.Close();
            progressBar1.Value = 80;

            WebClient myWebClient = new WebClient();
            progressText.AppendText("- Starting to download BrowserChooser.exe" + Environment.NewLine);
            myWebClient.DownloadFile(downloadLink, downloadPath); //Downloading BrowserChooser.exe
            progressBar1.Value = 90;

            progressText.AppendText("- Starting to download Uninstaller.exe" + Environment.NewLine);
            myWebClient.DownloadFile(downloadLink2, downloadPath2); //Downloading uninstaller.exe
            progressBar1.Value = 100;

            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
