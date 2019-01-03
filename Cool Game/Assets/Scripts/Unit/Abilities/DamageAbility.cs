using UnityEngine;

namespace Unit.Abilities
{
    [CreateAssetMenu(fileName = "Damage", menuName = "Creatures/Abilities/DamageTackle")]
    public class DamageAbility : Ability
    {
        [SerializeField]
        private int _damageBase = 15;

        protected override void Execute(Creature self, Creature opponent)
        {
            int damage = this._damageBase * this.Level;
            opponent.Damage(damage);
            LogHelper.Log(typeof(DamageAbility), $"{this.Name} did {damage} damage.");
        }
    }
}