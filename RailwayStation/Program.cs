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
        Console.Write(adjacentPoint.Name + ", ");
    }
    Console.WriteLine();
}
