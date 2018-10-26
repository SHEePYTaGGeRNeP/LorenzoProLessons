using System;
using UnityEngine;

namespace Unit
{
    public class Creature
    {
        private readonly HealthSystem _healthSystem;

        public string Name { get; private set; }

        public int CurrentHitPoints => this._healthSystem.CurrentHitPoints;
        public int MaxHitPoints => this._healthSystem.MaxHitPoints;
        
        public event EventHandler<HealthSystem.HealthChangeEventArgs> OnDamage;
        public event EventHandler<HealthSystem.HealthChangeEventArgs> OnHealing;

        public Creature(int maxHealth, int currentHealth)
        {
            this._healthSystem = new HealthSystem(maxHealth, currentHealth);
            this._healthSystem.OnDamage += this.HealthSystemOnOnDamage;
            this._healthSystem.OnHealing += this.HealthSystemOnOnHealing;  
        }

        public Creature(string name, int maxHealth, int currentHealth) : this(maxHealth, currentHealth)
        {
            this.Name = name;
        }

        ~Creature()
        {
            this._healthSystem.OnDamage -= this.HealthSystemOnOnDamage;
            this._healthSystem.OnHealing -= this.HealthSystemOnOnHealing;
        }

        private void HealthSystemOnOnHealing(object sender, HealthSystem.HealthChangeEventArgs healthChangeEventArgs)
        {
            this.OnHealing?.Invoke(this, healthChangeEventArgs);
        }

        private void HealthSystemOnOnDamage(object sender, HealthSystem.HealthChangeEventArgs healthChangeEventArgs)
        {
            this.OnDamage?.Invoke(this, healthChangeEventArgs);
        }

        public void Damage(int damage) => this._healthSystem.Damage(damage);
        public void Heal(int healing) => this._healthSystem.Heal(healing);
    }
}