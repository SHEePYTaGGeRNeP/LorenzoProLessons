using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Units;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public abstract class BaseItemBehavior : ScriptableObject
    {
        [Serializable]
        public class ItemBehaviorParameters
        {
            public HealthSystem HealthSystem { get; set; }
            public Unit Unit { get; set; }
            public SimpleCharacterControl SimpleCharacterControl { get; set; }
        }

        public abstract void OnEquip(ItemBehaviorParameters parameters, float[] value);
        public abstract void OnUnequip(ItemBehaviorParameters parameters, float[] value);
    }
}
