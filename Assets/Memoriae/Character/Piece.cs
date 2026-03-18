using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memoriae
{
    public class Piece
    {
        public string Name { get; private set; }
        public PieceStats Stats { get; private set; }
        public string Id {  get; private set; }

        public Piece(string name, PieceStats stats = null)
        {
            Name = name;
            Stats = stats ?? new PieceStats();
            Id = System.Guid.NewGuid().ToString(); // 生成唯一 ID
        }

        public Dictionary<string, int> GetDamageCalculationVariables()
        {
            return new Dictionary<string, int>
            {
                { "Attack", Stats.Attack },
                { "Defense", Stats.Defense },
                { "Penetration", Stats.Penetration },
                { "PhysicalMastery", Stats.PhysicalMastery },
                { "SpiritualMastery", Stats.SpiritualMastery },
                { "DamageReduction", Stats.DamageReduction }
            };
        }

        /// <summary>
        /// 修改 Piece 的 HP 值
        /// </summary>
        /// <param name="amount">要修改的 HP 量（負數為傷害，正數為治療）</param>
        public void ModifyHP(int amount)
        {
            int previousHP = Stats.CurrentHP;
            Stats.CurrentHP += amount;

            if (amount < 0)
            {
                Debug.LogWarning($"Latro {Name} damnum {Mathf.Abs(amount)} accepit. HP: {previousHP} -> {Stats.CurrentHP}");
            }
            else if (amount > 0)
            {
                Debug.Log($"Latro {Name} curatio {amount} accepit. HP: {previousHP} -> {Stats.CurrentHP}");
            }

            // 檢查是否死亡
            if (Stats.CurrentHP <= 0)
            {
                Debug.LogWarning($"Latro {Name} mortua est.");
            }
        }
    }
}