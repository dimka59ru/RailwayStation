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

    public AppCommandFactory(IUserInterface userInterface) 
    {
        this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
    }
    public AppCommand GetCommand(string input) 
    {
        return input.ToLower() switch {
            "q" or "quit" => new QuitCommand(userInterface),
            "?" => new HelpCommand(userInterface),
            _ => new UnknownCommand(userInterface),
        };
    }
}
