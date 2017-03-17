using Common;

namespace Yqblog.General
{
    public class GeneralConfigs
    {
        private static readonly object LockHelper = new object();

        private static readonly System.Timers.Timer GeneralConfigTimer = new System.Timers.Timer(15000);

        private static GeneralConfigInfo _mConfiginfo;

        static GeneralConfigs()
        {
            _mConfiginfo = GeneralConfigFile.LoadConfig();

            GeneralConfigTimer.AutoReset = true;
            GeneralConfigTimer.Enabled = true;
            GeneralConfigTimer.Elapsed += TimerElapsed;
            GeneralConfigTimer.Start();
        }

        private static void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ResetConfig();
        }

        public static void ResetConfig()
        {
            _mConfiginfo = GeneralConfigFile.LoadConfig();
        }

        public static GeneralConfigInfo GetConfig()
        {
            return _mConfiginfo;
        }

        public static bool SaveConfig(GeneralConfigInfo generalconfiginfo)
        {
            var gcf = new GeneralConfigFile();
            GeneralConfigFile.ConfigInfo = generalconfiginfo;
            return gcf.SaveConfig();
        }

        public static GeneralConfigInfo Serialiaze(GeneralConfigInfo configinfo, string configFilePath)
        {
            lock (LockHelper)
            {
                SerializationHelper.Save(configinfo, configFilePath);
            }
            return configinfo;
        }

        public static GeneralConfigInfo Deserialize(string configFilePath)
        {
            return (GeneralConfigInfo)SerializationHelper.Load(typeof(GeneralConfigInfo), configFilePath);
        }
    }
}
