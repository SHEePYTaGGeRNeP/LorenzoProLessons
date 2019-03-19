using System;
using UnityEngine;

namespace Units
{
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

    [Serializable]
    public class HealthSystem
    {
        [SerializeField]
        private int _maxHitPoints;
        public int MaxHitPoints
        {
            get { return this._maxHitPoints; }
            set
            {
                int change = value - this._maxHitPoints;
                this._maxHitPoints = value;
                this._currentHitPoints += change;
                this.OnHealthChanged?.Invoke(this, new HealthChangeEventArgs(change, this.CurrentHitPoints, this.MaxHitPoints));
            }
        }

        [SerializeField]
        private int _currentHitPoints;
        public int CurrentHitPoints
        {
            get => this._currentHitPoints;
            private set { this._currentHitPoints = value; }
        }

        public event EventHandler<HealthChangeEventArgs> OnHealthChanged;

        public HealthSystem(int startAndMaxHp) : this(startAndMaxHp, startAndMaxHp) { }
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
            this.OnHealthChanged?.Invoke(this, new HealthChangeEventArgs(healing, this.CurrentHitPoints, this.MaxHitPoints));
        }

        public void Damage(int damage)
        {
            if (damage < 0)
                throw new NegativeInputException(nameof(damage));
            this.CurrentHitPoints = Mathf.Clamp(this.CurrentHitPoints - damage, 0, this.MaxHitPoints);
            this.OnHealthChanged?.Invoke(this, new HealthChangeEventArgs(-damage, this.CurrentHitPoints, this.MaxHitPoints));
        }
    }
}