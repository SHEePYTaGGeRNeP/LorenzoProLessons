using Assets.Scripts.Helpers.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Items.Behaviors
{
    [CreateAssetMenu(fileName = "IncreaseMovementSpeedBehavior", menuName = "Items/Behaviors/CreateIncreaseMovementSpeedBehavior", order = 0)]
    public class IncreaseMovementSpeedBehavior : ItemBehavior
    {
        public override void OnEquip()
        {
            SimpleCharacterControl hs = Toolbox.Instance.GetToolboxComponent<SimpleCharacterControl>();
            hs.IncreaseMovementSpeed(parameters.values[0]);
        }
        public override void OnUnequip()
        {
            SimpleCharacterControl hs = Toolbox.Instance.GetToolboxComponent<SimpleCharacterControl>();
            hs.DecreaseMovementSpeed(parameters.values[0]);
        }
    }
}
