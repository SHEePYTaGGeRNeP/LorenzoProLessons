using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Assets.Editor.Tests.Utils
{
    public class DieTests
    {
        [Test]
        public void Can_Create()
        {
            Die d = new Die(6);
            Assert.NotNull(d);
        }

        [Test]
        public void Can_Roll()
        {
            const int sides = 6;
            const int nrOfDice = 1;
            Die d = new Die(sides);
            for (int i = 0; i < 100; i++)
            {
                int result = d.Roll();
                Assert.True(result > nrOfDice - 1 && result <= sides);
            }
        }

        [Test]
        public void Can_Roll_Static()
        {
            const int sides = 6;
            const int nrOfDice = 1;
            for (int i = 0; i < 100; i++)
            {
                int result = Die.Roll(sides, nrOfDice);
                Assert.True(result > nrOfDice - 1 && result <= sides);
            }
        }

        [Test]
        public void Can_Roll_Two_Static()
        {
            const int sides = 6;
            const int nrOfDice = 2;
            for (int i = 0; i < 100; i++)
            {
                int result = Die.Roll(sides, nrOfDice);
                Assert.True(result > nrOfDice - 1 && result <= sides * nrOfDice);
            }
        }

        [Test]
        public void Can_Roll_Dice()
        {
            (Die, int) die1 = (new Die(8), 1);
            (Die, int) die2 = (new Die(8), 1);
            (Die, int)[] dice = new(Die, int)[] { die1, die2 };
            int max = dice.Sum(x => x.Item2 * x.Item1.NrOfSides);
            for (int i = 0; i < 100; i++)
            {
                int result = Die.Roll(dice);
                Assert.True(result >= dice.Length && result <= max);
            }
        }
    }
}