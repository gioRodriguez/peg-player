using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegePlayer.Common;

namespace PegPlayer.Test
{
    [TestClass]
    public class PegFactoryTest
    {
        [TestMethod]
        public void PegsFactoryCreateUpAndRightFromBorderTest()
        {
            // Arrange
            var pegsFactory = new Peg.Factory(5, 9);
            var currentPeg = pegsFactory.CreatePeg(5, 7);

            // Act
            var actual = pegsFactory.CreateUpAndRigthFrom(currentPeg);
            var actual2 = pegsFactory.CreateUpAndRigthFrom(actual);

            // Assert
            Assert.AreEqual(Peg.Create(4, 8), actual);
            Assert.AreEqual(Peg.OutOfBoard, actual2);
        }

        [TestMethod]
        public void PegsFactoryCreateUpAndRightFromTest()
        {
            // Arrange
            var pegsFactory = new Peg.Factory(5, 9, new []
            {
                Peg.CreateMissingPeg(2, 1)
            });
            var currentPeg = pegsFactory.CreatePeg(5, 1);

            // Act
            var actual = pegsFactory.CreateUpAndRigthFrom(currentPeg);
            var actual2 = pegsFactory.CreateUpAndRigthFrom(actual);
            var actual3 = pegsFactory.CreateUpAndLeftFrom(actual2);

            // Assert
            Assert.AreEqual(Peg.Create(4, 2), actual);
            Assert.AreEqual(Peg.Create(3, 3), actual2);
            Assert.IsTrue(actual3.IsMissingPeg);
        }

        [TestMethod]
        public void PegsFactoryCreateUpAndLeftFromBorderTest()
        {
            // Arrange
            var pegsFactory = new Peg.Factory(5, 9);
            var currentPeg = pegsFactory.CreatePeg(2, 0);

            // Act
            var actual = pegsFactory.CreateUpAndLeftFrom(currentPeg);

            // Assert
            Assert.AreEqual(Peg.OutOfBoard, actual);
        }

        [TestMethod]
        public void PegsFactoryCreateUpAndLeftFromTest()
        {
            // Arrange
            var pegsFactory = new Peg.Factory(5, 9);
            var currentPeg = pegsFactory.CreatePeg(5, 1);

            // Act
            var actual = pegsFactory.CreateUpAndLeftFrom(currentPeg);
            var actual2 = pegsFactory.CreateUpAndLeftFrom(actual);

            // Assert
            Assert.AreEqual(Peg.Create(4, 0), actual);
            Assert.AreEqual(Peg.OutOfBoard, actual2);
        }

        [TestMethod]
        public void PegsFactoryCreatePegUpFromTest()
        {
            // Arrange
            var pegsFactory = new Peg.Factory(5, 9);
            var currentPeg = pegsFactory.CreatePeg(5, 1);

            // Act
            var actual = pegsFactory.CreatePegUpFrom(currentPeg);

            // Assert
            Assert.AreEqual(Peg.Create(3, 1), actual);
        }

        [TestMethod]
        public void PegsFactoryCreateTest()
        {
            // Arrange
            var pegsFactory = new Peg.Factory(5, 9);

            // Act
            var actual = pegsFactory.CreatePeg(5, 1);

            // Assert
            Assert.AreEqual(Peg.Create(5, 1), actual);
        }

        [TestMethod]
        public void PegsFactoryCreateOutOfBoardTest()
        {
            // Arrange
            var pegsFactory = new Peg.Factory(5, 9);

            // Act
            var actual = pegsFactory.CreatePeg(-1, 1);

            // Assert
            Assert.AreEqual(Peg.OutOfBoard, actual);
        }

        [TestMethod]
        public void PegsFactoryCreateOnMissingPegTest()
        {
            // Arrange
            var pegsFactory = new Peg.Factory(5, 9, new []
            {
                Peg.CreateMissingPeg(2, 1)
            });

            // Act
            var actual = pegsFactory.CreatePeg(2, 2);

            // Assert
            Assert.IsTrue(actual.IsMissingPeg);
        }
    }
}
