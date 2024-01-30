using DP_BUJ0034.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Engine
{
    public class Player : Spritable
    {
        public Dots position { get; set; }
        public int size { get; set; }

        public Player(Dots position,int size) 
        {
            SpritePath = "DP_BUJ0034.Resources.Images.astro.png"; //TODO hardcoded
            this.position = position;
            this.size = size;
        }
        public Dots getCenter()
        {
            return new Dots(position.x+size/2,position.y+size/2);
        }
    }
}
