using DP_BUJ0034.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.uiTests
{
    public class LSUITests
    {
        [Fact]
        public void TestPocetCest()
        {
            var viewModel = new LevelSettingsViewModel("type",
                                                        new ImageButton(),
                                                        new ImageButton(),
                                                        new ImageButton(),
                                                        new ImageButton(),
                                                        new ImageButton(),
                                                        new ImageButton(),
                                                        new ImageButton(),
                                                        "name");

            viewModel.pocetCest("1");
            Assert.Equal(1, viewModel.NumPaths);
        }

        [Fact]
        public void TestNumOfStars()
        {
            LevelSettingsViewModel viewModel = new LevelSettingsViewModel("type",
                                                        new ImageButton(),
                                                        new ImageButton(),
                                                        new ImageButton(),
                                                        new ImageButton(),
                                                        new ImageButton(),
                                                        new ImageButton(),
                                                        new ImageButton(),
                                                        "name");

            viewModel.num_of_stars("2");

            Assert.Equal(2, viewModel.difficulty);
        }
        [Fact]
        public void CheckClicks()
        {
            LevelSettingsViewModel viewModel = new LevelSettingsViewModel("type",
                                                        new ImageButton(),
                                                        new ImageButton(),
                                                        new ImageButton(),
                                                        new ImageButton(),
                                                        new ImageButton(),
                                                        new ImageButton(),
                                                        new ImageButton(),
                                                        "name");
            viewModel.pocetCest("1");
            viewModel.num_of_stars("1");
            Assert.Equal("path1_1_check.png",viewModel.path_1.Source.ToString().Split(" ")[1]);
            Assert.Equal("path2_2.png", viewModel.path_2.Source.ToString().Split(" ")[1]);
            Assert.Equal("path3_1.png", viewModel.path_3_1.Source.ToString().Split(" ")[1]);
            Assert.Equal("path3_3.png", viewModel.path_3.Source.ToString().Split(" ")[1]);

            Assert.Equal("star_1_check.png", viewModel.difficulty_1.Source.ToString().Split(" ")[1]);
            Assert.Equal("star_2.png", viewModel.difficulty_2.Source.ToString().Split(" ")[1]);
            Assert.Equal("star_3.png", viewModel.difficulty_3.Source.ToString().Split(" ")[1]);

            viewModel.pocetCest("2");
            viewModel.num_of_stars("2");
            Assert.Equal("path1_1.png", viewModel.path_1.Source.ToString().Split(" ")[1]);
            Assert.Equal("path2_2_check.png", viewModel.path_2.Source.ToString().Split(" ")[1]);
            Assert.Equal("path3_1.png", viewModel.path_3_1.Source.ToString().Split(" ")[1]);
            Assert.Equal("path3_3.png", viewModel.path_3.Source.ToString().Split(" ")[1]);

            Assert.Equal("star_1.png", viewModel.difficulty_1.Source.ToString().Split(" ")[1]);
            Assert.Equal("star_2_check.png", viewModel.difficulty_2.Source.ToString().Split(" ")[1]);
            Assert.Equal("star_3.png", viewModel.difficulty_3.Source.ToString().Split(" ")[1]);

            viewModel.pocetCest("3");
            viewModel.num_of_stars("3");
            Assert.Equal("path1_1.png", viewModel.path_1.Source.ToString().Split(" ")[1]);
            Assert.Equal("path2_2.png", viewModel.path_2.Source.ToString().Split(" ")[1]);
            Assert.Equal("path3_1.png", viewModel.path_3_1.Source.ToString().Split(" ")[1]);
            Assert.Equal("path3_3_check.png", viewModel.path_3.Source.ToString().Split(" ")[1]);

            Assert.Equal("star_1.png", viewModel.difficulty_1.Source.ToString().Split(" ")[1]);
            Assert.Equal("star_2.png", viewModel.difficulty_2.Source.ToString().Split(" ")[1]);
            Assert.Equal("star_3_check.png", viewModel.difficulty_3.Source.ToString().Split(" ")[1]);

            viewModel.pocetCest("4");
            Assert.Equal("path1_1.png", viewModel.path_1.Source.ToString().Split(" ")[1]);
            Assert.Equal("path2_2.png", viewModel.path_2.Source.ToString().Split(" ")[1]);
            Assert.Equal("path3_1_check.png", viewModel.path_3_1.Source.ToString().Split(" ")[1]);
            Assert.Equal("path3_3.png", viewModel.path_3.Source.ToString().Split(" ")[1]);
        }
        [Fact]
        public void TestPocetCestOutOfRange()
        {
            var viewModel = new LevelSettingsViewModel("type", new ImageButton(), new ImageButton(), new ImageButton(), new ImageButton(), new ImageButton(), new ImageButton(), new ImageButton(), "name");

            Assert.Throws<ArgumentOutOfRangeException>(() => viewModel.pocetCest("5"));
        }

        [Fact]
        public void TestNumOfStarsOutOfRange()
        {
            var viewModel = new LevelSettingsViewModel("type", new ImageButton(), new ImageButton(), new ImageButton(), new ImageButton(), new ImageButton(), new ImageButton(), new ImageButton(), "name");
            Assert.Throws<ArgumentOutOfRangeException>(() => viewModel.num_of_stars("4"));
        }
    }
}
