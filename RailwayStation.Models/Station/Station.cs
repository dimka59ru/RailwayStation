namespace RailwayStation.Models.Station;

public abstract class StationBase
{
    public abstract IReadOnlyList<Point> Points { get; }
    public abstract IReadOnlyList<Segment> Segments { get; }
    public abstract IReadOnlyList<Track> Traks { get; }
    public abstract IReadOnlyList<Park> Parks { get; }

    /// <summary>
    /// Вывод смежных вершин и расстояний до них
    /// </summary>
    public List<(Point Point, int Dist)> GetAdjacentPointList(Point point) {
        var result = new List<(Point Point, int Dist)>();

        foreach (var segment in Segments) {
            if (segment.From == point) {
                result.Add((segment.To, segment.Length));
            }
            if (segment.To == point) {
                result.Add((segment.From, segment.Length));
            }
        }
        return result;
    }

    public Segment? GetSegment(int indexPoint1, int indexPoint2) 
    {
        var point1 = Points[indexPoint1];
        var point2 = Points[indexPoint2];

        foreach (var segment in Segments) 
        {
            if (segment.From == point1 && segment.To == point2) 
            {
                return segment;
            }
            if (segment.From == point2 && segment.To == point1) 
            {
                return segment;
            }
        }
        return null;
    } 
}

public class Station : StationBase
{
    private readonly List<Point> points = [];
    private readonly List<Segment> segments = [];
    private readonly List<Track> traks = [];
    private readonly List<Park> parks = [];

    public override  IReadOnlyList<Point> Points => points;
    public override  IReadOnlyList<Segment> Segments => segments;
    public override  IReadOnlyList<Track> Traks => traks;
    public override  IReadOnlyList<Park> Parks => parks;

    public Station() 
    {
        // Все точки станции
        for (int i = 1; i < 33; i++) 
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
        var s8 = new Segment("У8", points[3], points[30], 2);
        var s9 = new Segment("У9", points[30], points[20], 21);
        var s10 = new Segment("У10", points[20], points[19], 7);
        var s11 = new Segment("У11", points[19], points[18], 1);
        var s12 = new Segment("У12", points[1], points[2], 3);
        var s13 = new Segment("У13", points[2], points[4], 2);
        var s14 = new Segment("У14", points[4], points[15], 23);
        var s15 = new Segment("У15", points[15], points[16], 2);
        var s16 = new Segment("У16", points[16], points[17], 2);
        var s17 = new Segment("У17", points[5], points[31], 1);
        var s18 = new Segment("У18", points[31], points[14], 17);
        var s19 = new Segment("У19", points[6], points[9], 2);
        var s20 = new Segment("У20", points[9], points[12], 8);
        var s21 = new Segment("У21", points[12], points[13], 8);
        var s22 = new Segment("У22", points[7], points[8], 5);
        var s23 = new Segment("У23", points[8], points[10], 6);
        var s24 = new Segment("У24", points[10], points[11], 2);
        var s25 = new Segment("У25", points[25], points[24], 2);
        var s26 = new Segment("У26", points[22], points[21], 2);
        var s27 = new Segment("У27", points[27], points[29], 2);
        var s28 = new Segment("У28", points[24], points[28], 2);
        var s29 = new Segment("У29", points[21], points[20], 3);
        var s30 = new Segment("У30", points[3], points[2], 2);
        var s32 = new Segment("У32", points[29], points[30], 1);
        var s31 = new Segment("У31", points[19], points[16], 2);
        var s33 = new Segment("У33", points[4], points[5], 2);
        var s34 = new Segment("У34", points[15], points[14], 3);
        var s35 = new Segment("У35", points[31], points[6], 2);
        var s36 = new Segment("У36", points[14], points[13], 3);
        var s37 = new Segment("У37", points[9], points[8], 2);
        var s38 = new Segment("У38", points[12], points[10], 2);

        segments.Add(s1);
        segments.Add(s2);
        segments.Add(s3);
        segments.Add(s4);
        segments.Add(s5);
        segments.Add(s6);
        segments.Add(s7);
        segments.Add(s8);
        segments.Add(s9);
        segments.Add(s10);
        segments.Add(s11);
        segments.Add(s12);
        segments.Add(s13);
        segments.Add(s14);
        segments.Add(s15);
        segments.Add(s16);
        segments.Add(s17);
        segments.Add(s18);
        segments.Add(s19);
        segments.Add(s20);
        segments.Add(s21);
        segments.Add(s22);
        segments.Add(s23);
        segments.Add(s24);
        segments.Add(s25);
        segments.Add(s26);
        segments.Add(s27);
        segments.Add(s28);
        segments.Add(s29);
        segments.Add(s30);
        segments.Add(s31);
        segments.Add(s32);
        segments.Add(s33);
        segments.Add(s34);
        segments.Add(s35);
        segments.Add(s36);
        segments.Add(s37);
        segments.Add(s38);


        var track1 = new Track("Путь 1", [s2]);
        var track2 = new Track("Путь 2", [s4, s5]); 
        var track3 = new Track("Путь 3", [s6]);
        var track4 = new Track("Путь 4", [s8, s9, s10]);
        var track5 = new Track("Путь 5", [s13, s14, s15]);
        var track6 = new Track("Путь 6", [s17, s18]);
        var track7 = new Track("Путь 7", [s19, s20, s21]);
        var track8 = new Track("Путь 8", [s23]);

        traks.Add(track1);
        traks.Add(track2);
        traks.Add(track3);
        traks.Add(track4);
        traks.Add(track5);
        traks.Add(track6);
        traks.Add(track7);
        traks.Add(track8);

        var park1 = new Park("Парк 1", [track1, track2]);
        var park2 = new Park("Парк 2", [track4, track7]);
        var park3 = new Park("Парк 3", [track5, track6, track7, track8]);

        parks.Add(park1);
        parks.Add(park2);
        parks.Add(park3);
    }    
}
