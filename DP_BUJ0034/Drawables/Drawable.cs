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
                //Modrá
                //DrawCurveTo(canvas);
                //Červená
                DrawQuadTo(canvas);
                //Zelená
                DrawLineTo(canvas);
                
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
        public void DrawCurveTo(ICanvas canvas)
        {
            PathF path_draw_Curve = new PathF();
            path_draw_Curve.MoveTo(gameBoard.start.x, gameBoard.start.y);

            //Application.Current.MainPage.DisplayAlert("Upozornění", "Toto je ukázkové upozornění." + gameBoard.path[0].dot.Count, "OK");
            for (int i = 0; i < gameBoard.path[0].controldots.Count; i++)
            {
                path_draw_Curve.CurveTo(gameBoard.path[0].controldots[i].Item1.x, gameBoard.path[0].controldots[i].Item1.y, gameBoard.path[0].controldots[i].Item2.x, gameBoard.path[0].controldots[i].Item2.y, gameBoard.path[0].dot[i].x, gameBoard.path[0].dot[i].y);
                //path_draw.QuadTo(gameBoard.controldot[i].x, gameBoard.controldot[i].y, gameBoard.path[0].dot[i].x, gameBoard.path[0].dot[i].y);
            }
            canvas.StrokeColor = Colors.Blue;
            canvas.StrokeSize = 4;
            canvas.StrokeLineJoin = LineJoin.Round;
            canvas.DrawPath(path_draw_Curve);
        }

        public void DrawQuadTo(ICanvas canvas)
        {
            PathF path_draw = new PathF();
            path_draw.MoveTo(gameBoard.start.x, gameBoard.start.y);

            //Application.Current.MainPage.DisplayAlert("Upozornění", "Toto je ukázkové upozornění." + gameBoard.path[0].dot.Count, "OK");
            for (int i = 0; i < gameBoard.path[0].controldot.Count; i++)
            {
                //path_draw.CurveTo(gameBoard.controldots[i].Item1.x, gameBoard.controldots[i].Item1.y, gameBoard.controldots[i].Item2.x, gameBoard.controldots[i].Item2.y, gameBoard.path[0].dot[i].x, gameBoard.path[0].dot[i].y);
                path_draw.QuadTo(gameBoard.path[0].controldot[i].x, gameBoard.path[0].controldot[i].y, gameBoard.path[0].dot[i + 1].x, gameBoard.path[0].dot[i + 1].y);
            }
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 2;
            canvas.StrokeLineJoin = LineJoin.Round;
            canvas.DrawPath(path_draw);
        }
        /*public void DrawQuadTo(ICanvas canvas)
        {
            PathF path_draw = new PathF();
            path_draw.MoveTo(gameBoard.start.x, gameBoard.start.y);

            //Application.Current.MainPage.DisplayAlert("Upozornění", "Toto je ukázkové upozornění." + gameBoard.path[0].dot.Count, "OK");
            for (int i = 0; i < gameBoard.path[0].controldot.Count; i++)
            {
                //path_draw.CurveTo(gameBoard.controldots[i].Item1.x, gameBoard.controldots[i].Item1.y, gameBoard.controldots[i].Item2.x, gameBoard.controldots[i].Item2.y, gameBoard.path[0].dot[i].x, gameBoard.path[0].dot[i].y);
                path_draw.QuadTo(gameBoard.path[0].controldot[i].x, gameBoard.path[0].controldot[i].y, gameBoard.path[0].dot[i + 1].x, gameBoard.path[0].dot[i + 1].y);
            }
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 2;
            canvas.StrokeLineJoin = LineJoin.Round;
            canvas.DrawPath(path_draw);
        }*/
        public void DrawLineTo(ICanvas canvas)
        {
            PathF path_draw_without_bezier = new PathF();
            path_draw_without_bezier.MoveTo(gameBoard.start.x, gameBoard.start.y);
            for (int i = 0; i < gameBoard.path[0].dot.Count; i++)
            {
                path_draw_without_bezier.LineTo(gameBoard.path[0].dot[i].x, gameBoard.path[0].dot[i].y);

            }
            canvas.StrokeColor = Colors.Green;
            canvas.StrokeSize = 1;
            canvas.StrokeLineJoin = LineJoin.Round;
            canvas.DrawPath(path_draw_without_bezier);
        }
        
    }
}
