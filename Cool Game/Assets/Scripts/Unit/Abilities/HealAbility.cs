using UnityEngine;

namespace Unit.Abilities
{
    [CreateAssetMenu(fileName = "Heal", menuName = "Creatures/Abilities/CreateHeal")]
    public class HealAbility : Ability
    {
        [SerializeField]
        private int _healingBase = 10;
        
        protected override void Execute(Creature self, Creature opponent)
        {
            int healing = this._healingBase * this.Level;
            self.Heal(healing);
            LogHelper.Log(typeof(HealAbility), $"{this.Name} healed {healing} hitpoints.");
        }
    }
}