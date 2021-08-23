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
        private TextBox logTextBox;

        public Form1()
        {
            

            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form2 form2 = new Form2();
            form2.Show();
            form2.Update(); //This is necessary to make the label display correctly, don't know if I'm just stupid or if it's a compiler bug
            this.Hide();

            progressBar = form2.progressBar1;
            logTextBox = form2.logTextBox;

            
            RegistryKey FileName = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Clients\\StartMenuInternet\\BrowserChooser\\shell\\open\\command");
            if(FileName!=null)
            {
                string filePath = FileName.GetValue("") as string;
                logTextBox.AppendText("- Deleting BrowserChooser.exe" + Environment.NewLine);


                if (File.Exists(filePath)) //Check if file exists
                {
                    File.Delete(filePath);
                    progressBar.Value = 20;
                }
            }
            

            
            
            if (Registry.ClassesRoot.OpenSubKey("BrowserChooserURL")!=null) //Check if the Key BrowserChooserURL exists
            {
                logTextBox.AppendText("- Deleting HKCR\\BrowserChooserURL" + Environment.NewLine);
                Registry.ClassesRoot.DeleteSubKeyTree("BrowserChooserURL");
                progressBar.Value = 40;

            }

            if (Registry.LocalMachine.OpenSubKey("Software\\Clients\\StartMenuInternet\\BrowserChooser")!=null) //Check if the key BrowserChooser exists
            {
                logTextBox.AppendText("- Deleting HKLM\\Software\\Clients\\StartMenuInternet\\BrowserChooser" + Environment.NewLine);
                Registry.LocalMachine.DeleteSubKeyTree("Software\\Clients\\StartMenuInternet\\BrowserChooser");
                progressBar.Value = 60;

            }


            RegistryKey regApps = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\RegisteredApplications", true);
            if (Registry.GetValue(@"HKEY_LOCAL_MACHINE\\SOFTWARE\\RegisteredApplications", "Browser Chooser", null) != null) //Check if the value Browser Chooser exists
            {
                logTextBox.AppendText("- Deleting HKLM\\SOFTWARE\\RegisteredApplications\\Browser Chooser" + Environment.NewLine);
                regApps.DeleteValue("Browser Chooser");
                progressBar.Value = 80;

            }

            progressBar.Value = 100;

            Form3 form3 = new Form3();
            form3.Show();
            form2.Hide();
           



        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}