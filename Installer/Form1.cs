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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                textBox1.Text = dialog.FileName;
            }
        }
    }
}
