using RailwayStation.Models;

namespace RailwayStation.Infrastructure.Commands;
public class QuitCommand : AppCommand
{
    public QuitCommand(IUserInterface userInterface) : base(true, userInterface) {
    }

    internal override bool InternalCommand() 
    {
        UserInterface.WriteMessage("Спасибо, до свидания!");
        return true;
    }
}
