using RailwayStation.Algorithms.Helpers;
using RailwayStation.Models.Station;

namespace RailwayStation.Algorithms;
public class FindShortPathDijkstraMathod : IFindPathOnStationStrategy
{
    public List<Segment> FindPath(StationBase station, int startSegmentIndex, int targetSegmentIndex) 
    {
        if (startSegmentIndex < 1 || startSegmentIndex > station.Segments.Count) 
        {
            throw new ArgumentOutOfRangeException($"{nameof(startSegmentIndex)} не может быть меньше 1 и больше {station.Segments.Count}");
        }

        if (targetSegmentIndex < 1 || targetSegmentIndex > station.Segments.Count) 
        {
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

        if (path1.Dist < path2.Dist) {
            return station.ConvertPointPath2SegmentPath(path1.Path);
        }

        return station.ConvertPointPath2SegmentPath(path2.Path);
    }

    private static (List<Point> Path, int Dist) FindPathInternal(StationBase station, int startPointIndex, int targetSegmentIndex) 
    {
        // в начале поиска расстояние до всех узлов кроме начального равны ∞        
        var distances = station.Points.ToDictionary(p => p, p => int.MaxValue);
        var previous = new Dictionary<Point, Point>();
        var notVisited = new HashSet<Point>(station.Points);

        distances[station.Points[startPointIndex]] = 0;

        while (notVisited.Count > 0) 
        {
            var nearestPoint = notVisited.OrderBy(v => distances[v]).FirstOrDefault();
            notVisited.Remove(nearestPoint);

            // получим соседей текущего узла
            var neighbours = station.GetAdjacentPointList(nearestPoint);

            foreach (var neighbour in neighbours) 
            {                
                if (notVisited.Contains(neighbour.Point)) 
                {
                    var currentDistance = distances[nearestPoint] + neighbour.Dist;
                    if (currentDistance < distances[neighbour.Point]) 
                    {
                        distances[neighbour.Point] = currentDistance;
                        previous[neighbour.Point] = nearestPoint;
                    }
                }
            }
        }

        // Найдем узлы целевого сегмента.
        var targetPointFrom = station.Segments[targetSegmentIndex - 1].From; 
        var targetPointTo = station.Segments[targetSegmentIndex - 1].To;
       

        var shortPath1 = distances[targetPointFrom];
        var shortPath2 = distances[targetPointTo];

        Point? targetPoint = null;
        if (shortPath1 < shortPath2) 
        {
            targetPoint = targetPointFrom;
        }
        else 
        {
            targetPoint = targetPointTo;
        }

        var foundPath = new List<Point>();
        foundPath.Add(targetPoint);

        // Восстанавливаем путь
        var distToTarget = distances[targetPoint];
        var shortPath = distToTarget;
        while (distToTarget != 0) 
        {
            var neighbours = station.GetAdjacentPointList(targetPoint);
            foreach (var neighbour in neighbours) 
            {                
                if (distToTarget - neighbour.Dist == distances[neighbour.Point]) 
                {
                    foundPath.Add(neighbour.Point);
                    targetPoint = neighbour.Point;
                    distToTarget = distances[targetPoint];
                    break;
                }
            }
        }

        return (foundPath, shortPath);
    }
}
