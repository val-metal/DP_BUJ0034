using DP_BUJ0034.Engine.Generator;
using DP_BUJ0034.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.levelTests
{
    public class GenratorTests
    {
        [Fact]
        public void Generator_Init_1()
        {
            IGenerator generator=GeneratorFactory.MakeGenerator(1);
            Assert.NotNull(generator);
            Assert.Equal(new PathOneDifficulty().GetType(), generator.GetType());
        }
        [Fact]
        public void Generator_Init_2()
        {
            IGenerator generator = GeneratorFactory.MakeGenerator(2);
            Assert.NotNull(generator);
            if (generator.GetType() == typeof(PathTwoDifficulty))
            {
                Assert.Equal(new PathTwoDifficulty().GetType(), generator.GetType());
            }
            else
            {
                Assert.Equal(new PathOneDifficulty().GetType(), generator.GetType());
            }
            
        }
        [Fact]
        public void Generator_Init_3()
        {
            IGenerator generator = GeneratorFactory.MakeGenerator(3);
            Assert.NotNull(generator);
            Assert.Equal(new PathThreeDifficulty().GetType(), generator.GetType());
        }
        [Fact]
        public void Generator_Init_3_1()
        {
            IGenerator generator = GeneratorFactory.MakeGenerator(4);
            Assert.NotNull(generator);
            Assert.Equal(new PathThreeToOne().GetType(), generator.GetType());
        }
            [Fact]
            public void GeneratedPathWithinBounds_1()
            {
                var generator = new PathOneDifficulty();
                float width = 1280;
                float height = 720;
                GameBoard gb = new GameBoard(720, 1280, 1, 1);

                gb.generate_paths(1280,720,0,generator);

                foreach (var dot in gb.path[0].dot)
                {
                    Assert.InRange(dot.y, height / 9, (height / 9) * 8);
                    Assert.InRange(dot.x, (width / 16), (width / 16) * 15);
                }
            }

        [Fact]
        public void GeneratedPathWithinBounds_3()
        {
            var generator = new PathThreeDifficulty();
            float width = 1280;
            float height = 720;
            GameBoard gb = new GameBoard(720, 1280, 1, 3);

            gb.generate_paths(1280, 720, 0, generator);

            foreach (var dot in gb.path[0].dot)
            {
                Assert.InRange(dot.y, height / 9, (height / 9) * 8);
                Assert.InRange(dot.x, (width / 16), (width / 16) * 15);
            }
        }
        [Fact]
        public void GeneratedPathWithinBounds_3_1()
        {
            var generator = new PathThreeToOne();
            float width = 1280;
            float height = 720;
            GameBoard gb = new GameBoard(720, 1280, 1, 3);

            gb.generate_paths(1280, 720, 0, generator);

            foreach (var dot in gb.path[0].dot)
            {
                Assert.InRange(dot.y, height / 9, (height / 9) * 8);
                Assert.InRange(dot.x, (width / 16), (width / 16) * 15);
            }
        }
        [Fact]
        public void GeneratedPathStartEnd_1()
        {
            var generator = new PathOneDifficulty();
            float width = 1280;
            float height = 720;
            GameBoard gb = new GameBoard(720, 1280, 1, 1);

            gb.generate_paths(1280, 720, 0, generator);

            int maxi = gb.path[0].dot.Count - 1;
            Assert.Equal(gb.start[0].x, gb.path[0].dot[0].x);
            Assert.Equal(gb.start[0].y, gb.path[0].dot[0].y);
            Assert.Equal(gb.end[0].x, gb.path[0].dot[maxi].x);
            Assert.Equal(gb.end[0].y, gb.path[0].dot[maxi].y);
        }
        [Fact]
        public void GeneratedPathStartEnd_2()
        {
            var generator = new PathTwoDifficulty();
            float width = 1280;
            float height = 720;
            GameBoard gb = new GameBoard(720, 1280, 1, 1);

            gb.generate_paths(1280, 720, 0, generator);

            int maxi = gb.path[0].dot.Count - 1;
            Assert.Equal(gb.start[0].x-20, gb.path[0].dot[0].x);
            Assert.Equal(gb.start[0].y, Math.Ceiling(gb.path[0].dot[0].y)+4);
        }
        [Fact]
        public void GeneratedPathStartEnd_3()
        {
            var generator = new PathThreeDifficulty();
            float width = 1280;
            float height = 720;
            GameBoard gb = new GameBoard(720, 1280, 1, 1);

            gb.generate_paths(1280, 720, 0, generator);

            int maxi = gb.path[0].dot.Count - 1;
            Assert.Equal(gb.start[0].x, gb.path[0].dot[0].x);
            Assert.Equal(gb.start[0].y, gb.path[0].dot[0].y);
            Assert.Equal(gb.end[0].x, gb.path[0].dot[maxi].x);
            Assert.Equal(gb.end[0].y, gb.path[0].dot[maxi].y);
        }
        [Fact]
        public void GeneratedPathStartEnd_31()
        {
            var generator = new PathThreeToOne();
            float width = 1280;
            float height = 720;
            GameBoard gb = new GameBoard(720, 1280, 3, 4);

            gb.generate_paths(1280, 720, 0, generator);
            gb.generate_paths(1280, 720, 2, generator);
            gb.generate_paths(1280, 720, 1, generator);

            int maxi = gb.path[0].dot.Count - 1;
            Assert.Equal(gb.start[0].x, gb.path[0].dot[0].x);
            Assert.Equal(gb.start[0].y, gb.path[0].dot[0].y);
            Assert.Equal(gb.start[1].x, gb.path[0].dot[maxi].x);
            Assert.Equal(gb.start[1].y, gb.path[0].dot[maxi].y);
        }

       
        [Fact]
        public void GeneratedPath_Distance_2()
        {
            var generator = new PathOneDifficulty();
            float width = 1280;
            float height = 720;
            GameBoard gb = new GameBoard(720, 1280, 1, 1);

            gb.generate_paths(1280, 720, 0, generator);

            for (int i = 0; i < gb.path[0].dot.Count - 1; i++)
            {
                var distanceX = Math.Abs(gb.path[0].dot[i].x - gb.path[0].dot[i + 1].x);
                var distanceY = Math.Abs(gb.path[0].dot[i].y - gb.path[0].dot[i + 1].y);
            }
        }

    }
}
