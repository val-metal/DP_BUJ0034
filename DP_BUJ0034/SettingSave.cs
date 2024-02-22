using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034
{
    public class SettingSave
    {
        public bool musicMute { get; set; }
        public bool moveView { get; set; }
        public SettingSave(bool musicMute, bool moveView) { 

            this.musicMute = musicMute;
            this.moveView = moveView;

        }
    }
}
