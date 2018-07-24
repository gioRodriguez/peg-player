using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PegePlayer.Common;
using PegePlayer.Common.Utils;

namespace PegPlayer.Test
{
    [TestClass]
    public class PegBoardTest
    {
        private Mock<IPegBoardSource> _mockPegBoardSource;
        private const int Columns = 5;
        private const int Rows = 5;
        private const int Goal = 0;
        private const int MissingPegs = 3;

        [TestInitialize]
        public void Setup()
        {
            _mockPegBoardSource = new Mock<IPegBoardSource>();
            _mockPegBoardSource
                .Setup(pegSource => pegSource.Columns)
                .Returns(Columns);
            _mockPegBoardSource
                .Setup(pegSource => pegSource.Rows)
                .Returns(Rows);
            _mockPegBoardSource
                .Setup(pegSource => pegSource.Goal)
                .Returns(Goal);
            _mockPegBoardSource
                .Setup(pegSource => pegSource.MissingPegs)
                .Returns(new[]
                {
                    Peg.CreateMissingPeg(1, 1),
                    Peg.CreateMissingPeg(2, 1),
                    Peg.CreateMissingPeg(3, 2),
                });
        }

        [TestMethod]
        public void PegBoardGetPegNeighboursFrom33Test()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(TestBoardSource.CreateDefault());

            // Act
            var actual = pegBoard.GetPegUpNeighboursFrom(Peg.Create(3, 3));

            // Assert
            Assert.AreEqual(2, actual.Count());
            Assert.IsTrue(actual.Contains(Peg.Create(1, 3)));
            Assert.IsTrue(actual.Contains(Peg.Create(2, 4)));
        }

        [TestMethod]
        public void PegBoardGetPegNeighboursTest()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(TestBoardSource.CreateDefault());

            // Act
            var actual = pegBoard.GetPegUpNeighboursFrom(Peg.Create(4, 2));

            // Assert
            Assert.AreEqual(3, actual.Count());
            Assert.IsTrue(actual.Contains(Peg.Create(3, 1)));
            Assert.IsTrue(actual.Contains(Peg.Create(1, 1)));
            Assert.IsTrue(actual.Contains(Peg.Create(3, 3)));
        }

        [TestMethod]
        public void PegBoardGetPegNeighboursFromBoardTest()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(TestBoardSource.CreateDefault());
            
            // Act
            var actual = pegBoard.GetPegUpNeighboursFrom(Peg.Create(4, 0));

            // Assert
            Assert.AreEqual(1, actual.Count());
            Assert.IsTrue(actual.Contains(Peg.Create(3, 1)));
            Assert.AreEqual(Peg.PegProbality, actual.ElementAt(0).Probability);
        }

        [TestMethod]
        public void PegBoardGetPegNeighboursFromBottonBoardTest()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(TestBoardSource.CreateDefault());

            // Act
            var actual = pegBoard.GetPegUpNeighboursFrom(Peg.Create(5, 1));

            // Assert
            Assert.AreEqual(2, actual.Count());
            Assert.IsTrue(actual.Contains(Peg.Create(4, 0)));
            Assert.IsTrue(actual.Contains(Peg.Create(4, 2)));
            Assert.AreEqual(Peg.PegAtBorderProbality, actual.ElementAt(1).Probability);
            Assert.AreEqual(Peg.PegProbality, actual.ElementAt(0).Probability);
        }

        [TestMethod]
        public void PegBoardGetGoalPegTest()
        {
            // Arrange                        
            var pegBoard = PegBoard.FromSource(_mockPegBoardSource.Object);

            // Act
            var actual = pegBoard.GoalPeg;

            // Assert            
            Assert.AreEqual(Peg.Create(5, 1), actual);
        }

        [TestMethod]
        public void PegBoardCreatePegBoardTest()
        {
            // Arrange                        

            // Act
            var pegBoard = PegBoard.FromSource(_mockPegBoardSource.Object);

            // Assert            
            Assert.AreEqual(Columns * 2 - 1, pegBoard.Columns);            
            Assert.AreEqual(Rows, pegBoard.Rows);
            Assert.AreEqual(Goal, pegBoard.Goal);
            Assert.AreEqual(MissingPegs, pegBoard.MissingPegs.Count());
            Assert.AreEqual(Peg.Create(1, 3), pegBoard.MissingPegs.ElementAt(0));
            Assert.AreEqual(Peg.Create(2, 2), pegBoard.MissingPegs.ElementAt(1));
            Assert.AreEqual(Peg.Create(3, 5), pegBoard.MissingPegs.ElementAt(2));
        }
    }
}
