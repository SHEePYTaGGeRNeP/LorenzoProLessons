using UnityEngine;

namespace Unit.Abilities
{
    [CreateAssetMenu(fileName = "Regenerate", menuName = "Creatures/Abilities/CreateRegenerate")]
    public class RegenerateAbility : Ability
    {
        protected override void Execute(Creature self, Creature opponent)
        {
            self.Heal(10);
        }
    }
}