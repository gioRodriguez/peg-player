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

        
    }
}
