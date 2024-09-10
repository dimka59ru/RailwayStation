namespace RailwayStation.Models.Station;

public class Station
{
    private readonly List<Point> points = [];

    public List<Segment> Segments { get; } = [];

    public Station() 
    {
        var p1 = new Point();
        var p2 = new Point();
        var p3 = new Point();
        var p4 = new Point();
        var p5 = new Point();
        var p6 = new Point();
        var p7 = new Point();
        var p8 = new Point();
        var p9 = new Point();
        var p10 = new Point();

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

        Segments.Add(new Segment("segment 1", p1, p2, 10));
        Segments.Add(new Segment("segment 2", p2, p3, 15));
        Segments.Add(new Segment("segment 3", p3, p4, 8));
        Segments.Add(new Segment("segment 4", p5, p6, 20));
        Segments.Add(new Segment("segment 5", p6, p3, 12));
        Segments.Add(new Segment("segment 6", p9, p7, 11));
        Segments.Add(new Segment("segment 7", p7, p6, 13));
        Segments.Add(new Segment("segment 8", p6, p8, 13));
        Segments.Add(new Segment("segment 9", p7, p8, 18));
        Segments.Add(new Segment("segment 10", p9, p10, 20));
        Segments.Add(new Segment("segment 11", p10, p8, 12));
    }
}
