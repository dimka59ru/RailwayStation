using Microsoft.Extensions.DependencyInjection;
using RailwayStation.Algorithms;
using RailwayStation.Infrastructure;
using RailwayStation.Models;
using RailwayStation.Models.Station;
using RailwayStation.Services;

IServiceCollection services = new ServiceCollection();
ConfigureServices(services);
IServiceProvider serviceProvider = services.BuildServiceProvider();

var service = serviceProvider.GetService<IStationInfoService>();
service.Run();


var station = new Station();

// Выведем список всех участков схемы станции
// в виде [Порядковый Номер].[Имя].
PrintSegments(station);

Console.WriteLine();
Console.WriteLine();

// Выведем смежные с текущей вершины
foreach (var point in station.Points)
{
    PrintAdjacentPoints(point);    
}

Console.WriteLine();
Console.WriteLine();

Console.ReadKey();


void ConfigureServices(IServiceCollection services) 
{
    services.AddTransient<IUserInterface, ConsoleUserInterface>();
    services.AddTransient<IStationInfoService,StationInfoService>();
    services.AddTransient<IAppCommandFactory, AppCommandFactory>();
    services.AddTransient<IFindPathOnStationAlgo, FindPathOnStationAlgo>();    
}

void PrintSegments(Station station)
{
    foreach (var item in station.Segments.Select((value, i) => new { i, value.Name }))
    {
        var segmentName = item.Name;
        var index = item.i;
        Console.WriteLine($"[{index + 1}].[{segmentName}]");
    }
}

void PrintAdjacentPoints(Point point)
{
    Console.Write($"{point.Name}: ");
    foreach (var adjacentPoint in station.GetAdjacentPointList(point))
    {
        Console.Write($"({adjacentPoint.Point.Name}: {adjacentPoint.Dist}), ");
    }
    Console.WriteLine();
}
