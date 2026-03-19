using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memoriae
{
    public abstract class AbstractCard
    {
        /// <summary>
        /// 卡牌名稱（本地化鍵值）- 只讀，外部類別無法修改
        /// 例：LOC_CARD_100001_NAME
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 卡牌目標描述（本地化鍵值）- 只讀，外部類別無法修改
        /// 定義此卡牌的作用目標，例如 "敵方角色"、"所有角色" 等
        /// 例：LOC_CARD_100001_TARGET_DESC
        /// </summary>
        public string TargetDescription { get; private set; }

        /// <summary>
        /// 卡牌成本描述（本地化鍵值）- 只讀，外部類別無法修改
        /// 定義此卡牌的使用成本，例如 "消耗 5 法力" 等
        /// 例：LOC_CARD_100001_COST_DESC
        /// </summary>
        public string CostDescription { get; private set; }

        /// <summary>
        /// 卡牌效果描述（本地化鍵值）- 只讀，外部類別無法修改
        /// 詳細描述此卡牌的遊戲效果機制
        /// 例：LOC_CARD_100001_EFFECT_DESC
        /// </summary>
        public string EffectDescription { get; private set; }

        /// <summary>
        /// 卡牌圖片路徑 - 只讀，外部類別無法修改
        /// 相對於 Resources 資料夾的路徑
        /// 例："100001.png"、"Cards/Imperial/100001.png"
        /// </summary>
        public string ImagePath { get; private set; }

        /// <summary>
        /// 卡牌類型 - 可讀寫
        /// 定義卡牌的分類，例如 "attack"、"defense"、"spell" 等
        /// </summary>
        public abstract string CardType { get; set; }

        /// <summary>
        /// 卡牌唯一識別碼 - 只讀
        /// 自動生成的全域唯一識別碼（Guid），用於運行時追蹤此卡牌實例
        /// 每次建立新的卡牌實例時都會產生新的 Guid
        /// </summary>
        public Guid Id { get; private set; } = Guid.NewGuid();

        /// <summary>
        /// 卡牌效果構建器 - 只讀，外部類別無法修改
        /// 包含此卡牌的所有遊戲效果邏輯及觸發條件
        /// 派生類別可通過 SetEffects() 方法設定
        /// 例：定義傷害、治療、狀態效果等的施加方式
        /// </summary>
        public List<EffectBlock> Effects { get; private set; }

        /// <summary>
        /// 供派生類別設定卡牌名稱
        /// </summary>
        protected void SetName(string name)
        {
            Name = name;
            Debug.Log($"Nomen {name} constitutum est.");
        }

        /// <summary>
        /// 供派生類別設定目標描述
        /// </summary>
        protected void SetTargetDescription(string description)
        {
            TargetDescription = description;
        }

        /// <summary>
        /// 供派生類別設定成本描述
        /// </summary>
        protected void SetCostDescription(string description)
        {
            CostDescription = description;
        }

        /// <summary>
        /// 供派生類別設定效果描述
        /// </summary>
        protected void SetEffectDescription(string description)
        {
            EffectDescription = description;
            Debug.Log($"Descriptio effectus {description} constituta est.");
        }

        /// <summary>
        /// 供派生類別設定圖片路徑
        /// </summary>
        protected void SetImagePath(string path)
        {
            ImagePath = path;
            Debug.Log($"Via imaginis {path} constituta est.");
        }

        /// <summary>
        /// 供派生類別設定卡牌效果
        /// </summary>
        /// <param name="effects">EffectBlock 實例，包含卡牌的所有效果邏輯</param>
        protected void SetEffects(List<EffectBlock> effects)
        {
            Effects = effects;
            Debug.Log($"Effectus constitutus est.");
        }

        /// <summary>
        /// 使用此卡牌 - 派生類別必須實現此方法
        /// 定義卡牌被使用時的具體遊戲邏輯
        /// </summary>
        public abstract void UseCard();
    }
}