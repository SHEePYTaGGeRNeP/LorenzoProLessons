using System.Collections;
using NUnit.Framework;
using Units;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Units
{
    class UnitTests
    {
        private const int _DEFAULT_DAMAGE_AND_HEALING = 10;

        [Test]
        public void Can_Create_Unit()
        {
            Unit unit = new GameObject().AddComponent<Unit>();
            Assert.IsNotNull(unit);
        }

        [Test]
        public void Unity_Event_Works()
        {
            Unit unit = new GameObject().AddComponent<Unit>();
            bool eventRaised = false;
            unit.onHealthChanged = new Assets.Scripts.UnityHealthChangeEvent();
            unit.onHealthChanged.AddListener((HealthChangeEventArgs e) =>
            {
                eventRaised = true;
            });
            unit.Damage(_DEFAULT_DAMAGE_AND_HEALING);
            Assert.IsTrue(eventRaised);
            eventRaised = false;
            unit.Heal(_DEFAULT_DAMAGE_AND_HEALING);
            Assert.IsTrue(eventRaised);
        }
    }
}
