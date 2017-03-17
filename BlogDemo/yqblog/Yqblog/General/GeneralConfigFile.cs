using System;
using System.IO;
using Common;

namespace Yqblog.General
{
    class GeneralConfigFile : DefaultConfigFile
    {
        private static GeneralConfigInfo _configinfo;

        private static DateTime _mFileoldchange;

        static GeneralConfigFile()
        {
            _mFileoldchange = File.GetLastWriteTime(ConfigFilePath);

            try
            {
                _configinfo = (GeneralConfigInfo)DeserializeInfo(ConfigFilePath, typeof(GeneralConfigInfo));
            }
            catch
            {
                if (File.Exists(ConfigFilePath))
                {
                    _configinfo = (GeneralConfigInfo)DeserializeInfo(ConfigFilePath, typeof(GeneralConfigInfo));
                }
            }
        }

        public new static IConfigInfo ConfigInfo
        {
            get { return _configinfo; }
            set { _configinfo = (GeneralConfigInfo)value; }
        }

        public static string Filename = null;

        public new static string ConfigFilePath
        {
            get { return Filename ?? (Filename = Utils.GetMapPath(WebUtils.GetWebConfigPath())); }
        }

        public static GeneralConfigInfo LoadConfig()
        {
            ConfigInfo = LoadConfig(ref _mFileoldchange, ConfigFilePath, ConfigInfo, false);
            return ConfigInfo as GeneralConfigInfo;
        }

        public override bool SaveConfig()
        {
            return SaveConfig(ConfigFilePath, ConfigInfo);
        }
    }
}

