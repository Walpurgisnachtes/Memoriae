using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Memoriae.Tests
{
    public class MapTests
    {
        [Test]
        [TestCase(10, 10)]
        [TestCase(5, 8)]
        public void Map_Initialization_ShouldHaveCorrectDimensions(int width, int height)
        {
            // Arrange & Act
            var map = new GameMap(width, height);

            // Assert
            Assert.AreEqual(width, map.Width);
            Assert.AreEqual(height, map.Height);
        }

        [Test]
        public void Piece_Placement_ShouldOccupyCorrectCoordinate()
        {
            // Arrange
            var map = new GameMap(10, 10);
            var piece = new Piece("TestPiece");
            Vector2Int targetPos = new Vector2Int(2, 3);

            // Act
            bool success = map.TryPlacePiece(piece, targetPos);

            // Assert
            Assert.IsTrue(success);
            Assert.AreEqual(piece, map.GetPieceAt(targetPos));
        }

        [Test]
        public void Piece_Placement_OutOfBounds_ShouldReturnFalse()
        {
            // Arrange
            var map = new GameMap(5, 5);
            var piece = new Piece("TestPiece");
            Vector2Int outOfBoundsPos = new Vector2Int(5, 5); // 0-indexed, 5 is out

            // Act
            bool success = map.TryPlacePiece(piece, outOfBoundsPos);

            // Assert
            Assert.IsFalse(success);
        }
    }
}
