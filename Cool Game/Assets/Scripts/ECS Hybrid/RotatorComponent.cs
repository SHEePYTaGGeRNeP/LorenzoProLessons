﻿using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace ECS_Hybrid
{
    public class RotatorComponent : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 1f;

        public float Speed { get => this._speed; set => this._speed = value; }
    }

    public class RotatorSystem : ComponentSystem
    {
        private struct Filter
        {
            public RotatorComponent Rotator { get; set; }
            public Transform Transform { get; set; }
        }

        protected override void OnUpdate()
        {
            float deltaTime = Time.deltaTime;
            foreach (var e in this.GetEntities<Filter>())
            {
                e.Transform.Rotate(0, e.Rotator.Speed * deltaTime, 0);
            }
        }
    }
}