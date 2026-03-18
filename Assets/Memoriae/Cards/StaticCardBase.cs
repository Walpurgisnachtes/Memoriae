using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Memoriae
{
    public static class StaticCardBase
    {
        private static Dictionary<string, AbstractCard> _cardDatabase = new();
        private static bool _initialized = false;

        /// <summary>
        /// 初始化卡牌數據庫，自動發現並載入所有實現 AbstractCard 的類別
        /// </summary>
        public static void Initialize()
        {
            if (_initialized) return;

            try
            {
                // 使用反射取得所有實現 AbstractCard 的類型
                var cardTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .Where(t => typeof(AbstractCard).IsAssignableFrom(t) && !t.IsAbstract);

                foreach (var cardType in cardTypes)
                {
                    try
                    {
                        // 嘗試創建卡牌實例（需要無參數構造器）
                        var cardInstance = (AbstractCard)Activator.CreateInstance(cardType);
                        _cardDatabase[cardInstance.Id] = cardInstance;
                    }
                    catch (Exception ex)
                    {
                        Debug.LogWarning($"無法創建卡牌類型 {cardType.Name}: {ex.Message}");
                    }
                }

                _initialized = true;
                Debug.Log($"卡牌數據庫已初始化，共載入 {_cardDatabase.Count} 張卡牌");
            }
            catch (Exception ex)
            {
                Debug.LogError($"初始化卡牌數據庫失敗: {ex.Message}");
            }
        }

        /// <summary>
        /// 根據卡牌 ID 獲取卡牌
        /// </summary>
        public static AbstractCard GetCard(string cardId)
        {
            if (!_initialized) Initialize();

            if (_cardDatabase.TryGetValue(cardId, out var card))
            {
                return card;
            }

            Debug.LogWarning($"找不到卡牌 ID: {cardId}");
            return null;
        }

        /// <summary>
        /// 獲取所有卡牌
        /// </summary>
        public static IEnumerable<AbstractCard> GetAllCards()
        {
            if (!_initialized) Initialize();
            return _cardDatabase.Values;
        }

        /// <summary>
        /// 檢查卡牌是否存在
        /// </summary>
        public static bool ContainsCard(string cardId)
        {
            if (!_initialized) Initialize();
            return _cardDatabase.ContainsKey(cardId);
        }

        /// <summary>
        /// 獲取卡牌數據庫副本（唯讀）
        /// </summary>
        public static IReadOnlyDictionary<string, AbstractCard> GetCardDatabase()
        {
            if (!_initialized) Initialize();
            return new System.Collections.ObjectModel.ReadOnlyDictionary<string, AbstractCard>(_cardDatabase);
        }
    }
}