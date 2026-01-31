using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Memoriae.Tests
{
    public class CardPrefabTest
    {
        [Test]
        public void CardPrefab_ShouldHaveImageComponent_InsteadOfSpriteRenderer()
        {
            // Arrange
            // 假設您的 Prefab 放在 Resources 或透過引用的方式獲取
            GameObject cardPrefab = Resources.Load<GameObject>("Prefabs/UI_Card");

            // Act
            var hasImage = cardPrefab.GetComponent<Image>() != null;
            var hasSpriteRenderer = cardPrefab.GetComponent<SpriteRenderer>() != null;
            var hasCardUIScript = cardPrefab.GetComponent<CardUI>() != null;

            // Assert
            // 這將會失敗，直到我們將 SpriteRenderer 替換為 Image
            Assert.IsTrue(hasImage, "Card Prefab 必須包含 Image 組件以在 UI Canvas 顯示。");
            Assert.IsFalse(hasSpriteRenderer, "Card Prefab 不應使用 SpriteRenderer。");
            Assert.IsTrue(hasCardUIScript, "Card Prefab 必須包含 CardUI 腳本以處理拖放行為。");
        }
    }
}