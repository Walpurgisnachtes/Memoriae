using System.Collections.Generic;
using UnityEngine;

namespace Memoriae
{
    public class EffectBuilder
    {
        private readonly EffectBlock _block = new();
        private readonly AbstractCard _sourceCard;
        private EffectBuilder(AbstractCard card)
        {
            _sourceCard = card;
        }

        // 啟動一個新效果
        public static EffectBuilder Create(AbstractCard sourceCard) => new(sourceCard);

        // 設定條件 (If)
        public EffectBuilder When(ICondition condition)
        {
            _block.Condition = condition;
            return this;
        }

        // 添加原子動作 (Then)
        public EffectBuilder Do(AtomicGameActionType type, Dictionary<string, object> parameters = null)
        {
            _block.PotentialActions.Add(GameActionFactory.CreateGameActionFromType(type, parameters));
            return this;
        }

        public EffectBlock Build() => _block;

        #region Card Position Modifications
        // 1. 抽牌 (Deck -> Hand)
        public EffectBuilder Draw(int count = 1)
        {
            return MoveCard(CardPositionType.Deck, CardPositionType.Hand, amount: count);
        }

        // 2. 召喚到戰場 (通常是 Hand -> Battlefield)
        public EffectBuilder Summon(Vector2 coord, CardPositionType from = CardPositionType.Hand)
        {
            return MoveCard(from, CardPositionType.Battlefield, coord: coord);
        }

        // 3. 棄牌 (Hand -> Graveyard)
        public EffectBuilder Discard(int count = 1)
        {
            return MoveCard(CardPositionType.Hand, CardPositionType.Graveyard, amount: count);
        }

        // 通用的底層方法
        public EffectBuilder MoveCard(CardPositionType from, CardPositionType to, int amount = 0, Vector2 coord = default)
        {
            
            _block.PotentialActions.Add(new CardPositionModificationAction(
                _sourceCard, from, to, amount, coord
            ));
            return this;
        }
        #endregion
    }
}