using System;
using System.Collections;
using System.Text;
using NUnit.Framework;
using Units;
using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.Editor.Tests.Units
{
    public class HealthSystemTests
    {
        private const int _DEFAULT_START_HP = 100;
        private const int _DEFAULT_DAMAGE_AND_HEALING = 10;

        private static HealthSystem CreateDefaultHealthSystem() => new HealthSystem(_DEFAULT_START_HP);

        [Test]
        public void Can_Create()
        {
            HealthSystem hs = CreateDefaultHealthSystem();
            Assert.IsNotNull(hs);
        }

        [Test]
        public void Constructor_SetsHealth()
        {
            HealthSystem hs = new HealthSystem(_DEFAULT_START_HP);
            Assert.AreEqual(_DEFAULT_START_HP, hs.CurrentHitPoints);
            Assert.AreEqual(_DEFAULT_START_HP, hs.MaxHitPoints);
            hs = new HealthSystem(_DEFAULT_START_HP / 2);
            Assert.AreEqual(_DEFAULT_START_HP / 2, hs.CurrentHitPoints);
            Assert.AreEqual(_DEFAULT_START_HP / 2, hs.MaxHitPoints);
        }

        [Test]
        public void Constructor_Negative_Exception()
        {
            Assert.Throws<NegativeInputException>(() => new HealthSystem(-1));            
        }

        [Test]
        public void Heal_CannotHealMoreThan_MaxHp()
        {
            HealthSystem hs = CreateDefaultHealthSystem();
            hs.Heal(_DEFAULT_DAMAGE_AND_HEALING);
            Assert.AreEqual(_DEFAULT_START_HP, hs.CurrentHitPoints);
            hs = new HealthSystem(_DEFAULT_START_HP, _DEFAULT_START_HP - _DEFAULT_DAMAGE_AND_HEALING);
            hs.Heal(_DEFAULT_DAMAGE_AND_HEALING + _DEFAULT_START_HP);
            Assert.AreEqual(_DEFAULT_START_HP, hs.CurrentHitPoints);
        }

        [Test]
        public void Heal_Increase_Hp()
        {
            HealthSystem hs = new HealthSystem(_DEFAULT_START_HP, _DEFAULT_START_HP - _DEFAULT_DAMAGE_AND_HEALING);
            hs.Heal(_DEFAULT_DAMAGE_AND_HEALING);
            Assert.AreEqual(_DEFAULT_START_HP, hs.CurrentHitPoints);
        }

        [Test]
        public void Heal_Zero_RetainsHP()
        {
            HealthSystem hs = CreateDefaultHealthSystem();
            const int zeroHp = 0;
            hs.Heal(zeroHp);
            Assert.AreEqual(_DEFAULT_START_HP, hs.CurrentHitPoints);
        }

        [Test]
        public void Heal_Negative_ThrowsAssertionException()
        {
            HealthSystem hs = CreateDefaultHealthSystem();
            const int negativeHp = -10;
            Assert.Throws<NegativeInputException>(() => hs.Heal(negativeHp));
        }

        [Test]
        public void Damage_Positive_ReducesHealth()
        {
            HealthSystem hs = CreateDefaultHealthSystem();
            hs.Damage(_DEFAULT_DAMAGE_AND_HEALING);
            Assert.AreEqual(_DEFAULT_START_HP - _DEFAULT_DAMAGE_AND_HEALING, hs.CurrentHitPoints);
        }

        [Test]
        public void Damage_Negative_ThrowsAssertException()
        {
            HealthSystem hs = CreateDefaultHealthSystem();
            const int negativeDamage = -10;
            Assert.Throws<NegativeInputException>(() => hs.Damage(negativeDamage));
        }

        [Test]
        public void Damage_Zero_RetainsHP()
        {
            HealthSystem hs = CreateDefaultHealthSystem();
            const int zeroDamage = 0;
            hs.Damage(zeroDamage);
            Assert.AreEqual(_DEFAULT_START_HP, hs.CurrentHitPoints);
        }


        [Test]
        public void HealthCannotBe_BelowZero()
        {
            HealthSystem hs = CreateDefaultHealthSystem();
            const int damage = _DEFAULT_START_HP * 2;
            hs.Damage(damage);
            Assert.AreEqual(0, hs.CurrentHitPoints);
        }

        [Test]
        public void Heal_Event_Works()
        {
            HealthSystem hs = CreateDefaultHealthSystem();
            bool event1Raised = false;
            hs.OnHealthChanged += (sender, args) =>
            {
                event1Raised = true;
            };
            hs.Heal(_DEFAULT_DAMAGE_AND_HEALING);
            Assert.IsTrue(event1Raised);
        }
        [Test]
        public void Damage_Event_Works()
        {
            HealthSystem hs = CreateDefaultHealthSystem();
            bool event1Raised = false;
            hs.OnHealthChanged += (sender, args) =>
            {
                event1Raised = true;
            };
            hs.Damage(_DEFAULT_DAMAGE_AND_HEALING);
            Assert.IsTrue(event1Raised);
        }
    }
}