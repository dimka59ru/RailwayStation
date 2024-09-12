using RailwayStation.Models;

namespace RailwayStation.Infrastructure.Commands;
public class HelpCommand : NonTerminatingCommand
{
    public HelpCommand(IUserInterface userInterface) : base(userInterface) {
    }

    internal override bool InternalCommand() {
        return true;
    }
}
