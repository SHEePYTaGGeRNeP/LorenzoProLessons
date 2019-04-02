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
            public float[] values;
        }

        public abstract void OnEquip(ItemBehaviorParameters parameters);
        public abstract void OnUnequip(ItemBehaviorParameters parameters);
    }
}
