using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;

namespace Uninstaller
{
    public partial class Form1 : Form
    {
        private ProgressBar progressBar;

        public Form1()
        {
            

            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form2 form2 = new Form2();
            progressBar = form2.progressBar1;


            RegistryKey FileName = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\shell\\open\\command");
            string filePath = (string)FileName.GetValue("");

            if (File.Exists(filePath)) //Check if file exists
            {
                File.Delete(filePath);
                progressBar.Value = 20;
            }
            
            if (Registry.ClassesRoot.OpenSubKey("BrowserChooserURL")!=null) //Check if the Key BrowserChooserURL exists
            {
                Registry.ClassesRoot.DeleteSubKeyTree("BrowserChooserURL");
                progressBar.Value = 40;

            }

            if (Registry.LocalMachine.OpenSubKey("Software\\Clients\\StartMenuInternet\\BrowserChooser")!=null) //Check if the key BrowserChooser exists
            {
                Registry.LocalMachine.DeleteSubKeyTree("Software\\Clients\\StartMenuInternet\\BrowserChooser");
                progressBar.Value = 60;

            }


            RegistryKey regApps = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\RegisteredApplications", true);
            if (Registry.GetValue(@"HKEY_LOCAL_MACHINE\\SOFTWARE\\RegisteredApplications", "Browser Chooser", null) != null) //Check if the value Browser Chooser exists
            {
                regApps.DeleteValue("Browser Chooser");
                progressBar.Value = 80;

            }

            progressBar.Value = 100;


            Process.Start(new ProcessStartInfo()
            {
                Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + Application.ExecutablePath + "\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            });
            Application.Exit();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}