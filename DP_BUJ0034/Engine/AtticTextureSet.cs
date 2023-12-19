using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Engine
{
    public class AtticTextureSet : TextureSet
    {
        public AtticTextureSet() {
            player.Add("DP_BUJ0034.Resources.Images.Attic.start.png");
            routes.Add("DP_BUJ0034.Resources.Images.Attic.path1.png");
            ends.Add("DP_BUJ0034.Resources.Images.Attic.end1.png");
            background = "DP_BUJ0034.Resources.Images.Attic.border.png";
            backgroudIsImage = false;
        }
    }
}
