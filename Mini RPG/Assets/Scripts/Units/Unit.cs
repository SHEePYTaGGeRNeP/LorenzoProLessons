using Assets.Scripts;
using Assets.Scripts.Items;
using Assets.Scripts.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Units
{
    [ExecuteInEditMode]
    public class Unit : MonoBehaviour
    {
        private HealthSystem _healthSystem;

        [SerializeField]
        private EquippedGear _equippedGear = new EquippedGear();
        
        public UnityHealthChangeEvent onHealthChanged;
        
        private void Awake()
        {
            this._healthSystem = new HealthSystem(50);
            this._healthSystem.OnDamage += _healthSystem_OnHealthChange;
            this._healthSystem.OnHealing += _healthSystem_OnHealthChange;
        }

        private void _healthSystem_OnHealthChange(object sender, HealthSystem.HealthChangeEventArgs e)
        {
            onHealthChanged?.Invoke(e);
        }
        public void Damage(int damage)
        {
            this._healthSystem.Damage(damage);
        }
        public void Heal(int heal)
        {
            this._healthSystem.Heal(heal);
        }
    }
}
