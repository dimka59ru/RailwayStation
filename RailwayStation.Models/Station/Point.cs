namespace RailwayStation.Models.Station;

// Vertex
public class Point
{
    public string Name { get; }

    // Пометки для использования в волновом алгоритме поиска пути
    public int? Mark { get; set; }

    public Point(string name) 
    {
        if (string.IsNullOrEmpty(name)) 
        {
            throw new ArgumentException($"\"{nameof(name)}\" cannot be undefined or empty.", nameof(name));
        }

        Name = name;
    }    
}
