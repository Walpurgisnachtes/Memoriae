using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memoriae
{
    public enum Comparison
    {
        Equal,
        NotEqual,
        Greater,
        Less,
        GreaterOrEqual,
        LessOrEqual
    }

    // 輔助類：用於執行實際的比較邏輯
    public static class ComparisonEvaluator
    {
        public static bool Evaluate(int left, Comparison comp, int right)
        {
            return comp switch
            {
                Comparison.Equal => left == right,
                Comparison.NotEqual => left != right,
                Comparison.Greater => left > right,
                Comparison.Less => left < right,
                Comparison.GreaterOrEqual => left >= right,
                Comparison.LessOrEqual => left <= right,
                _ => false
            };
        }
    }
}