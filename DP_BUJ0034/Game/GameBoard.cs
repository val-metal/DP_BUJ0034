using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Game
{
    public class GameBoard
    {
        public float height;
        public float width;
        public int num_paths { get; set; }
        public Paths[] path { get; set; }
        public Points start { get; set; }
        public Points end { get; set; }
        Points[] point;

        public GameBoard(float height, float width, int num_paths)
        {
            this.height = height;
            this.width = width;
            path = new Paths[num_paths];
            this.num_paths = num_paths;
            


        }

        public void generate_paths(float height, float width, int currentPath)
        {
            this.height = height;
            this.width = width;
            Application.Current.MainPage.DisplayAlert("Upozornění", "Toto je ukázkové upozornění." + height + " " + width, "OK");
            start = new Points(true, false, height / 8, width / 8*4);
            end = new Points(false, true, height / 9 *8, width / 9 *5);

            Generate_dots1(start, end,currentPath);

            for (int j = 0; j < path[currentPath].dot.Count - 2; j++)
            {
                CalculateAngels(path[currentPath].dot[j], path[currentPath].dot[j + 1], path[currentPath].dot[j + 2],currentPath);

            }
            path[currentPath].angels.Add(180);

            vyhlazeni(currentPath);
            
            

        }
        public void vyhlazeni(int currentPath)
        {
            for (int i = 0; i < path[currentPath].angels.Count - 1; i++)
            {
                //CalculateBezier_Points(path[currentPath].dot[i], path[currentPath].dot[i + 1]);
                Random random = new Random();
                float randomDistance = 0;
                if (path[currentPath].angels[i] < 90)
                {
                    float centerx = (path[currentPath].dot[i].x + path[currentPath].dot[i + 1].x) / 2;
                    float centery = (path[currentPath].dot[i].y + path[currentPath].dot[i + 1].y) / 2;
                    path[currentPath].dot.Insert(i + 1, new Dots(centerx, centery));
                    path[currentPath].angels.Insert(i + 1, 180);
                }
            }
            bool positive = false;
            for (int i = 0; i < path[currentPath].dot.Count - 1; i++)
            {
                Random random = new Random();
                float randomDistance = 0;

                if (path[currentPath].angels[i] == 180)
                {
                    if (positive == true)
                    {
                        randomDistance = (float)random.NextDouble() * 60 + 20;
                        positive = false;
                    }
                    else
                    {
                        randomDistance = (float)random.NextDouble() * 60 - 80;
                        positive = true;
                    }

                }
                else
                {
                    if (positive == false)
                    {
                        randomDistance = (float)random.NextDouble() * 60 + 20;
                       // positive = false;
                    }
                    else
                    {
                        randomDistance = (float)random.NextDouble() * 60 - 80;
                        //positive = true;
                    }
                }
                /*
                else if (path[currentPath].angels[i] < 180)
                {
                    randomDistance = (float)random.NextDouble() * 60 + 20;
                    positive = false;
                }
                else
                {
                    randomDistance = (float)random.NextDouble() * 60 - 80;
                    positive = true;
                }
                */
                CalculateBezier_Point1(path[currentPath].dot[i], path[currentPath].dot[i + 1], randomDistance, currentPath);
            }
        }

        //Funční body pro viditelné testování
        public void Generate_dots1(Points start, Points end, int currentPath)
        {
            path[currentPath] = new Paths();
            path[currentPath].dot.Add(new Dots(start.x, start.y));
            bool lastDot = false;
            int x = 0;
            while (x!=50)
            {
                Random random = new Random();
                int randomSmer = (int)(random.NextDouble() * 4);
                int randomDistancex = (int)(random.NextDouble() * 8 + 10);
                int randomDistancey = (int)(random.NextDouble() * 8 + 6);

                //float[] angles = { 0, (float)Math.PI / 4, (float)Math.PI / 2, 3 * (float)Math.PI / 4, (float)Math.PI, -3 * (float)Math.PI / 4, -(float)Math.PI / 2, -(float)Math.PI / 4 };
                float[] angles = { 0, (float)Math.PI / 4, (float)Math.PI / 2, 3 * (float)Math.PI / 4, (float)Math.PI};
                
                float angle = angles[randomSmer];
                float sirka = width / randomDistancex;
                float vyska = height / randomDistancey;


                float newX = path[currentPath].dot.Last().x + (float)(sirka * Math.Cos(angle));
                float newY = path[currentPath].dot.Last().y + (float)(vyska * Math.Sin(angle));

                path[currentPath].dot.Add(new Dots(newX, newY));
                x++;
               /* if (newX == width / 8  || newX == width / 8 * 7 || newY==height/15*14 || newY == height / 15 || x==50)
                {
                    lastDot = true;
                }*/

            }                    

        
            path[currentPath].dot.Add(new Dots(end.x, end.y));
        }

        public void Generate_dots(Points start, Points end, int currentPath)
        {

            path[currentPath] = new Paths();
            path[currentPath].dot.Add(new Dots(start.x, start.y));

            Random random = new Random();
            // Určení směru mezi startem a koncem
            float directionX = end.x - start.x;
            float directionY = end.y - start.y;

            // Normování směru
            float length = (float)Math.Sqrt(directionX * directionX + directionY * directionY);
            directionX /= length;
            directionY /= length;

            // Přidání 14 náhodných bodů po celém úseku s větší odchylkou
            for (int i = 0; i < 20; i++)
            {
                // Generování náhodného odchylky
                float randomDeviationX = (float)random.NextDouble() * 200 - 100; // Náhodná hodnota v rozmezí -30 až 30
                float randomDeviationY = (float)random.NextDouble() * 200 - 100; // Náhodná hodnota v rozmezí -30 až 30

                // Výpočet souřadnic náhodného bodu s odchylkou
                float randomX = start.x + directionX * (i + 1) * (length / 15) + randomDeviationX;
                float randomY = start.y + directionY * (i + 1) * (length / 15) + randomDeviationY;

                path[currentPath].dot.Add(new Dots(randomX, randomY));
            }
            path[currentPath].dot.Add(new Dots(end.x, end.y));
        }
        
          
        //Toto používám
        public void CalculateAngels(Dots pointA, Dots pointB, Dots pointC,int currentPath)
        {
            float directionABX = pointA.x - pointB.x;
            float directionABY = pointA.y - pointB.y;
            float directionBCX = pointC.x - pointB.x;
            float directionBCY = pointC.y - pointB.y;

            float angleAB = (float)Math.Atan2(directionABY, directionABX);
            float angleBC = (float)Math.Atan2(directionBCY, directionBCX);

            float angleBetween = angleBC - angleAB;
            if (angleBetween < 0)
            {
                angleBetween += 2 * (float)Math.PI;
            }

            float angleInDegrees = angleBetween * (180 / (float)Math.PI);
            path[currentPath].angels.Add(angleInDegrees);

        }

        
        public void CalculateBezier_Point1(Dots point1, Dots point2, float randomDistance, int currentPath)
        {
            Dots midPoint = new Dots((point1.x + point2.x) / 2, (point1.y + point2.y) / 2);

            float deltaX = point2.x - point1.x;
            float deltaY = point2.y - point1.y;

            // Výpočet směrového vektoru kolmice
            float perpendicularX = -deltaY;
            float perpendicularY = deltaX;

            float length = (float)Math.Sqrt(perpendicularX * perpendicularX + perpendicularY * perpendicularY);
            perpendicularX /= length;
            perpendicularY /= length;

            // Výpočet bodu na kolmici s náhodnou délkou
            float dx = midPoint.x + randomDistance * perpendicularX;
            float dy = midPoint.y + randomDistance * perpendicularY;
            Dots controldot1 = new Dots(dx, dy);

            path[currentPath].controldot.Add(controldot1);
        }
        
        /*public void CalculateBezier_Point1(Dots point1, Dots point2)
        {
            float t = 0.5f;  // Kontrolní parametr t, můžete zvolit jinou hodnotu

            float dx = (1 - t) * point1.x + t * point2.x;
            float dy = (1 - t) * point1.y + t * point2.y;

            Dots controldot1 = new Dots(dx, dy);

            path[currentPath].controldot.Add(controldot1);
        }
        */




    }


}
