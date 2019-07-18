using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using CMusicPlayer.Internal.Types.DataStructures;
using Newtonsoft.Json;

namespace CMusicPlayer.Configuration
{
    internal static partial class Config
    {
        public const string Authentication = "authentication";
        public const string JwtToken = "jwt_token";
        public const string UserId = "user_id";
        public const string UserName = "username";
        public const string ApiEndpoint = "api_endpoint";

        private const string Path = "./Configuration/settings.json";

        private static Dictionary<string, NDictionary<string, string>> settings;

        private static readonly string DefaultSettings = File.ReadAllText("./Configuration/default_settings.json");

        private static readonly Dictionary<string, NDictionary<string, string>> DefaultJson =
            JsonConvert.DeserializeObject<Dictionary<string, NDictionary<string, string>>>(DefaultSettings);

        static Config()
        {
            if (!File.Exists(Path)) File.WriteAllText(Path, DefaultSettings);
            Refresh();
        }

        public static IReadOnlyDictionary<string, NDictionary<string, string>> Settings
        {
            get => settings;
            set => settings = (Dictionary<string, NDictionary<string, string>>) value;
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

        public static void Default(string tablename)
        {
            settings[tablename] = DefaultJson[tablename];
        }

        private static void Refresh()
        {
            settings = Deserialize();
        }

        private static Dictionary<string, NDictionary<string, string>> Deserialize()
        {
            var json = File.ReadAllText(Path);
            return JsonConvert.DeserializeObject<Dictionary<string, NDictionary<string, string>>>(json);
        }

        private static void Serialize()
        {
            var json = JsonConvert.SerializeObject(Settings, Formatting.Indented);
            Debug.WriteLine(json);
            File.WriteAllText(Path, json);
        }
    }
}