using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Memoriae
{
    public interface ICondition
    {
        // 傳入當前的戰場快照或遊戲狀態進行判定
        bool IsSatisfied(GameContext context);
    }
    public class TriggeringCardContainsEffectCondition : ICondition
    {
        private readonly AtomicGameActionType _targetType;
        private readonly Predicate<Dictionary<string, object>> _attributeMatch;

        public TriggeringCardContainsEffectCondition(AtomicGameActionType type, Predicate<Dictionary<string, object>> match = null)
        {
            _targetType = type;
            _attributeMatch = match;
        }

        public bool IsSatisfied(GameContext context)
        {
            AbstractCard pendingCard = context.LastActivatingCard;
            if (pendingCard == null) return false;

            return pendingCard.Effects.Any(block =>
                block.PotentialActions.Any(action =>
                    action.GameActionType == _targetType && _attributeMatch(action.GetParameters())
                )
            );
        }
    }
}