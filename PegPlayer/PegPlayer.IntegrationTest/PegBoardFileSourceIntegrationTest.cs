using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegePlayer.Common;
using PegePlayer.Common.Utils;

namespace PegPlayer.IntegrationTest
{
    [TestClass]
    public class PegBoardFileSourceIntegrationTest
    {
        [TestMethod]
        public void PegBoardFileParseIntegrationTest()
        {
            // Arrange

            // Act
            var pegBoardFileSource = PegBoardFileSource.ParseFile("./PageBoardFile1.txt");

            // Assert
            Assert.AreEqual(5, pegBoardFileSource.Rows);
            Assert.AreEqual(5, pegBoardFileSource.Columns);
            Assert.AreEqual(0, pegBoardFileSource.Goal);
            Assert.AreEqual(3, pegBoardFileSource.MissingPegs.Count());
            Assert.AreEqual(Peg.Create(1, 1), pegBoardFileSource.MissingPegs.ElementAt(0));
            Assert.AreEqual(Peg.Create(2, 1), pegBoardFileSource.MissingPegs.ElementAt(1));
            Assert.AreEqual(Peg.Create(3, 2), pegBoardFileSource.MissingPegs.ElementAt(2));
        }
    }

    
}
