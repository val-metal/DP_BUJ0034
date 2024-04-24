using CommunityToolkit.Mvvm.Input;
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
    public class SLUItest
    {
        [Fact]
        public async void TestNavigationToLevelSettings()
        {
            var levelInfos = new List<LevelInfo>
            {
            new LevelInfo { name = "Level1" },
            new LevelInfo { name = "Level2" }
            };

            SelectLevelViewModel svm = new ();
            svm.levelInfos = levelInfos.ToArray();
            await Assert.ThrowsAsync<NullReferenceException>(async () => await svm.gotolevelSettings("Level1"));

        }

        [Fact]
        public async void TestNoMatchingLevel()
        {
            var levelInfos = new List<LevelInfo>
            {
            new LevelInfo { name = "Level1" },
            new LevelInfo { name = "Level2" }
            };
            SelectLevelViewModel svm = new();
            svm.levelInfos = levelInfos.ToArray();
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await svm.gotolevelSettings("NonExistentLevel"));
        }


        [Fact]
        public async void TestNullLevelInfos()
        {
            SelectLevelViewModel svm = new();
            await Assert.ThrowsAsync<NullReferenceException>(async () => await svm.gotolevelSettings("NonExistentLevel"));
        }
        [Fact]
        public void AddStarOrLockImage_WhenUnlockAtGreaterThanCompleteScore_AddsLockImage()
        {
            var grid = new Microsoft.Maui.Controls.Grid();
            var i = 0;
            var lvlin = new LevelInfo();
            lvlin.unlockAt = 5;
            var levelInfos = new LevelInfo[] { lvlin,};
            var Complete_score = 2;
            var selectLevelViewModel = new SelectLevelViewModel();
            selectLevelViewModel.Complete_score = Complete_score;
            selectLevelViewModel.levelInfos = levelInfos;
            
            var resultGrid = selectLevelViewModel.AddStarOrLockImage(grid, i);

            Assert.Equal("Microsoft.Maui.Controls.Image",resultGrid.Children[0].GetType().ToString());
            Assert.Contains("lock.png", ((Microsoft.Maui.Controls.Image)resultGrid.Children[0]).Source.ToString());
        }

        [Fact]
        public void AddStarOrLockImage_WhenUnlockAtLessThanOrEqualToCompleteScore_AddsStarImage()
        {
            var grid = new Microsoft.Maui.Controls.Grid();
            var i = 0;
            var lvlin = new LevelInfo();
            lvlin.unlockAt = 5;
            var levelInfos = new LevelInfo[] { lvlin, };
            var Complete_score = 6;
            var selectLevelViewModel = new SelectLevelViewModel();
            selectLevelViewModel.Complete_score = Complete_score;
            selectLevelViewModel.levelInfos = levelInfos;
            selectLevelViewModel.scores = new int[] { 1, 2, 3 };
            var resultGrid = selectLevelViewModel.AddStarOrLockImage(grid, i);


            Assert.Contains(resultGrid.Children, c => c is Microsoft.Maui.Controls.Image image && image.Source.ToString().Contains("star_"));
        }
        [Fact]
        public void isCompletScoreBindedgood()
        {
            var selectLevelViewModel = new SelectLevelViewModel();
            selectLevelViewModel.complete_score = 2;
            Assert.Equal(2, selectLevelViewModel.Complete_score);
        }

        [Fact]
        public async Task ConfigPage_ChecksIfBoxViewIsEnabled()
        {

            var gridMock = new Mock<Microsoft.Maui.Controls.Grid>();
            var levelLoaderMock = new Mock<LevelLoader>();
            var levelsLoadMock = new Microsoft.Maui.Controls.Grid();
            var lvlin = new LevelInfo();
            lvlin.unlockAt = 5;
            var levelInfos = new LevelInfo[] { lvlin, };
            var Complete_score = 6;
            var selectLevelViewModel = new SelectLevelViewModel();
            selectLevelViewModel.Complete_score = Complete_score;
            selectLevelViewModel.levelInfos = levelInfos;
            selectLevelViewModel.scores = new int[] { 1, 2, 3 };

            await selectLevelViewModel.configPage(levelsLoadMock);



            var boxView = ((Microsoft.Maui.Controls.Grid)levelsLoadMock.Children[0]).FirstOrDefault(c => c is Microsoft.Maui.Controls.BoxView) as Microsoft.Maui.Controls.BoxView;


            if (levelInfos.Any(info => info.unlockAt > Complete_score))
            {
                Assert.False(boxView.IsEnabled);
            }
            else
            {
                Assert.True(boxView.IsEnabled);
            }
        }

    }
}
