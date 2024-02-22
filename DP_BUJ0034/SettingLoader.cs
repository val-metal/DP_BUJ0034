using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DP_BUJ0034
{
    public static class SettingLoader
    {
        public static async Task<SettingSave> load()
        {
            string setting = await SecureStorage.GetAsync("setting");
            if(setting == null)
            {
                return new SettingSave(false,true);
            }
            else
            {
                return JsonSerializer.Deserialize<SettingSave>(setting);
            }
        }
        public static async Task save(SettingSave setting)
        {
            await SecureStorage.SetAsync("setting",JsonSerializer.Serialize(setting));
        }

    }
}
