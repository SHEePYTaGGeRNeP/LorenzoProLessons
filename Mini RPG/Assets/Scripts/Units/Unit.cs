using Assets.Scripts;
using Assets.Scripts.Units;
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

        [Header("DEBUG")]
        [SerializeField]
        private string _currentHP;

        private void Awake()
        {
            this._healthSystem = new HealthSystem(50);
            this._healthSystem.OnHealthChanged += _healthSystem_OnHealthChange;
            this._healthSystem.Heal(0);
        }

        private void _healthSystem_OnHealthChange(object sender, HealthChangeEventArgs e)
        {
            this._currentHP = $"{e.CurrentHitPoints} / {e.MaxHitPoints}";
            onHealthChanged?.Invoke(e);
        }
        public void Damage(int damage) => this._healthSystem.Damage(damage);
        public void Heal(int heal) => this._healthSystem.Heal(heal);
    }
}
