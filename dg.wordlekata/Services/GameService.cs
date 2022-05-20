using dg.wordlekata.Models;

namespace dg.wordlekata.Services;

public class GameService
{
    private readonly IWordService _wordService;
    private const int GuessLimit = 5;

    public GameState GameState { get; }

    public GameService(IWordService wordService)
    {
        _wordService = wordService;
        GameState = new GameState();
    }

    public void NewGame()
    {
        GameState.Clear();
        var chosenWord = _wordService.GetWord();
        GameState.ChosenWord = chosenWord;
    }

    public void Guess(string guessedWord)
    {
        GameState.Guesses.Add(guessedWord);
        CheckWon(guessedWord);
    }

    private void CheckWon(string guessedWord)
    {
        if (guessedWord == GameState.ChosenWord)
            GameState.Status = GameStatus.Won;
        else if (GameState.Guesses.Count >= GuessLimit)
        {
            GameState.Status = GameStatus.Lost;
        }
    }
}