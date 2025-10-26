using Microsoft.Extensions.DependencyInjection;
using ZooApp.Application.Abstractions;
using ZooApp.Application.Services;
using ZooApp.Infrastructure.Persistence;
using ZooApp.Infrastructure.VetClinic;
using ZooApp.Presentation;

namespace ZooApp.Composition;

public static class DiConfig
{
    public static IServiceProvider Build()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IVetClinic, VetClinic>();
        services.AddSingleton<IZooRepository, InMemoryZooRepository>();
        services.AddSingleton<IZooService, ZooService>();

        services.AddSingleton<ConsoleApp>();

        return services.BuildServiceProvider();
    }
}
