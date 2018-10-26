using System;
using System.Collections.Generic;

namespace Unit
{
    public class Ability
    {
        public string Name { get; private set; }
        public IEnumerable<Action> Effects { get; private set; }

        public Ability(string name, IEnumerable<Action> effects)
        {
            this.Name = name;
            this.Effects = effects;
        }

        public bool Use()
        {
            if (!this.IsAllowedToUse())
                return false;
            foreach(Action a in this.Effects)
                a.Invoke();
            return true;
        }

        public bool IsAllowedToUse()
        {
            return true;
        }
    }
}