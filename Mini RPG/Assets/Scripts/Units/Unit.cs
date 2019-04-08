using Assets.Scripts;
using Assets.Scripts.Helpers.Components;
using Assets.Scripts.Items;
using Assets.Scripts.Units;
using UnityEngine;

namespace Units
{
    [ExecuteInEditMode]
    public class Unit : MonoBehaviour
    {
        private HealthSystem _healthSystem;
        public EquippedGear EquippedGear { get; private set; }

        public UnityHealthChangeEvent onHealthChanged;

        public int MaxHp => this._healthSystem.MaxHitPoints;

        [SerializeField]
        private ItemSO _itemToEquip;

        [Header("DEBUG")]
        [SerializeField]
        private string _currentHP;

        private void Awake()
        {
            this._healthSystem = new HealthSystem(50, 50, this._healthSystem_OnHealthChange);
            this.EquippedGear = new EquippedGear(this, this._healthSystem);
        }
        private void Start()
        {
            if (this.GetComponent<Player>() != null)
                Toolbox.Instance.AddToToolbox(nameof(HealthSystem), this._healthSystem);
        }

        private void _healthSystem_OnHealthChange(object sender, HealthChangeEventArgs e)
        {
            this._currentHP = $"{e.CurrentHitPoints} / {e.MaxHitPoints}";
            onHealthChanged?.Invoke(e);
        }
        public void Damage(int damage) => this._healthSystem.Damage(damage);
        public void Heal(int heal) => this._healthSystem.Heal(heal);

        public void TestButton_Clicked()
        {
            this.EquippedGear.Equip(_itemToEquip);
        }
    }
}
