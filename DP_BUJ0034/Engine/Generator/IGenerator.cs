using DP_BUJ0034.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Engine.Generator
{
    public interface IGenerator
    {
        public abstract Paths generatePath(Points start, Points end, float width, float height);
    }
}
