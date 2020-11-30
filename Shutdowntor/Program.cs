using Shutdowntor.Command;
using Shutdowntor.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shutdowntor
{
    static class Program
    {    
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                using (Mutex mutex = new Mutex(false, "Global\\" + UIControl.SingleApplicationController.AppGuid))
                {
                    if (!mutex.WaitOne(0, false))
                    {
                        //MessageBox.Show("Program ONLY can open one", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        UIControl.SingleApplicationController.Instance.ShowRunningApp();
                        return;
                    }
                    ArgsReader.Instance.RegisterArg<bool>(ArgsReader.ArgOption.debug);
                    ArgsReader.Instance.RegisterArg<string>(ArgsReader.ArgOption.datetime);
                    ArgsReader.Instance.RegisterArg<bool>(ArgsReader.ArgOption.hide);
                    ArgsReader.Instance.RegisterArg<string>(ArgsReader.ArgOption.auto);
                    ArgsReader.Instance.Read(args);

                    string argDatetime = ArgsReader.Instance.GetArg<string>(ArgsReader.ArgOption.datetime);

                    bool autoStart = ArgsReader.Instance.CheckArg(ArgsReader.ArgOption.auto);
                    bool autoSetDateTime = ArgsReader.Instance.CheckArg(ArgsReader.ArgOption.datetime);

                    string action = ArgsReader.Instance.GetArg<string>(ArgsReader.ArgOption.auto); ;
                    DateTime targetDateTime = DateTime.Now.AddDays(1);
                    
                    if (argDatetime.Length == Global.DateTimeFormat.Length)
                    {
                        targetDateTime = DateTime.ParseExact(argDatetime, Global.DateTimeFormat, null);
                    }

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Main(autoStart, action, targetDateTime, autoSetDateTime));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
