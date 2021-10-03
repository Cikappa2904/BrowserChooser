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
    public partial class InstallationProgress : Form
    {
        public InstallationProgress()
        {
            InitializeComponent();
        }

        //public void SetProgress(int progress)
        //{
        //    progressBar1.Value = progress;
        //}
    }
}
