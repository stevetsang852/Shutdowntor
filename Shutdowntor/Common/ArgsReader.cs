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

        public enum autoOption
        {
            s,
            r
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

        #region Methods
        private static readonly string NULLSTRING = "***\t\t\tNULL\t\t\t***";
        private static Dictionary<string, Object> argsMap;
        private string prefix = "/";
        private char splitChar = ':';
        private string[] args;

        public int Read(string[] args)
        {
            if (argsMap == null)
            {
                throw new Exception("Plesae RegisterArg Before Call Read Method");
            }
            this.args = args;
            int Geted = 0;
            for (int i = 0; i < args.Length; i++)
            {
                try
                {
                    SetToMap(args[i].ToLower());
                    Geted++;
                }
                catch(Exception e)
                {
                    Geted = -1;
                }
            }
            return Geted;
        }

        public bool CheckArg(ArgOption argOption)
        {
            bool isExist = false;
            for (int i = 0; i < args.Length; i++)
            {
                isExist = isExist || GetKeyWithOutPrefix(args[i]).StartsWith(argOption.ToString());
            }
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
                if(valueType == typeof(bool))
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
            return argOption.Replace(prefix, "");
        }

        private string GetKeyWithPrefix(ArgOption argOption)
        {
            return prefix + argOption.ToString();
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
            argsMap.Add(argOption.ToString(), o);
        }

        public T GetArg<T>(ArgOption argOption)
        {
            Object obj = new object();
            argsMap.TryGetValue(argOption.ToString(), out obj);
            return (T)obj;
        }

        public bool isNullString(string s)
        {
            return string.Compare(s, NULLSTRING) == 0;
        }
        #endregion
    }
}
