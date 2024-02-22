using DP_BUJ0034.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Engine.Generator
{
    public class PathThreeDifficulty : IGenerator
    {
        public Paths generatePath(Points start, Points end, float width, float height)
        {
            Paths path = new Paths();
            path.dot.Add(new Dots(start.x, start.y));
            path.dot.Add(new Dots(start.x + width / 20, start.y));
            bool lastDot = false;
            int is_first = 1;
            Random random = new Random();
            float[] angles = { 0, 45, 90, 135, 180, 225, 270, 315 };
            int randomSmer;
            float maxX = 0;
            float goingbackCounter = 0;
            int back_count = 0;
            float rozdil = 0;
            bool startReturning = false;
            while (lastDot != true)
            {

                float distance = width / 8;
                float tolerance = distance / 10;
                float random_tolerance = (float)random.NextDouble() * (2 * tolerance) - tolerance;
                float final_distance = distance + random_tolerance;
                int angels_count = angles.Length;

                if (startReturning || back_count > 2)
                {
                    angles = new float[] { 0, 45, 90, 270, 315 };

                }
                else
                {
                    angles = new float[] { 0, 45, 90, 135, 180, 225, 270, 315 };
                }

                randomSmer = random.Next(0, angles.Length);
                float newX = path.dot.Last().x + (float)(final_distance * Math.Cos(angles[randomSmer]));
                float newY = path.dot.Last().y + (float)(final_distance * Math.Sin(angles[randomSmer]));
                Dots eventual_dot = new Dots(newX, newY);
                int count_of_dots = path.dot.Count();
                float is_angel_bad = 90;
                if (is_first > 0)
                {
                    is_angel_bad = CalculateAngel(path.dot[count_of_dots - 2], path.dot[count_of_dots - 1], eventual_dot);
                }

                //Pokud je bod v hracím poli a nemá ostrý ůhel (rozuměj 60ˇ)
                if (newX > width / 16 * 2 && newX < width / 16 * 13 && newY > height / 9 && newY < height / 9 * 7.5 && is_angel_bad >= 60 && is_angel_bad <= 300)
                {
                    if (maxX < eventual_dot.x)
                    {
                        maxX = eventual_dot.x;
                        startReturning = false;
                    }
                    else if (eventual_dot.x < path.dot.Last().x)
                    {
                        goingbackCounter += 1;
                    }
                    else if (goingbackCounter == 2)
                    {
                        goingbackCounter = 0;
                        startReturning = true;
                    }
                    rozdil = path.dot.Last().x - eventual_dot.x;
                    if (rozdil > (distance / 4))
                    {
                        back_count++;

                    }
                    path.dot.Add(eventual_dot);
                }

                //Pokud je bod blízko cíle
                else if (newX > width / 16 * 14)
                {
                    path.dot.Add(new Dots(end.x - width / 18, end.y));
                    path.dot.Add(new Dots(end.x, end.y));
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
                    if (newX <= width / 16 * 2)
                    {
                        try_angles = new float[] { 315, 0, 45 };
                    }
                    else if (newX >= width / 16 * 13)
                    {
                        try_angles = new float[] { 225, 180, 135 };
                    }
                    else if (newY <= height / 9)
                    {
                        //Application.Current.MainPage.DisplayAlert("Upozornění", "SHIT" + " HEIGHT:" + height + " WIDTH" + width, "OK");
                        try_angles = new float[] { 45, 90, 135 };
                    }
                    else if (newY >= (height / 9 * 8))
                    {
                        // Application.Current.MainPage.DisplayAlert("Upozornění", "SHIT" + " HEIGHT2222:" + height + " WIDTH" + width, "OK");
                        try_angles = new float[] { 315, 270, 225 };
                    }
                    else { try_angles = angles; }
                    bool found = false;
                    ShuffleArray(try_angles);
                    for (int j = 0; j < try_angles.Length; j++)
                    {
                        float try_newX = path.dot.Last().x + (float)(final_distance * Math.Cos(try_angles[j]));
                        float try_newY = path.dot.Last().y + (float)(final_distance * Math.Sin(try_angles[j]));
                        try_dot = new Dots(try_newX, try_newY);
                        if (is_first > 0)
                        {
                            try_angle = CalculateAngel(path.dot[count_of_dots - 2], path.dot[count_of_dots - 1], try_dot);
                        }

                        if (try_dot.x > width / 16 && try_dot.x < width / 16 * 13 && try_dot.y > height / 9 && try_dot.y < (height / 9) * 8 && try_angle >= 60 && try_angle <= 300)
                        {
                            if (try_dot.x > width / 16 * 14)
                            {
                                path.dot.Add(new Dots(end.x, end.y));
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
                        else if (try_dot.x < path.dot.Last().x)
                        {
                            goingbackCounter += 1;
                        }
                        else if (goingbackCounter == 2)
                        {
                            goingbackCounter = 0;
                            startReturning = true;
                        }
                        rozdil = path.dot.Last().x - eventual_dot.x;
                        if (rozdil > (distance / 4))
                        {
                            back_count++;

                        }
                        path.dot.Add(try_dot);
                    }
                    if (!found && is_first == 50)
                    {
                        path.dot.Add(new Dots(end.x, end.y));
                        lastDot = true;
                        continue;
                        //path.dot.Remove(path.dot[path.dot.Count - 1]);
                    }

                }
                is_first++;
            }
            return path;
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
        public float CalculateAngel(Dots pointA, Dots pointB, Dots pointC)
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
            return angleInDegrees;
        }
    }
}
