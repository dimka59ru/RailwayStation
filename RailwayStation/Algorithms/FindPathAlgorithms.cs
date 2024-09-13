using RailwayStation.Helpers;
using RailwayStation.Models.Station;

namespace RailwayStation.Algorithms;
public static class FindPathAlgorithms
{

    /// <summary>
    /// Общий алгоритм поиска. Если pointStorage стек, то ищем в глубину, если очередь, то в ширину
    /// </summary>
    /// <param name="station"></param>
    /// <param name="startSegment"></param>
    /// <param name="targetSegment"></param>
    /// <param name="pointStorage">Хранилище еще не посещенных узлов</param>
    /// <returns></returns>
    public static bool TryFindPath(Station station, int startSegmentIndex, int targetSegmentIndex, IPointStorage pointStorage, out List<Point> foundPath) 
    {        
        if (startSegmentIndex < 1 || startSegmentIndex > station.Segments.Count) 
        {
            throw new ArgumentOutOfRangeException($"{nameof(startSegmentIndex)} не может быть меньше 1 и больше {station.Segments.Count}");
        }

        if (targetSegmentIndex < 1 || targetSegmentIndex > station.Segments.Count) 
        {
            throw new ArgumentOutOfRangeException($"{nameof(targetSegmentIndex)} не может быть меньше 1 и больше {station.Segments.Count}");
        }

        foundPath = [];

        // Найдем узел From стартового сегмента и примем его за стартовый узел.
        var startPoint = station.Segments[startSegmentIndex - 1].From;    
        var startPointIndex = station.Points.FindIndex(point => point == startPoint);

        // Найдем узел To целевого сегмента и примем его за целевой узел.
        var targetPoint = station.Segments[targetSegmentIndex - 1].To;
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
}
