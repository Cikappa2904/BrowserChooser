using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Installer
{
    public partial class NetCoreInstaller : Form
    {
        string dotnetPath = System.IO.Path.GetTempPath() + "\\dotnet-runtime-3.1.20-win-x64.exe";
        Form1 downloadForm = new Form1();


        public NetCoreInstaller()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Uri downloadLink = new System.Uri("https://download.visualstudio.microsoft.com/download/pr/8f1a8283-54b1-46d0-96c3-02949986baba/5d1b2bf23eb9addb9a372f32f6992b25/dotnet-runtime-3.1.20-win-x64.exe");

            //InstallerClass.Download(downloadLink, dotnetPath);
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFileAsync(downloadLink, dotnetPath);
            myWebClient.DownloadProgressChanged += MyWebClient_DownloadProgressChanged;
            myWebClient.DownloadFileCompleted += MyWebClient_DownloadFileCompleted; //Event Handler to check if download has completed
  
        }

        private void MyWebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MyWebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.Close();
            Process dotnetInstaller = new Process();
            dotnetInstaller.StartInfo.FileName = dotnetPath;
            dotnetInstaller.Start();
            dotnetInstaller.WaitForExit();
            File.Delete(dotnetPath);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
