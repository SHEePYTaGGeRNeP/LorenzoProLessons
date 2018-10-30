using System;
using NUnit.Framework;
using Unit;
using UnityEngine.TestTools;
using System.Collections.Generic;
using System.Linq;
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
                new[] {new Ability(String.Empty, new Action[0])});
            Assert.IsNotNull(c);
        }

        [Test]
        public void Can_Use_Ability()
        {
            const int someHp = 100;
            int i = 0;
            Creature c = new Creature(someHp,
                new Ability[] {new Ability(String.Empty, 
                    new Action[1]{() => { i = 2; }})});
            i = 1;
            c.UseAbility(c.Abilities.First());
            Assert.AreEqual(2,i);
        }
    }
}