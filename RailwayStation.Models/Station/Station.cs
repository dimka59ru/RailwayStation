namespace RailwayStation.Models.Station;

public class Station
{
    private readonly List<Point> points = [];
    private readonly List<Segment> segments = [];
    private readonly List<Track> traks = [];

    public IReadOnlyList<Point> Points => points;
    public IReadOnlyList<Segment> Segments => segments;
    public IReadOnlyList<Track> Traks => traks;

    public Station() 
    {
        // Все точки станции
        for (int i = 1; i < 32; i++) 
        {
            points.Add(new Point($"Т{i}"));
        }

        var s1 = new Segment("У1", points[26], points[25], 7);
        var s2 = new Segment("У2", points[25], points[22], 7);
        var s3 = new Segment("У3", points[22], points[23], 3);
        var s4 = new Segment("У4", points[27], points[24], 3);
        var s5 = new Segment("У5", points[24], points[21], 11);
        var s6 = new Segment("У6", points[29], points[28], 4);
        var s7 = new Segment("У7", points[0], points[3], 2);

        segments.Add(s1);
        segments.Add(s2);
        segments.Add(s3);
        segments.Add(s4);
        segments.Add(s5);
        segments.Add(s6);
        segments.Add(s7);

        var track1 = new Track("Путь 1");
        track1.Segments.Add(s2);

        var track2 = new Track("Путь 2");
        track2.Segments.Add(s4);
        track2.Segments.Add(s5);

        //var p1 = new Point("p1");
        //var p2 = new Point("p2");
        //var p3 = new Point("p3");
        //var p4 = new Point("p4");
        //var p5 = new Point("p5");
        //var p6 = new Point("p6");
        //var p7 = new Point("p7");
        //var p8 = new Point("p8");
        //var p9 = new Point("p9");
        //var p10 = new Point("p10");

        //points.Add(p1);
        //points.Add(p2);
        //points.Add(p3);
        //points.Add(p4);
        //points.Add(p5);
        //points.Add(p6);
        //points.Add(p7);
        //points.Add(p8);
        //points.Add(p9);
        //points.Add(p10);

        //segments.Add(new Segment("segment 1", p1, p2, 10));
        //segments.Add(new Segment("segment 2", p2, p3, 15));
        //segments.Add(new Segment("segment 3", p3, p4, 8));
        //segments.Add(new Segment("segment 4", p5, p6, 20));
        //segments.Add(new Segment("segment 5", p6, p3, 12));
        //segments.Add(new Segment("segment 6", p9, p7, 11));
        //segments.Add(new Segment("segment 7", p7, p6, 13));
        //segments.Add(new Segment("segment 8", p6, p8, 13));
        //segments.Add(new Segment("segment 9", p7, p8, 18));
        //segments.Add(new Segment("segment 10", p9, p10, 20));
        //segments.Add(new Segment("segment 11", p10, p8, 12));

        //var p1 = new Point("A");
        //var p2 = new Point("M");
        //var p3 = new Point("N");
        //var p4 = new Point("P");
        //var p5 = new Point("B");        

        //points.Add(p1);
        //points.Add(p2);
        //points.Add(p3);
        //points.Add(p4);
        //points.Add(p5);        

        //segments.Add(new Segment("segment 1", p1, p2, 2));
        //segments.Add(new Segment("segment 2", p2, p3, 2));
        //segments.Add(new Segment("segment 3", p3, p5, 4));
        //segments.Add(new Segment("segment 4", p1, p4, 3));
        //segments.Add(new Segment("segment 5", p4, p5, 4));
        //segments.Add(new Segment("segment 6", p1, p5, 6));
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
            if (segment.To == point) 
            {
                result.Add((segment.From, segment.Length));
            }
        }
        return result;
    }
}
