using RailwayStation.Models.Station;

namespace RailwayStation.Algorithms;
public static class FindPathAlgorithms
{
    //public static bool FindPath(Station station, Segment startSegment, Segment targetSegment, ISegmentStorage segmentStorage)

    /// <summary>
    /// 
    /// </summary>
    /// <param name="station"></param>
    /// <param name="startSegment"></param>
    /// <param name="targetSegment"></param>
    /// <param name="pointStorage">Хранилище еще не посещенных узлов</param>
    /// <returns></returns>
    public static bool FindPath(Station station, int startSegmentIndex, int targetSegmentIndex, IPointStorage pointStorage) 
    {
        // TODO Написать тест
        if (startSegmentIndex < 1 || startSegmentIndex > station.Segments.Count) 
        {
            throw new ArgumentOutOfRangeException($"{nameof(startSegmentIndex)} не может быть меньше 1 и больше {station.Segments.Count}");
        }

        if (targetSegmentIndex < 1 || targetSegmentIndex > station.Segments.Count) 
        {
            throw new ArgumentOutOfRangeException($"{nameof(targetSegmentIndex)} не может быть меньше 1 и больше {station.Segments.Count}");
        }

        // Найдем узел To стартового сегмента и примем его за стартовый узел.
        var startPoint = station.Segments[startSegmentIndex - 1].To;    
        var startPointIndex = station.Points.FindIndex(point => point == startPoint);

        // Найдем узел From целевого сегмента и примем его за целевой узел.
        var targetPoint = station.Segments[targetSegmentIndex - 1].From;
        var targetPointIndex = station.Points.FindIndex(point => point == targetPoint);

        // в начале поиска расстояние до всех узлов кроме начального равны ∞        
        var dist = Enumerable.Repeat(double.PositiveInfinity, station.Points.Count).ToArray();
        dist[startPointIndex] = 0;

        // массив для пометки, заходили ли в узел
        var visitedPoints = new int[station.Points.Count]; // TODO можно убрать

        // положим startPointIndex внутрь pointStorage
        pointStorage.Insert(startPointIndex);

        while(!pointStorage.IsEmpty()) 
        {
            var currentPointIndex = pointStorage.GetFirst();

            if (currentPointIndex ==  targetPointIndex) 
            {
                // конец поиска, целевой узел найден, а значит и путь до него
                return true;
            }

            // получим соседей текущего узла
            var neighbours = station.GetAdjacentPointList(station.Points[currentPointIndex]);

            foreach (var neighbour in neighbours) 
            {
                var pointToGo = neighbour.Point;
                var distToGo = neighbour.Dist;
                // если этот узел встречается впервые, то помечаем его, как посещенный
                var pointToGoIndex = station.Points.FindIndex(point => point == pointToGo);

                if (visitedPoints[pointToGoIndex] == 0) 
                {
                    visitedPoints[pointToGoIndex] = 1;

                    // обновляем расстояние от стартового узла
                    dist[pointToGoIndex] = dist[currentPointIndex] + distToGo;

                    pointStorage.Insert(pointToGoIndex);
                }
                else 
                {
                    // иначе нам нужно решить конфликт дублирования(два пути к одному узлу). Выбираем из двух путей более короткий.
                    var distFromCurrentPoint = distToGo;
                    if (dist[currentPointIndex] + distFromCurrentPoint < dist[pointToGoIndex]) 
                    {
                        dist[pointToGoIndex] = dist[currentPointIndex] + distFromCurrentPoint;
                    }
                }
            }
        }

        return false;
    }


    public static bool WaveFindPath(Station station, int startSegmentIndex, int targetSegmentIndex) {
        // TODO Написать тест
        if (startSegmentIndex < 1 || startSegmentIndex > station.Segments.Count) 
        {
            throw new ArgumentOutOfRangeException($"{nameof(startSegmentIndex)} не может быть меньше 1 и больше {station.Segments.Count}");
        }

        if (targetSegmentIndex < 1 || targetSegmentIndex > station.Segments.Count) 
        {
            throw new ArgumentOutOfRangeException($"{nameof(targetSegmentIndex)} не может быть меньше 1 и больше {station.Segments.Count}");
        }

        // Найдем узел To стартового сегмента и примем его за стартовый узел.
        var startPoint = station.Segments[startSegmentIndex - 1].To;        

        // Найдем узел From целевого сегмента и примем его за целевой узел.
        var targetPoint = station.Segments[targetSegmentIndex - 1].From;     

        int lastMark = 0; 
        MarkPoints(station, startPoint, targetPoint, lastMark);


        Console.WriteLine();

        // Алгоритм не работает, пока не переделаем GetAdjacentPointList чтоб было не From To, а просто связи
        // Или сделать подобный алгоритм, но в обратную сторону
        if (targetPoint.Mark != null) 
        {
            var currentPoint = targetPoint;
            while (currentPoint != startPoint) 
            {
                Console.Write($"{currentPoint.Name} - ");
                var neighbours = station.GetAdjacentPointList(currentPoint);
                currentPoint = neighbours.First(p => p.Point.Mark == currentPoint.Mark - 1).Point;
            }
            return true;
        }

        return false;
    }

    private static void MarkPoints(Station station, Point startPoint, Point targetPoint, int lastMark) 
    {
        startPoint.Mark = lastMark;
        var neighbours = station.GetAdjacentPointList(startPoint);
        while (targetPoint.Mark is null && neighbours.Count > 0) 
        {
            lastMark++;
            foreach (var neighbour in neighbours) 
            {               
                MarkPoints(station, neighbour.Point, targetPoint, lastMark);
            }
        }        
    }

    public static int FindIndex<T>(this IEnumerable<T> list, Func<T, bool> predicate) 
    {
        var matchingIndices = list.Select((value, index) => new { value, index }).Where(x => predicate(x.value)).Select(x => (int?)x.index);
        return matchingIndices.FirstOrDefault() ?? -1;
    }
}
