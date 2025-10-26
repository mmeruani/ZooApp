using ZooApp.Domain.Abstractions;
namespace ZooApp.Domain.Things;

public abstract class Thing : IInventory
{
    public int Number { get; init; }   
    public string Name { get; init; }
    
    protected Thing(string name, int number)
    {
        Name = name;
        Number = number;
    }
}
