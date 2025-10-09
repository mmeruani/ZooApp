using Xunit;
using ZooApp.Application.Services;
using ZooApp.Application.Abstractions;
using ZooApp.Domain.Animals;
using Moq;
namespace ZooApp.Tests;

public class ZooServiceTests
{
    [Fact]
    public void TryAcceptAnimal_WhenHealthy_AddsAnimalAndReturnsTrue()
    {
        var vetMock = new Mock<IVetClinic>();
        var repoMock = new Mock<IZooRepository>();
        var service = new ZooService(vetMock.Object, repoMock.Object);
        
        var rabbit = new Rabbit("Bunny", 1, food: 3, kindness: 8);
        vetMock.Setup(v => v.IsHealthy(rabbit)).Returns(true);
        
        var result = service.TryAcceptAnimal(rabbit);

        Assert.True(result);
        vetMock.Verify(v => v.IsHealthy(rabbit), Times.Once);
        repoMock.Verify(r => r.AddAnimal(rabbit), Times.Once);
    }
    
    [Fact]
    public void TryAcceptAnimal_WhenNotHealthy_ReturnsFalseAndDoesNotAdd()
    {
        var vetMock = new Mock<IVetClinic>();
        var repoMock = new Mock<IZooRepository>();
        var service = new ZooService(vetMock.Object, repoMock.Object);

        var tiger = new Tiger("ShereKhan", 2, food: 10);
        vetMock.Setup(v => v.IsHealthy(tiger)).Returns(false);

        var result = service.TryAcceptAnimal(tiger);

        Assert.False(result);
        repoMock.Verify(r => r.AddAnimal(It.IsAny<Animal>()), Times.Never);
    }
    
    [Fact]
    public void GetTotalFoodPerDay_SumsFoodCorrectly()
    {
        var vetMock = new Mock<IVetClinic>();
        var repoMock = new Mock<IZooRepository>();
        var service = new ZooService(vetMock.Object, repoMock.Object);

        var animals = new List<Animal>
        {
            new Rabbit("R1", 1, 2, 8),
            new Monkey("M1", 2, 3, 7),
            new Tiger("T1", 3, 5)
        };

        repoMock.Setup(r => r.GetAnimals()).Returns(animals);

        var total = service.GetTotalFoodPerDay();

        Assert.Equal(10, total); // 2 + 3 + 5
    }
}