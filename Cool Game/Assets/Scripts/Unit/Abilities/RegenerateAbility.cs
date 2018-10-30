namespace Unit.Abilities
{
    public class RegenerateAbility : Ability
    {
        public override string Name => "Regenerate";

        protected override void Execute(Creature self, Creature opponent)
        {
            self.Heal(5);
        }

        protected override bool IsAllowedToUse(Creature self, Creature opponent) => true;
    }
}