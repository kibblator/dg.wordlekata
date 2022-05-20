using dg.wordlekata.data.Installers;
using dg.wordlekata.Installers;
using dg.wordlekata.Services;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = RegisterServices();

var gameService = new GameService(serviceProvider.GetRequiredService<IWordService>());
var gameState = gameService.NewGame();
    
Console.WriteLine("Game started!");
Console.WriteLine($"Chosen word was {gameState.ChosenWord}");

static IServiceProvider RegisterServices()
{
    var services = new ServiceCollection();
    
    ServiceInstaller.Install(services);
    RepositoryInstaller.Install(services);
    
    return services.BuildServiceProvider();
}