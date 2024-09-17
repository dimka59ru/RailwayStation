using RailwayStation.Models.Station;

namespace RailwayStation.Algorithms.Helpers;
public static class StationHelpers
{
    public static List<Segment> ConvertPointPath2SegmentPath(this StationBase station, List<Point> pointPath) 
    {
        var segmentPath = new List<Segment>();

        for (int i = 0; i < pointPath.Count - 1; i++) 
        {
            var indexPoint1 = station.Points.FindIndex(point => point == pointPath[i]);
            var indexPoint2 = station.Points.FindIndex(point => point == pointPath[i + 1]);
            var segment = station.GetSegment(indexPoint1, indexPoint2);
            if (segment != null) 
            {
                segmentPath.Add(segment);
            }
            else 
            {
                // Путь не найден
                return [];
            }
        }
        return segmentPath;
    }
}
