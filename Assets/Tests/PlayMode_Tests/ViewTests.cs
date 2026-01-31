using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Memoriae.Tests
{
    public class ViewTests
    {
        [UnityTest]
        public IEnumerator Piece_ShouldBeAtCenterOf7x7Map()
        {
            // Arrange
            GameObject mapObj = new GameObject("Map");
            var display = mapObj.AddComponent<MapDisplay>();
            int size = 7;
            Vector2Int center = new Vector2Int(3, 3);

            // Act
            display.InitializeMap(size, size);
            GameObject pieceObj = display.SpawnPieceAt(new Piece("Hero"), center);

            yield return null; // 等待一幀渲染

            // Assert
            // 預期座標：中心點應位於 (3, 3)，假設每個 Tile 間距為 1 單位
            Vector3 expectedPos = new Vector3(3, 3, 0);
            Assert.AreEqual(expectedPos, pieceObj.transform.position);
        }
    }
}