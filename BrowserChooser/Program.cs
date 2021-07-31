using System;
using System.Diagnostics;

namespace BrowserChooser
{
    class Program
    {
        static void Main(string[] args)
        {
            string argument = " Shell32.dll,OpenAs_RunDLL "  + args[0] ;
            Console.WriteLine(argument);
            Process.Start("Rundll32.exe", argument);
        }
    }
}
