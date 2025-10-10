
using ZooApp.Application.Abstractions;
using ZooApp.Application.Services;
using ZooApp.Domain.Animals;
using ZooApp.Domain.Things;
using Moq;

namespace ZooApp.Tests;

public class InventoryReportTests
{
    [Fact]
    public void InventoryReport_ContainsAnimalsAndThings()
    {
        var vet = new Mock<IVetClinic>();
        var repo = new Mock<IZooRepository>();
        var service = new ZooService(vet.Object, repo.Object);

        var inventory = new List<ZooApp.Domain.Abstractions.IInventory>
        {
            new Rabbit("Bunny", 1, 3, 8),
            new Table("Desk", 101),
            new Computer("PC", 202)
        };

        repo.Setup(r => r.GetInventory()).Returns(inventory);

        var report = service.GetInventoryReport().ToList();

        Assert.Equal(3, report.Count);
        Assert.Contains(report, i => i.Name == "Bunny" && i.Number == 1);
        Assert.Contains(report, i => i.Name == "Desk" && i.Number == 101);
        Assert.Contains(report, i => i.Name == "PC" && i.Number == 202);
    }
}
