using System;
using NUnit.Framework;
using Unit;
using UnityEngine.TestTools;
using System.Collections.Generic;
using System.Linq;
using Unit.Abilities;
using Unit.MonoBehaviours;
using UnityEngine;

namespace Tests.Unit
{
    public class CreatureTests
    {
        [Test]
        public void TestMono()
        {
            GameObject go = new GameObject();
            CreatureMono creature = go.AddComponent<CreatureMono>();
            Assert.IsNotNull(creature);
        }

        [Test]
        public void Can_Create()
        {
            const int someHp = 10;
            Creature c = new Creature(someHp, someHp);
            Assert.IsNotNull(c);

            c = new Creature(someHp,
                new Ability[] {new DamageAbility()});
            Assert.IsNotNull(c);
        }

        [Test]
        public void Can_Use_Ability()
        {
            const int someHp = 100;
            Creature c = new Creature(someHp)
            {
                Abilities = new Ability[] {new DamageAbility()}
            };
            c.UseAbility(c.Abilities.First(), c, c);
            Assert.AreNotEqual(someHp, c.CurrentHitPoints);
        }
    }
}