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
    private readonly IFindPathOnStationAlgo findPathAlgo;
    private readonly StationBase station;

    public AppCommandFactory(IUserInterface userInterface, IFindPathOnStationAlgo findPathAlgo, StationBase station) 
    {
        this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
        this.findPathAlgo = findPathAlgo ?? throw new ArgumentNullException(nameof(findPathAlgo));
        this.station = station ?? throw new ArgumentNullException(nameof(station));
    }
    public AppCommand GetCommand(string input) 
    {
        return input.ToLower() switch {
            "q" or "quit" => new QuitCommand(userInterface),
            "f" or "find" => new FindPathCommand(userInterface, findPathAlgo, station),
            "?" => new HelpCommand(userInterface),
            _ => new UnknownCommand(userInterface),
        };
    }
}
