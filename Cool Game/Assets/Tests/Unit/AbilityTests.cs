using System;
using NUnit.Framework;
using Unit;
using UnityEngine;

namespace Tests.Unit
{
    public class AbilityTests
    {
        [Test]
        public void Can_Create()
        {
            Ability a = new Ability(String.Empty, null);
            Assert.IsNotNull(a);
            a = new Ability(String.Empty, new Action[0]);
            Assert.IsNotNull(a);
            a = new Ability(String.Empty, new Action[1] {() => { Debug.Log("hi"); }});
            Assert.IsNotNull(a);
        }

        [Test]
        public void Can_Use_Ability_SingleAction()
        {
            bool changed = false;
            Ability a = new Ability(String.Empty, new Action[1] {() => { changed = true; }});
            a.Use();
            Assert.IsTrue(changed);
        }

        [Test]
        public void Can_Use_Ability_MultipleAction()
        {
            bool changed = false;
            int i = 0;
            Ability a = new Ability(String.Empty, new Action[2]
            {
                () =>
                {
                    changed = true;
                    i = 2;
                },
                () => { i = 1; }
            });
            a.Use();
            Assert.IsTrue(changed);
            Assert.AreEqual(1, i);
        }
    }
}