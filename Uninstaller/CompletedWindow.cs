using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Uninstaller
{
    public partial class CompletedWindow : Form
    {
        public CompletedWindow()
        {
            System.Media.SystemSounds.Asterisk.Play();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\"));

            //TODO: make sure to delete also the folder

            Process.Start(new ProcessStartInfo()
            {
                Arguments = "/C timeout /t 3 & rmdir /s /q \"" + fileName + "\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            });
            Application.Exit();
        }
    }
}
