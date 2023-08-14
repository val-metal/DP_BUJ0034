using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Game
{
    public class Points
    {
        bool isStart;
        bool isEnd;
        public float x { get;set; }
        public float y { get;set; }
        public Points(bool isStart, bool isEnd,float x,float y) { 
            this.isStart = isStart; 
            this.isEnd = isEnd;
            this.x = x;
            this.y = y;
        }
    }
}
