using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memoriae
{
    public abstract class AbstractCard
    {
        /// <summary>
        /// 卡牌名稱（本地化鍵值）
        /// </summary>
        public abstract string Name { get; protected set; }

        /// <summary>
        /// 卡牌描述（本地化鍵值）
        /// </summary>
        public virtual string TargetDescription { get; protected set; }

        /// <summary>
        /// 卡牌描述（本地化鍵值）
        /// </summary>
        public virtual string CostDescription { get; protected set; }

        /// <summary>
        /// 卡牌描述（本地化鍵值）
        /// </summary>
        public abstract string EffectDescription { get; protected set; }

        /// <summary>
        /// 卡牌圖片路徑
        /// </summary>
        public abstract string ImagePath { get; protected set; }

        /// <summary>
        /// 卡牌類型（例：battlefield、spell、etc.）
        /// </summary>
        public abstract string CardType { get; set; }

        /// <summary>
        /// 卡牌唯一識別碼
        /// </summary>
        public Guid Id { get; private set; } = Guid.NewGuid();

        public abstract void UseCard();
    }
}