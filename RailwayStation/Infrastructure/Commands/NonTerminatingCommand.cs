using RailwayStation.Models;

namespace RailwayStation.Infrastructure.Commands;
public abstract class NonTerminatingCommand : AppCommand
{
    protected NonTerminatingCommand(IUserInterface userInterface) : base(false, userInterface) {
    }
}
