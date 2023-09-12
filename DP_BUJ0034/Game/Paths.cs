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
        //Pro uzavření cesty vykreslení
        public List<(Dots, Dots)> controlbackdot;
        public List<Dots> backdot { get; set; }
        public List<float> angels;

        public Paths(){ 
            this.dot = new List<Dots>();
            this.controldots = new List<(Dots, Dots)>();
            this.controlbackdot = new List<(Dots, Dots)>();
            this.backdot = new List<Dots>();
            this.angels = new List<float>();
        }

    }
}
