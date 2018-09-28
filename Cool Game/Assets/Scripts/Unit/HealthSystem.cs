using System;
using Helpers.Classes;
using UnityEngine;

namespace Unit
{
    public class HealthSystem
    {
        public int MaxHitPoints { get; private set; }
        public int CurrentHitPoints { get; private set; }

        public event EventHandler<HealthChangeEventArgs> OnDamage;
        public class HealthChangeEventArgs : EventArgs
        {
            public int Change { get; }
            public int CurrentHitPoints { get; }

            public HealthChangeEventArgs(int change, int currentHitPoints)
            {
                this.Change = change;
                this.CurrentHitPoints = currentHitPoints;
            }
        }

        public delegate void DamageDelegate(int damage, int currentHp);
        public event DamageDelegate OnDamageDel;


        public HealthSystem(int startAndMaxHp)
        {
            if (startAndMaxHp < 0)
                throw new NegativeInputException(nameof(startAndMaxHp));
            this.MaxHitPoints = startAndMaxHp;
            this.CurrentHitPoints = startAndMaxHp;
        }

        public HealthSystem(int maxHitPoints, int currentHitPoints)
        {
            if (maxHitPoints < 0)
                throw new NegativeInputException(nameof(maxHitPoints));
            if (currentHitPoints < 0)
                throw new NegativeInputException(nameof(currentHitPoints));
            this.MaxHitPoints = maxHitPoints;
            this.CurrentHitPoints = currentHitPoints;
        }

        public void Heal(int healing)
        {
            if (healing < 0)
                throw new NegativeInputException(nameof(healing));
            this.CurrentHitPoints = Mathf.Clamp(this.CurrentHitPoints + healing, 0, this.MaxHitPoints);
        }

        public void Damage(int damage)
        {
            if (damage < 0)
                throw new NegativeInputException(nameof(damage));
            this.CurrentHitPoints = Mathf.Clamp(this.CurrentHitPoints - damage, 0, this.MaxHitPoints);
            this.OnDamage?.Invoke(this, new HealthChangeEventArgs(damage, this.CurrentHitPoints));
            this.OnDamageDel?.Invoke(damage, this.CurrentHitPoints);
        }
    }
}