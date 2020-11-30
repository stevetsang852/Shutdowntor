using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutdowntor.Common
{
    public static class Global
    {
        public static string APP_NAME = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        public static Version APP_VER = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        public const string DateTimeFormat = "yyyyMMddHHmmss";
        public static string ApplicationNameNVer { get { return $"{Global.APP_NAME}[{ Global.APP_VER}]"; } }
        public static object GetInstanceByClassName(string strFullyQualifiedName)
        {
            Type t = Type.GetType(strFullyQualifiedName);
            return Activator.CreateInstance(t);
        }
    }
}
