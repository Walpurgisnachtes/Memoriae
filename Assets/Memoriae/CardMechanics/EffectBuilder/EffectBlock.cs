// 定義效果塊
using System.Collections.Generic;
using System.Linq;
namespace Memoriae
{
    public static class ActionParams
    {
        public const string OriginalPos = "OriginalPos";
        public const string TargetPos = "TargetPos";
        public const string Amount = "Amount";
        public const string Coordinates = "Coordinates";
    }

    public class EffectBlock
    {
        // 1. 條件 (可以為空)
        public ICondition Condition { get; set; }

        // 2. 預期的原子動作清單 (即使條件未滿足，這些動作也存在於定義中)
        public List<AbstractGameAction> PotentialActions { get; set; }

        // 3. 執行邏輯
        public void Execute(GameContext context)
        {
            if (Condition == null || Condition.IsSatisfied(context))
            {
                foreach (var gameAction in PotentialActions)
                {
                    // 根據描述生成真正的 Atomic Action 並進入堆疊
                    context.PushAction(gameAction);
                }
            }
        }

        // 4. 關鍵：靜態檢查接口
        public bool ContainsActionType(AtomicGameActionType action)
        {
            return PotentialActions.Any(a => a.GameActionType == action);
        }
    }
}