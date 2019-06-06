using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security;
using CMusicPlayer.Internal.Types.DataStructures;
using CMusicPlayer.Properties;
using Newtonsoft.Json;
using static CMusicPlayer.Util.Constants;

namespace CMusicPlayer.Configuration
{
    internal static class Config
    {
        private static Dictionary<string, NDictionary<string, string>> settings;
        public static IReadOnlyDictionary<string, NDictionary<string, string>> Settings
        {
            get => settings;
            set => settings = (Dictionary<string, NDictionary<string, string>>) value;
        }

        private static readonly string DefaultString = File.ReadAllText("./Configuration/default_settings.json");

        private static readonly Dictionary<string, NDictionary<string, string>> DefaultJson =
            JsonConvert.DeserializeObject<Dictionary<string, NDictionary<string, string>>>(DefaultString);

        private const string Path = "./Configuration/settings.json";
        
        static Config()
        {
            if (!File.Exists(Path)) File.WriteAllText(Path, DefaultString);
            
            Refresh();
        }
        
        // Public methods

        /**
         * Creates new table if it doesn't already exist
         */
        public static void CreateNewTable(string name)
        {
            if (Settings.ContainsKey(name)) return;
            settings.Add(name, new NDictionary<string, string>());
        }

        public static void Save()
        {
            Debug.WriteLine("Persisting Settings To Disk");
            Serialize();
        }

        public static void Default(string tablename) => settings[tablename] = DefaultJson[tablename];

        private static void Refresh() => settings = Deserialize();

        private static Dictionary<string, NDictionary<string, string>> Deserialize()
        {
            var json = File.ReadAllText(Path);
            return JsonConvert.DeserializeObject<Dictionary<string, NDictionary<string, string>>>(json);
        }

        private static void Serialize()
        {
            var json = JsonConvert.SerializeObject(Settings, Formatting.Indented);
            Debug.WriteLine(json);
            File.WriteAllTextAsync(Path, json);
        }

    }
}
