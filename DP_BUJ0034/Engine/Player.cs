using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Engine
{
    public class Player : Spritable
    {
        public PointF position { get; set; }
        public int size { get; set; }

        public Player(PointF position,int size) 
        {
            SpritePath = "DP_BUJ0034.Resources.Images.astro.png"; //TODO hardcoded
            this.position = position;
            this.size = size;
        }
    }
}
