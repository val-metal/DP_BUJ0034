using DP_BUJ0034.Drawables;
using DP_BUJ0034.Game;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Engine
{
    public class PlayFrame
    {
        public GameBoard gameBoard { get; set; }
        
        public Drawable drawable { get; set; }
               
        public PlayFrame(Drawable drawable,int num_path,int difficulty) { 
            this.drawable = drawable;
            this.gameBoard = new GameBoard(drawable.height, drawable.width, num_path);

        }
        public void movePlayer(float x, float y)
        {
            
            for (int playerI = 0; playerI < gameBoard.player.Length; playerI++)
            {
                double distance = Math.Sqrt(Math.Pow((x - (gameBoard.player[playerI].size / 2)) - gameBoard.player[playerI].position.X, 2) + Math.Pow((y - (gameBoard.player[playerI].size / 2)) - gameBoard.player[playerI].position.Y, 2));
                if (distance < gameBoard.player[playerI].size)
                {
                    gameBoard.player[playerI].position = new PointF(x, y);
                    break;
                }
            }
           
        }
        public void play(float height, float width)
        {
            for (int i = 0; i < gameBoard.num_paths; i++)
            {
                gameBoard.generate_paths(height, width, i);
            }
            for (int i = 0; i < gameBoard.num_paths; i++)
            {
                gameBoard.player[i] = (new Player(new Point(gameBoard.start[i].x, gameBoard.start[i].y), 64)); //((gameBoard.width/9)*i)+gameBoard.width/9)

            }
            drawable.gameBoard = gameBoard;
            
        }
    }
}
