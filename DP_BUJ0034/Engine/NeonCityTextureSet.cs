using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Engine
{
    public class NeonCityTextureSet : TextureSet
    {
        public NeonCityTextureSet()
        {
            player.Add("DP_BUJ0034.Resources.Images.NeonCity.player1.png");
            player.Add("DP_BUJ0034.Resources.Images.NeonCity.player2.png");
            player.Add("DP_BUJ0034.Resources.Images.NeonCity.player3.png");
            routes.Add("DP_BUJ0034.Resources.Images.NeonCity.neonpath.png");
            routes.Add("DP_BUJ0034.Resources.Images.NeonCity.neonpath2.png");
            routes.Add("DP_BUJ0034.Resources.Images.NeonCity.neonpath3.png");
            ends.Add("DP_BUJ0034.Resources.Images.NeonCity.end3.png");
            ends.Add("DP_BUJ0034.Resources.Images.NeonCity.end2.png");
            ends.Add("DP_BUJ0034.Resources.Images.NeonCity.end1.png");
            background = "DP_BUJ0034.Resources.Images.NeonCity.background_1280x720.png";
            backgroudIsImage = true;
        }
    }
}
