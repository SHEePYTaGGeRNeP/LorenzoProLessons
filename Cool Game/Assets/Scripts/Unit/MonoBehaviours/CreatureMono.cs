using System;
using Unit.Abilities;
using UnityEngine;
using UnityEngine.Events;

namespace Unit.MonoBehaviours
{
    public class CreatureMono : MonoBehaviour
    {
        [SerializeField]
        private CreatureSO _creature;

        public UnityHealthChangeEvent onHealthChanged;

        public Creature Creature { get; private set; }

        private void Awake()
        {
            this.Creature = new Creature(this._creature.name,
                this._creature.CreatureStats.MaxHitPoints,
                this._creature.CreatureStats.CurrentHitPoints)
            {
                Abilities = new Ability[this._creature.Abilities.Length]
            };
            for (int i = 0; i < this._creature.Abilities.Length; i++)
                this.Creature.Abilities[i] = this._creature.Abilities[i];
            this.Creature.OnDamage += this.HealthSystemOnOnDamage;
            this.Creature.OnHealing += this.HealthSystemOnOnDamage;
        }

        private void HealthSystemOnOnDamage(object sender, HealthSystem.HealthChangeEventArgs healthChangeEventArgs)
        {
            this.onHealthChanged?.Invoke(healthChangeEventArgs);
        }

        // ReSharper disable once UnusedMember.Global
        public void UseAbility(int index)
        {
            if (index < this.Creature.Abilities.Length)
                CombatManagerMono.CombatManager.UseAbility(this.Creature, this.Creature.Abilities[index]);
            else
                Debug.LogWarning("Creature does not have so many abilities. Index:" + index );
        }
    }

    [Serializable]
    public class UnityHealthChangeEvent : UnityEvent<HealthSystem.HealthChangeEventArgs>
    {
    }
}