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
    public partial class MainWindow : Form
    {
        private ProgressBar progressBar;
        private TextBox logTextBox;

        public MainWindow()
        {
            InitializeComponent();           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ProgressWindow form2 = new ProgressWindow();
            form2.Show();
            form2.Update(); //This is necessary to make the label display correctly, don't know if I'm just stupid or if it's a compiler bug
            this.Hide();

            progressBar = form2.progressBar1;
            logTextBox = form2.logTextBox;

            logTextBox.AppendText("- Deleting BrowserChooser.exe" + Environment.NewLine);
            UninstallClass.DeleteUninstaller();
            progressBar.Value = 20;

            logTextBox.AppendText("- Deleting HKCR\\BrowserChooserURL" + Environment.NewLine);
            UninstallClass.DeleteBrowserChooserURL();
            progressBar.Value = 40;

            logTextBox.AppendText("- Deleting HKLM\\Software\\Clients\\StartMenuInternet\\BrowserChooser" + Environment.NewLine);
            UninstallClass.DeleteSMIBrowserChooser();
            progressBar.Value = 60;


            logTextBox.AppendText("- Deleting HKLM\\SOFTWARE\\RegisteredApplications\\Browser Chooser" + Environment.NewLine);
            UninstallClass.DeleteRegAppBrowserChooser();
            progressBar.Value = 80;


            logTextBox.AppendText("- Deleting HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Browser Chooser" + Environment.NewLine);
            UninstallClass.DeleteUninstallRegBrowserChooser();
            progressBar.Value = 100;

            CompletedWindow form3 = new CompletedWindow();
            form3.Show();
            form2.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}