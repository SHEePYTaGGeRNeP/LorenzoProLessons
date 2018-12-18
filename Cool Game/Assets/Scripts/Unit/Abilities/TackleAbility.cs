using UnityEngine;

namespace Unit.Abilities
{
    [CreateAssetMenu(fileName = "Tackle", menuName = "Creatures/Abilities/CreateTackle")]
    public class TackleAbility : Ability
    {
        [SerializeField]
        private AnimationCurve _curve;
        
        protected override void Execute(Creature self, Creature opponent)
        {
            int damage = (int)this._curve.Evaluate(Random.Range(0f, 1f)) * this.Level;
            opponent.Damage(damage);
        }
    }
}