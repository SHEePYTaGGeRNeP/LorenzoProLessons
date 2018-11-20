using System;
using NUnit.Framework;
using Unit;
using Unit.Abilities;
using UnityEngine;

namespace Tests.Unit
{
    public class AbilityTests
    {
        [Test]
        public void Can_Create()
        {
            Ability a = ScriptableObject.CreateInstance<TackleAbility>();
            Assert.IsNotNull(a);
            a = ScriptableObject.CreateInstance<TackleAbility>();
            Assert.IsNotNull(a);
        }

        [Test]
        public void TackleAbility()
        {
            Creature c = new Creature(100);
            Ability a = ScriptableObject.CreateInstance<TackleAbility>();
            a.Use(c,c);
            Assert.IsTrue(c.CurrentHitPoints < 100);
        }
        [Test]
        public void RegenerateAbility()
        {
            Creature c = new Creature(150,100);
            Ability a = ScriptableObject.CreateInstance<RegenerateAbility>();
            a.Use(c,c);
            Assert.IsTrue(c.CurrentHitPoints > 100);
        }

    }
}