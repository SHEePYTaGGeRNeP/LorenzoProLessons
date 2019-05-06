using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Units;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public abstract class ItemBehavior : ScriptableObject
    {
        public abstract void OnEquip();
        public abstract void OnUnequip();
    }
}
