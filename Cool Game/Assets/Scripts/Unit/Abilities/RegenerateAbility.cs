using UnityEngine;

namespace Unit.Abilities
{
    [CreateAssetMenu(fileName = "Regenerate", menuName = "Creatures/Abilities/CreateRegenerate")]
    public class RegenerateAbility : Ability
    {
        [SerializeField]
        private int _healingBase = 10;
        
        protected override void Execute(Creature self, Creature opponent)
        {
            int healing = this._healingBase * this.Level;
            self.Heal(healing);
        }
    }
}