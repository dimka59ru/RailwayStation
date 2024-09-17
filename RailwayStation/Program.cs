using Microsoft.Extensions.DependencyInjection;
using RailwayStation.Infrastructure;
using RailwayStation.Models;
using RailwayStation.Models.Station;
using RailwayStation.Services;

IServiceCollection services = new ServiceCollection();
ConfigureServices(services);
IServiceProvider serviceProvider = services.BuildServiceProvider();

var service = serviceProvider.GetService<IStationInfoService>();
service.Run();


void ConfigureServices(IServiceCollection services) 
{
    services.AddTransient<IUserInterface, ConsoleUserInterface>();
    services.AddTransient<IStationInfoService,StationInfoService>();
    services.AddTransient<IAppCommandFactory, AppCommandFactory>();    
    services.AddSingleton<StationBase, Station>(); 
}
