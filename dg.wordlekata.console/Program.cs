using System;
using dg.wordlekata.console.Helpers;
using dg.wordlekata.data.Installers;
using dg.wordlekata.Installers;
using dg.wordlekata.Services;
using Microsoft.Extensions.DependencyInjection;

namespace dg.wordlekata.console;

public static class Program
{
    private static GameService _gameService;
    private const int StartingLine = 4;
    private static void Main()
    {
        var serviceProvider = RegisterServices();

        _gameService = new GameService(serviceProvider.GetRequiredService<IWordService>());
        _gameService.NewGame();

        Console.WriteLine("Game started!");
        Console.WriteLine("Press the Escape (Esc) key to quit: \n");
        Console.WriteLine($"Chosen word was {_gameService.GameState.ChosenWord}");

        do
        {
            while (!Console.KeyAvailable)
            {
                var guess = ConsoleHelpers.ReadLineWithCancel();
                GuessSubmitted(guess);
            }
        } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
    }

    private static IServiceProvider RegisterServices()
    {
        var services = new ServiceCollection();
    
        ServiceInstaller.Install(services);
        RepositoryInstaller.Install(services);
    
        return services.BuildServiceProvider();
    }
    
    private static void GuessSubmitted(string guessWord)
    {
        _gameService.Guess(guessWord);

        for(var i = 0; i < _gameService.GameState.Guesses.Count; i++)
        {
            Console.SetCursorPosition(0, StartingLine + i);
            ConsoleHelpers.ClearCurrentConsoleLine();
            Console.WriteLine(_gameService.GameState.Guesses[i]);
        }
    }
}