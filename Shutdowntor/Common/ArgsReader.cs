using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutdowntor.Common
{
    public class ArgsReader
    {
        
        #region enum
        public enum ArgOption
        {
            debug,
            hide,
            auto,
            datetime
        }
        #endregion

        #region Instance
        private static readonly object padlock = new object();
        private static ArgsReader instance = null;
        public static ArgsReader Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new ArgsReader();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        #region ArgsReader
        private static readonly string NULLSTRING = "***\t\t\tNULLARGSTRING\t\t\t***";
        private static Dictionary<string, Object> argsMap;
        private string prefix = "/";
        private char splitChar = ':';
        private string[] args;
        public Exception LastException;

        public int Read(string[] args)
        {
            if (argsMap == null)
            {
                throw new Exception("Plesae RegisterArg Before Call Read Method");
            }
            this.args = args;
            int ret = 0;
            for (int i = 0; i < args.Length; i++)
            {
                try
                {
                    SetToMap(args[i].ToLower());
                    ret++;
                }
                catch (Exception e)
                {
                    LastException = e;
                    ret = -1;
                }
            }
            return ret;
        }

        public bool CheckArg(ArgOption argOption)
        {
            bool isExist = false;
            isExist = args.Where(i => GetKeyWithOutPrefix(i.ToString()).StartsWith(argOption.ToString())).FirstOrDefault() != null;
            return isExist;
        }

        private void SetToMap(string arg)
        {
            string keyWithOutprefix;
            keyWithOutprefix = GetKeyWithOutPrefix(arg.Split(splitChar)[0]);

            if (argsMap.ContainsKey(keyWithOutprefix))
            {
                Object valueObj = argsMap[keyWithOutprefix];
                Type valueType = valueObj.GetType();
                if (valueType == typeof(bool))
                {
                    argsMap[keyWithOutprefix] = true;
                }
                else
                {
                    object value = arg.Split(splitChar)[1];
                    var converter = TypeDescriptor.GetConverter(valueType);
                    argsMap[keyWithOutprefix] = converter.ConvertFrom(value);
                }
            }
        }

        private string GetKeyWithOutPrefix(string argOption)
        {
            return argOption.ToString().ToLower().Replace(prefix, "");
        }

        private string GetKeyWithPrefix(ArgOption argOption)
        {
            return prefix + argOption.ToString().ToLower();
        }

        public void RegisterArg<T>(ArgOption argOption)
        {
            if (argsMap == null)
            {
                argsMap = new Dictionary<string, object>();
            }
            Object o;
            if (typeof(T) == typeof(string) || typeof(T) == typeof(String))
            {
                o = NULLSTRING;
            }
            else
            {
                o = (T)Activator.CreateInstance(typeof(T));
            }
            argsMap.Add(argOption.ToString().ToLower(), o);
        }

        public T GetArg<T>(ArgOption argOption)
        {
            Object obj = new object();
            argsMap.TryGetValue(argOption.ToString().ToLower(), out obj);
            if (obj == null)
                throw new Exception("Plesae RegisterArg Before Call GetArg Method");

            Type valueType = obj.GetType();
            if ((obj.GetType() == typeof(string) || obj.GetType() == typeof(String)) && typeof(T) == typeof(bool))
            {
                obj = !isNullString(obj.ToString());
            }
            return (T)obj;
        }

        public string ToWinArgs(string input)
        {
            string args = "";
            if (isNullString(input))
                return args;
            try
            {
                string[] strArray = input.Split(';');
                foreach (string s in strArray)
                {
                    string temp = s.Replace('=', ':');
                    args += prefix + temp + " ";
                }
            }
            catch
            {

            }
            return args;
        }

        public bool isNullString(string s)
        {
            return string.Compare(s, NULLSTRING) == 0;
        }
        #endregion
    }
}
