﻿using System;
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
        [Serializable]
        public class ItemBehaviorParameters
        {
            public float[] values;
        }
        public ItemBehaviorParameters parameters;

        public abstract void OnEquip();
        public abstract void OnUnequip();
    }
}
