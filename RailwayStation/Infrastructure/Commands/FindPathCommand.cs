using RailwayStation.Models;
using RailwayStation.Models.Station;
using System.Text;

namespace RailwayStation.Infrastructure.Commands;
public class FindPathCommand : NonTerminatingCommand
{
    private readonly IFindPathOnStationAlgo findPathAlgo;

    public FindPathCommand(IUserInterface userInterface, IFindPathOnStationAlgo findPathAlgo) : base(userInterface) 
    {
        this.findPathAlgo = findPathAlgo ?? throw new ArgumentNullException(nameof(findPathAlgo));
    }

    internal override bool InternalCommand() 
    {
        var station = new Station();

        // Поиск кратчайшего пути волновым алгоритмом 
        var startIndex = 1;
        var targetIndex = 3;
        var result = findPathAlgo.TryFindPathWithoutWeignt(station, startIndex, targetIndex, out var foundPath);
        if (result) 
        {

            var pathBuilder = new StringBuilder();
            for (var i = 0; i < foundPath.Count; i++) 
            {
                var point = foundPath[i];
                pathBuilder.Append($"{point.Name}");
                
                if (i < foundPath.Count - 1) 
                {                    
                    pathBuilder.Append($" --> ");
                }
            }

            UserInterface.WriteMessage($"Короткий путь от {startIndex} к {targetIndex} без учета длины участков:");
            UserInterface.WriteMessage($"{pathBuilder}");
        }
        else 
        {
            UserInterface.WriteMessage($"Путь от {startIndex} к {targetIndex} не найден!");
        }
        
        return true;
    }
}
