using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegePlayer.Common;

namespace PegPlayer.Test
{
    [TestClass]
    public class PegsBoardIteratorTest
    {

        [TestMethod]
        public void PegsBoardIteratorCreateTest()
        {
            // Arrange
            var pegBoard = PegBoard.FromSource(TestBoardSource.CreateDefault());
            var pegsBoardIterator = new PegsBoardBruteForceIterator(pegBoard);
            var iteratedPegs = new List<Peg>();

            // Act
            foreach (var peg in pegsBoardIterator)
            {
                iteratedPegs.Add(peg);
            }

            // Assert
            Assert.IsTrue(iteratedPegs.Count > 0);
        }
    }
}
