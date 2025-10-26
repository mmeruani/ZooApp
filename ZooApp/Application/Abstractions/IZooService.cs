using ZooApp.Domain.Animals;
using ZooApp.Domain.Things;
using ZooApp.Domain.Abstractions;

namespace ZooApp.Application.Abstractions;

public interface IZooService
{
    bool TryAcceptAnimal(Animal animal);
    void AddThing(Thing thing);
    int GetAnimalsCount();
    int GetTotalFoodPerDay();
    IEnumerable<Herbo> GetPettingZooCandidates();
    IEnumerable<IInventory> GetInventoryReport();    
}
