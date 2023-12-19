using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Engine
{
    public class SteamMountainTextureSet :TextureSet
    {
        public SteamMountainTextureSet() {
            player.Add("DP_BUJ0034.Resources.Images.SteamMountains.ballon64x64.png");
            player.Add("DP_BUJ0034.Resources.Images.SteamMountains.ballon2.png");
            player.Add("DP_BUJ0034.Resources.Images.SteamMountains.ballon3.png");
            routes.Add("DP_BUJ0034.Resources.Images.SteamMountains.clouds64x.png");
            routes.Add("DP_BUJ0034.Resources.Images.SteamMountains.cloudtexture2.png");
            routes.Add("DP_BUJ0034.Resources.Images.SteamMountains.cloudtexture3.png");
            ends.Add("DP_BUJ0034.Resources.Images.SteamMountains.tower3.png");
            ends.Add("DP_BUJ0034.Resources.Images.SteamMountains.tower2.png");
            ends.Add("DP_BUJ0034.Resources.Images.SteamMountains.tower1.png");
            background = "DP_BUJ0034.Resources.Images.SteamMountains.mountains1280x720_single.png";
            backgroudIsImage = true;
        }
    }
}
