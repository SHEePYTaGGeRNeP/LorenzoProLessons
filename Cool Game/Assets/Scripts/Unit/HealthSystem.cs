using System;
using Helpers.Classes;
using UnityEngine;

namespace Unit
{
    [Serializable]
    public class HealthSystem
    {
        [SerializeField]
        private int _maxHitPoints;
        public int MaxHitPoints { get { return this._maxHitPoints; } }

        [SerializeField]
        private int _currentHitPoints;
        public int CurrentHitPoints
        {
            get { return this._currentHitPoints; }
            private set { this._currentHitPoints = value; }
        }

        public event EventHandler<HealthChangeEventArgs> OnDamage;
        public event EventHandler<HealthChangeEventArgs> OnHealing;

        public class HealthChangeEventArgs : EventArgs
        {
            public int Change { get; }
            public int CurrentHitPoints { get; }
            public int MaxHitPoints { get; }

            public HealthChangeEventArgs(int change, int currentHitPoints, int maxHp)
            {
                this.Change = change;
                this.CurrentHitPoints = currentHitPoints;
                this.MaxHitPoints = maxHp;
            }
        }

        public HealthSystem(int startAndMaxHp)
        {
            if (startAndMaxHp < 0)
                throw new NegativeInputException(nameof(startAndMaxHp));
            this._maxHitPoints = startAndMaxHp;
            this.CurrentHitPoints = startAndMaxHp;
        }

        public HealthSystem(int maxHitPoints, int currentHitPoints)
        {
            if (maxHitPoints < 0)
                throw new NegativeInputException(nameof(maxHitPoints));
            if (currentHitPoints < 0)
                throw new NegativeInputException(nameof(currentHitPoints));
            this._maxHitPoints = maxHitPoints;
            this.CurrentHitPoints = currentHitPoints;
        }

        public void Heal(int healing)
        {
            if (healing < 0)
                throw new NegativeInputException(nameof(healing));
            this.CurrentHitPoints = Mathf.Clamp(this.CurrentHitPoints + healing, 0, this.MaxHitPoints);
            this.OnHealing?.Invoke(this, new HealthChangeEventArgs(healing, this.CurrentHitPoints, this.MaxHitPoints));
        }

        public void Damage(int damage)
        {
            if (damage < 0)
                throw new NegativeInputException(nameof(damage));
            this.CurrentHitPoints = Mathf.Clamp(this.CurrentHitPoints - damage, 0, this.MaxHitPoints);
            this.OnDamage?.Invoke(this, new HealthChangeEventArgs(damage, this.CurrentHitPoints, this.MaxHitPoints));
        }
    }
}