using dg.wordlekata.Models;

namespace dg.wordlekata.Services;

public class GameService
{
    private readonly IWordService _wordService;
    private readonly GameState _gameState;
    
    public GameState GameState => _gameState;

    public GameService(IWordService wordService)
    {
        _wordService = wordService;
        _gameState = new GameState();
    }

    public void NewGame()
    {
        _gameState.Clear();
        var chosenWord = _wordService.GetWord();
        _gameState.ChosenWord = chosenWord;
    }

    public void Guess(string guessedWord)
    {
        _gameState.Guesses.Add(guessedWord);
    }
}