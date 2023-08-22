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
        float height;
        float width;
        int num_paths;
        public Paths[] path { get; set; }
        public Points start { get; set; }
        public Points end { get; set; } 
        Points[] point;
        
        public GameBoard(float height, float width, int num_paths){
            this.height = height;
            this.width = width;
            path = new Paths[num_paths];
            this.num_paths = num_paths;
            
        }

        public void generate_paths(float height,float width)
        {
            Application.Current.MainPage.DisplayAlert("Upozornění", "Toto je ukázkové upozornění." +height+" "+width, "OK");
            start = new Points(true, false, height/8, width/8);
            end = new Points(false, true, height/8*7, width/8*7);

            Generate_dots(start, end);

            for (int i = 0; i < path[0].dot.Count-1; i++)
            {
                CalculateBezier_Points(path[0].dot[i], path[0].dot[i + 1]);
                CalculateBezier_Point1(path[0].dot[i], path[0].dot[i + 1]);
            }

        }

        public void Generate_dots(Points start, Points end)
        {
            path[0] = new Paths();
            path[0].dot.Add(new Dots(start.x, start.y));

            Random random = new Random();

            for (int i = 0; i < 20; i++)
            {
                float randomAngle = (float)random.NextDouble() * 360;
                float randomDistance = (float)random.NextDouble() *200 + 100;
                float newX = start.x + randomDistance * (float)Math.Cos(randomAngle);
                float newY = start.y + randomDistance * (float)Math.Sin(randomAngle);

                path[0].dot.Add(new Dots(newX, newY));
            }

            path[0].dot.Add(new Dots(end.x, end.y));
        }

        /*
        public void Generate_dots(Points start, Points end)
        {

            path[0] = new Paths();
            path[0].dot.Add(new Dots(start.x, start.y));

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

                path[0].dot.Add(new Dots(randomX, randomY));
            }
            path[0].dot.Add(new Dots(end.x, end.y));
        }*/
        public void CalculateBezier_Points(Dots point1,Dots point2){
            
            float dx = point1.x - point2.x;
            float dy = point1.y - point2.y;

            Dots controlPoint1 = new Dots(point1.x + dx / 16f, point1.y + dy / 16f);
            Dots controlPoint2 = new Dots(point2.x - dx / 16f, point2.y - dy / 16f);
            path[0].controldots.Add((controlPoint1,controlPoint2));
        }

        public void CalculateBezier_Point(Dots point1, Dots point2)
        {
            Random random = new Random();
            float rand_num = (float)random.NextDouble() * 60 - 30;

            float dx = point1.x - point2.x;
            float mpx = point1.x - dx;
            float dy = point1.y - point2.y;
            float mpy = point1.y - dy;
            Dots controldot1=new Dots(mpx-rand_num, mpy-rand_num);


            path[0].controldot.Add(controldot1);
        }

        public void CalculateBezier_Point1(Dots point1, Dots point2)
        {
            
            Dots midPoint = new Dots((point1.x + point2.x) / 2, (point1.y+ point2.y) / 2);

            Random random = new Random();
            float randomDistance = (float)random.NextDouble() * 60 - 30;

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
            Dots controldot1 = new Dots(dx,dy);

            path[0].controldot.Add(controldot1);
        }

        /*public void CalculateBezier_Point1(Dots point1, Dots point2)
        {
            float t = 0.5f;  // Kontrolní parametr t, můžete zvolit jinou hodnotu

            float dx = (1 - t) * point1.x + t * point2.x;
            float dy = (1 - t) * point1.y + t * point2.y;

            Dots controldot1 = new Dots(dx, dy);

            path[0].controldot.Add(controldot1);
        }
        */




    }


}
