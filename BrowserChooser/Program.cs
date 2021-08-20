using System;
using System.Diagnostics;

namespace BrowserChooser
{
    class Program
    {
        static void Main(string[] args)
        {

            if(args.Length>0) //Checking if there are command line arguments
            {
                string argument = " Shell32.dll,OpenAs_RunDLL "  + args[0]; //Creating the command line args for Rundll32.exe (args[0] is the URL of the file/link you want to open with Browser Chooser)
                Process.Start("Rundll32.exe", argument); //The actual process to run the "Open with" dialog
            }
            else
            {
                Console.WriteLine("Browser Chooser is meant to be run with a command line argument");
            }
        }
            
    }
}
