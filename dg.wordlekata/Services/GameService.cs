using dg.wordlekata.Models;

namespace dg.wordlekata.Services;

public class GameService
{
    private readonly IWordService _wordService;
    private readonly IGuessService _guessService;
    private const int GuessLimit = 5;

    public GameState GameState { get; }

    public GameService(IWordService wordService, IGuessService guessService)
    {
        _wordService = wordService;
        _guessService = guessService;
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
        var guess = _guessService.SubmitGuess(GameState.ChosenWord, guessedWord);
        GameState.Guesses.Add(guess);
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