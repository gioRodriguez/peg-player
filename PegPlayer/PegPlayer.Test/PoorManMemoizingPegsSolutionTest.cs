using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegePlayer.Common;

namespace PegPlayer.Test
{
    [TestClass]
    public class PoorManMemoizingPegsSolutionTest
    {
        [TestMethod]
        public void PoorManMemoizingPegsSolutionResolveTest()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(TestBoardSource.CreateDefault());
            var pegsBoardIterator = new PoorManMemoizingPegsSolution(pegBoard);            

            // Act
            pegsBoardIterator.Resolve();
            var actual = pegsBoardIterator.GetBestPositions();

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(Peg.Create(1, 1), actual.ElementAt(0));
            Assert.AreEqual(0.625, actual.ElementAt(0).Probability);
        }

        [TestMethod]
        public void PoorManMemoizingPegsSolutionResolveWithFreefallSolutionTest()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(new TestBoardSource(5, 5, 1, new[]
            {
                Peg.CreateMissingPeg(1, 1),
                Peg.CreateMissingPeg(2, 1),
                Peg.CreateMissingPeg(3, 1),
            }));
            var pegsBoardIterator = new PoorManMemoizingPegsSolution(pegBoard);

            // Act
            pegsBoardIterator.Resolve();
            var actual = pegsBoardIterator.GetBestPositions();

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(Peg.Create(1, 3), actual.ElementAt(0));
            Assert.AreEqual(1, actual.ElementAt(0).Probability);
        }

        [TestMethod]
        public void PoorManMemoizingPegsSolutionResolveWith2Test()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(new TestBoardSource(5, 5, 2, new[]
            {
                Peg.CreateMissingPeg(1, 1),
                Peg.CreateMissingPeg(2, 1),
                Peg.CreateMissingPeg(3, 2),
            }));
            var pegsBoardIterator = new PoorManMemoizingPegsSolution(pegBoard);

            // Act
            pegsBoardIterator.Resolve();
            var actual = pegsBoardIterator.GetBestPositions();

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(Peg.Create(1, 5), actual.ElementAt(0));
            Assert.AreEqual(0.625, actual.ElementAt(0).Probability);
        }

        [TestMethod]
        public void PoorManMemoizingPegsSolutionResolveWith7x7Test()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(new TestBoardSource(7, 7, 1, new[]
            {
                Peg.CreateMissingPeg(1, 1),
                Peg.CreateMissingPeg(2, 1),
                Peg.CreateMissingPeg(3, 2),
            }));
            var pegsBoardIterator = new PoorManMemoizingPegsSolution(pegBoard);

            // Act
            pegsBoardIterator.Resolve();
            var actual = pegsBoardIterator.GetBestPositions();

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.ElementAt(0).Probability <= 1);
        }

        [TestMethod]
        public void PoorManMemoizingPegsSolutionResolveWith11x11Test()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(new TestBoardSource(11, 11, 2, new[]
            {
                Peg.CreateMissingPeg(1, 1),
                Peg.CreateMissingPeg(2, 1),
                Peg.CreateMissingPeg(3, 2),
            }));
            var pegsBoardIterator = new PoorManMemoizingPegsSolution(pegBoard);

            // Act
            pegsBoardIterator.Resolve();
            var actual = pegsBoardIterator.GetBestPositions();

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.ElementAt(0).Probability <= 1);
        }

        [TestMethod]
        public void PoorManMemoizingPegsSolutionResolveWith3x3Test()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(new TestBoardSource(3, 3, 2, new[]
            {
                Peg.CreateMissingPeg(1, 1),
                Peg.CreateMissingPeg(2, 1),
                Peg.CreateMissingPeg(3, 2),
            }));
            var pegsBoardIterator = new PoorManMemoizingPegsSolution(pegBoard);

            // Act
            pegsBoardIterator.Resolve();
            var actual = pegsBoardIterator.GetBestPositions();

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.ElementAt(0).Probability <= 1);
        }

        [TestMethod]
        public void PoorManMemoizingPegsSolutionResolveWith21x21Test()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(new TestBoardSource(21, 21, 2, new[]
            {
                Peg.CreateMissingPeg(1, 1),
                Peg.CreateMissingPeg(2, 1),
                Peg.CreateMissingPeg(3, 2),
            }));
            var pegsBoardIterator = new PoorManMemoizingPegsSolution(pegBoard);

            // Act
            pegsBoardIterator.Resolve();
            var actual = pegsBoardIterator.GetBestPositions();

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.ElementAt(0).Probability <= 1);
        }

        [TestMethod]
        public void PoorManMemoizingPegsSolutionRetsolveWith23x23Test()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(new TestBoardSource(23, 23, 2, new[]
            {
                Peg.CreateMissingPeg(1, 1),
                Peg.CreateMissingPeg(2, 1),
                Peg.CreateMissingPeg(3, 2),
            }));
            var pegsBoardIterator = new PoorManMemoizingPegsSolution(pegBoard);

            // Act
            pegsBoardIterator.Resolve();
            var actual = pegsBoardIterator.GetBestPositions();

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.ElementAt(0).Probability <= 1);
        }

        [TestMethod]
        public void PoorManMemoizingPegsSolutionResolveWith25x25Test()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(new TestBoardSource(25, 25, 2, new[]
            {
                Peg.CreateMissingPeg(1, 1),
                Peg.CreateMissingPeg(2, 1),
                Peg.CreateMissingPeg(3, 2),
            }));
            var pegsBoardIterator = new PoorManMemoizingPegsSolution(pegBoard);

            // Act
            pegsBoardIterator.Resolve();
            var actual = pegsBoardIterator.GetBestPositions();

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.ElementAt(0).Probability <= 1);
        }

        [TestMethod]
        public void PoorManMemoizingPegsSolutionResolveWith31x31Test()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(new TestBoardSource(31, 31, 2, new[]
            {
                Peg.CreateMissingPeg(1, 1),
                Peg.CreateMissingPeg(2, 1),
                Peg.CreateMissingPeg(3, 2),
            }));
            var pegsBoardIterator = new PoorManMemoizingPegsSolution(pegBoard);

            // Act
            pegsBoardIterator.Resolve();
            var actual = pegsBoardIterator.GetBestPositions();

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.ElementAt(0).Probability <= 1);
        }

        [TestMethod]
        public void PoorManMemoizingPegsSolutionResolveWith111x111Test()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(new TestBoardSource(111, 111, 33, null));
            var pegsBoardIterator = new PoorManMemoizingPegsSolution(pegBoard);

            // Act
            pegsBoardIterator.Resolve();
            var actual = pegsBoardIterator.GetBestPositions();

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.ElementAt(0).Probability <= 1);
        }
    }
}
