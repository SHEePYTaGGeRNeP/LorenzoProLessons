namespace Unit.Abilities
{
    public class TackleAbility : Ability
    {
        public override string Name => "Tackle";

        protected override void Execute(Creature self, Creature opponent)
        {
            opponent.Damage(10);
        }

        protected override bool IsAllowedToUse(Creature self, Creature opponent) => true;
    }
}