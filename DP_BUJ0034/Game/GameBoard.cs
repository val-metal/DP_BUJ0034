using DP_BUJ0034.Engine;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace DP_BUJ0034.Game{

    public class GameBoard{

        public float height;
        public float width;
        public bool playerIsLoaded { get; set; }
        public Player[] player { get; set; }
        public int num_paths { get; set; }
        public int difficulty { get; set; }
        public Paths[] path { get; set; }
        public Points[] start { get; set; }
        public Points[] end { get; set; }
        Points[] point;

        public GameBoard(float height, float width, int num_paths,int difficulty){

            this.height = height;
            this.width = width;
            path = new Paths[num_paths];
            player=new Player[num_paths];
            start=new Points[num_paths];
            end=new Points[num_paths];
            this.num_paths = num_paths;
            this.difficulty = difficulty;

        }

        public bool isAllVisited()
        {
            for(int i=0;i<end.Length; i++)
            {
                if (end[i].isVisited==false)
                {
                    return false;
                }
                    
            }
            return true;
        }

        public void generate_paths(float width, float height, int currentPath)
        {

            this.height = height;
            this.width = width;
            start[currentPath] = new Points(true, false, width / 16 + 15, height/(this.num_paths+1)*(currentPath+1));
            end[currentPath] = new Points(false, true, width / 16 * 15, height /(this.num_paths + 1) * (currentPath + 1));

            if (difficulty == 1)
            {
                Generate_dot_for_path_1star(start[currentPath], end[currentPath], currentPath);
            }
            else if(difficulty == 2)
            {
                Generate_dot_for_path_2star(start[currentPath], end[currentPath], currentPath);
            }
            else
            {
                Generate_dots1(start[currentPath], end[currentPath], currentPath);
            }

            for (int i = 0; i < path[currentPath].dot.Count - 1; i++)
            {
                Dots point1 = path[currentPath].dot[i];
                Dots point2 = path[currentPath].dot[i + 1];

                CalculateBezier_Points(point1, point2, i, currentPath);
            }
            Generate_dots_for_back_path(currentPath);
            Generate_dots_for_back_path_with_t(currentPath);
        }

        public void Try_smer(Points start, Points end, int currentPath)
        {
            path[currentPath] = new Paths();
            path[currentPath].dot.Add(new Dots(start.x, start.y));
            float[] angles = { 0, 45, 90, 135, 180, 225, 270, 315 };
            for (int i = 0; i < 8; i++)
            {
                float newX = path[currentPath].dot.Last().x + (float)(40 * Math.Cos(angles[i]));
                float newY = path[currentPath].dot.Last().y + (float)(40 * Math.Sin(angles[i]));
                Dots eventual_dot = new Dots(newX, newY);
                path[currentPath].dot.Add(eventual_dot);
            }
            path[currentPath].dot.Add(new Dots(end.x, end.y));
        }

        static void ShuffleArray<T>(T[] try_angles)
        {
            Random random = new Random();
            int n = try_angles.Length;

            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);

                // Prohodí prvky na pozicích i a j
                T temp = try_angles[i];
                try_angles[i] = try_angles[j];
                try_angles[j] = temp;
            }
        }
        public void Generate_dots_for_back_path(int currentPath)
        {
            int distance = 20;

            for (int i = 0; i < path[currentPath].dot.Count; i++)
            {
                float dx, dy;
                float dx_normalized, dy_normalized;

                if (i == 0)
                {
                    // Pro první bod použijeme směr vektoru k dalšímu bodu
                    dx = path[currentPath].dot[i + 1].x - path[currentPath].dot[i].x;
                    dy = path[currentPath].dot[i + 1].y - path[currentPath].dot[i].y;
                }
                else if (i == path[currentPath].dot.Count - 1)
                {
                    // Pro poslední bod použijeme směr vektoru od předchozího bodu
                    dx = path[currentPath].dot[i].x - path[currentPath].dot[i - 1].x;
                    dy = path[currentPath].dot[i].y - path[currentPath].dot[i - 1].y;
                }
                else
                {
                    // Pro všechny ostatní body interpolujeme směrový vektor
                    dx = path[currentPath].dot[i + 1].x - path[currentPath].dot[i - 1].x;
                    dy = path[currentPath].dot[i + 1].y - path[currentPath].dot[i - 1].y;
                }

                // Normalizace směrového vektoru
                float length = (float)Math.Sqrt(dx * dx + dy * dy);
                dx_normalized = dx / length;
                dy_normalized = dy / length;

                /*float ang = -MathF.PI / 2;
                double x_orthogonal = dx_normalized * Math.Cos(ang) - dy_normalized * Math.Sin(ang);
                double y_orthogonal = dx_normalized * Math.Sin(ang) + dy_normalized * Math.Cos(ang);
                dx_normalized = (float)x_orthogonal;
                dy_normalized = (float)y_orthogonal;
                */
                // Vytvoření bodu pro zpětnou trasu posunutého kolmo o vzdálenost "distance"
                float back_x = path[currentPath].dot[i].x - (dy_normalized * distance);
                float back_y = path[currentPath].dot[i].y + (dx_normalized * distance);

                if (i != path[currentPath].dot.Count - 1)
                {
                    var controll = path[currentPath].controldots[i];
                    float controll1x = controll.Item1.x - (dy_normalized * distance);
                    float controll1y = controll.Item1.y + (dx_normalized * distance);
                    float controll2x = controll.Item2.x - (dy_normalized * distance);
                    float controll2y = controll.Item2.y + (dx_normalized * distance);
                    path[currentPath].controlbackdot.Add((new Dots(controll1x, controll1y), new Dots(controll2x, controll2y)));
                }
                

                // Uložení bodu do back_dot
                path[currentPath].backdot.Add(new Dots(back_x, back_y));
            }
        }

        public void Generate_dots_for_back_path_with_t(int currentPath)
        {
            int distance = 20;
            for (int i = 0; i < path[currentPath].dot.Count()-1; i++)
            {
                Dots p1 = path[currentPath].dot[i];
                Dots p2 = path[currentPath].controldots[i].Item1;
                Dots p3 = path[currentPath].controldots[i].Item2;
                Dots p4 = path[currentPath].dot[i + 1];

                for (float t = 0; t <= 0.95; t = t + (float)0.05)
                {
                    Dots point = getPointAt(t, p1, p2, p3, p4);
                    Dots normal = getNormalAt(t, p1, p2, p3, p4);

                    Dots offsetPoint = new Dots(
                x: point.x + normal.x * distance,
                y: point.y + normal.y * distance);


                    path[currentPath].backdot_with_t.Add(offsetPoint);
                }
            }

        }

        public Dots getPointAt(float t, Dots p1, Dots p2, Dots p3, Dots p4)
        {
            float x = (float)Math.Pow(1 - t, 3) * p1.x +
                    3 * (float)Math.Pow(1 - t, 2) * t * p2.x +
                    3 * (1 - t) * (float)Math.Pow(t, 2) * p3.x +
                   (float)Math.Pow(t, 3) * p4.x;
            float y = (float)Math.Pow(1 - t, 3) * p1.y +
                    3 * (float)Math.Pow(1 - t, 2) * t * p2.y +
                    3 * (1 - t) * (float)Math.Pow(t, 2) * p3.y +
                    (float)Math.Pow(t, 3) * p4.y;

            Dots send = new Dots(x, y);
            return send;
        }

        public Dots getNormalAt(float t, Dots p1, Dots p2, Dots p3, Dots p4)
        {
            Dots d = getDerivateAt(t, p1, p2, p3, p4);
            float q = (float)Math.Sqrt(d.x * d.x + d.y * d.y);

            float x = -d.y / q;
            float y = d.x / q;

            Dots send = new Dots(x, y);
            return send;
        }
        public Dots getDerivateAt(float t, Dots p1, Dots p2, Dots p3, Dots p4)
        {
            float x = -3 * (float)Math.Pow(1 - t, 2) * p1.x +
            (3 * (float)Math.Pow(1 - t, 2) - 6 * (1 - t) * t) * p2.x +
            (6 * (1 - t) * t - 3 * (float)Math.Pow(t, 2)) * p3.x +
            3 * (float)Math.Pow(t, 2) * p4.x;
            float y = -3 * (float)Math.Pow(1 - t, 2) * p1.y +
                    (3 * (float)Math.Pow(1 - t, 2) - 6 * (1 - t) * t) * p2.y +
                    (6 * (1 - t) * t - 3 * (float)Math.Pow(t, 2)) * p3.y +
                    3 * (float)Math.Pow(t, 2) * p4.y;


            Dots send = new Dots(x, y);
            return send;
        }

        public void Generate_dot_for_path_1star(Points start, Points end, int currentPath)
        {
            Random random = new Random();
            path[currentPath] = new Paths();
            path[currentPath].dot.Add(new Dots(start.x, start.y));
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

                float newX = path[currentPath].dot.Last().x +offset_x;
                float newY = path[currentPath].dot.Last().y +offset_y;

                if ( newY > height / 9 && newY < height / 9 * 8 && blizko_cile==false)
                {
                    path[currentPath].dot.Add(new Dots(newX,newY));
                    
                }
                if(blizko_cile==true)
                {
                    if (newY < end.y)
                    {
                        newY = newY + random.Next(0, (int)(end.y - newY));
                    }
                    else
                    {
                        newY = newY - random.Next(0, (int)(newY - end.y));
                    }
                    path[currentPath].dot.Add(new Dots(newX, newY));

                }


                if (newX > ((width / 16) * 12)){
                    blizko_cile = true;
                }

                if(newX > ((width / 16) * 14))
                {
                    lastDot = true;
                }

            }
            path[currentPath].dot.Add(new Dots(end.x, end.y));

        }

        public void Generate_dot_for_path_2star(Points start, Points end, int currentPath)
        {
            path[currentPath] = new Paths();
            path[currentPath].dot.Add(new Dots(start.x, start.y));
            bool lastDot = false;
        }
        public void Generate_dots1(Points start, Points end, int currentPath){
            //Application.Current.MainPage.DisplayAlert("Upozornění", "SHIT" + " HEIGHT:" + height + " WIDTH" + width, "OK");
            path[currentPath] = new Paths();
            path[currentPath].dot.Add(new Dots(start.x, start.y));
            bool lastDot = false;
            int is_first = 0;
            Random random = new Random();
            float[] angles = { 0, 45, 90, 135, 180, 225, 270, 315 };
            int randomSmer;
            float maxX = 0;
            float goingbackCounter = 0;
            bool startReturning = false;
            while (lastDot != true) {


                float distance = width / 8;
                float tolerance = distance / 10;
                float random_tolerance = (float)random.NextDouble() * (2 * tolerance) - tolerance;
                float final_distance = distance + random_tolerance;
                int angels_count = angles.Length;

                if (startReturning)
                {
                    angles = new float[] { 0, 45, 90, 270, 315 };
                   
                }
                else
                {
                    angles = new float[] { 0, 45, 90, 135, 180, 225, 270, 315 };
                }

                randomSmer=random.Next(0,angles.Length);
                float newX = path[currentPath].dot.Last().x + (float)(final_distance * Math.Cos(angles[randomSmer]));
                float newY = path[currentPath].dot.Last().y + (float)(final_distance * Math.Sin(angles[randomSmer]));
                Dots eventual_dot = new Dots(newX, newY);
                int count_of_dots = path[currentPath].dot.Count();
                float is_angel_bad = 90;
                if (is_first > 0) {
                    is_angel_bad = CalculateAngel(path[currentPath].dot[count_of_dots - 2], path[currentPath].dot[count_of_dots - 1], eventual_dot, currentPath);
                }
               
                //Pokud je bod v hracím poli a nemá ostrý ůhel (rozuměj 60ˇ)
                if (newX > width / 16 && newX < width / 16 * 15 && newY > height / 9 && newY < height / 9 * 8 && is_angel_bad >= 60 && is_angel_bad <= 300)
                {
                    if (maxX < eventual_dot.x)
                    {
                        maxX = eventual_dot.x;
                        startReturning = false;
                    }
                    else if (eventual_dot.x < path[currentPath].dot.Last().x)
                    {
                        goingbackCounter += 1;
                    }
                    else if (goingbackCounter == 3)
                    {
                        goingbackCounter = 0;
                        startReturning = true;
                    }
                    path[currentPath].dot.Add(eventual_dot);
                }

                //Pokud je bod blízko cíle
                else if (newX > width / 16 * 13)
                {
                    path[currentPath].dot.Add(new Dots(end.x, end.y));
                    lastDot = true;
                    continue;
                }
                else
                {
                    
                    if (is_angel_bad >= 300)
                    {
                        is_angel_bad = is_angel_bad - 300;

                    }
                    float[] try_angles;
                    float try_angle = is_angel_bad;
                    Dots try_dot = new Dots(0, 0);
                    if (newX <= width / 16)
                    {
                        try_angles = new float[] { 315, 0, 45 };
                    }
                    else if (newX >= width / 16 * 15)
                    {
                        try_angles = new float[] { 225, 180, 135 };
                    }
                    else if (newY <= height / 9)
                    {
                        //Application.Current.MainPage.DisplayAlert("Upozornění", "SHIT" + " HEIGHT:" + height + " WIDTH" + width, "OK");
                        try_angles = new float[] { 45, 90, 135 };
                    }
                    else if(newY >=( height/ 9 * 8))
                    {
                       // Application.Current.MainPage.DisplayAlert("Upozornění", "SHIT" + " HEIGHT2222:" + height + " WIDTH" + width, "OK");
                        try_angles = new float[] { 315, 270, 225 };
                    }
                    else { try_angles = angles; }
                    bool found = false;
                    ShuffleArray(try_angles);
                    for (int j = 0; j < try_angles.Length; j++)
                    {
                         float try_newX = path[currentPath].dot.Last().x + (float)(final_distance * Math.Cos(try_angles[j]));
                         float try_newY = path[currentPath].dot.Last().y + (float)(final_distance * Math.Sin(try_angles[j]));
                        try_dot = new Dots(try_newX, try_newY);
                        if (is_first > 0)
                        {
                            try_angle = CalculateAngel(path[currentPath].dot[count_of_dots - 2], path[currentPath].dot[count_of_dots - 1], try_dot, currentPath);
                        }

                        if (try_dot.x > width / 16 && try_dot.x < width / 16 * 15 && try_dot.y > height / 9 && try_dot.y < (height / 9) * 8 && try_angle >= 60 && try_angle <= 300)
                         {
                            if (try_dot.x > width / 16 * 14)
                            {
                                path[currentPath].dot.Add(new Dots(end.x, end.y));
                                lastDot = true;
                                continue;
                            }
                            found = true;
                            break;
                         }
                    }
                    if (!lastDot && found)
                    {
                        if (maxX < try_dot.x)
                        {
                            maxX = try_dot.x;
                            startReturning = false;
                        }
                        else if (try_dot.x < path[currentPath].dot.Last().x)
                        {
                            goingbackCounter+=1;
                        }
                        else if (goingbackCounter == 3)
                        {
                            goingbackCounter = 0;
                            startReturning = true;
                        }
                        path[currentPath].dot.Add(try_dot);
                    }
                    if (!found && is_first==50)
                    {
                        path[currentPath].dot.Add(new Dots(end.x, end.y));
                        lastDot = true;
                        continue;
                        //path[currentPath].dot.Remove(path[currentPath].dot[path[currentPath].dot.Count - 1]);
                    }

                }
                is_first++;
            }
            
        }
        /*

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
        }*/
        
          
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
