using RailwayStation.Infrastructure.Commands;
using RailwayStation.Models;

namespace RailwayStation.Services;

public interface IAppCommandFactory 
{
    AppCommand GetCommand(string input);
}

public class AppCommandFactory : IAppCommandFactory
{
    private readonly IUserInterface userInterface;
    private readonly IFindPathOnStationAlgo findPathAlgo;

    public AppCommandFactory(IUserInterface userInterface, IFindPathOnStationAlgo findPathAlgo) 
    {
        this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
        this.findPathAlgo = findPathAlgo ?? throw new ArgumentNullException(nameof(findPathAlgo));
    }
    public AppCommand GetCommand(string input) 
    {
        return input.ToLower() switch {
            "q" or "quit" => new QuitCommand(userInterface),
            "f" or "find" => new FindPathCommand(userInterface, findPathAlgo),
            "?" => new HelpCommand(userInterface),
            _ => new UnknownCommand(userInterface),
        };
    }
}
