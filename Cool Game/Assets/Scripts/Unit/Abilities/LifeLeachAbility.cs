using UnityEngine;

namespace Unit.Abilities
{
    [CreateAssetMenu(fileName = "LifeLeach", menuName = "Creatures/Abilities/CreateLifeLeach")]
    public class LifeLeachAbility : Ability
    {
        [SerializeField]
        private int _damageBase = 5;
        [SerializeField]
        private int _healingBase = 5;

        protected override void Execute(Creature self, Creature opponent)
        {
            int damage = this._damageBase * this.Level;
            int healing = this._healingBase * this.Level;
            opponent.Damage(damage);
            self.Heal(healing);
            LogHelper.Log(typeof(TackleAbility), $"LifeLeach did {damage} damage and healed {healing}.");
            base.Execute(self, opponent);
        }
    }
}