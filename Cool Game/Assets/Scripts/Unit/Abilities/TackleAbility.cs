using UnityEngine;

namespace Unit.Abilities
{
    [CreateAssetMenu(fileName = "Tackle", menuName = "Creatures/Abilities/CreateTackle")]
    public class TackleAbility : Ability
    {
        protected override void Execute(Creature self, Creature opponent)
        {
            opponent.Damage(15);
        }
    }
}