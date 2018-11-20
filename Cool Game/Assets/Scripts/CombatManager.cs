using System.Runtime.CompilerServices;
using Unit;
using Unit.Abilities;
using UnityEngine;

public class CombatManager
{
    public readonly Creature creature1;
    public readonly Creature creature2;

    public Creature CurrentCreature { get; private set; }
    public Creature OtherCreature => this.CurrentCreature == this.creature1 ? this.creature2 : this.creature1;


    public CombatManager(Creature creature1, Creature creature2)
    {
        this.creature1 = creature1;
        this.creature2 = creature2;
        this.CurrentCreature = creature1;
    }

    private void NextTurn()
    {
        this.CurrentCreature = this.CurrentCreature == this.creature1 ? this.creature2 : this.creature1;
        Debug.Log($"Its now {this.CurrentCreature.Name}'s turn.");
    }

    private bool IsCreaturesTurn(Creature c)
    {
        return this.CurrentCreature == c;
    }

    public void UseAbility(Creature c, Ability a)
    {
        if (!this.IsCreaturesTurn(c))
        {
            Debug.Log($"Its not {c}'s turn yet!");
            return;
        }
        a.Use(c, this.OpponentOf(c));
        this.NextTurn();
    }

    public Creature OpponentOf(Creature c) => c == this.creature1 ? this.creature2 : this.creature1;

    public void Damage(int damage)
    {
        this.OtherCreature.Damage(damage);
    }

    public void Heal(int heal)
    {
        this.CurrentCreature.Heal(heal);
    }
}