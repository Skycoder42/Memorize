using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using Memorize.Core.Services;
using Memorize.WPF.Properties;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Memorize.WPF.Services
{
    public class WpfSettingsService : ISettingsService
    {
        private const string BasePath = @"SOFTWARE\Skycoder42\Memorize";

        public void Save<TValue>(string key, TValue value)
        {
            var entry = Registry.CurrentUser.CreateSubKey(BasePath);
            entry?.SetValue(key, JsonConvert.SerializeObject(value));
        }

        public TValue Load<TValue>(string key, TValue defaultValue = default(TValue))
        {
            try {
                var entry = Registry.CurrentUser.OpenSubKey(BasePath, false);
                var value = entry?.GetValue(key, null) as string;
                if (value != null)
                    return JsonConvert.DeserializeObject<TValue>(value);
            } catch { }

            return defaultValue;
        }

        public bool Contains(string key)
        {
            var entry = Registry.CurrentUser.OpenSubKey(BasePath, false);
            return entry?.GetValueNames().Contains(key) ?? false;
        }

        public void Remove(string key)
        {
            var entry = Registry.CurrentUser.CreateSubKey(BasePath);
            entry?.DeleteValue(key);
        }

        public void Reset()
        {
            Registry.CurrentUser.DeleteSubKeyTree(BasePath);
        }
    }
}
