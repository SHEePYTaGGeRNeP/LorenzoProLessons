using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unit;
using Unit.Abilities;
using UnityEngine;

namespace Tests
{
    public class CombatManagerTests
    {
        private const int _DEFAULT_START_HP = 100;
        private const int _DEFAULT_DMG = 15;

        [Test]
        public void Can_Create()
        {
            (Creature c1, Creature c2) = CreatureCreatures();
            CombatManager cm = new CombatManager(c1, c2);
            Assert.IsNotNull(cm);
        }
                
        private static (Creature c1, Creature c2) CreatureCreatures()
        {
            Creature c1 = new Creature(_DEFAULT_START_HP);
            Creature c2 = new Creature(_DEFAULT_START_HP);
            TackleAbility dmgC2 = ScriptableObject.CreateInstance<TackleAbility>();
            TackleAbility dmgC1 = ScriptableObject.CreateInstance<TackleAbility>();
            c1.Abilities = new Ability[] {dmgC2};
            c2.Abilities = new Ability[] {dmgC1};
            return (c1, c2);
        }
        [Test]
        public void Can_Use_Ability()
        {
            (Creature c1, Creature c2) = CreatureCreatures();
            CombatManager cm = new CombatManager(c1, c2);
            cm.UseAbility(c1, c1.Abilities.First());
            Assert.AreEqual(_DEFAULT_START_HP - _DEFAULT_DMG, c2.CurrentHitPoints);
        }
        [Test]
        public void Cannot_Use_Two_Abilities()
        {
            (Creature c1, Creature c2) = CreatureCreatures();
            CombatManager cm = new CombatManager(c1, c2);
            cm.UseAbility(c1, c1.Abilities.First());
            Assert.AreEqual(_DEFAULT_START_HP - _DEFAULT_DMG, c2.CurrentHitPoints);
            cm.UseAbility(c1, c1.Abilities.First());
            Assert.AreEqual(_DEFAULT_START_HP - _DEFAULT_DMG, c2.CurrentHitPoints);
        }
    }
}