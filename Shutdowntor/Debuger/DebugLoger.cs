using Shutdowntor.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutdowntor.Debuger
{
    public class DebugLoger
    {
        #region Instance
        private static readonly object padlock = new object();
        private static DebugLoger instance = null;
        public static DebugLoger Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new DebugLoger();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        #region Methods
        private string logFloder = "./debuglog";
        public string logFile;
        public DebugLoger()
        {
            if ((bool)ArgsReader.Instance.GetArg<bool>(ArgsReader.ArgOption.debug))
            {
                if (!Directory.Exists(logFloder))
                {
                    Directory.CreateDirectory(logFloder);
                }
                logFile = $"{logFloder}{Path.DirectorySeparatorChar}" + System.DateTime.Now.ToString("yyyy_MM_dd") + ".log";
            }
        }
        public void WriteLog(string s)
        {
            if (!(bool)ArgsReader.Instance.GetArg<bool>(ArgsReader.ArgOption.debug))
            {
                return;
            }
            using (StreamWriter sw = File.AppendText(logFile))
            {
                string log = System.DateTime.Now.ToString("HH:mm:ss") + "\t" + s;
                sw.WriteLine(log);
                sw.Close();
            }
        }
        #endregion
    }
}
