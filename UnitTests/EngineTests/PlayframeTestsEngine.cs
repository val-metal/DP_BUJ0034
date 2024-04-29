using DP_BUJ0034.Engine;
using DP_BUJ0034.Expectations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.EngineTests
{
    public class PlayframeTestsEngine
    {
        [Fact]
        public void Playframe_Init()
        {
            PlayFrame pf = new PlayFrame(new DP_BUJ0034.Drawables.Drawable(),1,1);
            Assert.True(pf.num_difficulty == 1);
            Assert.True(pf.num_paths == 1);
            Assert.NotNull(pf.gameBoard);
        }
        [Fact]
        public void Playframe_Init_3_1()
        {
            PlayFrame pf = new PlayFrame(new DP_BUJ0034.Drawables.Drawable(), 4, 1);
            Assert.True(pf.num_difficulty == 4);
            Assert.True(pf.num_paths == 4);
            Assert.NotNull(pf.gameBoard);
        }

        [Fact]
        public void Playframe_Init_bad()
        {
            Assert.Throws<ArgumentException>(() => new PlayFrame(new DP_BUJ0034.Drawables.Drawable(), -1, 1));
        }
        [Fact]
        public void Playframe_Init_bad_2()
        {
            Assert.Throws<ArgumentException>(() => new PlayFrame(new DP_BUJ0034.Drawables.Drawable(), -1, 5));
        }
        [Fact]
        public void Playframe_not_Running_Move()
        {
            PlayFrame pf = new PlayFrame(new DP_BUJ0034.Drawables.Drawable(), 1, 1);
            Assert.Throws<EngineNotRunningException>(()=> pf.movePlayer(120, 120));
        }
        [Fact]
        public void Plaframe_Running()
        { 
            PlayFrame pf = new PlayFrame(new DP_BUJ0034.Drawables.Drawable(), 1, 1);
            pf.play(720,1280);
            Assert.True(pf.stopwatch.IsRunning);
            Assert.True(pf.running);
            Assert.False(pf.gameEnds);

        }
        [Fact]
        public void Plaframe_CountPercentage()
        {
            PlayFrame pf = new PlayFrame(new DP_BUJ0034.Drawables.Drawable(), 1, 1);
            pf.play(720, 1280);
            for(int i=0;i< pf.gameBoard.path[0].t_path.Count;i++)
            {
                pf.gameBoard.path[0].t_path[i] = true;
            }
            Assert.True(pf.countMovePercentage() == 100);

        }
        [Fact]
        public void Plaframe_CountPercentage_2()
        {
            PlayFrame pf = new PlayFrame(new DP_BUJ0034.Drawables.Drawable(), 1, 1);
            pf.play(720, 1280);
            for (int i = 0; i < pf.gameBoard.path[0].t_path.Count; i+=2)
            {
                pf.gameBoard.path[0].t_path[i] = true;
            }
            Assert.True(Math.Round(pf.countMovePercentage()) == 50);

        }
        [Fact]
        public void Plaframe_MoveBy_Path()
        {
            PlayFrame pf = new PlayFrame(new DP_BUJ0034.Drawables.Drawable(), 1, 1);
            pf.play(720, 1280);
            for (int i = 0; i < pf.gameBoard.path[0].backdot_with_t.Count; i ++)
            {
                pf.movePlayer(pf.gameBoard.path[0].backdot_with_t[i].x-10, pf.gameBoard.path[0].backdot_with_t[i].y-10);
            }
            Assert.True(pf.countMovePercentage() == 100);

        }
        [Fact]
        public void Plaframe_LastplayerMove()
        {
            PlayFrame pf = new PlayFrame(new DP_BUJ0034.Drawables.Drawable(), 3, 1);
            pf.play(720, 1280);
            for (int i = 0; i < pf.gameBoard.path[0].backdot_with_t.Count; i++)
            {
                pf.movePlayer(pf.gameBoard.path[0].backdot_with_t[i].x, pf.gameBoard.path[0].backdot_with_t[i].y);


            }
            Assert.True(pf.id_last_player_move == 0);

        }
        [Fact]
        public void Plaframe_Bad_Move()
        {
            PlayFrame pf = new PlayFrame(new DP_BUJ0034.Drawables.Drawable(), 1, 1);
            pf.play(720, 1280);
            float x = pf.gameBoard.player[0].position.x;
            float y = pf.gameBoard.player[0].position.y;

            pf.movePlayer(720, 1280);
            Assert.True(pf.gameBoard.player[0].position.x == x);
            Assert.True(pf.gameBoard.player[0].position.y == y);


        }
    }
}
