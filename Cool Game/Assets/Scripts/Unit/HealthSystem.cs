using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Unit
{
    public class HealthSystem
    {
        public int MaxHitPoints { get; private set; }
        public int CurrentHitPoints { get; private set; }

        public HealthSystem(int startAndMaxHp)
        {
            Assert.IsTrue(startAndMaxHp >= 0);
            this.MaxHitPoints = startAndMaxHp;
            this.CurrentHitPoints = startAndMaxHp;
        }

        public HealthSystem(int maxHitPoints, int currentHitPoints)
        {
            Assert.IsTrue(maxHitPoints >= 0);
            this.MaxHitPoints = maxHitPoints;
            this.CurrentHitPoints = currentHitPoints;
        }

        public void Heal(int healing)
        {
            Assert.IsTrue(healing >= 0);
            this.CurrentHitPoints = Mathf.Clamp(this.CurrentHitPoints + healing, 0, this.MaxHitPoints);
        }

        public void Damage(int damage)
        {
            Assert.IsTrue(damage >= 0);
            this.CurrentHitPoints = Mathf.Clamp(this.CurrentHitPoints - damage, 0, this.MaxHitPoints);
        }
    }
}