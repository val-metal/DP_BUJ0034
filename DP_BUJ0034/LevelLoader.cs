using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DP_BUJ0034
{
    public class LevelLoader
    {
        public static async Task<LevelInfo[]> LoadAllLevels()
        {
            string levelJson = await LoadJsonData();
            LevelInfo[] levelInfos = JsonSerializer.Deserialize<LevelInfo[]>(levelJson);
            return levelInfos;
        }
        private static async Task<string> LoadJsonData()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("AllLevels.json");
            using var reader = new StreamReader(stream);

            string contents = reader.ReadToEnd();
            return contents;
        }
    }
}
