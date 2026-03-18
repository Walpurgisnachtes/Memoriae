using UnityEngine;
using UnityEngine.UI;

namespace Memoriae
{
    /// <summary>
    /// 管理卡牌 UI 的更新，包含圖片、文字等視覺元素
    /// </summary>
    public static class CardUIUpdater
    {
        /// <summary>
        /// 根據卡牌物件更新 UI_Card GameObject 的圖片
        /// </summary>
        public static void UpdateCardImage(GameObject uiCardObject, AbstractCard card)
        {
            if (uiCardObject == null)
            {
                Debug.LogWarning("UI_Card GameObject 為空");
                return;
            }

            if (card == null)
            {
                Debug.LogWarning("卡牌物件為空");
                return;
            }

            UpdateCardImage(uiCardObject, card.ImagePath);
        }

        /// <summary>
        /// 根據卡牌 ID 更新 UI_Card GameObject 的圖片
        /// </summary>
        public static void UpdateCardImage(GameObject uiCardObject, string cardImagePath)
        {
            if (uiCardObject == null)
            {
                Debug.LogWarning("UI_Card GameObject 為空");
                return;
            }

            if (string.IsNullOrEmpty(cardImagePath))
            {
                Debug.LogWarning("卡牌圖片路徑為空");
                return;
            }

            // 嘗試取得 Image 組件
            Image imageComponent = uiCardObject.GetComponent<Image>();
            if (imageComponent == null)
            {
                Debug.LogError($"GameObject '{uiCardObject.name}' 上找不到 Image 組件");
                return;
            }

            // 從 CardImageManager 載入 Sprite
            Sprite cardSprite = CardImageManager.GetCardSprite(cardImagePath);
            if (cardSprite != null)
            {
                imageComponent.sprite = cardSprite;
                Debug.Log($"成功更新卡牌圖片: {cardImagePath}");
            }
            else
            {
                Debug.LogWarning($"無法載入卡牌圖片: {cardImagePath}");
            }
        }

        /// <summary>
        /// 根據卡牌 ID 更新 UI_Card GameObject 的圖片
        /// </summary>
        public static void UpdateCardImageByCardId(GameObject uiCardObject, string cardId)
        {
            if (uiCardObject == null)
            {
                Debug.LogWarning("UI_Card GameObject 為空");
                return;
            }

            if (string.IsNullOrEmpty(cardId))
            {
                Debug.LogWarning("卡牌 ID 為空");
                return;
            }

            // 從卡牌數據庫取得卡牌
            AbstractCard card = StaticCardBase.GetCard(cardId);
            if (card != null)
            {
                UpdateCardImage(uiCardObject, card);
            }
            else
            {
                Debug.LogWarning($"找不到卡牌 ID: {cardId}");
            }
        }

        /// <summary>
        /// 清除 UI_Card 的圖片
        /// </summary>
        public static void ClearCardImage(GameObject uiCardObject)
        {
            if (uiCardObject == null)
            {
                Debug.LogWarning("UI_Card GameObject 為空");
                return;
            }

            Image imageComponent = uiCardObject.GetComponent<Image>();
            if (imageComponent != null)
            {
                imageComponent.sprite = null;
            }
        }
    }
}