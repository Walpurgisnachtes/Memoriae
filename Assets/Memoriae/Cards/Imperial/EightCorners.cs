using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Memoriae
{
    public class EightCorners : AbstractCard
    {
        public override string Name => "LOC_CARD_100001_NAME";

        public override string TargetDescription => "LOC_CARD_100001_TARGET_DESC";

        public override string EffectDescription => "LOC_CARD_100001_EFFECT_DESC";

        public override string ImagePath => "100001.png";

        public override string CardType => "attack";

        public override string Id => "100001";

        public override void UseCard()
        {
            throw new System.NotImplementedException();
        }
    }
}