using RailwayStation.Models;
using RailwayStation.Models.Station;
using System.Text;

namespace RailwayStation.Infrastructure.Commands;
public class FindPathCommand : NonTerminatingCommand
{
    private readonly IFindPathOnStationAlgo findPathAlgo;
    public int? FromIndex { get; private set; }
    public int? TargetIndex { get; private set; }

    public FindPathCommand(IUserInterface userInterface, IFindPathOnStationAlgo findPathAlgo) : base(userInterface) 
    {
        this.findPathAlgo = findPathAlgo ?? throw new ArgumentNullException(nameof(findPathAlgo));
    }

    public override (bool wasSuccessful, bool souldQuit) Run() 
    {        
        var allParametersCompleted = false;

        while (!allParametersCompleted) 
        {
            allParametersCompleted = GetParameters();            
        }

        return base.Run();
    }    

    internal override bool InternalCommand() 
    {
        var station = new Station();

        if (FromIndex == null || TargetIndex == null) 
        {
            UserInterface.WriteMessage($"Не заданы {nameof(FromIndex)} и/или {nameof(TargetIndex)}");
            return false;
        }

        // Поиск кратчайшего пути волновым алгоритмом       
        var result = findPathAlgo.TryFindPathWithoutWeignt(station, (int)FromIndex, (int)TargetIndex, out var foundPath);
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

            UserInterface.WriteMessage($"Короткий путь от {FromIndex} к {TargetIndex} без учета длины участков:");
            UserInterface.WriteMessage($"{pathBuilder}");
        }
        else 
        {
            UserInterface.WriteMessage($"Путь от {FromIndex} к {TargetIndex} не найден!");
        }
        
        return true;
    }

    // TODO добавить проверку на диапазон
    private bool GetParameters() 
    {
        if (FromIndex == null) 
        {
            string fromIndexStr = GetParameter(nameof(FromIndex));
            if (int.TryParse(fromIndexStr, out int index)) 
            {
                FromIndex = index;                
            }
            else 
            {
                UserInterface.WriteWarning($"Не удалось распознать число!");
                return false;
            }
        }

        if (TargetIndex == null) 
        {
            string fromIndexStr = GetParameter(nameof(TargetIndex));
            if (int.TryParse(fromIndexStr, out int index)) 
            {
                TargetIndex = index;
            }
            else 
            {
                UserInterface.WriteWarning($"Не удалось распознать число!");
                return false;
            }
        }

        return true;
    }
}
