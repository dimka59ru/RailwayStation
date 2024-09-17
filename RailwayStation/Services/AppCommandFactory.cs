using RailwayStation.Algorithms;
using RailwayStation.Algorithms.Filling;
using RailwayStation.Infrastructure.Commands;
using RailwayStation.Models;
using RailwayStation.Models.Station;

namespace RailwayStation.Services;

public interface IAppCommandFactory 
{
    AppCommand GetCommand(string input);
}

public class AppCommandFactory : IAppCommandFactory
{
    private readonly IUserInterface userInterface;    
    private readonly StationBase station;

    public AppCommandFactory(IUserInterface userInterface, StationBase station) 
    {
        this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));        
        this.station = station ?? throw new ArgumentNullException(nameof(station));
    }
    public AppCommand GetCommand(string input) 
    {
        return input.ToLower() switch {
            "q" or "quit" => new QuitCommand(userInterface),
            "fw" or "findwave" => new FindPathCommand(userInterface, new FindShortPathWaveMethod(), station),
            "fd" or "finddijkstra" => new FindPathCommand(userInterface, new FindShortPathDijkstraMethod(), station),
            "ps" or "printsegments" => new PrintSegmentsCommand(userInterface, station),
            "pp" or "printparks" => new PrintParksCommand(userInterface, new FillingPark(), station),
            "?" => new HelpCommand(userInterface),
            _ => new UnknownCommand(userInterface),
        };
    }
}
