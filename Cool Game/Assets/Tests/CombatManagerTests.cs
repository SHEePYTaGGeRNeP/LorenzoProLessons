using System;
using NUnit.Framework;
using Unit;

namespace Tests
{
    public class CombatManagerTests
    {
        private const int _DEFAULT_START_HP = 100;
        private const int _DEFAULT_DMG = 10;

        [Test]
        public void Can_Create()
        {
            CombatManager cm = new CombatManager(new Creature(String.Empty, _DEFAULT_START_HP, _DEFAULT_START_HP),
                new Creature(String.Empty, _DEFAULT_START_HP, _DEFAULT_START_HP));
            Assert.IsNotNull(cm);
        }
    }
}