using NUnit.Framework;
using UnityEngine;

namespace Memoriae.Tests
{
    public class CommandReturnTest
    {
        [Test]
        public void CommandBlock_Clear_ShouldReleaseCardReference()
        {
            // Arrange
            GameObject blockObj = new GameObject();
            var block = blockObj.AddComponent<CommandBlock>();
            GameObject cardObj = new GameObject();
            var card = cardObj.AddComponent<CardUI>();

            // Act
            block.SetCard(card);
            block.Clear();

            // Assert
            Assert.IsNull(block.OccupyingCard, "清除後，槽位不應再持有卡片引用。");
        }
    }
}