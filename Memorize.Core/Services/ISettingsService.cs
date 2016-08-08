namespace Memorize.Core.Services
{
    public interface ISettingsService
    {
        void Save<TValue>(string key, TValue value);
        TValue Load<TValue>(string key, TValue defaultValue = default(TValue));
        bool Contains(string key);
        void Remove(string key);

        void Reset();
    }

    public static class DefaultSettings
    {
        public static long ReminderId
        {
            get { return CoreApp.Service<ISettingsService>().Load(nameof(ReminderId), 0); }
            set { CoreApp.Service<ISettingsService>().Save(nameof(ReminderId), value);}
        }
    }
}
