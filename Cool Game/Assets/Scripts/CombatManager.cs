using Unit;
using UnityEngine;

public class CombatManager
    {
        public readonly Creature creature1;
        public readonly Creature creature2;

        public Creature currentCreature { get; private set; }


        public CombatManager(Creature creature1, Creature creature2)
        {
            this.creature1 = creature1;
            this.creature2 = creature2;
            this.currentCreature = creature1;
        }

        public void NextTurn()
        {
            this.currentCreature = this.currentCreature == this.creature1 ? this.creature2 : this.creature1;
            Debug.Log($"Its now {this.currentCreature.Name}'s turn.");
        }

        public bool IsCreaturesTurn(Creature c) => this.currentCreature == c;
        
        
        
        
    }