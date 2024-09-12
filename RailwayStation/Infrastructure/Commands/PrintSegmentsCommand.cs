using RailwayStation.Models;
using RailwayStation.Models.Station;

namespace RailwayStation.Infrastructure.Commands;
public class PrintSegmentsCommand : NonTerminatingCommand
{
    private readonly StationBase station;

    public PrintSegmentsCommand(IUserInterface userInterface, StationBase station) : base(userInterface) {
        this.station = station ?? throw new ArgumentNullException(nameof(station));
    }

    internal override bool InternalCommand() {
        PrintSegments();
        return true;
    }

    private void PrintSegments() {
        foreach (var item in station.Segments.Select((value, i) => new { i, value.Name })) {
            var segmentName = item.Name;
            var index = item.i;
            UserInterface.WriteMessage($"[{index + 1}].[{segmentName}]");
        }
    }
}
