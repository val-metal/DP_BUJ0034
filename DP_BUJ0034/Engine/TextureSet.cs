using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Engine
{
    public class TextureSet
    {
        public List<string> player;
        public List<string> routes;
        public List<string> ends;
        public string background { get; set; }
        public bool backgroudIsImage;
        protected TextureSet() { player = new List<string>();routes = new List<string>();ends = new List<string>(); }

    }
}
