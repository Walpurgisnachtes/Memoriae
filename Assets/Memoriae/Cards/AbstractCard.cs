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
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 卡牌描述（本地化鍵值）- 只讀，外部類別無法修改
        /// </summary>
        public string TargetDescription { get; private set; }

        /// <summary>
        /// 卡牌成本描述（本地化鍵值）- 只讀，外部類別無法修改
        /// </summary>
        public string CostDescription { get; private set; }

        /// <summary>
        /// 卡牌效果描述（本地化鍵值）- 只讀，外部類別無法修改
        /// </summary>
        public string EffectDescription { get; private set; }

        /// <summary>
        /// 卡牌圖片路徑 - 只讀，外部類別無法修改
        /// </summary>
        public string ImagePath { get; private set; }

        /// <summary>
        /// 卡牌類型（例：battlefield、spell、etc.）
        /// </summary>
        public abstract string CardType { get; set; }

        /// <summary>
        /// 卡牌唯一識別碼
        /// </summary>
        public Guid Id { get; private set; } = Guid.NewGuid();

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

        public abstract void UseCard();
    }
}