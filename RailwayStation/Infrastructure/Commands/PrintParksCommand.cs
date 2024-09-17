using RailwayStation.Algorithms.Filling;
using RailwayStation.Models;
using RailwayStation.Models.Station;

namespace RailwayStation.Infrastructure.Commands;
public class PrintParksCommand : NonTerminatingCommand
{
    private readonly IFillingStrategy fillingAlgo;
    private readonly StationBase station;
    public PrintParksCommand(IUserInterface userInterface, IFillingStrategy fillingAlgo, StationBase station) : base(userInterface) 
    {
        this.fillingAlgo = fillingAlgo ?? throw new ArgumentNullException(nameof(fillingAlgo));
        this.station = station ?? throw new ArgumentNullException(nameof(station));
    }

    internal override bool InternalCommand() 
    {
        
        foreach (var parkName in station.Parks.Select(park => park.Name)) 
        {
            var parkVertices = fillingAlgo.GetParkVertices(station, parkName);
            var verticesNamesArray = parkVertices.Select(t => t.Name);
            var verticesNames = string.Join(", ", verticesNamesArray);
            UserInterface.WriteMessage($"{parkName}: [{verticesNames}]");
        }
        return true;
    }
}
