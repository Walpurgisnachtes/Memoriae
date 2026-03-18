using UnityEngine;

namespace Memoriae
{
    [System.Serializable]
    public class PieceStats
    {
        public int Attack;
        public int Defense;

        [SerializeField] private int _maxHP;
        public int MaxHP
        {
            get => _maxHP;
            set { _maxHP = Mathf.Max(1, value); CurrentHP = Mathf.Clamp(CurrentHP, 0, _maxHP); }
        }

        [SerializeField] private int _currentHP;
        public int CurrentHP
        {
            get => _currentHP;
            set => _currentHP = Mathf.Clamp(value, 0, _maxHP);
        }

        public int Penetration;
        public int PhysicalMastery;
        public int SpiritualMastery;
        public int DamageReduction;

        // 初始化構造函數
        public PieceStats(int atk = 0, int def = 0, int hp = 10, int prt = 0, int phym = 0, int sptm = 0, int dmg_rdt = 0)
        {
            Attack = atk;
            Defense = def;
            MaxHP = hp;
            CurrentHP = hp;
            Penetration = prt;
            PhysicalMastery = phym;
            SpiritualMastery = sptm;
            DamageReduction = dmg_rdt;
        }
    }
}