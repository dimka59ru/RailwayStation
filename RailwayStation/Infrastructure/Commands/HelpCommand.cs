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
        UserInterface.WriteMessage("\tНапечатать все участки схемы станции (ps)");
        UserInterface.WriteMessage("\tНапечатать все парки схемы станции (pp)");
        UserInterface.WriteMessage("\tПоиск кратчайщего пути волновым методом (без учета веса) (fw)");
        UserInterface.WriteMessage("\tПоиск кратчайщего пути методом  Дейкстры (fd)");
        UserInterface.WriteMessage("\tСправка (?)");
        return true;
    }
}
