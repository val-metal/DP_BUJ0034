using System;
using System.Collections.Generic;
using System.Linq;
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

            generate_paths(height, width);
        }

        public void generate_paths(float height,float width)
        {
            Application.Current.MainPage.DisplayAlert("Upozornění", "Toto je ukázkové upozornění." +height+" "+width, "OK");
            start = new Points(true, false, height/2, width/2);
            end = new Points(false, true, height/5, width/5);
            path[0] = new Paths();
            // Požadovaná délka mezi body (můžete upravit dle potřeby)
            float desiredDistanceBetweenPoints = 1;

            // Výpočet délky úsečky
            float distance = (float)Math.Sqrt(Math.Pow(end.x - start.x, 2) + Math.Pow(end.y - start.y, 2));

            // Výpočet počtu bodů na interpolaci
            int numberOfPointsToInterpolate = (int)Math.Ceiling(distance / desiredDistanceBetweenPoints);
            Application.Current.MainPage.DisplayAlert("Upozornění", "distance"+distance + " desiredDistanceBetweenPoints " + desiredDistanceBetweenPoints+ " numberOfPointsToInterpolate"+ numberOfPointsToInterpolate, "OK");
            // Propojení bodů pomocí for smyčky
            for (float i = 0; i < numberOfPointsToInterpolate; i++)
            {
                float t = i / (numberOfPointsToInterpolate - 1);
                float x = start.x + t * (end.x- start.x);
                float y = start.y + t * (end.y - start.y);
                if (i == 0 || i == 1 || i == 2 || i == 3) {
                    Application.Current.MainPage.DisplayAlert("Upozornění", "T:" + i + ":" + t, "OK");
                }


                Dots interpolatedDots = new Dots( x, y);

                path[0].dot.Add(interpolatedDots);
            }

        }
    }

    
}
