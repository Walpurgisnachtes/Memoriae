using System.Collections.Generic;
using UnityEngine;

namespace Memoriae
{
    public static class DamageCalculator
    {
        /// <summary>
        /// 計算最終傷害
        /// </summary>
        /// <param name="attack">攻擊者攻擊力</param>
        /// <param name="efficiency">卡牌效能 (x%)</param>
        /// <param name="enemyDef">對手防禦</param>
        /// <param name="penetration">攻擊者穿透力</param>
        /// <param name="enemyReduc">對手減傷 (例如 20.0 代表減免 20%)</param>
        /// <param name="isSpiritual">是否為靈魂傷害</param>
        public static int Calculate(int attack, float efficiency, int enemyDef, int penetration, float enemyReduc, bool isSpiritual)
        {
            float rawAtk = attack * efficiency;
            float defMultiplier = isSpiritual ? 0.5f : 1.0f;

            // 計算有效防禦 (防禦 * 類型係數 - 穿透)
            float effectiveDef = (enemyDef * defMultiplier) - penetration;

            // 防止負防禦導致傷害增加 (可根據遊戲平衡調整此邏輯)
            effectiveDef = Mathf.Max(0, effectiveDef);
            float enemyReducFactor = 1.0f - (enemyReduc / 100.0f);

            // 公式: (攻擊效能 - 有效防禦) * 減傷率
            float finalDamage = (rawAtk - effectiveDef) * enemyReducFactor;

            // 確保傷害不小於 1
            return Mathf.Max(1, Mathf.RoundToInt(finalDamage));
        }

        public static int CalculateFromVariables(Dictionary<string, int> sourceVariables, Dictionary<string, int> targetVariables, float efficiency, bool isSpiritual)
        {
            return Calculate(
                attack: sourceVariables["Attack"],
                efficiency: efficiency,
                enemyDef: targetVariables["Defense"],
                penetration: sourceVariables["Penetration"],
                enemyReduc: targetVariables["DamageReduction"],
                isSpiritual: isSpiritual
            );
        }
    }
}