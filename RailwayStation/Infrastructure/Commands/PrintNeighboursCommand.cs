using RailwayStation.Models;
using RailwayStation.Models.Station;
using System.Text;

namespace RailwayStation.Infrastructure.Commands;
public class PrintNeighboursCommand : NonTerminatingCommand
{
    private readonly StationBase station;
    public PrintNeighboursCommand(IUserInterface userInterface, StationBase station) : base(userInterface) 
    {
        this.station = station ?? throw new ArgumentNullException(nameof(station));
    }

    internal override bool InternalCommand() 
    {
        foreach (var point in station.Points) 
        {
            PrintAdjacentPoints(point);
        }

        return true;
    }

    private void PrintAdjacentPoints(Point point)
    {  
        var stringBuilder = new StringBuilder();

        foreach (var adjacentPoint in station.GetAdjacentPointList(point)) 
        {
            stringBuilder.Append($"({adjacentPoint.Point.Name}: {adjacentPoint.Dist}), ");
        }

        UserInterface.WriteMessage($"{point.Name}: {stringBuilder}");
    }
}
