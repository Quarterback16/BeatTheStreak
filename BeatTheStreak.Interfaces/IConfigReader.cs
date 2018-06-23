namespace BeatTheStreak.Interfaces
{
    public interface IConfigReader
    {
        string GetSetting(string settingKey);
    }
}