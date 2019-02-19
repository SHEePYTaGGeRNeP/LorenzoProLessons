using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace Utils
{
    public class Die
    {
        public enum Dice { D2, D3, D4, D6, D8, D10, D12, D20, D100 }
        public static readonly Die D2 = new Die(2);
        public static readonly Die D3 = new Die(3);
        public static readonly Die D4 = new Die(4);
        public static readonly Die D6 = new Die(6);
        public static readonly Die D8 = new Die(8);
        public static readonly Die D10 = new Die(10);
        public static readonly Die D12 = new Die(12);
        public static readonly Die D20 = new Die(20);
        public static readonly Die D100 = new Die(100);

        public int NrOfSides { get; }

        public Die(int nrOfSides)
        {
            this.NrOfSides = nrOfSides;
        }

        public static int Roll(IEnumerable<(Die, int)> dice) => dice.Sum(x => Roll(x.Item1.NrOfSides, x.Item2));
        public int Roll() => Roll(this.NrOfSides, 1);
        public static int Roll(int nrOfSides) => Roll(nrOfSides, 1);
        public static int Roll(int nrOfSides, int nrOfDice)
        {
            if (nrOfSides < 1)
                throw new ArgumentException(nameof(nrOfSides));
            if (nrOfDice < 1)
                throw new ArgumentException(nameof(nrOfDice));
            return Enumerable.Repeat(UnityEngine.Random.Range(1, nrOfSides),
                nrOfDice).Sum();            
        }

        public override string ToString()
        {
            return $"d{this.NrOfSides}";
        }

    }
}