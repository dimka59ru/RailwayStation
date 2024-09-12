using RailwayStation.Models;

namespace RailwayStation.Infrastructure.Commands;
public abstract class AppCommand
{
    private readonly bool isTerminatingCommand;
    protected IUserInterface UserInterface { get; }

    protected AppCommand(bool commandIsTerminating, IUserInterface userInterface) 
    {
        isTerminatingCommand = commandIsTerminating;
        UserInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
    }
   

    public virtual (bool wasSuccessful, bool souldQuit) Run() 
    {        
        return (InternalCommand(), isTerminatingCommand);
    }

    internal string GetParameter(string parameterName) 
    {
        return UserInterface.ReadValue($"Введите {parameterName}: ");
    }

    internal abstract bool InternalCommand();
}
