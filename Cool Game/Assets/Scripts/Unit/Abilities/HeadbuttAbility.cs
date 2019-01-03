using UnityEngine;

namespace Unit.Abilities
{
    [CreateAssetMenu(fileName = "Headbutt", menuName = "Creatures/Abilities/CreateHeadbutt")]
    public class HeadbuttAbility : Ability
    {
        [SerializeField]
        private int _damageBase = 25;
        [SerializeField]
        private int _damageSelfBase = 10;

        protected override void Execute(Creature self, Creature opponent)
        {
            int damage = this._damageBase * this.Level;
            opponent.Damage(damage);
            int selfDamage = this._damageSelfBase * this.Level;
            self.Damage(selfDamage);
            LogHelper.Log(typeof(TackleAbility), $"{this.Name} did {damage} damage to the enemy and {selfDamage} to self.");
        }
    }
}