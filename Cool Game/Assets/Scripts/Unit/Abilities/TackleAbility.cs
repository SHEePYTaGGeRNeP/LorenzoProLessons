using UnityEngine;

namespace Unit.Abilities
{
    [CreateAssetMenu(fileName = "Tackle", menuName = "Creatures/Abilities/CreateTackle")]
    public class TackleAbility : Ability
    {
        [SerializeField]
        private int _damageBase = 15;

        protected override void Execute(Creature self, Creature opponent)
        {
            int damage = this._damageBase * this.Level;
            opponent.Damage(damage);
            LogHelper.Log(typeof(TackleAbility), $"Tackle did {damage} damage.");
        }
    }
}