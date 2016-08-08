using System.Linq;
using Memorize.Core.Services;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Memorize.WPF.Services
{
    public class WpfSettingsService : ISettingsService
    {
        private const string BasePath = @"SOFTWARE\Skycoder42\Memorize";

        private RegistryKey _mainKey;

        public WpfSettingsService()
        {
            this._mainKey = Registry.CurrentUser.CreateSubKey(BasePath);
        }

        public void Save<TValue>(string key, TValue value)
        {
            this._mainKey?.SetValue(key, JsonConvert.SerializeObject(value));
        }

        public TValue Load<TValue>(string key, TValue defaultValue = default(TValue))
        {
            try {
                var value = this._mainKey?.GetValue(key, null) as string;
                if (value != null)
                    return JsonConvert.DeserializeObject<TValue>(value);
            } catch { }

            return defaultValue;
        }

        public bool Contains(string key)
        {
            return this._mainKey?.GetValueNames().Contains(key) ?? false;
        }

        public void Remove(string key)
        {
            this._mainKey?.DeleteValue(key);
        }

        public void Reset()
        {
            this._mainKey = null;
            Registry.CurrentUser.DeleteSubKeyTree(BasePath);
            this._mainKey = Registry.CurrentUser.CreateSubKey(BasePath);
        }
    }
}
