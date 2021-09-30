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
using System.Reflection;
using System.Security.Principal;


namespace Installer
{
    public partial class Form1 : Form
    {
        private ProgressBar progressBar1;
        private TextBox progressText;
        private FolderBrowserDialog folderBrowserDialog;

      
        public Form1()
        {

            InitializeComponent();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog.ShowNewFolderButton = true;
            this.folderBrowserDialog.RootFolder = Environment.SpecialFolder.ProgramFiles;
        }

        public string Get_Form1Text()
        {
            return textBox1.Text;
        }


        private void button2_Click(object sender, EventArgs e)
        {


            //CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            //    dialog.InitialDirectory = Environment.SpecialFolder.ProgramFiles.ToString();
            //    dialog.IsFolderPicker = true;
            //    if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            //    {
            //       textBox1.Text = dialog.FileName;
            //    }

            System.Windows.Forms.OpenFileDialog folderBrowser = new System.Windows.Forms.OpenFileDialog();
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            folderBrowser.FileName = "Folder Selection.";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
               textBox1.Text = Path.GetDirectoryName(folderBrowser.FileName);
                
            }

        }

   
        private void button1_Click(object sender, EventArgs e)
        {
            string version = "0.3.0.1";

            Form3 form3 = new Form3();
            progressBar1 = form3.progressBar1;
            progressText = form3.textBox1;

            this.Hide();
            form3.Show();
            form3.Update(); //Like in uninstaller, don't exactly now if this is the right way to do it, but this makes the label display correctly lol

            //Selecting where the file will be stored
            string browserChooserDownloadPath = textBox1.Text + "\\BrowserChooser.exe";
            string uninstallerDownloadPath = textBox1.Text + "\\Uninstaller.exe";

            Uri browserChooserDownloadLink = new System.Uri("https://raw.githubusercontent.com/Cikappa2904/BrowserChooser/main/BrowserChooser/bin/Release/netcoreapp3.1/publish/BrowserChooser.exe");
            Uri uninstallerDownloadLink = new System.Uri("https://raw.githubusercontent.com/Cikappa2904/BrowserChooser/main/Uninstaller/bin/Release/Uninstaller.exe");


            InstallerClass.CreateBrowserChooserFolder(textBox1.Text);
            progressText.AppendText("Creating " + textBox1.Text + Environment.NewLine);

            string filePath = "\"" + textBox1.Text + "\\BrowserChooser.exe" + "\" %1";

            InstallerClass.CreateUninstallerRegistryKeys(version, browserChooserDownloadPath, uninstallerDownloadPath);
            progressText.AppendText("- Adding registry keys to HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall" + Environment.NewLine);



            progressText.AppendText("- Adding registry keys to HKCR" + Environment.NewLine);
            InstallerClass.CreateBrowserChooserURL();
            progressBar1.Value = 20;

            progressText.AppendText("- Adding registry keys to HKCR\\BrowserChooserURL" + Environment.NewLine);
            InstallerClass.ShellBrowserChooserURL(filePath);
            progressBar1.Value = 30;

            progressText.AppendText("- Adding registry keys to HKLM\\SOFTWARE\\RegisteredApplications" + Environment.NewLine);
            InstallerClass.RegisteredApplications();
            progressBar1.Value = 40;

            progressText.AppendText("- Adding registry keys to HKLM\\SOFTWARE\\Clients\\StartMenuInternet" + Environment.NewLine);
            InstallerClass.AddBrowserChooserToStartMenuInternet(textBox1.Text);
            progressBar1.Value = 50;

            progressText.AppendText("- Adding registry keys to HKLM\\SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\Capabilities" + Environment.NewLine);
            InstallerClass.StartMenuInternetCapabilities();
            progressBar1.Value = 60;

            progressText.AppendText("- Adding registry keys to HKLM\\SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\Capabilities" + Environment.NewLine);
            InstallerClass.RegisterBrowserChooserForHTTPAndHTTPS();
            progressBar1.Value = 70;

            progressText.AppendText("- Adding registry keys to HKLM\\SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser" + Environment.NewLine);
            InstallerClass.DefaultIcon(textBox1.Text);
            progressBar1.Value = 80;

            progressText.AppendText("- Adding registry values to HKLM\\SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\shell\\open\\command" + Environment.NewLine);
            InstallerClass.StartMenuInternetShell(textBox1.Text);
            progressBar1.Value = 80;

            
            progressText.AppendText("- Starting to download BrowserChooser.exe" + Environment.NewLine);
            InstallerClass.Download(browserChooserDownloadLink, browserChooserDownloadPath); //Download BrowserChooser.exe from GitHub
            progressBar1.Value = 90;

            progressText.AppendText("- Starting to download Uninstaller.exe" + Environment.NewLine);
            InstallerClass.Download(uninstallerDownloadLink, uninstallerDownloadPath); //Download Uninstaller.exe from GitHub
            progressBar1.Value = 100;

            Form2 form2 = new Form2();
            form2.Show();
        }

         
       
    }
}
