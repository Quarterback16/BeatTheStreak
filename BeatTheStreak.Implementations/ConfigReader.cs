using BeatTheStreak.Interfaces;
using System.Configuration;

namespace BeatTheStreak.Implementations
{
    public class ConfigReader : IConfigReader
    {
        public string GetSetting(string settingKey)
        {
            var setting = ConfigurationManager.AppSettings[settingKey];
            return setting;
        }
    }
}