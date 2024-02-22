using DP_BUJ0034.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Engine.Generator
{
    public class PathOneDifficulty : IGenerator
    {
      
        public Paths generatePath(Points start, Points end, float width, float height)
        {
            Random random = new Random();
            Paths path = new Paths();
            path.dot.Add(new Dots(start.x, start.y));
            float distance_x = width / 12;
            float distance_y = height / 12;
            bool lastDot = false;
            bool blizko_cile = false;

            bool zapor;
            int offset_x;
            int offset_y;
            while (lastDot == false)
            {

                offset_x = (random.Next((int)(distance_x / 2), (int)distance_x));
                zapor = (random.Next(0, 2) == 0);

                if (zapor == true)
                {
                    offset_y = (random.Next((int)(-distance_y), (int)(-distance_y / 4)));
                }
                else
                {
                    offset_y = (random.Next((int)(distance_y / 4), (int)distance_y));
                }

                float newX = path.dot.Last().x + offset_x;
                float newY = path.dot.Last().y + offset_y;

                if (newY > height / 9 && newY < height / 9 * 7.5 && blizko_cile == false)
                {
                    path.dot.Add(new Dots(newX, newY));

                }
                if (blizko_cile == true)
                {
                    if (newY < end.y)
                    {
                        newY = newY + random.Next(0, (int)(end.y - newY));
                    }
                    else
                    {
                        newY = newY - random.Next(0, (int)(newY - end.y));
                    }
                    path.dot.Add(new Dots(newX, newY));

                }
                float borderOfGame = end.x-((width / 16) *3);
                float finalBorderOfGame = end.x-(width / 16);
                if (newX > borderOfGame)
                {
                    blizko_cile = true;
                }

                if (newX > finalBorderOfGame)
                {
                    lastDot = true;
                }



            }
         
            path.dot.Add(new Dots(end.x, end.y));
            return path;

        }

    }
}
