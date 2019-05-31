using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace CMusicPlayer.Configuration
{
    internal static class SettingsManager
    {
        private static readonly Dictionary<string, Dictionary<string, string>> WritableSettings;
        public static IReadOnlyDictionary<string, Dictionary<string, string>> Settings => WritableSettings;

        private const string Path = "./settings.json";

        static SettingsManager()
        {
            if (!File.Exists(Path)) File.WriteAllText(Path, "{}");
            WritableSettings = Deserialize();
        }

        private static Dictionary<string, Dictionary<string, string>> Deserialize()
        {
            var json = File.ReadAllText(Path);
            return JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);
        }

        /**
         * Creates new table if it doesn't already exist
         */
        public static void CreateNewTable(string name)
        {
            if (Settings.ContainsKey(name)) return;
            WritableSettings.Add(name, new Dictionary<string, string>());
        } 

        private static void Serialize()
        {
            var json = JsonConvert.SerializeObject(Settings, Formatting.Indented);
            Debug.WriteLine(json);
            File.WriteAllTextAsync(Path, json);
        }

        public static void Save()
        {
            Debug.WriteLine("Persisting Settings To Disk");
            Serialize();
        }
    }
}
