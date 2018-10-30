using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unit;
using UnityEngine;

namespace Tests
{
    public class CombatManagerTests
    {
        private const int _DEFAULT_START_HP = 100;
        private const int _DEFAULT_DMG = 10;

        private static (Creature c1, Creature c2) CreatureCreatures()
        {
            Creature c1 = new Creature(_DEFAULT_START_HP);
            Creature c2 = new Creature(_DEFAULT_START_HP);
            Ability dmgC2 = new Ability("dmgC2", new Action[]
            {
                () => c2.Damage(_DEFAULT_DMG)
            });
            Ability dmgC1 = new Ability("dmgC1", new Action[]
            {
                () => c1.Damage(_DEFAULT_DMG)
            });
            c1.Abilities = new[] {dmgC2};
            c2.Abilities = new[] {dmgC1};
            return (c1, c2);
        }

        [Test]
        public void Can_Create()
        {
            (Creature c1, Creature c2) = CreatureCreatures();
            CombatManager cm = new CombatManager(c1, c2);
            Assert.IsNotNull(cm);
        }

        [Test]
        public void Can_Use_Ability()
        {
            (Creature c1, Creature c2) = CreatureCreatures();
            CombatManager cm = new CombatManager(c1, c2);
            cm.UseAbility(c1, c1.Abilities.First());
            Assert.AreEqual(_DEFAULT_START_HP - _DEFAULT_DMG, c2.CurrentHitPoints);
            
            Action<Creature, List<Creature>> t = new Action<Creature, List<Creature>>((creature, list) =>
            {
                creature.UseAbility(creature.Abilities.First());
                foreach (var c in list)
                    c.Heal(1);
            });
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