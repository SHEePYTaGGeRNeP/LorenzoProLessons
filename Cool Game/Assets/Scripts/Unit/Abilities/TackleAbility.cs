using UnityEngine;

namespace Unit.Abilities
{
    [CreateAssetMenu(fileName = "Tackle", menuName = "Creatures/Abilities/CreateTackle")]
    public class TackleAbility : Ability
    {
        [SerializeField]
        private int _damage = 15;
        
        protected override void Execute(Creature self, Creature opponent)
        {
            opponent.Damage(this._damage);
        }
    }
}