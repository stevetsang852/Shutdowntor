using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shutdowntor.UIControl
{
    #region enum
    public enum ShowWindowOptions
    {
        FORCEMINIMIZE = 11,
        HIDE = 0,
        MAXIMIZE = 3,
        MINIMIZE = 6,
        RESTORE = 9,
        SHOW = 5,
        SHOWDEFAULT = 10,
        SHOWMAXIMIZED = 3,
        SHOWMINIMIZED = 2,
        SHOWMINNOACTIVE = 7,
        SHOWNA = 8,
        SHOWNOACTIVATE = 4,
        SHOWNORMAL = 1
    }
    #endregion

    public class SingleApplicationController
    {
        #region DllImport
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr handle);

        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr handle, int nCmdShow);

        [DllImport("User32.dll")]
        private static extern bool IsIconic(IntPtr handle);
        #endregion

        #region Instance
        private static readonly object padlock = new object();
        private static SingleApplicationController instance = null;
        public static SingleApplicationController Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new SingleApplicationController();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        #region Single Application
        public static string CurrentExeName = GetCurrentExeName();
        public static string AppGuid = SingleInstance();
        private static IntPtr handle;

        public void ShowRunningApp()
        {
            Process current = Process.GetCurrentProcess();
            foreach (Process process in Process.GetProcesses())
            {
                if (process.Id == current.Id)
                {
                    continue;
                }

                try
                {
                    Assembly assembly = Assembly.LoadFrom(process.MainModule.FileName);

                    string processGuid = GetAssemblyGuid(assembly);
                    if (AppGuid.Equals(processGuid))
                    {
                        BringProcessToFront(process);
                        return;
                    }
                }
                catch { }
            }
        }

        static string SingleInstance()
        {
            return GetAssemblyGuid(Assembly.GetExecutingAssembly());
        }

        private static string GetAssemblyGuid(Assembly assembly)
        {
            object[] customAttribs = assembly.GetCustomAttributes(typeof(GuidAttribute), false);
            if (customAttribs.Length < 1)
            {
                return null;
            }

            return ((GuidAttribute)(customAttribs.GetValue(0))).Value.ToString();
        }

        private static string GetCurrentExeName()
        {
            string mExeName = "";
            string strLoc = Assembly.GetExecutingAssembly().Location;
            FileSystemInfo fileInfo = new FileInfo(strLoc);
            mExeName = fileInfo.Name;
            return mExeName;
        }

        private static bool IsAlreadyRunning(string exeName)
        {
            bool bCreatedNew;
            Mutex mutex = new Mutex(true, "Global\\" + exeName, out bCreatedNew);

            if (bCreatedNew)
                mutex.ReleaseMutex();
            return !bCreatedNew;
        }

        private static void KillPreviousExe()
        {
            Process[] runningProcesses = Process.GetProcesses();
            foreach (Process process in runningProcesses)
            {
                foreach (ProcessModule module in process.Modules)
                {
                    if (module.FileName.Equals(CurrentExeName))
                    {
                        process.Kill();
                    }
                    else
                    {
                    }
                }
            }
        }

        private static void BringPreviousExe()
        {
            Process[] runningProcesses = Process.GetProcesses();
            foreach (Process process in runningProcesses)
            {
                foreach (ProcessModule module in process.Modules)
                {
                    if (module.FileName.Equals(CurrentExeName))
                    {
                        handle = process.MainWindowHandle;
                        break;
                    }
                }
            }
            SetForegroundWindow(handle);
        }

        private static void BringProcessToFront(Process process)
        {
            IntPtr handle = process.MainWindowHandle;
            if (IsIconic(handle))
            {
                ShowWindow(handle, (int)ShowWindowOptions.SHOWDEFAULT);
            }
            SetForegroundWindow(handle);
        }
        #endregion
    }
}
