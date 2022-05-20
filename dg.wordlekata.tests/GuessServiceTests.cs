using System.Linq;
using dg.wordlekata.Models;
using dg.wordlekata.Services;
using Xunit;

namespace dg.wordlekata.tests;

public class GuessServiceTests
{
    private readonly GuessService _guessService;
    
    public GuessServiceTests()
    {
        _guessService = new GuessService();
    }

    [Fact]
    public void GuessService_WrongLetter_IsDetected()
    {
        //Arrange
        const string selectedWord = "xxxxx";
        const string guessedWord = "yxxdx";
        
        //Act
        var guessResult = _guessService.SubmitGuess(selectedWord, guessedWord);
        
        //Assert
        Assert.Equal(2, guessResult.Count(gr => gr.LetterMatchType == LetterMatchType.Grey));
    }

    [Fact]
    public void GuessService_CorrectLetterCorrectPlace_IsDetected()
    {
        //Arrange
        const string selectedWord = "piano";
        const string guessedWord = "burnt";
        
        //Act
        var guessResult = _guessService.SubmitGuess(selectedWord, guessedWord);
        
        //Assert
        Assert.Equal(1, guessResult.Count(gr => gr.LetterMatchType == LetterMatchType.Green));
    }

    [Fact]
    public void GuessService_CorrectLetterWrongPlace_IsDetected()
    {
        //Arrange
        const string selectedWord = "xyxxx";
        const string guessedWord = "ydddd";
        
        //Act
        var guessResult = _guessService.SubmitGuess(selectedWord, guessedWord);
        
        //Assert
        Assert.Equal(1, guessResult.Count(gr => gr.LetterMatchType == LetterMatchType.Yellow));
    }

    [Fact]
    public void GuessService_SameLettersAllJumbled_IsDetected()
    {
        //Arrange
        const string selectedWord = "abcde";
        const string guessedWord = "baecd";
        
        //Act
        var guessResult = _guessService.SubmitGuess(selectedWord, guessedWord);
        
        //Assert
        Assert.Equal(5, guessResult.Count(gr => gr.LetterMatchType == LetterMatchType.Yellow));
        Assert.Equal(0, guessResult.Count(gr => gr.LetterMatchType == LetterMatchType.Grey));
        Assert.Equal(0, guessResult.Count(gr => gr.LetterMatchType == LetterMatchType.Green));
    }
    
    [Fact]
    public void GuessService_SameLettersMoreThanOnce_OneCorrectOneWrong_IsDetected()
    {
        //Arrange
        const string selectedWord = "pxxxx";
        const string guessedWord = "cpcpx";
        
        //Act
        var guessResult = _guessService.SubmitGuess(selectedWord, guessedWord);
        
        //Assert
        Assert.Equal(1, guessResult.Count(gr => gr.LetterMatchType == LetterMatchType.Yellow));
        Assert.Equal(1, guessResult.Count(gr => gr.LetterMatchType == LetterMatchType.Green));
        Assert.Equal(3, guessResult.Count(gr => gr.LetterMatchType == LetterMatchType.Grey));
    }
    
    [Fact]
    public void GuessService_SameLettersMoreThanOnce_BothCorrect_IsDetected()
    {
        //Arrange
        const string selectedWord = "pxpxx";
        const string guessedWord = "cpcpx";
        
        //Act
        var guessResult = _guessService.SubmitGuess(selectedWord, guessedWord);
        
        //Assert
        Assert.Equal(2, guessResult.Count(gr => gr.LetterMatchType == LetterMatchType.Yellow));
        Assert.Equal(1, guessResult.Count(gr => gr.LetterMatchType == LetterMatchType.Green));
    }
}