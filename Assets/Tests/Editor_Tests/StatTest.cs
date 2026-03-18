using NUnit.Framework;
using UnityEngine;

namespace Memoriae.Tests
{
    public class StatTest
    {
        [Test]
        public void HP_Current_ShouldNotExceedMax()
        {
            // Arrange
            var stats = new PieceStats();
            stats.MaxHP = 100;

            // Act
            stats.CurrentHP = 150;

            // Assert
            Assert.AreEqual(100, stats.CurrentHP);
        }

        [Test]
        public void HP_Current_ShouldNotGoBelowZero()
        {
            // Arrange
            var stats = new PieceStats();
            stats.CurrentHP = 50;

            // Act
            stats.CurrentHP -= 100;

            // Assert
            Assert.AreEqual(0, stats.CurrentHP);
        }
    }
}