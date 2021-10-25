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
    public partial class MainWindow : Form
    {
        private ProgressBar progressBar1;
        private TextBox progressText;
        public bool isDownloadFinished;



        public MainWindow()
        {
            InitializeComponent();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog folderBrowser = new System.Windows.Forms.OpenFileDialog();
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            folderBrowser.FileName = "Select a folder.";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
               textBox1.Text = Path.GetDirectoryName(folderBrowser.FileName);
            }

        }

   
        public void button1_Click(object sender, EventArgs e)
        {
            string version = "0.3.0.1";

            //Creating new form based on InstallationProgress form object
            InstallationProgress form3 = new InstallationProgress(); 
            progressBar1 = form3.progressBar1;
            progressText = form3.textBox1;

            this.Hide(); 
            form3.Show();
            form3.Update(); //Like in uninstaller, don't exactly know if this is the right way to do it, but this makes the label display correctly lol

            //Selecting where the file will be stored
            string browserChooserDownloadPath = textBox1.Text + "\\BrowserChooser.exe";
            string uninstallerDownloadPath = textBox1.Text + "\\Uninstaller.exe";
            string filePath = "\"" + textBox1.Text + "\\BrowserChooser.exe" + "\" %1";

            //Where to download the files from
            Uri browserChooserDownloadLink = new System.Uri("https://raw.githubusercontent.com/Cikappa2904/BrowserChooser/main/BrowserChooser/bin/Release/netcoreapp3.1/publish/BrowserChooser.exe");
            Uri uninstallerDownloadLink = new System.Uri("https://raw.githubusercontent.com/Cikappa2904/BrowserChooser/main/Uninstaller/bin/Release/Uninstaller.exe");

            progressText.AppendText("Creating " + textBox1.Text + Environment.NewLine);
            InstallerClass.CreateBrowserChooserFolder(textBox1.Text);

            InstallerClass.CreateUninstallerRegistryKeys(version, browserChooserDownloadPath, uninstallerDownloadPath);
            progressText.AppendText("- Adding registry keys to HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall" + Environment.NewLine);


            progressText.AppendText("- Starting to download BrowserChooser.exe" + Environment.NewLine);
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFileAsync(browserChooserDownloadLink, browserChooserDownloadPath);
                webClient.DownloadFileCompleted += MyWebClient_DownloadFileCompleted;
            }
            

            progressText.AppendText("- Starting to download Uninstaller.exe" + Environment.NewLine);
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFileAsync(uninstallerDownloadLink, uninstallerDownloadPath);
                webClient.DownloadFileCompleted += MyWebClient_DownloadFileCompleted;
            }
                

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
            progressBar1.Value = 75;


            progressText.AppendText("- Adding registry values to HKLM\\SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\shell\\open\\command" + Environment.NewLine);
            InstallerClass.StartMenuInternetShell(textBox1.Text);
            progressBar1.Value = 80;

            InstallationCompleted form2 = new InstallationCompleted();
            form2.Show();
        }

        private void MyWebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            progressBar1.Value += 10;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            
        }
    }
}
