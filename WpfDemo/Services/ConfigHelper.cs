using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WpfDemo.Services
{
    public class AppConfig
    {
        public string AppConfigItem1 { get; set; } = "AppConfigItem1";
        public string AppConfigItem2 { get; set; } = "AppConfigItem2";
    }

    public static class ConfigHelper
    {
        //漫游用户路径
        private readonly static string configPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "WPFDemo",
            "config.json"
        );

        public static AppConfig LoadConfig()
        {
            if (!File.Exists(configPath))
                return new AppConfig();

            var json = File.ReadAllText(configPath);
            return JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
        }

        public static void SaveConfig(AppConfig? config)
        {
            if (config == null)
            {
                return;
            }
            Directory.CreateDirectory(Path.GetDirectoryName(configPath)!);
            var json = JsonSerializer.Serialize(
                config,
                new JsonSerializerOptions { WriteIndented = true }
            );
            File.WriteAllText(configPath, json);
        }
    }
}
