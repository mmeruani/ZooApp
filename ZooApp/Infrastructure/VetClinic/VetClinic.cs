using ZooApp.Application.Abstractions;
using ZooApp.Domain.Animals;

namespace ZooApp.Infrastructure.VetClinic;

public class VetClinic : IVetClinic
{
    private static readonly Random Rnd = new();
    public bool IsHealthy(Animal _) => Rnd.Next(0, 100) < 80;     // вероятность, что здоров > 0,8
}
