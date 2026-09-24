using Shutdowntor.Common;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Shutdowntor
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                using (Mutex mutex = new Mutex(false, "Global\\" + UIControl.SingleApplicationController.AppGuid))
                {
                    if (!mutex.WaitOne(0, false))
                    {
                        UIControl.SingleApplicationController.Instance.ShowRunningApp();
                        return;
                    }

                    ArgsReader.Instance.RegisterArg<bool>(ArgsReader.ArgOption.debug);
                    ArgsReader.Instance.RegisterArg<string>(ArgsReader.ArgOption.datetime);
                    ArgsReader.Instance.RegisterArg<bool>(ArgsReader.ArgOption.hide);
                    ArgsReader.Instance.RegisterArg<string>(ArgsReader.ArgOption.auto);
                    ArgsReader.Instance.Read(args);

                    var options = StartupOptions.Parse(args);

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Main(options.AutoStart, options.ActionToken, options.TargetDateTime, options.HasDateTime));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
