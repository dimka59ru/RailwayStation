using RailwayStation.Models.Station;

namespace RailwayStation.Algorithms;
public interface IFindPathStrategy<G, V>
    where G : class
    where V : class
{
    List<V> FindPath(G graph, int startIndex, int targetIndex);
}

public interface IFindPathOnStationStrategy : IFindPathStrategy<StationBase, Segment> { }
