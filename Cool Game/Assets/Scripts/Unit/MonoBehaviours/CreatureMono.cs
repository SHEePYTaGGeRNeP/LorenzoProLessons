using System;
using UnityEngine;
using UnityEngine.Events;

namespace Unit.MonoBehaviours
{
    public class CreatureMono : MonoBehaviour
    {
        public UnityHealthChangeEvent onHealthChanged;

        [SerializeField]
        private CombatManagerMono _combatManagerMono;

        public Creature Creature { get; private set; }

        [Serializable]
        private class CreatureConstructorParams
        {
            public int maxHealth;
            public int curHealth;
        }

        [SerializeField]
        private CreatureConstructorParams _creatureConstructorParams;

        private void Awake()
        {
            this.Creature = new Creature(this._creatureConstructorParams.maxHealth,
                this._creatureConstructorParams.curHealth);
            this.Creature.OnDamage += this.HealthSystemOnOnDamage;
            this.Creature.OnHealing += this.HealthSystemOnOnDamage;
        }

        private void HealthSystemOnOnDamage(object sender, HealthSystem.HealthChangeEventArgs healthChangeEventArgs)
        {
            this.onHealthChanged?.Invoke(healthChangeEventArgs);
        }

        public void UseAbility(int index)
        {
            this._combatManagerMono.CombatManager.UseAbility(this.Creature, this.Creature.Abilities[index]);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
                this.Creature.Damage(10);
            else if (Input.GetKeyDown(KeyCode.Backslash))
                this.Creature.Heal(10);
        }
    }

    [Serializable]
    public class UnityHealthChangeEvent : UnityEvent<HealthSystem.HealthChangeEventArgs>
    {
    }
}