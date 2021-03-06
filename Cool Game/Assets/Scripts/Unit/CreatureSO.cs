﻿using System.Runtime.CompilerServices;
using Unit.Abilities;
using UnityEngine;

namespace Unit
{
    [CreateAssetMenu(fileName = "Creature", menuName = "Creatures/CreateCreatureAsset", order = 0)]
    public class CreatureSO : ScriptableObject
    {
        [SerializeField]
        private string _name;
        public string Name => this._name;

        [SerializeField]
        private Sprite _sprite;
        public Sprite Sprite => this._sprite;

        [SerializeField]
        private Ability[] _abilities;
        public Ability[] Abilities => this._abilities;

        [SerializeField]
        private int _health;
        public int Health => this._health;

    }
}