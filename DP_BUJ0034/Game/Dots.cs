using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Game
{
    public class Dots
    {
       public float x { get; set; }
       public float y { get; set; }

        public Dots(float x, float y) { 
            this.x = x;
            this.y = y;
        }
        public static Dots Subtract(Dots a, Dots b)
        {
            return new Dots(a.x - b.x, a.y - b.y);
        }

        public static float EuclidDistance(Dots a, Dots b)
        {
            float deltaX = a.x - b.x;
            float deltaY = a.y - b.y;

            return MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }


        public static Dots Multiply(Dots dot, float scalar)
        {
            return new Dots(dot.x * scalar, dot.y * scalar);
        }

        public Dots Normalized()
        {
            float length = (float)Math.Sqrt(x * x + y * y);
            return new Dots(x / length, y / length);
        }

        public static Dots Normalize(Dots vector)
        {
            float length = (float)Math.Sqrt(vector.x * vector.x + vector.y * vector.y); // Pokud má Dots složky X a Y
            if (length > 0)
            {
                float invLength = (float)1/ length;
                return new Dots(vector.x * invLength, vector.y * invLength); // Normalizovaný vektor
            }
            else
            {
                return new Dots(0, 0); // Pokud vektor má délku 0, vrátí nulový vektor
            }
        }
        public static Dots Add(Dots a, Dots b)
        {
            return new Dots(a.x + b.x, a.y + b.y);
        }

        public float Length()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }

       
    }

}
