using RailwayStation.Models.Station;

namespace RailwayStation.Algorithms.Filling;
public interface IFillingStrategy
{
    List<Point> GetParkVertex(StationBase station, int parkIndex);
}
