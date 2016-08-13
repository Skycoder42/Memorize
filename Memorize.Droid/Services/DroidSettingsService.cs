using System;
using Android.Content;
using Memorize.Core.Services;
using Newtonsoft.Json;

namespace Memorize.Droid.Services
{
    internal class DroidSettingsService : ISettingsService
    {
        private const string SharedPreferencesKey = "memorize.droid";
        private readonly ISharedPreferences _preferences;

        public DroidSettingsService(Context context)
        {
            this._preferences = context.GetSharedPreferences(SharedPreferencesKey, FileCreationMode.Private);
        }

        public void Save<TValue>(string key, TValue value)
        {
            this._preferences.Edit()
                .PutString(key, JsonConvert.SerializeObject(value))
                .Apply();
        }

        public TValue Load<TValue>(string key, TValue defaultValue = default(TValue))
        {
            try {
                var str = this._preferences.GetString(key, null);
                if (str != null)
                    return JsonConvert.DeserializeObject<TValue>(str);
            } catch { }

            return defaultValue;
        }

        public bool Contains(string key) => this._preferences.Contains(key);

        public void Remove(string key)
        {
            this._preferences.Edit()
                .Remove(key)
                .Apply();
        }

        public void Reset()
        {
            this._preferences.Edit()
                .Clear()
                .Apply();
        }
    }
}