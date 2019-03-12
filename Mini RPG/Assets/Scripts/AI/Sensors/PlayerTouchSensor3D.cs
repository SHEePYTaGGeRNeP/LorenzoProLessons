﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI.Sensors
{
    class PlayerTouchSensor3D : TouchSensor3D
    {
        [Header("Debug PlayerTouchSensor3D")]
        [SerializeField]
        private Player _player;
        public Player Player { get; private set; }

        protected override void OnTouch(Collider col)
        {
            Player p = col.GetComponentInParent<Player>();
            if (p == null)
                throw new Exception("We should have caught this in ShouldAddOrRemove");
            this.Player = p;
            this._player = p;
        }

        protected override void OnTouchExit(Collider col)
        {
            Player p = col.GetComponentInParent<Player>();
            if (p == null)
                throw new Exception("We should have caught this in ShouldAddOrRemove");
            this.Player = null;
            this._player = null;
        }

        protected override bool ShouldAddOrRemove(Collider col) => col.GetComponentInParent<Player>() != null;

    }
}