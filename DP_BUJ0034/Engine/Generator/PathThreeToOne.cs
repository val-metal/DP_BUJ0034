using DP_BUJ0034.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Engine.Generator
{
    public class PathThreeToOne : IGenerator
    {
        int state = 0;
        Paths first=new Paths();
        Points middle;
        public Paths generatePath(Points start, Points end, float width, float height)
        {
            if (state == 0)
            {
                float x = 0;
                float y = 0;
                Random random = new Random();
                x = (float)random.Next((int)(width / 16 * 10), (int)(width / 16 * 12));
                y = (float)random.Next((int)(height / 9 * 3), (int)(height / 9 * 6));
                middle = new Points(false, false, x, y);
                first = GeneratorFactory.MakeGenerator(1).generatePath(start,middle,width,height);
                state++;
                first.noback= true;
                return first;
            }
            if (state == 1)
            {
                state++;
                return GeneratorFactory.MakeGenerator(start.df).generatePath(start, end, width, height);
            }
            else
            {
                Paths path = GeneratorFactory.MakeGenerator(1).generatePath(start, middle, width, height);
                path.readyToConnect = true;
                state++;
                return path;
            }
            

            //start[0] = new Points(true, false, width / 16 + 15, height / 4);
            //start[1] = new Points(true, false, width / 16 + 15, height / 4 * 2);
            //start[2] = new Points(true, false, width / 16 + 15, height / 4 * 3);
            //end[0] = new Points(false, true, width / 16 * 15, height / 2);


            //Generate_dot_for_path_1star(start[0], middle, 0,true);
            //Generate_dot_for_path_1star(start[2], middle, 2,true);



        }
    }
}
