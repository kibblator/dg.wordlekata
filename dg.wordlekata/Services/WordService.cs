using System.Linq;
using dg.wordlekata.data.Repositories;

namespace dg.wordlekata.Services;

public interface IWordService
{
    string GetWord();
}

public class WordService : IWordService
{
    private readonly IWordRepository _wordRepository;

    public WordService(IWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }

    public string GetWord()
    {
        var wordList = _wordRepository.GetWordList();
        var usedWordList = _wordRepository.GetUsedWords();
        var word = wordList.Except(usedWordList).First();
        
        _wordRepository.SetWordAsUsed(word);
        
        return word;
    }
}