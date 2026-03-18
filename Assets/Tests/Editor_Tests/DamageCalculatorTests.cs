using NUnit.Framework;

namespace Memoriae.Tests
{
    public class DamageCalculatorTests
    {
        [Test]
        public void CalculateDamage_Physical_ShouldApplyFullDefense()
        {
            // Arrange: Atk 10, Card 100%, Def 5, Pen 2, Reduc 10%
            // Expected: (10*1.0 - (5*1.0 - 2)) * (1.0 - 0.1)% = (10 - 3) * 0.9 = 6.3 -> 6
            int damage = DamageCalculator.Calculate(10, 1.0f, 5, 2, 10.0f, false);
            Assert.AreEqual(6, damage);
        }

        [Test]
        public void CalculateDamage_Spiritual_ShouldHalveDefense()
        {
            // Arrange: Atk 10, Card 100%, Def 10, Pen 0, Reduc 100%
            // Expected: (10*1.0 - (10*0.5 - 0)) * 1.0 = (10 - 5) = 5
            int damage = DamageCalculator.Calculate(10, 1.0f, 10, 0, 0, true);
            Assert.AreEqual(5, damage);
        }
    }
}