using RailwayStation.Algorithms;
using RailwayStation.Models.Station;

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

// Поиск в ширину
var result = FindPathAlgorithms.TryFindPath(station, 1, 3, new QueuePointStorage(), out var foundPath);


Console.WriteLine();
Console.WriteLine();

// Поиск кратчайшего пути волновым алгоритмом 
var startIndex = 1;
var targetIndex = 3;
result = FindPathAlgorithms.TryFindPathWaveMethod(station, startIndex, targetIndex, out foundPath);
if (result) 
{
    Console.WriteLine($"Короткий путь от {startIndex} к {targetIndex} без учета длины участков:");
    for (var i = 0; i < foundPath.Count; i++) 
    {
        var point = foundPath[i];
        Console.Write($"{point.Name}");
        if (i < foundPath.Count - 1) 
        {
            Console.Write($" --> ");
        }        
    }
}
else 
{
    Console.WriteLine($"Путь от {startIndex} к {targetIndex} не найден!");
}

Console.ReadKey();


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
