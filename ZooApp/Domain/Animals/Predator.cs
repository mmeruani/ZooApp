namespace ZooApp.Domain.Animals;

public abstract class Predator : Animal
{
    protected Predator(string name, int number, int food)  : base(name, number, food) {} 
}
