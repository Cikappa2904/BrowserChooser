using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Installer
{
    public partial class NetCoreInstaller : Form
    {
        public NetCoreInstaller()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string downloadPath = System.IO.Path.GetTempPath() + "\\dotnet-runtime-3.1.20-win-x64.exe";
            Uri downloadLink = new System.Uri ("https://download.visualstudio.microsoft.com/download/pr/8f1a8283-54b1-46d0-96c3-02949986baba/5d1b2bf23eb9addb9a372f32f6992b25/dotnet-runtime-3.1.20-win-x64.exe");

            InstallerClass.Download(downloadLink, downloadPath);


        }
    }
}
