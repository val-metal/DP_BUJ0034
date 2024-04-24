using DP_BUJ0034;
using DP_BUJ0034.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.uiTests
{
    public class FPUItest
    {
        [Fact]
        public void Constructor_SetsProperties()
        {
            var levelName = "Test Level";
            var percentage = 90.5;
            var numPaths = 3;
            var difficulty = 2;
            var time = 120000;

            var viewModel = new FinishPageViewModel(numPaths, difficulty, "type", time, levelName, percentage);

            Assert.Equal(levelName, viewModel.LevelName);
            Assert.Equal("90,5%", viewModel.Percentage);
            Assert.Equal("2:00", viewModel.TimeString); 
            Assert.Equal("star_2.png", viewModel.ImgName);
        }


        }

}
