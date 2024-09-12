using RailwayStation.Infrastructure.Commands;
using RailwayStation.Models;

namespace RailwayStation.Services;

public interface IAppCommandeFactory 
{
    AppCommand GetCommand(string input);
}
public class AppCommandFactory : IAppCommandeFactory
{
    private readonly IUserInterface userInterface;

    public AppCommandFactory(IUserInterface userInterface) 
    {
        this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
    }
    public AppCommand GetCommand(string input) 
    {
        switch (input) 
        {
            case "q":
            case "quit":
                return new QuitCommand(userInterface);
            case "?":
                return new HelpCommand(userInterface);

            default: throw new NotImplementedException();
                //return new UnknownCommand(userInterface);
        }
    }
}
