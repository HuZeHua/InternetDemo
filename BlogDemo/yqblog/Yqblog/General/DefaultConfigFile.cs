using System;
using Common;

namespace Yqblog.General
{
    public class DefaultConfigFile
    {
        private static readonly object MLockHelper = new object();

        public static string ConfigFilePath { get; set; }

        public static IConfigInfo ConfigInfo { get; set; }

        protected static IConfigInfo LoadConfig(ref DateTime fileoldchange, string configFilePath, IConfigInfo configinfo)
        {
            return LoadConfig(ref fileoldchange, configFilePath, configinfo, true);
        }

        protected static IConfigInfo LoadConfig(ref DateTime fileoldchange, string configFilePath, IConfigInfo configinfo, bool checkTime)
        {
            lock (MLockHelper)
            {
                ConfigFilePath = configFilePath;
                ConfigInfo = configinfo;

                if (checkTime)
                {
                    var mFilenewchange = System.IO.File.GetLastWriteTime(configFilePath);

                    if (fileoldchange != mFilenewchange)
                    {
                        fileoldchange = mFilenewchange;
                        ConfigInfo = DeserializeInfo(configFilePath, configinfo.GetType());
                    }
                }
                else
                {   ConfigInfo = DeserializeInfo(configFilePath, configinfo.GetType());}

                return ConfigInfo;
            }
        }

        public static IConfigInfo DeserializeInfo(string configfilepath, Type configtype)
        {
            return (IConfigInfo)SerializationHelper.Load(configtype, configfilepath);
        }

        public virtual bool SaveConfig()
        {
            return true;
        }

        public bool SaveConfig(string configFilePath, IConfigInfo configinfo)
        {
            return SerializationHelper.Save(configinfo, configFilePath);
        }
    }
}
