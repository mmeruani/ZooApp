using ZooApp.Application.Abstractions;
using ZooApp.Application.Services;
using ZooApp.Domain.Animals;
using Moq;

namespace ZooApp.Tests;

public class PettingZooTests
{
    [Fact]
    public void OnlyHerbivoresWithKindnessGreaterThanFiveAreIncluded()
    {
        var vet = new Mock<IVetClinic>();
        var repo = new Mock<IZooRepository>();
        var service = new ZooService(vet.Object, repo.Object);

        var animals = new List<Animal>
        {
            new Rabbit("R_low", 1, 2, kindness: 5),   // не должен попасть
            new Rabbit("R_high", 2, 2, kindness: 6), // должен попасть
            new Monkey("M_high", 3, 3, kindness: 8), // должен попасть
            new Tiger("T", 4, 5)                     // хищник, не попадает
        };
        repo.Setup(r => r.GetAnimals()).Returns(animals);

        var candidates = service.GetPettingZooCandidates().Select(a => a.Name).ToList();

        Assert.Contains("R_high", candidates);
        Assert.Contains("M_high", candidates);
        Assert.DoesNotContain("R_low", candidates);
        Assert.DoesNotContain("T", candidates);
    }
}
