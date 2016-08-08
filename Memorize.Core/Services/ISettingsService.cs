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
}
