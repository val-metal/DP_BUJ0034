using DP_BUJ0034.Engine;
using DP_BUJ0034.Game;
using DP_BUJ0034.Game.Generator;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.levelTests
{
    public class GameBoardTests
    {
        [Fact]
        public void GeneratePaths_ValidInput_CreatesPathAndGeneratesPoints()
        {
            float width = 100f;
            float height = 100f;
            int currentPath = 0;

            var generatorMock = new Mock<IGenerator>();
            generatorMock.Setup(g => g.generatePath(It.IsAny<Points>(), It.IsAny<Points>(), width, height))
                         .Returns(new Paths());

            var gameBoard = new GameBoard(height, width, 1, 1);

            
            gameBoard.generate_paths(width, height, currentPath, generatorMock.Object);

            Assert.NotNull(gameBoard.path[currentPath]); 
            Assert.NotNull(gameBoard.start[currentPath]); 
            Assert.NotNull(gameBoard.end[currentPath]); 
           
        }



        [Fact]
        public void IsAllVisited_EndPointsNotVisited_ReturnsFalse()
        {
            var gameBoard = new GameBoard(500, 500, 2, 1);
            gameBoard.end[0] = new Points(false, true, 0f, 0f);
            gameBoard.end[1] = new Points(false, true, 0f, 0f);

            bool result = gameBoard.isAllVisited();

            Assert.False(result);
        }

        [Fact]
        public void AddUserMove_SaveHistoryEnabled_AddsMoveToHistory()
        {
            var gameBoard = new GameBoard(500, 500, 1, 1);
            gameBoard.saveHistory = true;
            float x = 10f;
            float y = 20f;

            gameBoard.addUserMove(x, y);

            Assert.Single(gameBoard.playerHistory); 
            Assert.Equal(x, gameBoard.playerHistory[0].x);
            Assert.Equal(y, gameBoard.playerHistory[0].y); 
        }

        [Fact]
        public void AddUserMove_SaveHistoryDisabled_NoMoveAddedToHistory()
        {
            var gameBoard = new GameBoard(500, 500, 1, 1);
            gameBoard.saveHistory = false;
            float x = 10f;
            float y = 20f;

            gameBoard.addUserMove(x, y);

            Assert.Empty(gameBoard.playerHistory); 
        }



    }
}
