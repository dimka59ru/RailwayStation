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

        var foundPath = new List<Point>();

        // Найдем узел From стартового сегмента и примем его за стартовый узел.
        var startPoint = station.Segments[startSegmentIndex - 1].From;
        var startPointIndex = station.Points.FindIndex(point => point == startPoint);

        // Найдем узел To целевого сегмента и примем его за целевой узел.
        var targetPoint = station.Segments[targetSegmentIndex - 1].To;
        var targetPointIndex = station.Points.FindIndex(point => point == targetPoint);

        // Массив, в котором будем помечать, на каком шаге посетили узел
        var marks = Enumerable.Repeat<int?>(null, station.Points.Count).ToArray();

        // стартовая ячейка помечена в 0
        int d = 0;     
        marks[startPointIndex] = d;
        
        bool stop;

        do 
        {
            stop = true; // предполагаем, что все свободные точки уже помечены
            for (var i = 0; i < station.Points.Count; i++)
            {  
                if (marks[i] == d)
                {
                    var point = station.Points[i];
                    var neighbours = station.GetAdjacentPointList(point);
                    // Проходим по всем соседям
                    for (var j = 0; j < neighbours.Count; j++) 
                    {
                        var p = neighbours[j].Point;
                        var pIndex = station.Points.FindIndex(point => point == p);

                        if (marks[pIndex] == null) 
                        {   
                            // найдены непомеченные узлы                         
                            stop = false;
                            marks[pIndex] = d + 1; // распространяем волну
                        }
                    }                    
                }
            }
            d++;
        } while (!stop && marks[targetPointIndex] == null);

        // Восстанавливаем путь
        if (marks[targetPointIndex] != null) 
        {

            var currentPoint = targetPoint;
            while (d > 0) 
            {
                foundPath.Add(currentPoint);
                d--;
                var pIndex = Array.FindIndex(marks, x => x == d);
                currentPoint = station.Points[pIndex];
            }            
            foundPath.Add(startPoint);            
        }

        return foundPath;
    }    
}
