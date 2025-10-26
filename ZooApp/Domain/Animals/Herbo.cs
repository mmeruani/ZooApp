namespace ZooApp.Domain.Animals;

public abstract class Herbo : Animal
{
    public int Kindness { get; init; }

    protected Herbo(string name, int number, int food, int kindness) : base (name, number, food)
    {
        Kindness = kindness;
    }

}
