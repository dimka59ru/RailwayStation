using RailwayStation.Helpers;
using RailwayStation.Models.Station;

namespace RailwayStation.Algorithms;
public class FindShortPathWaveMethod : IFindPathOnStationStrategy
{
    public List<Point> FindPath(StationBase station, int startSegmentIndex, int targetSegmentIndex) {

        if (startSegmentIndex < 1 || startSegmentIndex > station.Segments.Count) {
            throw new ArgumentOutOfRangeException($"{nameof(startSegmentIndex)} не может быть меньше 1 и больше {station.Segments.Count}");
        }

        if (targetSegmentIndex < 1 || targetSegmentIndex > station.Segments.Count) {
            throw new ArgumentOutOfRangeException($"{nameof(targetSegmentIndex)} не может быть меньше 1 и больше {station.Segments.Count}");
        }
        

        // Найдем узлы стартового сегмента
        var startPointFrom = station.Segments[startSegmentIndex - 1].From;
        var startPointFromIndex = station.Points.FindIndex(point => point == startPointFrom);

        var startPointTo = station.Segments[startSegmentIndex - 1].To;
        var startPointToIndex = station.Points.FindIndex(point => point == startPointTo);
        

        // Ищем от узла From заданного участка
        var path1 = FindPathInternal(station, startPointFromIndex, targetSegmentIndex);

        // Ищем от узла To заданного участка
        var path2 = FindPathInternal(station, startPointToIndex, targetSegmentIndex);

        if (path1.Count < path2.Count) {
            return path1;
        }
        
        return path2;       
    }


    private static List<Point> FindPathInternal(StationBase station, int startPointIndex, int targetSegmentIndex) 
    {
        var foundPath = new List<Point>();

        // Найдем узлы целевого сегмента.
        var targetPointFrom = station.Segments[targetSegmentIndex - 1].From;
        var targetPointFromIndex = station.Points.FindIndex(point => point == targetPointFrom);

        var targetPointTo = station.Segments[targetSegmentIndex - 1].To;
        var targetPointToIndex = station.Points.FindIndex(point => point == targetPointTo);

        // Массив, в котором будем помечать, на каком шаге посетили узел
        var marks = Enumerable.Repeat<int?>(null, station.Points.Count).ToArray();

        // стартовая ячейка помечена в 0
        int d = 0;
        marks[startPointIndex] = d;

        bool stop;

        do {
            stop = true; // предполагаем, что все свободные точки уже помечены
            for (var i = 0; i < station.Points.Count; i++) {
                if (marks[i] == d) {
                    var point = station.Points[i];
                    var neighbours = station.GetAdjacentPointList(point);
                    // Проходим по всем соседям
                    for (var j = 0; j < neighbours.Count; j++) {
                        var p = neighbours[j].Point;
                        var pIndex = station.Points.FindIndex(point => point == p);

                        if (marks[pIndex] == null) {
                            // найдены непомеченные узлы                         
                            stop = false;
                            marks[pIndex] = d + 1; // распространяем волну
                        }
                    }
                }
            }
            d++;
        // Останвливаемся, если дошли до точки From или To целевого сегмента, или если идти больше некуда
        } while (!stop && marks[targetPointFromIndex] == null && marks[targetPointToIndex] == null);

        // Восстанавливаем путь
        int? targetPointIndex = null;
        if (marks[targetPointFromIndex] != null) 
        {
            targetPointIndex = targetPointFromIndex;
        }
        else if (marks[targetPointToIndex] != null) 
        {
            targetPointIndex = targetPointToIndex;
        }

        if (targetPointIndex != null)
        {
            var currentPoint = station.Points[targetPointIndex.Value];
            
            while (d > 0) 
            {
                foundPath.Add(currentPoint);
                d--;
                // Берем всех соседей текущей точки и находим такого соседа,
                // у которого отметка (d меньше на 1, чем у текущего)
                var neighbours = station.GetAdjacentPointList(currentPoint);
                foreach (var neighbor in neighbours) 
                {
                    var pIndex = station.Points.FindIndex(point => point == neighbor.Point);
                    if (marks[pIndex] == d) 
                    {
                        currentPoint = station.Points[pIndex];
                        break;
                    }
                }
            }
            foundPath.Add(station.Points[startPointIndex]);
        }

        return foundPath;
    }
}
