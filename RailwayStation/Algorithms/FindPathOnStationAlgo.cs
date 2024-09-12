using RailwayStation.Models;
using RailwayStation.Models.Station;

namespace RailwayStation.Algorithms;

public class FindPathOnStationAlgo : IFindPathOnStationAlgo
{
    public bool TryFindPathWithoutWeignt(Station station, int startSegmentIndex, int targetSegmentIndex, out List<Point> foundPath) 
    {
        if (startSegmentIndex < 1 || startSegmentIndex > station.Segments.Count) {
            throw new ArgumentOutOfRangeException($"{nameof(startSegmentIndex)} не может быть меньше 1 и больше {station.Segments.Count}");
        }

        if (targetSegmentIndex < 1 || targetSegmentIndex > station.Segments.Count) {
            throw new ArgumentOutOfRangeException($"{nameof(targetSegmentIndex)} не может быть меньше 1 и больше {station.Segments.Count}");
        }

        foundPath = [];

        // Найдем узел From стартового сегмента и примем его за стартовый узел.
        var startPoint = station.Segments[startSegmentIndex - 1].From;

        // Найдем узел To целевого сегмента и примем его за целевой узел.
        var targetPoint = station.Segments[targetSegmentIndex - 1].To;

        int d = 0;
        // стартовая ячейка помечена в 0
        startPoint.Mark = 0;
        bool stop;
        do {
            stop = true; // предполагаем, что все свободные точки уже помечены
            foreach (var point in station.Points) {
                if (point.Mark == d) {
                    var neighbours = station.GetAdjacentPointList(point);
                    // Проходим по всем не помеченным соседям
                    foreach (var p in neighbours.Select(neighbour => neighbour.Point).Where(point => point.Mark is null)) {
                        stop = false;    // найдены непомеченные клетки
                        p.Mark = d + 1;  // распространяем волну
                    }
                }
            }
            d++;
        } while (!stop && targetPoint.Mark is null);

        // Восстанавливаем путь
        if (targetPoint.Mark != null) {
            var currentPoint = targetPoint;
            while (currentPoint != startPoint) {
                foundPath.Add(currentPoint);
                var neighbours = station.GetAdjacentPointList(currentPoint);
                currentPoint = neighbours.First(p => p.Point.Mark == currentPoint.Mark - 1).Point;
            }
            foundPath.Add(startPoint);
            return true;
        }

        return false;
    }

    public bool TryFindPathWithWeignt(Station graph, int startIndex, int targetIndex, out List<Point> foundPath) => throw new NotImplementedException();
}
