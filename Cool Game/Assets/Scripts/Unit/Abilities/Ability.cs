using System;
using System.Collections.Generic;

namespace Unit.Abilities
{
    public abstract class Ability
    {
        public abstract string Name { get; }


        public bool Use(Creature self, Creature opponent)
        {
            if (!this.IsAllowedToUse(self, opponent))
                return false;
            this.Execute(self, opponent);
            return true;
        }

        protected abstract void Execute(Creature self, Creature opponent);
        protected abstract bool IsAllowedToUse(Creature self, Creature opponent);
    }
}