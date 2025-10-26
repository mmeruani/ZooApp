using Microsoft.Extensions.DependencyInjection;
using ZooApp.Composition;
using ZooApp.Presentation;

var provider = DiConfig.Build();
var app = provider.GetRequiredService<ConsoleApp>();
app.Run();
