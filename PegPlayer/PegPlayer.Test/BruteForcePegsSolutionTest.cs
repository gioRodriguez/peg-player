using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegePlayer.Common;

namespace PegPlayer.Test
{
    [TestClass]
    public class BruteForcePegsSolutionTest
    {
        [TestMethod]
        public void BruteForcePegsSolutionResolveTest()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(TestBoardSource.CreateDefault());
            var pegsBoardIterator = new BruteForcePegsSolution(pegBoard);            

            // Act
            pegsBoardIterator.Resolve();
            var actual = pegsBoardIterator.GetBestPositions();

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(Peg.Create(1, 1), actual.ElementAt(0));
            Assert.AreEqual(0.625, actual.ElementAt(0).Probability);
        }

        [TestMethod]
        public void BruteForcePegsSolutionResolveWithFreefallSolutionTest()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(new TestBoardSource(5, 5, 1, new[]
            {
                Peg.CreateMissingPeg(1, 1),
                Peg.CreateMissingPeg(2, 1),
                Peg.CreateMissingPeg(3, 1),
            }));
            var pegsBoardIterator = new BruteForcePegsSolution(pegBoard);

            // Act
            pegsBoardIterator.Resolve();
            var actual = pegsBoardIterator.GetBestPositions();

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(Peg.Create(1, 3), actual.ElementAt(0));
            Assert.AreEqual(1, actual.ElementAt(0).Probability);
        }

        [TestMethod]
        public void BruteForcePegsSolutionResolveWith2Test()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(new TestBoardSource(5, 5, 2, new[]
            {
                Peg.CreateMissingPeg(1, 1),
                Peg.CreateMissingPeg(2, 1),
                Peg.CreateMissingPeg(3, 2),
            }));
            var pegsBoardIterator = new BruteForcePegsSolution(pegBoard);

            // Act
            pegsBoardIterator.Resolve();
            var actual = pegsBoardIterator.GetBestPositions();

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(Peg.Create(1, 5), actual.ElementAt(0));
            Assert.AreEqual(0.625, actual.ElementAt(0).Probability);
        }

        [TestMethod]
        public void BruteForcePegsSolutionResolveWith7x7Test()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(new TestBoardSource(7, 7, 1, new[]
            {
                Peg.CreateMissingPeg(1, 1),
                Peg.CreateMissingPeg(2, 1),
                Peg.CreateMissingPeg(3, 2),
            }));
            var pegsBoardIterator = new BruteForcePegsSolution(pegBoard);

            // Act
            pegsBoardIterator.Resolve();
            var actual = pegsBoardIterator.GetBestPositions();

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.ElementAt(0).Probability <= 1);
        }

        [TestMethod]
        public void BruteForcePegsSolutionResolveWith11x11Test()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(new TestBoardSource(11, 11, 2, new[]
            {
                Peg.CreateMissingPeg(1, 1),
                Peg.CreateMissingPeg(2, 1),
                Peg.CreateMissingPeg(3, 2),
            }));
            var pegsBoardIterator = new BruteForcePegsSolution(pegBoard);

            // Act
            pegsBoardIterator.Resolve();
            var actual = pegsBoardIterator.GetBestPositions();

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.ElementAt(0).Probability <= 1);
        }

        [TestMethod]
        public void BruteForcePegsSolutionResolveWith3x3Test()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(new TestBoardSource(3, 3, 2, new[]
            {
                Peg.CreateMissingPeg(1, 1),
                Peg.CreateMissingPeg(2, 1),
                Peg.CreateMissingPeg(3, 2),
            }));
            var pegsBoardIterator = new BruteForcePegsSolution(pegBoard);

            // Act
            pegsBoardIterator.Resolve();
            var actual = pegsBoardIterator.GetBestPositions();

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.ElementAt(0).Probability <= 1);
        }

        [TestMethod]
        public void BruteForcePegsSolutionResolveWith21x21Test()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(new TestBoardSource(21, 21, 2, new[]
            {
                Peg.CreateMissingPeg(1, 1),
                Peg.CreateMissingPeg(2, 1),
                Peg.CreateMissingPeg(3, 2),
            }));
            var pegsBoardIterator = new BruteForcePegsSolution(pegBoard);

            // Act
            pegsBoardIterator.Resolve();
            var actual = pegsBoardIterator.GetBestPositions();

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.ElementAt(0).Probability <= 1);
        }

        [TestMethod]
        public void BruteForcePegsSolutionRetsolveWith23x23Test()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(new TestBoardSource(23, 23, 2, new[]
            {
                Peg.CreateMissingPeg(1, 1),
                Peg.CreateMissingPeg(2, 1),
                Peg.CreateMissingPeg(3, 2),
            }));
            var pegsBoardIterator = new BruteForcePegsSolution(pegBoard);

            // Act
            pegsBoardIterator.Resolve();
            var actual = pegsBoardIterator.GetBestPositions();

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.ElementAt(0).Probability <= 1);
        }

        [TestMethod]
        public void BruteForcePegsSolutionResolveWith25x25Test()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(new TestBoardSource(25, 25, 2, new[]
            {
                Peg.CreateMissingPeg(1, 1),
                Peg.CreateMissingPeg(2, 1),
                Peg.CreateMissingPeg(3, 2),
            }));
            var pegsBoardIterator = new BruteForcePegsSolution(pegBoard);

            // Act
            pegsBoardIterator.Resolve();
            var actual = pegsBoardIterator.GetBestPositions();

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.ElementAt(0).Probability <= 1);
        }

        [TestMethod]
        public void BruteForcePegsSolutionResolveWith31x31Test()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(new TestBoardSource(31, 31, 2, new[]
            {
                Peg.CreateMissingPeg(1, 1),
                Peg.CreateMissingPeg(2, 1),
                Peg.CreateMissingPeg(3, 2),
            }));
            var pegsBoardIterator = new BruteForcePegsSolution(pegBoard);

            // Act
            pegsBoardIterator.Resolve();
            var actual = pegsBoardIterator.GetBestPositions();

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.ElementAt(0).Probability <= 1);
        }
    }
}
