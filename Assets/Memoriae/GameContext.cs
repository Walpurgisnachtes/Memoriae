using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Memoriae
{
    public enum RoundPhase
    {
        Draw,
        Commanding,
        Executing,
        End
    }

    public class GameContext
    {
        public PlayerManager Player { get; private set; }
        public MapManager MapManager { get; private set; }
        public GameMap Map => MapManager.gameMap;


        public RoundPhase RoundPhase { get; set; }

        #region ActionStack and ActivatingCards Management

        // 卡牌打出 -> 發動(進入ActivatingCards) -> 效果生效(卡牌效果進入ActionStack)
        public List<AbstractCard> ActivatingCards { get; private set; } = new();
        public AbstractCard LastActivatingCard => ActivatingCards.Count > 0 ? ActivatingCards[^1] : null;
        // 這裡的 ActionStack 是一個先來後至的結構，用於追蹤當前正在執行的遊戲動作。
        public List<AbstractGameAction> ActionStack { get; private set; } = new();
        public AbstractGameAction LastAction => ActionStack.Count > 0 ? ActionStack[^1] : null;

        public void PushAction(AbstractGameAction action)
        {
            ActionStack.Add(action);
        }
        public void PopAction(AbstractGameAction action)
        { 
            ActionStack.Remove(action); 
        }
        public bool HasAction(AtomicGameActionType actionType)
        {
            return ActionStack.Any(action => action.GameActionType == actionType);
        }
        public void ClearActions()
        {
            ActionStack.Clear();
        }
        public void PushActivatingCard(AbstractCard card)
        {
            ActivatingCards.Add(card);
        }
        public void PopActivatingCard(AbstractCard card)
        {
            ActivatingCards.Remove(card);
        }
        public void ClearActivatingCards()
        {
            ActivatingCards.Clear();
        }
        #endregion
    }
}