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
        public override void OnEquip()
        {
            HealthSystem hs = Toolbox.Instance.GetToolboxComponent<HealthSystem>();
            hs.MaxHitPoints += (int)parameters.values[0];
        }
        public override void OnUnequip()
        {
            HealthSystem hs = Toolbox.Instance.GetToolboxComponent<HealthSystem>();
            hs.MaxHitPoints -= (int)parameters.values[0];
        }
    }
}
