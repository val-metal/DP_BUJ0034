using DP_BUJ0034.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Game.Generator
{
    public class PathThreeToOne : IGenerator
    {
        int state = 0;
        Paths first = new Paths();
        Points middle1;
        Points middle2;
        Dots middle1_add;
        Dots middle2_add;
        Dots middle_add;
        public Paths generatePath(Points start, Points end, float width, float height)
        {
            if (state == 0)
            {
                float x = 0;
                float y = 0;
                Random random = new Random();
                x = random.Next((int)(width / 16 * 9), (int)(width / 16 * 10));
                y = random.Next((int)(height / 9 * 2), (int)(height / 9 * 4));
                middle1 = new Points(false, false, x, y);
                middle1_add = new Dots(x + width / 16, y + height / 18);
                middle2 = new Points(false, false, x, y + height / 9 * 3);
                middle2_add = new Dots(x + width / 16, y + height / 9 * 3 - height / 18);
                middle_add = new Dots(x + width / 16 * 2, y + height / 9 * (float)1.5);

                first = GeneratorFactory.MakeGenerator(1).generatePath(start, middle1, width, height);
                first.dot.RemoveAt(first.dot.Count - 1);
                first.dot.Add(middle1_add);

                state++;
                first.noback = true;
                return first;
            }
            if (state == 1)
            {
                state++;
                return GeneratorFactory.MakeGenerator(start.df).generatePath(start, end, width, height);
            }
            else
            {
                Paths path = GeneratorFactory.MakeGenerator(1).generatePath(start, middle2, width, height);
                path.dot.RemoveAt(path.dot.Count - 1);
                path.dot.Add(middle2_add);
                path.dot.Add(middle_add);
                path.readyToConnect = true;
                state++;
                return path;
            }
        }
    }
}
