using ZooApp.Application.Abstractions;
using ZooApp.Domain.Animals;
using ZooApp.Domain.Abstractions;
using ZooApp.Domain.Things;

namespace ZooApp.Infrastructure.Persistence;

public class InMemoryZooRepository : IZooRepository
{
    private readonly List<Animal> _animals = new();
    private readonly List<IInventory> _inventory = new();

    public void AddAnimal(Animal animal)
    {
        if (animal is null)
        {
            throw new ArgumentNullException(nameof(animal));
        }
        _animals.Add(animal);
        _inventory.Add(animal);
    }
    
    public void AddThing(Thing thing)
    {
        if (thing is null)
        {
            throw new ArgumentNullException(nameof(thing));
        }    
        _inventory.Add(thing);
    }
    
    public IReadOnlyList<Animal> GetAnimals() => _animals.AsReadOnly();
    public IReadOnlyList<IInventory> GetInventory() => _inventory.AsReadOnly();
}
