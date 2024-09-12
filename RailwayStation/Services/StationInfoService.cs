using RailwayStation.Models;

namespace RailwayStation.Services;

public interface IStationInfoService 
{
    void Run();
}

public class StationInfoService : IStationInfoService
{
    private readonly IUserInterface userInterface;
    private readonly IAppCommandFactory commandFactory;

    public StationInfoService(IUserInterface userInterface, IAppCommandFactory commandFactory) 
    {
        this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
        this.commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));
    }

   

    public void Run() 
    {
        var response = commandFactory.GetCommand("?").Run();

        while (!response.souldQuit) 
        {
            var input = userInterface.ReadValue("> ");
            var command = commandFactory.GetCommand(input);
            response = command.Run();

            if (!response.wasSuccessful) 
            {
                userInterface.WriteMessage("Введите ? для получения справки по командам.");
            }
        }
    }
}
