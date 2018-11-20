using UnityEngine;

namespace Unit.Abilities
{
    [CreateAssetMenu(fileName = "Regenerate", menuName = "Creatures/Abilities/CreateRegenerate")]
    public class RegenerateAbility : Ability
    {
        [SerializeField]
        private int _healing = 10;
        
        protected override void Execute(Creature self, Creature opponent)
        {
            self.Heal(this._healing);
        }
    }
}