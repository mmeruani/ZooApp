using ZooApp.Domain.Abstractions;
namespace ZooApp.Domain.Animals;

public abstract class Animal : IInventory, IAlive
{
    public int Number { get; init; }
    public string Name { get; init; }
    public int Food { get; init; }      
    
    protected Animal(string name, int number, int food)
    {
        Name = name;
        Number = number;
        Food = food;
    }
}
