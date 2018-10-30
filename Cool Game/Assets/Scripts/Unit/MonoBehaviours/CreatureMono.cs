using System;
using Unit.Abilities;
using UnityEngine;
using UnityEngine.Events;

namespace Unit.MonoBehaviours
{
    public class CreatureMono : MonoBehaviour
    {
        public UnityHealthChangeEvent onHealthChanged;

        public Creature Creature { get; private set; }

        [Serializable]
        private class CreatureConstructorParams
        {
            public int maxHealth;
            public int curHealth;
            public string name;
        }

        [SerializeField]
        private CreatureConstructorParams _creatureConstructorParams;

        private void Awake()
        {
            this.Creature = new Creature(this._creatureConstructorParams.name,
                this._creatureConstructorParams.maxHealth,
                this._creatureConstructorParams.curHealth)
            {
                Abilities = new Ability[]
                {
                    new RegenerateAbility(),
                    new TackleAbility(),
                    new RegenerateAbility(),
                    new TackleAbility()
                }
            };
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
            CombatManagerMono.CombatManager.UseAbility(this.Creature, this.Creature.Abilities[index]);
        }
    }

    [Serializable]
    public class UnityHealthChangeEvent : UnityEvent<HealthSystem.HealthChangeEventArgs>
    {
    }
}