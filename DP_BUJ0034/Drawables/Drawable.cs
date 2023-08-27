using DP_BUJ0034.Game;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
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

            if (gameBoard != null)
            {
                DrawBorder(canvas, dirtyRect);
                DrawStardEnd(canvas);
                
                for (int currentPath = 0; currentPath < gameBoard.num_paths; currentPath++)
                {
                    DrawDots(canvas, currentPath);
                    //Modrá
                    //DrawCurveTo(canvas,currentPath);
                    //Červená
                    DrawQuadTo(canvas, currentPath);
                    //Zelená
                    DrawLineTo(canvas, currentPath);
                    
                }



            }

        }
        public void DrawBorder(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeColor = Colors.White;
            canvas.StrokeSize = 4;
            canvas.StrokeLineJoin = LineJoin.Round;
            float left = dirtyRect.Left;
            float top = dirtyRect.Top;
            float right = dirtyRect.Right;
            float bottom = dirtyRect.Bottom;
            Console.WriteLine(right + " " + bottom);
            canvas.DrawRectangle(left, top, right, bottom);

        }
        public void DrawStardEnd(ICanvas canvas)
        {
            canvas.StrokeColor = Colors.LightYellow;
            canvas.StrokeSize = 4;
            //Application.Current.MainPage.DisplayAlert("Upozornění", "Toto je ukázkové upozornění." + gameBoard.start.x + " " + gameBoard.start.y, "OK");
            canvas.DrawCircle(gameBoard.start.x, gameBoard.start.y, 5);
            canvas.StrokeColor = Colors.LightGreen;
            canvas.StrokeSize = 4;
            canvas.DrawCircle(gameBoard.end.x, gameBoard.end.y, 5);
        }
        public void DrawDots(ICanvas canvas, int currentPath)
        {
            for(int i = 0;i< gameBoard.path[currentPath].dot.Count-1;i++)
            {
                canvas.StrokeColor = Colors.Red;
                canvas.StrokeSize = 1;
                canvas.DrawCircle(gameBoard.path[currentPath].dot[i].x, gameBoard.path[currentPath].dot[i].y, i*2);
            }
            
            
            
        }
        public void DrawCurveTo(ICanvas canvas, int currentPath)
        {
            PathF path_draw_Curve = new PathF();
            path_draw_Curve.MoveTo(gameBoard.start.x, gameBoard.start.y);

            //Application.Current.MainPage.DisplayAlert("Upozornění", "Toto je ukázkové upozornění." + gameBoard.path[currentPath].dot.Count, "OK");
            for (int i = 0; i < gameBoard.path[currentPath].controldots.Count; i++)
            {
                path_draw_Curve.CurveTo(gameBoard.path[currentPath].controldots[i].Item1.x, gameBoard.path[currentPath].controldots[i].Item1.y, gameBoard.path[currentPath].controldots[i].Item2.x, gameBoard.path[currentPath].controldots[i].Item2.y, gameBoard.path[currentPath].dot[i].x, gameBoard.path[currentPath].dot[i].y);
                //path_draw.QuadTo(gameBoard.controldot[i].x, gameBoard.controldot[i].y, gameBoard.path[currentPath].dot[i].x, gameBoard.path[currentPath].dot[i].y);
            }
            canvas.StrokeColor = Colors.Blue;
            canvas.StrokeSize = 4;
            canvas.StrokeLineJoin = LineJoin.Round;
            canvas.DrawPath(path_draw_Curve);
        }

        public void DrawQuadTo(ICanvas canvas, int currentPath)
        {

            PathF path_draw = new PathF();
            path_draw.MoveTo(gameBoard.start.x, gameBoard.start.y);

            //Application.Current.MainPage.DisplayAlert("Upozornění", "Toto je ukázkové upozornění." + gameBoard.path[currentPath].dot.Count, "OK");
            for (int i = 0; i < gameBoard.path[currentPath].controldot.Count; i++)
            {
                //path_draw.CurveTo(gameBoard.controldots[i].Item1.x, gameBoard.controldots[i].Item1.y, gameBoard.controldots[i].Item2.x, gameBoard.controldots[i].Item2.y, gameBoard.path[currentPath].dot[i].x, gameBoard.path[currentPath].dot[i].y);
                path_draw.QuadTo(gameBoard.path[currentPath].controldot[i].x, gameBoard.path[currentPath].controldot[i].y, gameBoard.path[currentPath].dot[i + 1].x, gameBoard.path[currentPath].dot[i + 1].y);
            }
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 2;
            canvas.StrokeLineJoin = LineJoin.Round;
            canvas.DrawPath(path_draw);
        }
        /*public void DrawQuadTo(ICanvas canvas, int currentPath)
        {
            PathF path_draw = new PathF();
            path_draw.MoveTo(gameBoard.start.x, gameBoard.start.y);

            //Application.Current.MainPage.DisplayAlert("Upozornění", "Toto je ukázkové upozornění." + gameBoard.path[currentPath].dot.Count, "OK");
            for (int i = 0; i < gameBoard.path[currentPath].controldot.Count; i++)
            {
                //path_draw.CurveTo(gameBoard.controldots[i].Item1.x, gameBoard.controldots[i].Item1.y, gameBoard.controldots[i].Item2.x, gameBoard.controldots[i].Item2.y, gameBoard.path[currentPath].dot[i].x, gameBoard.path[currentPath].dot[i].y);
                path_draw.QuadTo(gameBoard.path[currentPath].controldot[i].x, gameBoard.path[currentPath].controldot[i].y, gameBoard.path[currentPath].dot[i + 1].x, gameBoard.path[currentPath].dot[i + 1].y);
            }
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 2;
            canvas.StrokeLineJoin = LineJoin.Round;
            canvas.DrawPath(path_draw);
        }*/
        public void DrawLineTo(ICanvas canvas, int currentPath)
        {
            PathF path_draw_without_bezier = new PathF();
            path_draw_without_bezier.MoveTo(gameBoard.start.x, gameBoard.start.y);
            for (int i = 0; i < gameBoard.path[currentPath].dot.Count; i++)
            {
                path_draw_without_bezier.LineTo(gameBoard.path[currentPath].dot[i].x, gameBoard.path[currentPath].dot[i].y);

            }
            canvas.StrokeColor = Colors.Green;
            canvas.StrokeSize = 1;
            canvas.StrokeLineJoin = LineJoin.Round;
            canvas.DrawPath(path_draw_without_bezier);
        }

    }
}
