using UnityEngine;

namespace Unit.Abilities
{
    [CreateAssetMenu(fileName = "LifeLeach", menuName = "Creatures/Abilities/CreateLifeLeach")]
    public class LifeLeachAbility : Ability
    {
        [SerializeField]
        private int _damage = 5;

        [SerializeField]
        private int _healing = 5;
        protected override void Execute(Creature self, Creature opponent)
        {
            opponent.Damage(this._damage);
            self.Heal(this._healing);
        }
    }
}