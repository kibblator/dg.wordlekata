namespace dg.wordlekata.data.Repositories;

public interface IWordRepository
{
    IEnumerable<string> GetWordList();
    IEnumerable<string> GetUsedWords();
    void SetWordAsUsed(string word);
}

public class WordRepository : IWordRepository
{
    private const string UsedWordPath = "./Resources/UsedWordList.txt";
    private const string WordListPath = "./Resources/WordList.txt";
    public IEnumerable<string> GetWordList()
    {
        return File.ReadAllLines(WordListPath);
    }

    public IEnumerable<string> GetUsedWords()
    {
        if (File.Exists(UsedWordPath))
            return File.ReadAllLines(UsedWordPath);
        return Enumerable.Empty<string>();
    }

    public void SetWordAsUsed(string word)
    {
        using var sw = new StreamWriter(UsedWordPath, true);
        sw.WriteLine(word);
    }
}