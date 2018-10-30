namespace Unit.Abilities
{
    public class LifeLeachAbility : Ability
    {
        public override string Name => "Life Leach";

        protected override void Execute(Creature self, Creature opponent)
        {
            opponent.Damage(10);
            self.Heal(5);
        }

        protected override bool IsAllowedToUse(Creature self, Creature opponent) => true;
    }
}