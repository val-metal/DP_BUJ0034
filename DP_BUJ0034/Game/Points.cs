using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Game
{
    public class Points:Dots
    {
        bool isStart;
        bool isEnd;
        public bool isVisited { get; set; }
        public int df { get; set; }
        public Points(bool isStart, bool isEnd,float x,float y):base(x,y) { 
            this.isStart = isStart; 
            this.isEnd = isEnd;
        }
    }
}
