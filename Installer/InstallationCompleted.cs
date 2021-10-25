using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;




namespace Installer
{
   
    public partial class InstallationCompleted : Form
    {
        
        public InstallationCompleted()
        {

            RegistryKey reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
            string productName = reg.GetValue("ProductName").ToString();


            SystemSounds.Asterisk.Play(); //Playing Windows default sound

            InitializeComponent();
            if (productName.Contains("10") || productName.Contains("11"))
            {
                label2.Text = "You'll now need to set Browser Chooser as the default browser inside Windows Settings!";
                winSettings.Text = "Open Windows Settings";
            }
            else
            {
                label2.Text = "You'll now need to set Browser Chooser as the default browser inside Control Panel!";
                winSettings.Text = "Open Control Panel";
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            if (Application.OpenForms.OfType<Form1>().Any())
                MessageBox.Show("Form is opened");
        }

        private void winSettings_Click(object sender, EventArgs e)
        {
            RegistryKey reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
            string productName = (string)reg.GetValue("ProductName");


            if (productName.Contains("10") || productName.Contains("11"))
            {
                Process.Start("ms-settings:defaultapps");
            }
            else
            {
                Process.Start("control.exe", "/name Microsoft.DefaultPrograms");
            }
            



        }
    }
}
