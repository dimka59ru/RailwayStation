using RailwayStation.Algorithms;
using RailwayStation.Models.Station;

namespace RailwayStation.Tests;
public class FindPathDijkstraMethodTests
{
    [Fact]
    public void GetPathFromY1ToY24_ShouldGet13Segments() 
    {
        StationBase station = new Station();
        var findPathAlgo = new FindShortPathDijkstraMethod();
        var path = findPathAlgo.FindPath(station, 1, 24);
        Assert.Equal(13, path.Count);
    }

    [Fact]
    public void GetPathFromY2ToY21_ShouldBe22() 
    {
        StationBase station = new Station();
        var findPathAlgo = new FindShortPathDijkstraMethod();
        var path = findPathAlgo.FindPath(station, 2, 21);
        var length = path.Sum(s => s.Length);
        Assert.Equal(22, length);
    }

    [Theory]
    [InlineData(1, 24, 29)]
    [InlineData(1, 23, 23)]
    [InlineData(1, 16, 21)]
    [InlineData(7, 11, 29)]
    [InlineData(35, 29, 26)]
    public void GetPathLenght(int startSegmentIndex, int targetSegmentIndex, int expectedLength) 
    {
        StationBase station = new Station();
        var findPathAlgo = new FindShortPathDijkstraMethod();
        var path = findPathAlgo.FindPath(station, startSegmentIndex, targetSegmentIndex);
        var length = path.Sum(s => s.Length);
        Assert.Equal(expectedLength, length);
    }

    [Theory]
    [InlineData(1, 29, new string[] { "У26", "У2" })]   
    [InlineData(6, 18, new string[] { "У17", "У33", "У13", "У30", "У8", "У32" })]
    [InlineData(29, 30, new string[] { "У8", "У32", "У27", "У4", "У5" })]
    public void GetPath(int startSegmentIndex, int targetSegmentIndex, string[] expectedResult) 
    {
        StationBase station = new Station();
        var findPathAlgo = new FindShortPathDijkstraMethod();
        var path = findPathAlgo.FindPath(station, startSegmentIndex, targetSegmentIndex);
        var segments = path.Select(s => s.Name);
        Assert.Equal(expectedResult, segments);
    }
}
