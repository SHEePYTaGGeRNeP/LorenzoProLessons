using UnityEngine;

namespace Unit.Abilities
{
    [CreateAssetMenu(fileName = "Tackle", menuName = "Creatures/Abilities/CreateTackle")]
    public class TackleAbility : Ability
    {
        [SerializeField]
        private int _damage = 15;

        [SerializeField]
        private AnimationCurve _curve;
        
        protected override void Execute(Creature self, Creature opponent)
        {
            float damage = this._curve.Evaluate(Random.Range(0f, 1f)) * this.Level;
            opponent.Damage(this._damage);
        }
    }
}