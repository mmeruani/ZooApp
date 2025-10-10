# ZooApp — учёт зоопарка (ООП, SOLID, DI, тесты)

Консольное приложение для учёта животных и вещей в зоопарке.  
Перед приёмом животного выполняется проверка здоровья (ветклиника).  
Поддерживается инвентаризация (вещи и животные считаются объектами на балансе), расчёт суммарного корма и список животных для контактного зоопарка.

## Содержание
- [Функциональность](#функциональность)
- [Архитектура и SOLID](#архитектура-и-solid)
- [DI-контейнер](#di-контейнер)
- [Структура проекта](#структура-проекта)
- [Сборка и запуск](#сборка-и-запуск)
- [Тесты и покрытие](#тесты-и-покрытие)
- [Примеры работы](#примеры-работы)

## Функциональность
1. Добавление животных (Monkey, Rabbit, Tiger, Wolf) и вещей (Table, Computer).
2. Перед приёмом животного — проверка здоровья (`VetClinic`). Шанс, что животное здорово - 80% (определяется рандомно).
3. Отчёт:
   - **сколько животных**;
   - **сколько кг корма** требуется в сутки.
4. Список животных для **контактного зоопарка** (травоядные с `Kindness > 5`).
5. Инвентарный отчёт: **наименование и инвентарный номер** всех вещей и животных на балансе.

## Архитектура и SOLID
**Слои:**
- `Domain` — сущности и интерфейсы:  
  - `IInventory` (на балансе: `Number`, `Name`), `IAlive` (живые: `Food`);  
  - `Animal` (база), `Herbo` (травоядные, `Kindness`), `Predator`;  
  - конкретные животные: `Monkey`, `Rabbit`, `Tiger`, `Wolf`;  
  - вещи: `Thing`, `Table`, `Computer`.
- `Application` — вынесение бизнес-логики:  
  - `IVetClinic`, `IZooRepository`, `IZooService`;  
  - `ZooService` — операции сервиса зоопарка.
- `Infrastructure` — реализации:  
  - `VetClinic` — проверка здоровья;  
  - `InMemoryZooRepository` — in-memory хранилище.
- `Presentation` — консоль: `ConsoleApp`, `MenuStrings`.

**SOLID:**
- **S**: каждый класс выполняет одну роль (UI, сервис, репозиторий, ветклиника, сущности).
- **O**: добавление новых видов животных/вещей без изменения существующего кода сервиса.
- **L**: `Herbo` и `Predator` корректно подставимы вместо `Animal`.
- **I**: раздельные `IInventory` и `IAlive` — вещи не всегда живые.
- **D**: `ZooService` зависит от абстракций `IVetClinic`, `IZooRepository`; конкретные реализации внедряет DI.

## DI-контейнер
Используется `Microsoft.Extensions.DependencyInjection`.  
Регистрации в `Composition/DiConfig.cs`:
```csharp
services.AddSingleton<IVetClinic, VetClinic>();
services.AddSingleton<IZooRepository, InMemoryZooRepository>();
services.AddSingleton<IZooService, ZooService>();
services.AddSingleton<ConsoleApp>();
```

## Структура проекта
```csharp src/ZooApp/
  Program.cs
  Composition/DiConfig.cs
  Presentation/
    ConsoleApp.cs
    MenuStrings.cs
  Domain/
    Abstractions/{IInventory.cs, IAlive.cs}
    Animals/{Animal.cs, Herbo.cs, Predator.cs, Monkey.cs, Rabbit.cs, Tiger.cs, Wolf.cs}
    Things/{Thing.cs, Table.cs, Computer.cs}
  Application/
    Abstractions/{IVetClinic.cs, IZooRepository.cs, IZooService.cs}
    Services/ZooService.cs
  Infrastructure/
    VetClinic/VetClinic.cs
    Persistence/InMemoryZooRepository.cs

tests/ZooApp.Tests/
  ZooServiceTests.cs
  RepositoryTests.cs
  PettingZooTests.cs
  InventoryReportTests.cs
```

## Сборка и запуск
**Точка входа Program.cs:**
```csharp
var provider = DiConfig.Build();
var app = provider.GetRequiredService<ConsoleApp>();
app.Run();
```

## Тесты и покрытие
```csharp
# запуск тестов
dotnet test
```
**В тестах проверяются:**
   - приём/отказ животного (вызовы `IVetClinic`, добавление в репозиторий);
   - подсчёт суммарного корма и количества животных;
   - фильтр контактного зоопарка (только травоядные с `Kindness > 5`);
   - инвентаризация (включает и животных, и вещи);
   - неизменяемость возвращаемых коллекций (`AsReadOnly()`).

## Примеры работы
``` csharp
============== ЗООПАРК ==============
1. Добавить животное
2. Добавить вещь
3. Показать статистику животных
4. Показать животных для контактного зоопарка
5. Показать инвентарный список
0. Выход
====================================
> 1
Введите тип животного (Monkey / Rabbit / Tiger / Wolf):
Rabbit
Имя: Bunny
Инвентарный номер: 101
Кг корма в день: 3
Уровень доброты (0–10): 8
Bunny принят(а) в зоопарк!

> 3
🐾 В зоопарке 1 животных.
Всего требуется корма: 3 кг/день.

> 4
Животные, которых можно в контактный зоопарк:
• Bunny (доброта: 8, корм: 3 кг/день)

> 5
Инвентаризация:
Bunny — №101
```
