using DP_BUJ0034.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Engine
{
    public class Player 
    {
        public Dots position { get; set; }
        public int size { get; set; }

        public Player(Dots position,int size) 
        {
            this.position = position;
            this.size = size;
        }
        public Dots getCenter()
        {
            return new Dots(position.x+size/2,position.y+size/2);
        }
    }
}
