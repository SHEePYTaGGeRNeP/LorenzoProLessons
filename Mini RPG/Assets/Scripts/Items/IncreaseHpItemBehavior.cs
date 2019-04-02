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
    public class IncreaseHpItemBehavior : BaseItemBehavior
    {
        [Serializable]
        public class IncreaseHpItemBehaviorParameters : ItemBehaviorParameters
        {
            public HealthSystem HealthSystem { get; set; }
        }
        public override void OnEquip(ItemBehaviorParameters parameters)
        {
            if (parameters is IncreaseHpItemBehaviorParameters par)
                par.HealthSystem.MaxHitPoints += (int)parameters.values[0];
            else throw new ArgumentException($"must be {nameof(IncreaseHpItemBehaviorParameters)}");
        }
        public override void OnUnequip(ItemBehaviorParameters parameters)
        {
            if (parameters is IncreaseHpItemBehaviorParameters par)
                par.HealthSystem.MaxHitPoints -= (int)parameters.values[0];
            else throw new ArgumentException($"must be {nameof(IncreaseHpItemBehaviorParameters)}");
        }
    }
}
