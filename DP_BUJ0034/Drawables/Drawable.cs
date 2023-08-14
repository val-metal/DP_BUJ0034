using DP_BUJ0034.Game;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Drawables
{
    public class Drawable : IDrawable
    {
        public float height { get; set; }
        public float width { get; set; }
        public GameBoard gameBoard { get; set; }
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            this.width = dirtyRect.Height;
            this.height = dirtyRect.Width;

            if (gameBoard == null)
            {

            }
            else
            {
                canvas.StrokeColor = Colors.Green;
                canvas.StrokeSize = 4;
                float left = dirtyRect.Left;
                float top = dirtyRect.Top;
                float right = dirtyRect.Right;
                float bottom = dirtyRect.Bottom;
                Console.WriteLine(right + " " + bottom);
                canvas.DrawRectangle(left, top, right, bottom);

                canvas.StrokeColor = Colors.Red;
                canvas.StrokeSize = 4;
                Application.Current.MainPage.DisplayAlert("Upozornění", "Toto je ukázkové upozornění." + gameBoard.start.x + " " + gameBoard.start.y, "OK");
                canvas.DrawCircle(gameBoard.start.x, gameBoard.start.y, 20);
                canvas.StrokeColor = Colors.Blue;
                canvas.StrokeSize = 4;
                canvas.DrawCircle(gameBoard.end.x, gameBoard.end.y, 25);
                for (int i = 0; i < gameBoard.path[0].dot.Count; i++)
                {
                    canvas.StrokeColor = Colors.Green;
                    canvas.StrokeSize = 4;
                    canvas.DrawCircle(new Point(gameBoard.path[0].dot[i].x, gameBoard.path[0].dot[i].y), 15.0);
                }
                
                
            }
        }
    }
}
