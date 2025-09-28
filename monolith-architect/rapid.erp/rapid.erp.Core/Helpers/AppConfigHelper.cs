using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace rapid.erp.Core.Helpers
{
    public static class AppConfigHelper
    {
        private static IConfigurationRoot? _configuration;
        private static string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

        /// <summary>
        /// Initialize once in Program.cs
        /// </summary>
        public static void Init(IConfiguration configuration)
        {
            _configuration = (IConfigurationRoot)configuration;
        }

        /// <summary>
        /// Get config value
        /// </summary>
        public static string? Get(string key)
        {
            return _configuration?[key];
        }

        /// <summary>
        /// Set value in-memory only
        /// </summary>
        public static void Set(string key, string value)
        {
            if (_configuration == null)
                throw new InvalidOperationException("AppConfigManager not initialized. Call AppConfigManager.Init() in Program.cs");

            _configuration[key] = value;
        }

        /// <summary>
        /// Persist value to appsettings.json
        /// </summary>
        public static void Persist(string key, string value)
        {
            Set(key, value);

            var json = File.ReadAllText(_filePath);
            var jsonDoc = JsonSerializer.Deserialize<Dictionary<string, object>>(json)!;

            var keys = key.Split(':');
            var current = jsonDoc;
            for (int i = 0; i < keys.Length - 1; i++)
            {
                if (!current.ContainsKey(keys[i]))
                    current[keys[i]] = new Dictionary<string, object>();

                current = (Dictionary<string, object>)current[keys[i]];
            }

            current[keys[^1]] = value;

            var updatedJson = JsonSerializer.Serialize(jsonDoc, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, updatedJson);
        }
    }
}
