using RailwayStation.Models.Station;

var station = new Station();

foreach (var item in station.Segments.Select((value, i) => new {i, value.Name}))
{
    var segmentName = item.Name;
    var index = item.i;
    Console.WriteLine($"[{index+1}].[{segmentName}]");
}

Console.ReadKey();
