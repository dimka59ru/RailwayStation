using RailwayStation.Models.Station;

namespace RailwayStation.Tests;

public class StationTest
{

    [Fact]
    public void GetAdjacentPointListTest_MustHaveOneNeighbor() 
    {
        StationBase station = new Station();
        var neighbours = station.GetAdjacentPointList(station.Points[0]);
        Assert.Single(neighbours);
    }

    [Fact]
    public void GetAdjacentPointListTest_MustHaveThreeNeighbor()
    {
        StationBase station = new Station();
        var neighbours = station.GetAdjacentPointList(station.Points[25]);
        Assert.Equal(3, neighbours.Count);
    }

    [Theory]
    [InlineData("Т26", new string[] { "Т27", "Т23", "Т25" })]
    public void GetAdjacentPointListTest1(string pointName, string[] expectedResult)
    {
        StationBase station = new Station();
        var point = station.Points.First(x => x.Name == pointName);
        Assert.NotNull(point);

        var neighboursNames = station.GetAdjacentPointList(point).Select(n => n.Point.Name).ToArray();
        
        Assert.Equal(expectedResult, neighboursNames);
        
    }

    [Fact]
    public void ShouldGetAllPoints() 
    {
        StationBase station = new Station();       
        Assert.Equal(32, station.Points.Count);
    }

    [Fact]
    public void ShouldGetAllSegments() 
    {
        StationBase station = new Station();
        Assert.Equal(38, station.Segments.Count);
    }

    [Fact]
    public void ShouldGetAllTraks() 
    {
        StationBase station = new Station();
        Assert.Equal(8, station.Traks.Count);
    }

    [Fact]
    public void GetSegmentTest_ShouldGetY7Segment() 
    {
        StationBase station = new Station();
        var segment = station.GetSegment(0, 3);
        Assert.NotNull(segment);
        Assert.Equal("У7", segment.Name);
    }

    [Fact]
    public void GetSegmentTest_ShouldGetNull() 
    {
        StationBase station = new Station();
        var segment = station.GetSegment(0, 1);
        Assert.Null(segment);       
    }
}
