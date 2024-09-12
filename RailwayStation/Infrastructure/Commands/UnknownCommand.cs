using RailwayStation.Models;

namespace RailwayStation.Infrastructure.Commands;
public class UnknownCommand : NonTerminatingCommand
{
    public UnknownCommand(IUserInterface userInterface) : base(userInterface) {
    }

    internal override bool InternalCommand() 
    {
        UserInterface.WriteWarning("Не удалось распознать команду.");
        return false;
    }
}
