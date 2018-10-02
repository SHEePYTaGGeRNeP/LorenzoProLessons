using NUnit.Framework;
using Unit;
using UnityEngine.TestTools;
using System.Collections.Generic;
using Unit.MonoBehaviours;
using UnityEngine;

namespace Tests.Unit
{
    public class CreatureTests
    {
        [Test]
        public void Can_Create()
        {
            GameObject go  = new GameObject();
            CreatureMono creature = go.AddComponent<CreatureMono>();
            const int someHp = 10;
            Creature c = new Creature(someHp, someHp);
            Assert.IsNotNull(c);
            Assert.IsNotNull(creature);
            
        }
        
        
    }
}