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
            Process.Start(new ProcessStartInfo() //Deletes the program after 3 seconds
            {
                Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + Application.ExecutablePath + "\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            });
            Application.Exit();
        }
    }
}
