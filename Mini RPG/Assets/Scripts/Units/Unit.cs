﻿using Assets.Scripts.Items;
using Assets.Scripts.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Units
{
    public class Unit : MonoBehaviour
    {
        private HealthSystem _healthSystem;

        [SerializeField]
        private EquippedGear _equippedGear = new EquippedGear();

        private void Awake()
        {
            this._healthSystem = new HealthSystem(50);
        }
    }
}
