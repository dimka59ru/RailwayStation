namespace RailwayStation.Models.Station;

public class Station
{
    private readonly List<Point> points = [];
    private readonly List<Segment> segments = [];

    public IReadOnlyList<Point> Points => points;
    public IReadOnlyList<Segment> Segments => segments;

    public Station() 
    {
        var p1 = new Point("p1");
        var p2 = new Point("p2");
        var p3 = new Point("p3");
        var p4 = new Point("p4");
        var p5 = new Point("p5");
        var p6 = new Point("p6");
        var p7 = new Point("p7");
        var p8 = new Point("p8");
        var p9 = new Point("p9");
        var p10 = new Point("p10");

        points.Add(p1);
        points.Add(p2);
        points.Add(p3);
        points.Add(p4);
        points.Add(p5);
        points.Add(p6);
        points.Add(p7);
        points.Add(p8);
        points.Add(p9);
        points.Add(p10);
        
        segments.Add(new Segment("segment 1", p1, p2, 10));
        segments.Add(new Segment("segment 2", p2, p3, 15));
        segments.Add(new Segment("segment 3", p3, p4, 8));
        segments.Add(new Segment("segment 4", p5, p6, 20));
        segments.Add(new Segment("segment 5", p6, p3, 12));
        segments.Add(new Segment("segment 6", p9, p7, 11));
        segments.Add(new Segment("segment 7", p7, p6, 13));
        segments.Add(new Segment("segment 8", p6, p8, 13));
        segments.Add(new Segment("segment 9", p7, p8, 18));
        segments.Add(new Segment("segment 10", p9, p10, 20));
        segments.Add(new Segment("segment 11", p10, p8, 12));
    }

    /// <summary>
    /// Вывод смежных вершин и расстояний до них
    /// </summary>
    public List<(Point Point, int Dist)> GetAdjacentPointList(Point point) 
    {
        var result = new List<(Point Point, int Dist)>();

        foreach (var segment in Segments) 
        {            
            if (segment.From == point) 
            {
                result.Add((segment.To, segment.Length));
            }
        }
        return result;
    }
}
