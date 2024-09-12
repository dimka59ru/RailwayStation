using RailwayStation.Models;

namespace RailwayStation.Infrastructure.Commands;
public class HelpCommand : NonTerminatingCommand
{
    public HelpCommand(IUserInterface userInterface) : base(userInterface) {
    }

    internal override bool InternalCommand() 
    {
        UserInterface.WriteMessage("Команды:");
        UserInterface.WriteMessage("\tВыход (q)");
        UserInterface.WriteMessage("\tПоиск пути (f)");
        UserInterface.WriteMessage("\tСправка (?)");
        return true;
    }
}
