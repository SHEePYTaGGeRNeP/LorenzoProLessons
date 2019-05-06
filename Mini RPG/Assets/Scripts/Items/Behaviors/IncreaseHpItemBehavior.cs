using Assets.Scripts.Helpers.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Units;
using UnityEngine;

namespace Assets.Scripts.Items
{
    [CreateAssetMenu(fileName = "IncreaseHPItemBehavior", menuName = "Items/Behaviors/CreateHpItemBehavior", order = 0)]
    public class IncreaseHpItemBehavior : ItemBehavior
    {
        [SerializeField]
        private int _hpIncrease = 20;
        public int HpIncrease { get => this._hpIncrease; set { this._hpIncrease = value; } } 
        public override void OnEquip()
        {
            HealthSystem hs = Toolbox.Instance.GetToolboxComponent<HealthSystem>();
            hs.MaxHitPoints += this._hpIncrease;
        }
        public override void OnUnequip()
        {
            HealthSystem hs = Toolbox.Instance.GetToolboxComponent<HealthSystem>();
            hs.MaxHitPoints -= this._hpIncrease;
        }
    }
}
