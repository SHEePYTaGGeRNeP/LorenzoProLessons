
using JetBrains.Annotations;

namespace Unit
{
    public class Creature
    {
        private readonly HealthSystem _healthSystem;

        public int CurrentHitPoints => this._healthSystem.CurrentHitPoints;
        public int MaxHitPoints => this._healthSystem.MaxHitPoints;

        public Creature(int maxHealth, int currentHealth)
        {
            this._healthSystem = new HealthSystem(maxHealth, currentHealth);
        }
    }
}