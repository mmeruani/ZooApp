using ZooApp.Application.Abstractions;
using ZooApp.Domain.Animals;
using ZooApp.Domain.Things;
using ZooApp.Domain.Abstractions;

namespace ZooApp.Application.Services;

public class ZooService : IZooService
{
    private readonly IVetClinic _vet;
    private readonly IZooRepository _repo;
    
    public ZooService(IVetClinic vet, IZooRepository repo)
    {
        _vet = vet;
        _repo = repo;
    }

    public bool TryAcceptAnimal(Animal animal)
    {
        if (!_vet.IsHealthy(animal))
        {
            return false;    
        }

        _repo.AddAnimal(animal);
        return true;    
    }
    
    public void AddThing(Thing thing) => _repo.AddThing(thing);
    public int GetAnimalsCount() => _repo.GetAnimals().Count;
    public int GetTotalFoodPerDay() => _repo.GetAnimals().Sum(a => a.Food);
    public IEnumerable<Herbo> GetPettingZooCandidates() =>
        _repo.GetAnimals().OfType<Herbo>().Where(h => h.Kindness > 5);
    public IEnumerable<IInventory> GetInventoryReport() => _repo.GetInventory();
}
