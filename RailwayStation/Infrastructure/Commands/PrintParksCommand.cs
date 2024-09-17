using RailwayStation.Models;
using RailwayStation.Models.Station;

namespace RailwayStation.Infrastructure.Commands;
public class PrintParksCommand : NonTerminatingCommand
{
    private readonly StationBase station;
    public PrintParksCommand(IUserInterface userInterface, StationBase station) : base(userInterface) 
    {
        this.station = station ?? throw new ArgumentNullException(nameof(station));
    }

    internal override bool InternalCommand() 
    {
        foreach (var park in station.Parks) 
        {
            var traks = park.Traks.Select(t => t.Name);
            var traksNames = string.Join(", ", traks);
            UserInterface.WriteMessage($"{park.Name}: [{traksNames}]");
        }
        return true;
    }
}
