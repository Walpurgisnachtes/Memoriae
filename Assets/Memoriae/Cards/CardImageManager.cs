using System;
using System.Collections.Generic;
using UnityEngine;

namespace Memoriae
{
    /// <summary>
    /// 管理卡牌圖片資源的載入和緩存
    /// </summary>
    public static class CardImageManager
    {
        private const string CARD_IMAGE_PATH = "artwork/card/";
        private static Dictionary<string, Sprite> _spriteCache = new Dictionary<string, Sprite>();
        private static Dictionary<string, Texture2D> _textureCache = new Dictionary<string, Texture2D>();
        private static bool _initialized = false;

        /// <summary>
        /// 初始化卡牌圖片管理器
        /// </summary>
        public static void Initialize()
        {
            if (_initialized) return;

            _spriteCache.Clear();
            _textureCache.Clear();
            _initialized = true;
            Debug.Log("卡牌圖片管理器已初始化");
        }

        /// <summary>
        /// 根據圖片路徑載入卡牌 Sprite（支援緩存）
        /// </summary>
        public static Sprite GetCardSprite(string cardImagePath)
        {
            if (string.IsNullOrEmpty(cardImagePath))
            {
                Debug.LogWarning("卡牌圖片路徑為空");
                return null;
            }

            if (!_initialized) Initialize();

            // 檢查緩存
            if (_spriteCache.TryGetValue(cardImagePath, out var cachedSprite))
            {
                return cachedSprite;
            }

            // 移除副檔名以便 Resources.Load 使用
            string imageName = System.IO.Path.GetFileNameWithoutExtension(cardImagePath);
            string resourcePath = CARD_IMAGE_PATH + imageName;

            try
            {
                var sprite = Resources.Load<Sprite>(resourcePath);
                if (sprite != null)
                {
                    _spriteCache[cardImagePath] = sprite;
                    return sprite;
                }
                else
                {
                    Debug.LogWarning($"找不到卡牌圖片: {resourcePath}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"載入卡牌圖片失敗 {resourcePath}: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 根據圖片路徑載入卡牌 Texture2D（支援緩存）
        /// </summary>
        public static Texture2D GetCardTexture(string cardImagePath)
        {
            if (string.IsNullOrEmpty(cardImagePath))
            {
                Debug.LogWarning("卡牌圖片路徑為空");
                return null;
            }

            if (!_initialized) Initialize();

            // 檢查緩存
            if (_textureCache.TryGetValue(cardImagePath, out var cachedTexture))
            {
                return cachedTexture;
            }

            // 移除副檔名以便 Resources.Load 使用
            string imageName = System.IO.Path.GetFileNameWithoutExtension(cardImagePath);
            string resourcePath = CARD_IMAGE_PATH + imageName;

            try
            {
                var texture = Resources.Load<Texture2D>(resourcePath);
                if (texture != null)
                {
                    _textureCache[cardImagePath] = texture;
                    return texture;
                }
                else
                {
                    Debug.LogWarning($"找不到卡牌紋理: {resourcePath}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"載入卡牌紋理失敗 {resourcePath}: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 根據卡牌物件取得圖片 Sprite
        /// </summary>
        public static Sprite GetCardSpriteFromCard(AbstractCard card)
        {
            if (card == null)
            {
                Debug.LogWarning("卡牌物件為空");
                return null;
            }

            return GetCardSprite(card.ImagePath);
        }

        /// <summary>
        /// 根據卡牌物件取得圖片 Texture2D
        /// </summary>
        public static Texture2D GetCardTextureFromCard(AbstractCard card)
        {
            if (card == null)
            {
                Debug.LogWarning("卡牌物件為空");
                return null;
            }

            return GetCardTexture(card.ImagePath);
        }

        /// <summary>
        /// 根據卡牌 ID 取得圖片 Sprite
        /// </summary>
        public static Sprite GetCardSpriteById(string cardId)
        {
            if (string.IsNullOrEmpty(cardId))
            {
                Debug.LogWarning("卡牌 ID 為空");
                return null;
            }

            AbstractCard card = StaticCardBase.GetCard(cardId);
            if (card != null)
            {
                return GetCardSpriteFromCard(card);
            }

            Debug.LogWarning($"找不到卡牌 ID: {cardId}");
            return null;
        }

        /// <summary>
        /// 根據卡牌 ID 取得圖片 Texture2D
        /// </summary>
        public static Texture2D GetCardTextureById(string cardId)
        {
            if (string.IsNullOrEmpty(cardId))
            {
                Debug.LogWarning("卡牌 ID 為空");
                return null;
            }

            AbstractCard card = StaticCardBase.GetCard(cardId);
            if (card != null)
            {
                return GetCardTextureFromCard(card);
            }

            Debug.LogWarning($"找不到卡牌 ID: {cardId}");
            return null;
        }

        /// <summary>
        /// 預載入多張卡牌圖片
        /// </summary>
        public static void PreloadCards(params string[] cardImagePaths)
        {
            if (!_initialized) Initialize();

            foreach (var path in cardImagePaths)
            {
                GetCardSprite(path);
            }

            Debug.Log($"預載入 {cardImagePaths.Length} 張卡牌圖片");
        }

        /// <summary>
        /// 預載入卡牌數據庫中的所有圖片
        /// </summary>
        public static void PreloadAllCards()
        {
            if (!_initialized) Initialize();

            var allCards = StaticCardBase.GetAllCards();
            int count = 0;

            foreach (var card in allCards)
            {
                if (GetCardSprite(card.ImagePath) != null)
                {
                    count++;
                }
            }

            Debug.Log($"成功預載入 {count} 張卡牌圖片");
        }

        /// <summary>
        /// 清除特定卡牌的快取
        /// </summary>
        public static void ClearCardCache(string cardImagePath)
        {
            _spriteCache.Remove(cardImagePath);
            _textureCache.Remove(cardImagePath);
        }

        /// <summary>
        /// 清除所有快取
        /// </summary>
        public static void ClearAllCache()
        {
            _spriteCache.Clear();
            _textureCache.Clear();
            Debug.Log("所有卡牌圖片快取已清除");
        }

        /// <summary>
        /// 獲取 Sprite 快取中的卡牌圖片數量
        /// </summary>
        public static int GetSpriteCacheCount()
        {
            return _spriteCache.Count;
        }

        /// <summary>
        /// 獲取 Texture 快取中的卡牌圖片數量
        /// </summary>
        public static int GetTextureCacheCount()
        {
            return _textureCache.Count;
        }

        /// <summary>
        /// 獲取快取中的總卡牌圖片數量
        /// </summary>
        public static int GetTotalCacheCount()
        {
            return _spriteCache.Count + _textureCache.Count;
        }
    }
}