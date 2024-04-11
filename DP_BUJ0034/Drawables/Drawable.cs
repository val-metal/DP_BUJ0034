using DP_BUJ0034.Game;
using Microsoft.Maui.Graphics.Skia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


using System.Reflection;
using IImage = Microsoft.Maui.Graphics.IImage;
using System.Numerics;
using Microsoft.Maui.Graphics;
using DP_BUJ0034.Engine;
using Windows.System.Update;


#if IOS || ANDROID || MACCATALYST
using Microsoft.Maui.Graphics.Platform;
#elif WINDOWS
using Microsoft.Maui.Graphics.Win2D;
#endif

namespace DP_BUJ0034.Drawables{
    public class Drawable : IDrawable{
        public float height { get; set; }
        public float width { get; set; }
        public GameBoard gameBoard { get; set; }

        public int curretPlayer { get; set; }
        public TextureProvider textureProvider { get; set; }
        public Drawable() {textureProvider=new(); }
        public void Draw(ICanvas canvas, RectF dirtyRect) {
            
            this.width = dirtyRect.Height;
            this.height = dirtyRect.Width;

            DrawBackground(canvas);
                
            if (gameBoard != null)
            {
                //DrawBorder(canvas, dirtyRect);
                DrawGameBorder(canvas, dirtyRect);

                for (int currentPath = 0; currentPath < gameBoard.num_paths; currentPath++){

                    if (curretPlayer > gameBoard.path.Length-1)
                    {
                        curretPlayer = 0;
                    }
                    if (currentPath == curretPlayer)
                    { 
                        continue;
                    }
                    DrawCurveTo(canvas,currentPath);

                }
                DrawPathHistory(canvas);
                DrawCurveTo(canvas, curretPlayer);
                for (int currentPath = 0; currentPath < gameBoard.num_paths; currentPath++)
                {
                    // DrawStardEnd(canvas);


                    // DrawControlDots(canvas, currentPath);

                    //DrawDots(canvas, currentPath);
                    DrawEnd(canvas, currentPath);
                    //Zelená
                    //DrawLineTo(canvas, currentPath);
                }
                
                DrawPlayer(canvas);
            }
            string mainDir = FileSystem.Current.AppDataDirectory;

            string filePath = Path.Combine(mainDir, "MyTest.png");
            
        }
        private void DrawPathHistory(ICanvas canvas)
        {
            canvas.FillColor=Color.FromArgb("#a83232");
            float hop = gameBoard.player[1].size / 2;
            
            foreach (Dots d in gameBoard.playerHistory)
            {
                
                canvas.FillCircle(d.x, d.y, hop);
            }
        }
        private void DrawBackground(ICanvas canvas)
        {
            if (textureProvider.isBackgroundTexture())
            {
                ImagePaint imagePaint = new ImagePaint
                {
                    Image = textureProvider.getBackgroundTexture().Downsize(100)
                };
                canvas.SetFillPaint(imagePaint, RectF.Zero);

                canvas.FillRectangle(0, 0, height, width);
            }
            else {
                canvas.DrawImage(textureProvider.getBackgroundTexture(), 0,0,height,width);
            }
        }
        private void DrawPlayer(ICanvas canvas)
        {
                for (int i = 0; i < gameBoard.player.Length; i++)
                {
                    //canvas.DrawRectangle(gameBoard.player[i].position.x - gameBoard.player[i].size / 2, gameBoard.player[i].position.y - gameBoard.player[i].size / 2, gameBoard.player[i].size, gameBoard.player[i].size);
                    canvas.DrawImage(textureProvider.getPlayerTexture(i), gameBoard.player[i].position.x - gameBoard.player[i].size / 2, gameBoard.player[i].position.y - gameBoard.player[i].size / 2, gameBoard.player[i].size, gameBoard.player[i].size);
                }

            
        }
        private void DrawEnd(ICanvas canvas, int currentPath)
        {
            for (int i = 0; i < gameBoard.path[currentPath].dot.Count; i++)
            {
                if (i == gameBoard.path[currentPath].dot.Count() - 1)
                {
                    if (!gameBoard.path[currentPath].noback)
                    {
                        canvas.DrawImage(textureProvider.getEndsTexture(currentPath), gameBoard.path[currentPath].dot[i].x - 22.5f, gameBoard.path[currentPath].dot[i].y - 22.5f, 80, 80);
                    }
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
        public void DrawGameBorder(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeColor = Colors.White;
            canvas.StrokeSize = 1;
            canvas.DrawRectangle(height / 16, width / 9, height / 16 * 14, width / 9 * 7);
        }
     
         
        public void DrawDots(ICanvas canvas, int currentPath){

            for(int i = 0;i< gameBoard.path[currentPath].dot.Count;i++){
                if (i == 0){
                    canvas.StrokeColor = Colors.LightYellow;
                    canvas.StrokeSize = 5;
                    
                }
                else if (i == gameBoard.path[currentPath].dot.Count()-1){
                    if (!gameBoard.path[currentPath].noback)
                    {
                        canvas.DrawImage(textureProvider.getEndsTexture(currentPath), gameBoard.path[currentPath].dot[i].x - 22.5f, gameBoard.path[currentPath].dot[i].y - 22.5f, 80, 80);
                    }
                }
                else{
                    canvas.StrokeColor = Colors.CadetBlue;
                    canvas.StrokeSize = 1;
                    canvas.DrawCircle(gameBoard.path[currentPath].dot[i].x, gameBoard.path[currentPath].dot[i].y, 3);
                } 
            }                     
        }

        public void DrawControlDots(ICanvas canvas, int currentPath){

            for (int i = 0; i < gameBoard.path[currentPath].controldots.Count - 1; i++){
                if (i % 2 == 0){
                    canvas.StrokeColor = Colors.Red;
                    canvas.StrokeSize = 1;
                    canvas.DrawCircle(gameBoard.path[currentPath].controldots[i].Item1.x, gameBoard.path[currentPath].controldots[i].Item1.y, 6);

                    canvas.StrokeSize = 3;
                    canvas.FillColor = Colors.Yellow;
                }
                else{
                    canvas.StrokeColor = Colors.Blue;
                    canvas.StrokeSize = 1;
                    canvas.DrawCircle(gameBoard.path[currentPath].controldots[i].Item1.x, gameBoard.path[currentPath].controldots[i].Item1.y, 6);

                    canvas.StrokeSize = 3;
                    canvas.DrawCircle(gameBoard.path[currentPath].controldots[i].Item2.x, gameBoard.path[currentPath].controldots[i].Item2.y, 8);
                
                }       
            }
        }
        public void DrawCurveTo(ICanvas canvas, int currentPath){
            PathF path_draw_Curve = new PathF();
            PathF path_draw_gray = new PathF();
            path_draw_Curve.MoveTo(gameBoard.path[currentPath].dot[0].x, gameBoard.path[currentPath].dot[0].y);

            for (int i = 0; i < gameBoard.path[currentPath].controldots.Count; i++)
            {
                path_draw_Curve.CurveTo(
                    gameBoard.path[currentPath].controldots[i].Item1.x,
                    gameBoard.path[currentPath].controldots[i].Item1.y,
                    gameBoard.path[currentPath].controldots[i].Item2.x,
                    gameBoard.path[currentPath].controldots[i].Item2.y, 
                    gameBoard.path[currentPath].dot[i + 1].x,
                    gameBoard.path[currentPath].dot[i + 1].y);
            }
            path_draw_gray = path_draw_Curve;
            canvas.DrawPath(path_draw_gray);
            canvas.StrokeColor = Colors.Gray;
            canvas.StrokeSize = 1;
            canvas.StrokeLineJoin = LineJoin.Round;

            for (int i = gameBoard.path[currentPath].backdot_with_t.Count-1;i>=0; i--)
            {
                path_draw_Curve.LineTo(gameBoard.path[currentPath].backdot_with_t[i].x, gameBoard.path[currentPath].backdot_with_t[i].y);
            }

            
            if (!textureProvider.isRouteOfItems())
            {
                ImagePaint imagePaint = new ImagePaint
                {
                    Image = textureProvider.getRouteTexture(currentPath).Downsize(100)
                };
                path_draw_Curve.Close();

                canvas.SetFillPaint(imagePaint, RectF.Zero);
                canvas.FillPath(path_draw_Curve, WindingMode.NonZero);
                canvas.StrokeDashOffset = 5;
                //canvas.DrawPath(path_draw_gray);
            }
            else
            {
                double traveled_distance = 0;

                for (int i = 1; i < gameBoard.path[currentPath].backdot_with_t.Count; i++)
                {
                    double deltaX = gameBoard.path[currentPath].backdot_with_t[i].x - gameBoard.path[currentPath].backdot_with_t[i - 1].x;
                    double deltaY = gameBoard.path[currentPath].backdot_with_t[i].y - gameBoard.path[currentPath].backdot_with_t[i - 1].y;

                    traveled_distance += Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
                    if (traveled_distance > 70)
                    { traveled_distance = 0; canvas.DrawImage(textureProvider.getRouteTexture(currentPath), gameBoard.path[currentPath].backdot_with_t[i].x - 20, gameBoard.path[currentPath].backdot_with_t[i].y - 20, 20, 20); }

                }
                //canvas.DrawPath(path_draw_gray);

            }
        }


    }
}
