using System;
using System.Linq;
using Unit.Abilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Unit.MonoBehaviours
{
    public class CreatureMono : MonoBehaviour
    {
        [SerializeField]
        private CreatureCollectionSO _creatures;

        [SerializeField]
        private Image _image;

        public UnityHealthChangeEvent onHealthChanged;

        public Creature Creature { get; private set; }

        private void Awake()
        {
            CreatureSO data = this._creatures.Creatures.ElementAt(
                UnityEngine.Random.Range(0, this._creatures.Creatures.Count()));
            this.Setup(data);
        }
        public void Setup(CreatureSO data)
        {
            if (this.Creature != null)
            {
                this.Creature.OnDamage -= this.HealthSystemOnDamage;
                this.Creature.OnHealing -= this.HealthSystemOnDamage;
            }
            this._image.sprite = data.Sprite;
            this.Creature = new Creature(data.name, data.Health)
            {
                Abilities = new Ability[data.Abilities.Length]
            };
            for (int i = 0; i < data.Abilities.Length; i++)
                this.Creature.Abilities[i] = data.Abilities[i];
            this.Creature.OnDamage += this.HealthSystemOnDamage;
            this.Creature.OnHealing += this.HealthSystemOnDamage;
        }
        private void Start()
        {
            this.onHealthChanged?.Invoke(new HealthSystem.HealthChangeEventArgs(0,
                this.Creature.CurrentHitPoints, this.Creature.MaxHitPoints));
        }

        private void HealthSystemOnDamage(object sender, HealthSystem.HealthChangeEventArgs healthChangeEventArgs)
        {
            this.onHealthChanged?.Invoke(healthChangeEventArgs);
        }

        // ReSharper disable once UnusedMember.Global
        public void UseAbility(int index)
        {
            if (index < this.Creature.Abilities.Length)
                CombatManagerMono.CombatManager.UseAbility(this.Creature, this.Creature.Abilities[index]);
            else
                Debug.LogWarning("Creature does not have so many abilities. Index:" + index);
        }
    }

    [Serializable]
    public class UnityHealthChangeEvent : UnityEvent<HealthSystem.HealthChangeEventArgs>
    {
    }
}