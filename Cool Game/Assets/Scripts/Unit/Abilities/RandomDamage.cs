﻿using UnityEngine;

namespace Unit.Abilities
{
    [CreateAssetMenu(fileName = "RandomDamage", menuName = "Creatures/Abilities/CreateRandomDamage")]
    public class RandomDamageAbility : Ability
    {
        [SerializeField]
        private AnimationCurve _curve;
        
        protected override void Execute(Creature self, Creature opponent)
        {
            int damage = (int)this._curve.Evaluate(Random.Range(0f, 1f)) * this.Level;
            opponent.Damage(damage);
            LogHelper.Log(typeof(TackleAbility), $"Random Damage did {damage} damage.");
            base.Execute(self, opponent);
        }
    }
}