using DP_BUJ0034.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.levelTests
{
    public class PlayFrameLevelTests
    {

        [Fact]
        public async Task GeneratePaths_ValidInput_path_num_player_num_1()
        {
            float width = 100f;
            float height = 100f;
            int currentPath = 0;
            PlayFrame pf = new PlayFrame(new DP_BUJ0034.Drawables.Drawable(), 1, 1);

            pf.play(height, width);

            Assert.NotNull(pf.gameBoard.path[currentPath]);
            Assert.NotNull(pf.gameBoard.start[currentPath]);
            Assert.NotNull(pf.gameBoard.end[currentPath]);

            Assert.Single(pf.gameBoard.path);
            Assert.Single(pf.gameBoard.start);
            Assert.Single(pf.gameBoard.end);

        }
        [Fact]
        public async Task GeneratePaths_ValidInput_path_num_player_num_2()
        {
            float width = 100f;
            float height = 100f;
            int currentPath = 0;
            PlayFrame pf = new PlayFrame(new DP_BUJ0034.Drawables.Drawable(), 2, 1);

            pf.play(height, width);

            Assert.NotNull(pf.gameBoard.path[currentPath]);
            Assert.NotNull(pf.gameBoard.start[currentPath]);
            Assert.NotNull(pf.gameBoard.end[currentPath]);

            Assert.Equal(2, pf.gameBoard.path.Length);
            Assert.Equal(2, pf.gameBoard.start.Length);
            Assert.Equal(2, pf.gameBoard.end.Length);

        }
        [Fact]
        public async Task GeneratePaths_ValidInput_path_num_player_num_3()
        {
            float width = 100f;
            float height = 100f;
            int currentPath = 0;
            PlayFrame pf = new PlayFrame(new DP_BUJ0034.Drawables.Drawable(), 3, 1);

            pf.play(height, width);

            Assert.NotNull(pf.gameBoard.path[currentPath]);
            Assert.NotNull(pf.gameBoard.start[currentPath]);
            Assert.NotNull(pf.gameBoard.end[currentPath]);

            Assert.Equal(3, pf.gameBoard.path.Length);
            Assert.Equal(3, pf.gameBoard.start.Length);
            Assert.Equal(3, pf.gameBoard.end.Length);

        }
        [Fact]
        public async Task GeneratePaths_ValidInput_path_num_player_num_3_1()
        {
            float width = 100f;
            float height = 100f;
            int currentPath = 0;
            PlayFrame pf = new PlayFrame(new DP_BUJ0034.Drawables.Drawable(), 4, 1);

            pf.play(height, width);

            Assert.NotNull(pf.gameBoard.path[currentPath]);
            Assert.NotNull(pf.gameBoard.start[currentPath]);
            Assert.NotNull(pf.gameBoard.end[currentPath]);

            Assert.Equal(2, pf.gameBoard.path.Length);
            Assert.Equal(3, pf.gameBoard.start.Length);
        }
        [Fact]
        public async Task Atributes_right_3_1()
        {
            float width = 100f;
            float height = 100f;
            PlayFrame pf = new PlayFrame(new DP_BUJ0034.Drawables.Drawable(), 4, 1);

            pf.play(height, width);

            Assert.NotEqual(pf.num_paths, pf.gameBoard.num_paths);
            Assert.Equal(4, pf.num_paths);
            Assert.Equal(2, pf.gameBoard.num_paths);

            Assert.False(pf.gameBoard.path[1].noback);
            Assert.False(pf.gameBoard.path[1].readyToConnect);
        }

        [Fact]
        public async Task Atributes_right_others()
        {
            float width = 100f;
            float height = 100f;
            PlayFrame pf = new PlayFrame(new DP_BUJ0034.Drawables.Drawable(), 3, 1);

            pf.play(height, width);

            Assert.Equal(pf.num_paths, pf.gameBoard.num_paths);
            Assert.False(pf.gameEnds);
        }

        [Fact]
        public async Task Optimalization_Tests()
        {
            float width = 100f;
            float height = 100f;
            PlayFrame pf = new PlayFrame(new DP_BUJ0034.Drawables.Drawable(), 3, 1);

            pf.play(height, width);

            Assert.Equal(pf.gameBoard.path[0].dot.Count, pf.gameBoard.path[0].controldots.Count+1);
            
        }
        [Fact]
        public async Task Optimalization_len()
        {
            float width = 100f;
            float height = 100f;
            PlayFrame pf = new PlayFrame(new DP_BUJ0034.Drawables.Drawable(), 3, 1);

            pf.play(height, width);
            double lx=Math.Pow(pf.gameBoard.path[0].dot[1].x - pf.gameBoard.path[0].dot[2].x,2d);
            double ly = Math.Pow(pf.gameBoard.path[0].dot[1].y- pf.gameBoard.path[0].dot[2].y,2d);
            double dist = Math.Sqrt(lx + ly);

            double lcx=Math.Pow(pf.gameBoard.path[0].dot[1].x - pf.gameBoard.path[0].controldots[0].Item1.x,2);
            double lcy=Math.Pow(pf.gameBoard.path[0].dot[1].y - pf.gameBoard.path[0].controldots[0].Item1.y,2);
            double fcdist = Math.Sqrt(lcx + lcy);

            double lcx2 = Math.Pow(pf.gameBoard.path[0].dot[1].x - pf.gameBoard.path[0].controldots[0].Item2.x, 2);
            double lcy2 = Math.Pow(pf.gameBoard.path[0].dot[1].y - pf.gameBoard.path[0].controldots[0].Item2.y, 2);
            double fcdist2 = Math.Sqrt(lcx2 + lcy2);
            Assert.Equal(pf.gameBoard.path[0].dot.Count, pf.gameBoard.path[0].controldots.Count + 1);

            Assert.True(dist<fcdist);
            Assert.True(dist < fcdist2);

        }


       


    }
}
