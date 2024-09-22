using Microsoft.Win32.TaskScheduler;
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

            //TODO: delete the folder with a scheduled task

            Process.Start(new ProcessStartInfo()
            {
                Arguments = "/C timeout /t 3 & rmdir /s /q \"" + fileName + "\"",
                //WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe",
                UseShellExecute= true,
            });

            // Create a new TaskService instance
            using (TaskService ts = new TaskService())
            {
                // Create a new task definition and assign properties
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "My Task that runs after 1 minute";

                // Define a new action for the task (e.g., a console application or script)
                td.Actions.Add(new ExecAction("cmd.exe", "-c rmdir /q /s \"" + fileName + "\"", null));

                // Set a trigger to start the task 1 minute from now
                td.Triggers.Add(new TimeTrigger { StartBoundary = DateTime.Now.AddSeconds(4) });

                // Register the task in the root folder
                ts.RootFolder.RegisterTaskDefinition("tempsask", td);
            }

            Application.Exit();
        }
    }
}
