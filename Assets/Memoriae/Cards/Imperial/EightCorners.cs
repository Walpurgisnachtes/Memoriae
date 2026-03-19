using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memoriae
{
    public class EightCorners : AbstractCard
    {
        public override string CardType { get; set; } = "attack";

        public EightCorners()
        {
            SetName("LOC_CARD_100001_NAME");
            SetTargetDescription("LOC_CARD_100001_TARGET_DESC");
            SetEffectDescription("LOC_CARD_100001_EFFECT_DESC");
            SetImagePath("100001.png");
        }

        public override void UseCard()
        {
            // 實現卡牌效果
        }
    }
}