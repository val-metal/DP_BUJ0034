using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Game
{
    public class Paths
    {
        public List<Dots> dot { get; set; }
        //Pro CurveTo
        public List<(Dots, Dots)> controldots;
        //Pro QuadroTo
        public List<Dots> controldot;

        public Paths(){ 
            this.dot = new List<Dots>();
            this .controldots = new List<(Dots, Dots)>();
            this.controldot = new List<Dots>();
        }

    }
}
