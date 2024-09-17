using RailwayStation.Models.Station;

namespace RailwayStation.Algorithms.Filling;
public interface IFillingStrategy
{
    List<Point> GetParkVertices(StationBase station, string parkName);
}
