using ZooApp.Infrastructure.Persistence;
using ZooApp.Domain.Animals;
using ZooApp.Domain.Things;
namespace ZooApp.Tests;

public class RepositoryTests
{
    [Fact]
    public void AddAnimal_AddsAnimalToAnimalsAndInventory()
    {
        var repo = new InMemoryZooRepository();
        var rabbit = new Rabbit("Bunny", 1, 3, 8);

        repo.AddAnimal(rabbit);

        var animals = repo.GetAnimals();
        var inventory = repo.GetInventory();

        Assert.Single(animals);
        Assert.Single(inventory);
        Assert.Equal("Bunny", animals[0].Name);
        Assert.Equal("Bunny", inventory[0].Name);
    }
    
    [Fact]
    public void AddThing_AddsThingOnlyToInventory()
    {
        var repo = new InMemoryZooRepository();
        var table = new Table("Desk", 42);

        repo.AddThing(table);

        var animals = repo.GetAnimals();
        var inventory = repo.GetInventory();

        Assert.Empty(animals);
        Assert.Single(inventory);
        Assert.Equal("Desk", inventory[0].Name);
    }
    
    [Fact]
    public void GetAnimalsAndInventory_AreReadOnly()
    {
        var repo = new InMemoryZooRepository();
        repo.AddThing(new Computer("PC", 101));

        var animals = repo.GetAnimals();
        var inventory = repo.GetInventory();

        Assert.NotNull(animals);
        Assert.NotNull(inventory);

        Assert.Throws<NotSupportedException>(() =>
        {
            ((ICollection<Animal>)animals).Remove(null!);
        });

        Assert.Throws<NotSupportedException>(() =>
        {
            ((ICollection<Domain.Abstractions.IInventory>)inventory).Clear();
        });
    }
}
