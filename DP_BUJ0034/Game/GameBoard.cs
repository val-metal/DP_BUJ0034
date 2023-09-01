using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DP_BUJ0034.Game{

    public class GameBoard{

        public float height;
        public float width;
        public int num_paths { get; set; }
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

        public void generate_paths(float height, float width, int currentPath){

            this.height = width;
            this.width = height;
            start = new Points(true, false, height / 8, width / 8*4);
            end = new Points(false, true, height / 9 *8, width / 9 *5);

            Generate_dots(start, end, currentPath);
            //Generate_dots1(start, end, currentPath);

            for (int i = 0; i < path[currentPath].dot.Count - 1; i++){
                Dots point1 = path[currentPath].dot[i];
                Dots point2 = path[currentPath].dot[i + 1];

                CalculateBezier_Points(point1, point2, i,currentPath);
            }
        }

        public void Generate_dots1(Points start, Points end, int currentPath){

            path[currentPath] = new Paths();
            path[currentPath].dot.Add(new Dots(start.x, start.y));
            bool lastDot = false;
            int x = 0;
            while (lastDot != true){
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
                
                x++;
                if (newX > 0 && newX < width && newY > 0 && newY < height) { path[currentPath].dot.Add(new Dots(newX, newY)); }
                else{ 
                    break;
                }
                /*
                if (newX <= width / 8  || newX >=( width / 8) * 7 || newY>=(height/8)*7 || newY <= height / 8 || x==50){
                    lastDot = true;
                }
                else{
                    path[currentPath].dot.Add(new Dots(newX, newY));
                }
                */
            }
            path[currentPath].dot.Add(new Dots(end.x, end.y));
        }

        //Funční body pro viditelné testování
        public void Generate_dots(Points start, Points end, int currentPath){

            path[currentPath] = new Paths();
            path[currentPath].dot.Add(new Dots(start.x, start.y));

            Random random = new Random();
            // Určení směru mezi startem a koncem
            float directionX = end.x - start.x;
            float directionY = end.y - start.y;

            float length = (float)Math.Sqrt(directionX * directionX + directionY * directionY);
            directionX /= length;
            directionY /= length;

            // Přidání 14 náhodných bodů po celém úseku s větší odchylkou
            for (int i = 0; i < 15; i++){
                float randomDeviationX = (float)random.NextDouble() * 300 - 150; 
                float randomDeviationY = (float)random.NextDouble() * 300 - 150; 

                float randomX = start.x + directionX * (i + 1) * (length / 10) + randomDeviationX;
                float randomY = start.y + directionY * (i + 1) * (length / 10) + randomDeviationY;
                Dots newdot = new Dots(randomX, randomY);
                if (i == 0){
                    path[currentPath].dot.Add(newdot);
                }
                else{
                    int index = path[currentPath].dot.Count();
                    float angel=CalculateAngel(path[currentPath].dot[index - 2], path[currentPath].dot[index-1], newdot, currentPath);
                    if (angel < 60 || angel > 300){
                        i--;
                    }
                    else{
                        path[currentPath].dot.Add(newdot);
                    }
                }

            }

            path[currentPath].dot.Add(new Dots(end.x, end.y));
        }
        
          
        //Toto používám

        public float CalculateAngel(Dots pointA, Dots pointB, Dots pointC, int currentPath){
            float directionABX = pointA.x - pointB.x;
            float directionABY = pointA.y - pointB.y;
            float directionBCX = pointC.x - pointB.x;
            float directionBCY = pointC.y - pointB.y;

            float angleAB = (float)Math.Atan2(directionABY, directionABX);
            float angleBC = (float)Math.Atan2(directionBCY, directionBCX);

            float angleBetween = angleBC - angleAB;
            if (angleBetween < 0){
                angleBetween += 2 * (float)Math.PI;
            }

            float angleInDegrees = angleBetween * (180 / (float)Math.PI);
            return angleInDegrees;
        }

        //Funkční výpočet kontrolních bodů
        public void CalculateBezier_Points(Dots point1, Dots point2,int i, int currentPath){  
            Dots controlPoint1;
            Dots controlPoint2;

            //Pokud ještě neexistuje kontrolní bod
            if (i == 0){
                Dots midPoint1 = Dots.Add(point1, Dots.Multiply(Dots.Subtract(point2, point1), (float)0.333)); 
                Dots midPoint2 = Dots.Add(point1, Dots.Multiply(Dots.Subtract(point2, point1), (float)0.666));            

                Random random = new Random();
                float random1 = random.Next(10, 31);
                float random2 = random.Next(10, 31);

                Dots vector1 = Dots.Subtract(point1, midPoint1);
                Dots vector2 = Dots.Subtract(point1, midPoint2);

                Dots normalizedVector1 = Dots.Normalize(vector1);
                controlPoint1 = Dots.Add(midPoint2, Dots.Multiply(normalizedVector1, random1));

                Dots normalizedVector2 = Dots.Normalize(vector2);
                controlPoint2 = Dots.Add(midPoint2, Dots.Multiply(normalizedVector2, random2));
            }
            //první kontrolní bod se vypočte 2xpočáteční bod-poslední kontrolní bod
            //druhý již náhodně
            else{
                Random random = new Random();
                var lastItem = path[currentPath].controldots.Last();
                Dots LastControlDot = lastItem.Item2;
                controlPoint1 = Dots.Subtract(Dots.Multiply(point1, 2), LastControlDot);

                float random_distance = (float)(random.NextDouble() * 0.2 + 0.5);
                Dots midPoint = Dots.Add(point1, Dots.Multiply(Dots.Subtract(point2, point1), random_distance));  

                float randomR = random.Next(10, 21);
                                
                // Vypočítání vektoru mezi středním bodem a controlPoint1
                Dots vector = Dots.Subtract(controlPoint1, midPoint);

                // Normalizace vektoru a násobení náhodnou hodnotou r
                Dots normalizedVector = Dots.Normalize(vector);
                controlPoint2 = Dots.Add(midPoint, Dots.Multiply(normalizedVector, randomR));

            }
            path[currentPath].controldots.Add((controlPoint1, controlPoint2));
        }
    }
}
