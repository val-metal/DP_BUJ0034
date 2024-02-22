using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Engine.Generator
{
    public static class GeneratorFactory
    {
        public static IGenerator MakeGenerator(int difficulty)
        {
            if (difficulty == 1)
            {
                return new PathOneDifficulty();

            }
            else if (difficulty == 2)
            {
                return new PathTwoDifficulty();
            }
            else if(difficulty == 3)
            {

                return new PathThreeDifficulty();
            }
            else
            {
                return new PathThreeToOne();
            }
        }
    }
}
