using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutdowntor.Common
{
    public static class Global
    {
        public const string APP_NAME = "Shutdowntor";
        public const string APP_VER = "V1.8";
        public const string DateTimeFormat = "yyyyMMddHHmmss";
        public static string ApplicationNameNVer { get { return $"{Global.APP_NAME}[{ Global.APP_VER}]"; } }
        public static object GetInstanceByClassName(string strFullyQualifiedName)
        {
            Type t = Type.GetType(strFullyQualifiedName);
            return Activator.CreateInstance(t);
        }
    }
}
