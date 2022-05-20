using System.Collections.Generic;
using dg.wordlekata.data.Repositories;
using dg.wordlekata.Services;
using Moq;
using Xunit;

namespace dg.wordlekata.tests;

public class WordServiceTests
{
    private readonly Mock<IWordRepository> _wordRepoMock;
    private readonly WordService _wordService;
    
    public WordServiceTests()
    {
        _wordRepoMock = new Mock<IWordRepository>();
        
        var wordList = new List<string>
        {
            "woman",
            "guest"
        };
        var usedWords = new List<string>
        {
            "woman"
        };
        _wordRepoMock.Setup(wr => wr.GetWordList()).Returns(wordList);
        _wordRepoMock.Setup(wr => wr.GetUsedWords()).Returns(usedWords);
        _wordRepoMock.Invocations.Clear();
        
        _wordService = new WordService(_wordRepoMock.Object);
    }
    
    [Fact]
    public void WordService_GetWord_PreviouslyUsedWordShouldNotBeGenerated()
    {
       //Act
        var word = _wordService.GetWord();

        //Assert
        Assert.NotNull(word);
        Assert.Equal("guest", word);
    }

    [Fact]
    public void WordService_GetWord_MarksWordAsUsed()
    {
        //Act
        var word = _wordService.GetWord();

        //Assert
        _wordRepoMock.Verify(wr => wr.SetWordAsUsed(word), Times.Once);
    }
}