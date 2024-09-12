using RailwayStation.Models.Station;

namespace RailwayStation.Models;
public interface IFindPathAlgo<G, V> 
    where G : class
    where V : class
{
    bool TryFindPathWithoutWeignt(G graph, int startIndex, int targetIndex, out List<V> foundPath);
    bool TryFindPathWithWeignt(G graph, int startIndex, int targetIndex, out List<V> foundPath);
}

public interface IFindPathOnStationAlgo : IFindPathAlgo<StationBase, Point> { }
