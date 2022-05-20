using dg.wordlekata.Services;

var gameService = new GameService();
var gameState = gameService.NewGame();
    
Console.WriteLine("Game started!");
Console.WriteLine($"Chosen word was {gameState.ChosenWord}");