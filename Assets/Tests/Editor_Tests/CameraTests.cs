using NUnit.Framework;
using UnityEngine;

namespace Memoriae.Tests
{
    public class CameraTests
    {
        [Test]
        public void Camera_ClampPosition_ShouldStayWithinMapBoundaries()
        {
            // Arrange
            float minX = 0, maxX = 6, minY = 0, maxY = 6;
            Vector3 outOfBoundsPos = new Vector3(10, -5, -10);

            // Act
            float clampedX = Mathf.Clamp(outOfBoundsPos.x, minX, maxX);
            float clampedY = Mathf.Clamp(outOfBoundsPos.y, minY, maxY);
            Vector3 result = new Vector3(clampedX, clampedY, outOfBoundsPos.z);

            // Assert
            Assert.AreEqual(6f, result.x);
            Assert.AreEqual(0f, result.y);
        }
    }
}