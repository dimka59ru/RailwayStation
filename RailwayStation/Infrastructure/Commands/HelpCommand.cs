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
        UserInterface.WriteMessage("\tНапечатать все участки схемы станции (p)");
        UserInterface.WriteMessage("\tПоиск пути возновым методом (fw)");
        UserInterface.WriteMessage("\tСправка (?)");
        return true;
    }
}
