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
        public override void OnEquip(ItemBehaviorParameters parameters, float[] value)
        {
            parameters.HealthSystem.MaxHitPoints += (int)value[0];
        }
        public override void OnUnequip(ItemBehaviorParameters parameters, float[] value)
        {
            parameters.HealthSystem.MaxHitPoints -= (int)value[0];
        }
    }
}
