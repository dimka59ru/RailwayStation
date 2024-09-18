using RailwayStation.Algorithms;
using RailwayStation.Models;
using RailwayStation.Models.Station;
using System.Text;

namespace RailwayStation.Infrastructure.Commands;
public class FindPathCommand : NonTerminatingCommand
{
    private readonly IFindPathOnStationStrategy findPathAlgo;
    private readonly StationBase station;

    public int? FromIndex { get; private set; }
    public int? TargetIndex { get; private set; }

    public FindPathCommand(IUserInterface userInterface, IFindPathOnStationStrategy findPathAlgo, StationBase station) : base(userInterface) 
    {
        this.findPathAlgo = findPathAlgo ?? throw new ArgumentNullException(nameof(findPathAlgo));
        this.station = station ?? throw new ArgumentNullException(nameof(station));
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
        if (FromIndex == null || TargetIndex == null) 
        {
            UserInterface.WriteMessage($"Не заданы {nameof(FromIndex)} и/или {nameof(TargetIndex)}");
            return false;
        }
           
        var foundPath = findPathAlgo.FindPath(station, (int)FromIndex, (int)TargetIndex);
        if (foundPath.Count > 0) 
        {

            var pathBuilder = new StringBuilder();
            for (var i = 0; i < foundPath.Count; i++) 
            {
                var segment = foundPath[i];
                pathBuilder.Append($"{segment.Name}");
                
                if (i < foundPath.Count - 1) 
                {                    
                    pathBuilder.Append($" --> ");
                }
            }

            UserInterface.WriteMessage($"Короткий путь от {FromIndex} к {TargetIndex}:");
            UserInterface.WriteMessage($"{pathBuilder}");
        }
        else 
        {
            UserInterface.WriteMessage($"Путь от {FromIndex} к {TargetIndex} не найден!");
        }
        
        return true;
    }

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
