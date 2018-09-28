using System;
using UnityEngine;
using UnityEngine.Events;

namespace Unit.MonoBehaviours
{
    public class CreatureMono : MonoBehaviour
    {
        public UnityHealthChangeEvent onHealthChanged;

        private Creature _creature;

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
            this._creature = new Creature(this._creatureConstructorParams.maxHealth,this._creatureConstructorParams.curHealth);
            this._creature.OnDamage += this.HealthSystemOnOnDamage;
            this._creature.OnHealing += this.HealthSystemOnOnDamage;
        }

        private void HealthSystemOnOnDamage(object sender, HealthSystem.HealthChangeEventArgs healthChangeEventArgs)
        {
            this.onHealthChanged?.Invoke(healthChangeEventArgs);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
                this._creature.Damage(10);
            else if (Input.GetKeyDown(KeyCode.Backslash))
                this._creature.Heal(10);
        }
    }

    [Serializable]
    public class UnityHealthChangeEvent : UnityEvent<HealthSystem.HealthChangeEventArgs>
    {
    }
}