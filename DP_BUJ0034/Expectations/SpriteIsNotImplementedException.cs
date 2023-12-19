using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Expectations
{
    public class SpriteIsNotFoundException : Exception
    {
        public SpriteIsNotFoundException() { }
        public SpriteIsNotFoundException(string message) : base(message) { }
        public SpriteIsNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
