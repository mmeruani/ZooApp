using ZooApp.Domain.Animals;
namespace ZooApp.Application.Abstractions;

public interface IVetClinic
{
    bool IsHealthy(Animal animal);
}
