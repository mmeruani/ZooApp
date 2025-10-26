using ZooApp.Domain.Abstractions;
using ZooApp.Domain.Animals;
using ZooApp.Domain.Things;
namespace ZooApp.Application.Abstractions;

public interface IZooRepository
{
    void AddAnimal(Animal animal);
    void AddThing(Thing thing);
    IReadOnlyList<Animal> GetAnimals();
    IReadOnlyList<IInventory> GetInventory();    
}
