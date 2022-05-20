using System;
using System.Threading;
using dg.wordlekata.console.Helpers;
using dg.wordlekata.data.Installers;
using dg.wordlekata.Installers;
using dg.wordlekata.Services;
using Microsoft.Extensions.DependencyInjection;

namespace dg.wordlekata.console;

public static class Program
{
    private static GameService _gameService;
    private const int StartingLine = 5;
    private static void Main()
    {
        var serviceProvider = RegisterServices();

        _gameService = new GameService(serviceProvider.GetRequiredService<IWordService>(),
            serviceProvider.GetRequiredService<IGuessService>());
        _gameService.NewGame();

        Console.WriteLine();
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
        if (guessWord.Length != _gameService.GameState.ChosenWord.Length)
        {
            GetUserToGuessAgain();
        }
        else
        {
            _gameService.Guess(guessWord);

            for(var i = 0; i < _gameService.GameState.Guesses.Count; i++)
            {
                Console.SetCursorPosition(0, StartingLine + i);
                ConsoleHelpers.ClearCurrentConsoleLine();
                ConsoleHelpers.WriteGuessWord(_gameService.GameState.Guesses[i]);
            }
        }
    }

    private static void GetUserToGuessAgain()
    {
        var cursorTop = Console.GetCursorPosition().Top;
        Console.SetCursorPosition(0, Console.WindowTop);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Word length incorrect, please guess again");
        Thread.Sleep(1000);
        ConsoleHelpers.ClearCurrentConsoleLine();
        Console.ResetColor();
        Console.SetCursorPosition(0, cursorTop);
        ConsoleHelpers.ClearCurrentConsoleLine();
    }
}