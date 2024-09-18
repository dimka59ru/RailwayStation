using RailwayStation.Algorithms.Filling;
using RailwayStation.Models.Station;

namespace RailwayStation.Tests;
public class FillingParkTests 
{
    [Theory]
    [InlineData("Парк 1", 4)]
    [InlineData("Парк 2", 4)]
    [InlineData("Парк 3", 8)]
    public void GetParkVerticesCount(string parkName, int verticesCount) 
    {
        StationBase station = new Station();
        var fillingAlgo = new FillingPark();
        var parkVertices = fillingAlgo.GetParkVertices(station, parkName);
        Assert.Equal(verticesCount, parkVertices.Count);
    }

    [Theory]
    [InlineData("Парк 1", new string[] { "Т26", "Т23", "Т28", "Т22" })]
    [InlineData("Парк 2", new string[] { "Т4", "Т20", "Т7", "Т14" })]
    [InlineData("Парк 3", new string[] { "Т3", "Т17", "Т6", "Т15", "Т7", "Т14", "Т9", "Т11" })]
    public void GetParkVerticesNames(string parkName, string[] expectedResult) 
    {
        StationBase station = new Station();
        var fillingAlgo = new FillingPark();
        var parkVertices = fillingAlgo.GetParkVertices(station, parkName);
        var verticesNames = parkVertices.Select(x => x.Name);
        Assert.Equal(expectedResult, verticesNames);
    }
}
