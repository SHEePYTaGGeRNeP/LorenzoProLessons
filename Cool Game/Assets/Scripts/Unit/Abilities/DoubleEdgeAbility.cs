using UnityEngine;

namespace Unit.Abilities
{
    [CreateAssetMenu(fileName = "DoubleEdge", menuName = "Creatures/Abilities/CreateDoubleEdge")]
    public class DoubleEdgeAbility : Ability
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
            LogHelper.Log(typeof(DoubleEdgeAbility), $"{this.Name} did {damage} damage to the enemy and {selfDamage} to self.");
        }
    }
}