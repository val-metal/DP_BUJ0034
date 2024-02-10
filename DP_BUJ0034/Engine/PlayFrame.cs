﻿
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
        public bool gameEnds { get; set; }
        public int num_paths { get; set; }
        public int num_difficulty { get; set; }
        private int id_last_player_move; 
               
        public PlayFrame(Drawable drawable,int num_path,int difficulty) {
            gameEnds = false;
            this.drawable = drawable;
            this.gameBoard = new GameBoard(drawable.height, drawable.width, num_path, difficulty);

            this.num_paths = num_path;
            this.num_difficulty = difficulty;
        }
        public void movePlayer(float x, float y)
        {
            if (gameBoard.isAllVisited() == true)
            {
                gameEnds = true;
            }
            double distance_last = Math.Sqrt(Math.Pow((x - (gameBoard.player[id_last_player_move].size / 2)) - gameBoard.player[id_last_player_move].position.x, 2) + Math.Pow((y - (gameBoard.player[id_last_player_move].size / 2)) - gameBoard.player[id_last_player_move].position.y, 2));
            if (distance_last < gameBoard.player[id_last_player_move].size)
            {
                gameBoard.player[id_last_player_move].position = new Dots(x, y);

                float end_distance = Dots.EuclidDistance(gameBoard.player[id_last_player_move].getCenter(), gameBoard.end[id_last_player_move]);
                if (end_distance < gameBoard.player[id_last_player_move].size)
                {
                    gameBoard.end[id_last_player_move].isVisited = true;
                }
                //else
                //{
                //    gameBoard.end[playerI].isVisited = false;
                //}
                drawable.curretPlayer = id_last_player_move;
            }
            else
            {
                for (int playerI = 0; playerI < gameBoard.player.Length; playerI++)
                {
                    double distance = Math.Sqrt(Math.Pow((x - (gameBoard.player[playerI].size / 2)) - gameBoard.player[playerI].position.x, 2) + Math.Pow((y - (gameBoard.player[playerI].size / 2)) - gameBoard.player[playerI].position.y, 2));
                    if (distance < gameBoard.player[playerI].size)
                    {
                        gameBoard.player[playerI].position = new Dots(x, y);

                        float end_distance = Dots.EuclidDistance(gameBoard.player[playerI].getCenter(), gameBoard.end[playerI]);
                        if (end_distance < gameBoard.player[playerI].size)
                        {
                            gameBoard.end[playerI].isVisited = true;
                        }
                        //else
                        //{
                        //    gameBoard.end[playerI].isVisited = false;
                        //}
                        drawable.curretPlayer = playerI;
                        id_last_player_move = playerI;
                        break;
                    }
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
                gameBoard.player[i] = (new Player(new Dots(gameBoard.start[i].x, gameBoard.start[i].y), 64)); //((gameBoard.width/9)*i)+gameBoard.width/9)

            }
            drawable.gameBoard = gameBoard;
            
        }
    }
}
