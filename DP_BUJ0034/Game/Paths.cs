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
       // public List<(Dots, Dots)> controlbackdot_with_t;
        public List<Dots> backdot_with_t { get; set; }
        public List<bool> t_path { get; set; }
        public List<float> angels;
        public bool readyToConnect { get; set; }
        public bool noback { get; set; }

        public Paths(){ 
            this.dot = new List<Dots>();
            this.controldots = new List<(Dots, Dots)>();
            this.controlbackdot = new List<(Dots, Dots)>();
            this.backdot = new List<Dots>();
            this.t_path = new List<bool>();
           // this.controlbackdot_with_t = new List<(Dots, Dots)>();
            this.backdot_with_t = new List<Dots>();
            this.angels = new List<float>();
        }

    }
}
