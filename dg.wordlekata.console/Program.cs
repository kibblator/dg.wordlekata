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
                Console.WriteLine();
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
        Console.WriteLine($"You guessed '{guessWord}'");
        Console.WriteLine("Guesses so far are:");
        _gameService.GameState.Guesses.ForEach(g =>
        {
            Console.WriteLine(g);
        });
    }
}