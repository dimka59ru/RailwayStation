using RailwayStation.Models.Station;

namespace RailwayStation.Algorithms.Filling;
public class FillingPark : IFillingStrategy
{

    /// <summary>
    /// Поиск вершин парка
    /// </summary>
    /// <param name="station"></param>
    /// <param name="parkName">Имя парка</param>
    /// <returns>Список вершин парка или пустой список, если такого парка нет в станции</returns>
    public List<Point> GetParkVertices(StationBase station, string parkName) 
    {
        var park = station.Parks.FirstOrDefault(p => p.Name == parkName);
        if (park == null) 
        {
            return [];
        }

        var vertices = new List<Point>();

        foreach (var track in park.Traks) 
        {
            
            // словарь, в котороый будем записывать, сколько раз в пути встречается точка,
            // если 1 раз, значит это конец пути, если 2 - то это промежуточная точка
            var pointTimes = new Dictionary<Point, int>();
            foreach(var segment in track.Segments) 
            {
                if (pointTimes.TryGetValue(segment.From, out int value))
                {
                    pointTimes[segment.From] = value + 1;
                }
                else 
                {
                    pointTimes[segment.From] = 1;
                }


                if (pointTimes.TryGetValue(segment.To, out value)) 
                {
                    pointTimes[segment.To] = value + 1;
                }
                else 
                {
                    pointTimes[segment.To] = 1;
                }
            }

            // Выберем концы пути и сложим в результирующий список
            foreach (var pair in pointTimes.Where(p => p.Value == 1)) 
            {
                vertices.Add(pair.Key);
            }
        }

        return vertices;
    }
}
