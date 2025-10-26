using ZooApp.Application.Abstractions;
using ZooApp.Domain.Animals;
using ZooApp.Domain.Things;
namespace ZooApp.Presentation;

public class ConsoleApp
{
    private readonly IZooService _service;

    public ConsoleApp (IZooService service)
    {
        _service = service;        
    }

    public void Run()
    {
        while (true)
        {
            MenuStrings.ShowMainMenu();
            Console.Write("> ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    MenuStrings.AddAnimal(_service);
                    break;
                case "2":
                    MenuStrings.AddThing(_service);
                    break;
                case "3":
                    MenuStrings.ShowSummary(_service);
                    break;
                case "4":
                    MenuStrings.ShowPettingZoo(_service);
                    break;
                case "5":
                    MenuStrings.ShowInventory(_service);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Некорректный ввод.\n");
                    break;
            }
        }    
    }
}
