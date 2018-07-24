using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegePlayer.Common;

namespace PegPlayer.Test
{
    [TestClass]
    public class PegBoardBruteforceSolutionStrategyTest
    {

        [TestMethod]
        public void PegBoardBruteForceSolutionStrategyResolveTest()
        {
            // Arrange
            var actual = PegBoardBruteForceSolutionStrategy.Create(
                PegBoard.FromSource(TestBoardSource.CreateDefault())
            );

            // Act
            actual.Resolve();

            // Assert
            CollectionAssert.AreEqual(new [] {0}, actual.GetBestPositions().ToArray());
        }
    }
}
