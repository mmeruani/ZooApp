using ZooApp.Application.Abstractions;
using ZooApp.Domain.Animals;
using ZooApp.Domain.Things;
namespace ZooApp.Presentation;

public static class MenuStrings
{
    public static void ShowMainMenu()
    {
        Console.WriteLine("============== ЗООПАРК ==============");
        Console.WriteLine("1. Добавить животное");
        Console.WriteLine("2. Добавить вещь");
        Console.WriteLine("3. Показать статистику животных");
        Console.WriteLine("4. Показать животных для контактного зоопарка");
        Console.WriteLine("5. Показать инвентарный список");
        Console.WriteLine("0. Выход");
        Console.WriteLine("====================================");
    }

    public static void AddAnimal(IZooService service)
    {
        Console.WriteLine("\nВведите тип животного (Monkey / Rabbit / Tiger / Wolf):");
        var type = Console.ReadLine()?.Trim();
        
        Console.Write("Имя: ");
        var name = Console.ReadLine() ?? "Безымянный";

        Console.Write("Инвентарный номер: ");
        if (!int.TryParse(Console.ReadLine(), out int number))
        {
            Console.WriteLine("Ошибка: номер должен быть числом.\n");
            return;
        }

        Console.Write("Кг корма в день: ");
        if (!int.TryParse(Console.ReadLine(), out int food))
        {
            Console.WriteLine("Ошибка: корм должен быть числом.\n");
            return;
        }

        Animal animal;
        if (type?.ToLower() is "monkey" or "rabbit")
        {
            Console.Write("Уровень доброты (0–10): ");
            if (!int.TryParse(Console.ReadLine(), out int kindness))
            {
                Console.WriteLine("Ошибка: доброта должна быть числом.\n");
                return;
            }

            animal = type.ToLower() switch
            {
                "monkey" => new Monkey(name, number, food, kindness),
                "rabbit" => new Rabbit(name, number, food, kindness),
                _ => throw new InvalidOperationException()
            };
        }
        else if (type?.ToLower() is "tiger" or "wolf")
        {
            animal = type.ToLower() switch
            {
                "tiger" => new Tiger(name, number, food),
                "wolf" => new Wolf(name, number, food),
                _ => throw new InvalidOperationException()
            };
        }
        else
        {
            Console.WriteLine("Неизвестный тип животного.\n");
            return;
        }
        
        var accepted = service.TryAcceptAnimal(animal);
        if (accepted)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{animal.Name} принят(а) в зоопарк!");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{animal.Name} не прошёл(-а) проверку ветклиники.\n");
            Console.ResetColor();
        }
 
    }
    
    public static void AddThing(IZooService service)
    {
        Console.WriteLine("\nВведите тип вещи (Table / Computer):");
        var type = Console.ReadLine()?.Trim();

        Console.Write("Название: ");
        var name = Console.ReadLine() ?? "Без названия";

        Console.Write("Инвентарный номер: ");
        if (!int.TryParse(Console.ReadLine(), out int number))
        {
            Console.WriteLine("Ошибка: номер должен быть числом.\n");
            return;
        }

        Thing thing = type?.ToLower() switch
        {
            "table" => new Table(name, number),
            "computer" => new Computer(name, number),
            _ => throw new InvalidOperationException("Неизвестный тип вещи.")
        };

        service.AddThing(thing);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Вещь \"{thing.Name}\" добавлена на баланс.\n");
        Console.ResetColor();
    }
    
    public static void ShowSummary(IZooService service)
    {
        var count = service.GetAnimalsCount();
        var totalFood = service.GetTotalFoodPerDay();
        Console.WriteLine($"\nВ зоопарке {count} животных.");
        Console.WriteLine($"Всего требуется корма: {totalFood} кг/день.\n");
    }

    public static void ShowPettingZoo(IZooService service)
    {
        var animals = service.GetPettingZooCandidates().ToList();
        Console.WriteLine("\nЖивотные, которым можно в контактный зоопарк:");
        if (!animals.Any())
        {
            Console.WriteLine("(нет подходящих)");
        }
        else
        {
            foreach (Herbo a in animals)
            {
                Console.WriteLine($"• {a.Name} (доброта: {a.Kindness}, корм: {a.Food} кг/день)");
            }
        }
        Console.WriteLine();
    }
    
    public static void ShowInventory(IZooService service)
    {
        var items = service.GetInventoryReport().ToList();
        Console.WriteLine("\nИнвентаризация:");
        foreach (var i in items)
        {
            Console.WriteLine($"{i.Name} — №{i.Number}");    
        }    
        Console.WriteLine();
    }
}
