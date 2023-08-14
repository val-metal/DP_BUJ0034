using DP_BUJ0034.Drawables;
using DP_BUJ0034.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Engine
{
    public class PlayFrame
    {
        GameBoard gameBoard;
        public Drawable drawable { get; set; }
               
        public PlayFrame(Drawable drawable,int num_path) { 
            this.drawable = drawable;
            this.gameBoard =new GameBoard(drawable.height,drawable.width,num_path);
            
            drawable.gameBoard = gameBoard;
            

        }
        public void play(float height ,float width)
        {
            gameBoard.generate_paths(height,width);
            drawable.gameBoard = gameBoard;
        }
    }
}
