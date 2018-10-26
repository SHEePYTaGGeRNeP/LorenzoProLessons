namespace Unit
{
    public class Ability
    {
        public string Name { get; private set; }
        public int Damage { get; private set; }

        public Ability(string name, int damage)
        {
            this.Name = name;
            this.Damage = damage;
        }
    }
}