using UnityEngine;

namespace Unit.Abilities
{
    [CreateAssetMenu(fileName = "LifeLeach", menuName = "Creatures/Abilities/CreateLifeLeach")]
    public class LifeLeachAbility : Ability
    {
        protected override void Execute(Creature self, Creature opponent)
        {
            opponent.Damage(5);
            self.Heal(5);
        }
    }
}