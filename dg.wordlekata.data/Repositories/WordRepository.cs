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
        if (!File.Exists(UsedWordPath))
            File.Create(UsedWordPath);
        
        using var fileStream = File.OpenWrite(UsedWordPath);
        using var streamWriter = new StreamWriter(fileStream);
        
        streamWriter.WriteLine(word);
    }
}