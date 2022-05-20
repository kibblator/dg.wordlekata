using dg.wordlekata.Models;

namespace dg.wordlekata.Services;

public class GameService
{
    private readonly IWordService _wordService;

    public GameService(IWordService wordService)
    {
        _wordService = wordService;
    }
    public GameState NewGame()
    {
        var chosenWord = _wordService.GetWord();
        return new GameState
        {
            ChosenWord = chosenWord
        };
    }
}