﻿using DP_BUJ0034.Game;
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
        public TextureProvider textureProvider { get; set; }
        public Drawable() { textureProvider = new(); }
        IImage imageback;
        public void Draw(ICanvas canvas, RectF dirtyRect) {
            this.width = dirtyRect.Height;
            this.height = dirtyRect.Width;
            // SkiaBitmapExportContext skiaBitmapExportContext = new((int)width, (int)height, 1);
            // ICanvas canvas = skiaBitmapExportContext.Canvas;

            DrawBackground(canvas);
            //canvas.DrawRectangle(height / 16, width / 9, height / 16 * 14, width / 9 * 7);
            //Size playerpos =new Size(height/16, width-((width / 9)*2))
                
            if (gameBoard != null)
            {
                DrawBorder(canvas, dirtyRect);
                DrawGameBorder(canvas, dirtyRect);

                for (int currentPath = 0; currentPath < gameBoard.num_paths; currentPath++){
                   // DrawStardEnd(canvas);

                    
                   // DrawControlDots(canvas, currentPath);
                    //Žlutá
                    DrawCurveTo(canvas,currentPath);
                    //Zelená
                    //DrawLineTo(canvas, currentPath);
                }
                for (int currentPath = 0; currentPath < gameBoard.num_paths; currentPath++)
                {
                    // DrawStardEnd(canvas);


                    // DrawControlDots(canvas, currentPath);
                    //Žlutá
                    DrawDots(canvas, currentPath);
                    //Zelená
                    //DrawLineTo(canvas, currentPath);
                }
                
                DrawPlayer(canvas);
            }
            string mainDir = FileSystem.Current.AppDataDirectory;

            string filePath = Path.Combine(mainDir, "MyTest.png");
            // Save the image as a PNG file  
            //Application.Current.MainPage.DisplayAlert("Upozornění", "Toto je ukázkové upozornění." + mainDir, "OK");
            //skiaBitmapExportContext.WriteToStream
            //canvass = canvas;
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
                for (int i = 0; i < gameBoard.num_paths; i++)
                {
                    canvas.DrawRectangle(gameBoard.player[i].position.X - gameBoard.player[i].size / 2, gameBoard.player[i].position.Y - gameBoard.player[i].size / 2, gameBoard.player[i].size, gameBoard.player[i].size);
                    canvas.DrawImage(textureProvider.getPlayerTexture(i), gameBoard.player[i].position.X - gameBoard.player[i].size / 2, gameBoard.player[i].position.Y - gameBoard.player[i].size / 2, gameBoard.player[i].size, gameBoard.player[i].size);
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
        //public void DrawStardEnd(ICanvas canvas){

        //    canvas.StrokeColor = Colors.LightYellow;
        //    canvas.StrokeSize = 4;
        //    //Application.Current.MainPage.DisplayAlert("Upozornění", "Toto je ukázkové upozornění." + gameBoard.start.x + " " + gameBoard.start.y, "OK");
        //    canvas.DrawCircle(gameBoard.start.x, gameBoard.start.y, 5);
        //    canvas.StrokeColor = Colors.LightGreen;
        //    canvas.StrokeSize = 4;
        //    canvas.DrawCircle(gameBoard.end.x, gameBoard.end.y, 5);
        //}
        public void DrawDots(ICanvas canvas, int currentPath){

            for(int i = 0;i< gameBoard.path[currentPath].dot.Count;i++){
                if (i == 0){
                    canvas.StrokeColor = Colors.LightYellow;
                    canvas.StrokeSize = 5;
                    
                    //canvas.DrawCircle(gameBoard.path[currentPath].dot[i].x, gameBoard.path[currentPath].dot[i].y, 5);
                }
                else if (i == gameBoard.path[currentPath].dot.Count()-1){
                   
                    canvas.DrawImage(textureProvider.getEndsTexture(currentPath), gameBoard.path[currentPath].dot[i].x, gameBoard.path[currentPath].dot[i].y, 45, 45);
// canvas.DrawCircle(gameBoard.path[currentPath].dot[i].x, gameBoard.path[currentPath].dot[i].y, 10);
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
                    //canvas.DrawCircle(gameBoard.path[currentPath].controldots[i].Item2.x, gameBoard.path[currentPath].controldots[i].Item2.y, 8);
                }
                else{
                    canvas.StrokeColor = Colors.Blue;
                    canvas.StrokeSize = 1;
                    canvas.DrawCircle(gameBoard.path[currentPath].controldots[i].Item1.x, gameBoard.path[currentPath].controldots[i].Item1.y, 6);

                    canvas.StrokeSize = 3;
                    //canvas.FillColor = Colors.Yellow;
                    canvas.DrawCircle(gameBoard.path[currentPath].controldots[i].Item2.x, gameBoard.path[currentPath].controldots[i].Item2.y, 8);
                
                }       
            }
        }
        public void DrawCurveTo(ICanvas canvas, int currentPath){
            PathF path_draw_Curve = new PathF();
            path_draw_Curve.MoveTo(gameBoard.path[currentPath].dot[0].x, gameBoard.path[currentPath].dot[0].y);

            //Application.Current.MainPage.DisplayAlert("Upozornění", "Toto je ukázkové upozornění." + gameBoard.path[currentPath].dot.Count, "OK");
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
            //path_draw_Curve.MoveTo(gameBoard.path[currentPath].backdot[0].x, gameBoard.path[currentPath].backdot[0].y);
            //path_draw_Curve.LineTo(gameBoard.path[currentPath].dot[0].x, gameBoard.path[currentPath].dot[0].y);
            //path_draw_Curve.MoveTo(gameBoard.path[currentPath].backdot[0].x, gameBoard.path[currentPath].backdot[0].y);
            path_draw_Curve.LineTo(gameBoard.path[currentPath].dot[gameBoard.path[currentPath].backdot.Count - 1].x,
                gameBoard.path[currentPath].dot[gameBoard.path[currentPath].dot.Count - 1].y);

            for (int i = gameBoard.path[currentPath].backdot_with_t.Count-1;i>=0; i--)
            {
                path_draw_Curve.LineTo(gameBoard.path[currentPath].backdot_with_t[i].x, gameBoard.path[currentPath].backdot_with_t[i].y);
            }
            //Toto když tak odkomentovat -- je to to zpětné vykreslení cesty bez vzálenosti
            /*for (int i = gameBoard.path[currentPath].controlbackdot.Count - 2; i >= 0; i--)
            {
                path_draw_Curve.CurveTo(
                    gameBoard.path[currentPath].controlbackdot[i].Item2.x,
                    gameBoard.path[currentPath].controlbackdot[i].Item2.y,
                    gameBoard.path[currentPath].controlbackdot[i].Item1.x,
                    gameBoard.path[currentPath].controlbackdot[i].Item1.y,
                    gameBoard.path[currentPath].backdot[i ].x,
                    gameBoard.path[currentPath].backdot[i ].y
                );
            }*/

            //path_draw_Curve.LineTo(gameBoard.path[currentPath].dot[gameBoard.path[currentPath].dot.Count - 1].x, gameBoard.path[currentPath].dot[gameBoard.path[currentPath].dot.Count - 1].y);


            //path_draw_Curve.MoveTo(gameBoard.start.x, gameBoard.start.y);
            /*
            path_draw_Curve.LineTo(gameBoard.start.x - 5, gameBoard.start.y - 5);
            path_draw_Curve.LineTo(gameBoard.start.x+5, gameBoard.start.y + 5);
            for (int i = 0; i < gameBoard.path[currentPath].controldots.Count; i++)
            {
                path_draw_Curve.CurveTo(gameBoard.path[currentPath].controldots[i].Item1.x-5, gameBoard.path[currentPath].controldots[i].Item1.y - 5, gameBoard.path[currentPath].controldots[i].Item2.x - 5, gameBoard.path[currentPath].controldots[i].Item2.y - 5, gameBoard.path[currentPath].dot[i + 1].x - 5, gameBoard.path[currentPath].dot[i + 1].y - 5);
            }
            path_draw_Curve.LineTo(gameBoard.end.x - 5, gameBoard.end.y - 5);
           */
            //gameBoard.path[currentPath].dot[i + 1].x, gameBoard.path[currentPath].dot[i + 1].y - 2
            /* path_draw_Curve.LineTo(gameBoard.path[currentPath].dot[gameBoard.path[currentPath].dot.Count-1].x, gameBoard.path[currentPath].dot[gameBoard.path[currentPath].dot.Count-1].y + 5);
             for (int j = gameBoard.path[currentPath].controldots.Count-1; j > 0; j--)
             {
                 //Application.Current.MainPage.DisplayAlert("Upozornění", "Toto je ukázkové upozornění." + gameBoard.path[currentPath].dot.Count, "OK");
                 path_draw_Curve.CurveTo(gameBoard.path[currentPath].controldots[j].Item2.x+5, gameBoard.path[currentPath].controldots[j].Item2.y + 5, gameBoard.path[currentPath].controldots[j].Item1.x+5, gameBoard.path[currentPath].controldots[j].Item1.y + 5, gameBoard.path[currentPath].dot[j].x+5, gameBoard.path[currentPath].dot[j].y + 5);

             }
             path_draw_Curve.LineTo(gameBoard.start.x, gameBoard.start.y - 5);*/



            canvas.StrokeColor = Colors.Yellow;
            canvas.StrokeSize = 1;
            canvas.StrokeLineJoin = LineJoin.Round;
            //canvas.ClipPath(path_draw_Curve,WindingMode.NonZero);
            

                ImagePaint imagePaint = new ImagePaint
                {
                    Image = textureProvider.getRouteTexture(currentPath).Downsize(100)
                };
                path_draw_Curve.Close();

                canvas.SetFillPaint(imagePaint, RectF.Zero);
                canvas.FillPath(path_draw_Curve,WindingMode.NonZero);
                //canvas.FillRectangle(0, 0, height, width);
                canvas.StrokeDashOffset = 5;
            

            // canvas.StrokeSize = 8;
            canvas.DrawPath(path_draw_Curve);
            //canvas.BlendMode = BlendMode.Color;
            

           //canvas.FillPath(path_draw_Curve,WindingMode.NonZero);
        }

        public void DrawLineTo(ICanvas canvas, int currentPath)
        {
            PathF path_draw_without_bezier = new PathF();
            PathF path_draw = new PathF();
            path_draw_without_bezier.MoveTo(gameBoard.path[currentPath].dot[0].x, gameBoard.path[currentPath].dot[0].y);
            path_draw.MoveTo(gameBoard.path[currentPath].backdot[0].x, gameBoard.path[currentPath].backdot[0].y);
            for (int i = 1; i < gameBoard.path[currentPath].dot.Count; i++)
            {
                path_draw_without_bezier.LineTo(gameBoard.path[currentPath].dot[i].x, gameBoard.path[currentPath].dot[i].y);
                path_draw.LineTo(gameBoard.path[currentPath].backdot[i].x, gameBoard.path[currentPath].backdot[i].y);
            }
            canvas.StrokeColor = Colors.Green;
            canvas.StrokeSize = 1;
            canvas.StrokeLineJoin = LineJoin.Round;
            //canvas.DrawPath(path_draw_without_bezier);
            canvas.StrokeColor = Colors.Red;
            //canvas.DrawPath(path_draw);
        }
    }
}
