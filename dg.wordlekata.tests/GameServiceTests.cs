using dg.wordlekata.Models;
using dg.wordlekata.Services;
using Moq;
using Xunit;

namespace dg.wordlekata.tests;

public class WordSelectionServiceTests
{
    [Fact]
    public void WhenGameStarts_WordIsPicked()
    {
        //Arrange
        var wordServiceMock = new Mock<IWordService>();
        const string chosenWord = "slide";
        wordServiceMock.Setup(ws => ws.GetWord()).Returns(chosenWord);
        
        var gameService = new GameService(wordServiceMock.Object);

        //Act
        gameService.NewGame();
        var gameState = gameService.GameState;

        //Assert
        wordServiceMock.Verify(ws => ws.GetWord(), Times.Once);
        Assert.False(string.IsNullOrEmpty(gameState.ChosenWord));
        Assert.Equal(chosenWord, gameState.ChosenWord);
    }
    
    [Fact]
    public void GameService_GuessLimitReached_GameOver()
    {
        //Arrange
        var wordServiceMock = new Mock<IWordService>();
        const string chosenWord = "slide";
        wordServiceMock.Setup(ws => ws.GetWord()).Returns(chosenWord);
        
        var gameService = new GameService(wordServiceMock.Object);
        var gameState = gameService.GameState;

        //Act
        gameService.NewGame();
        gameService.Guess("wrong");
        gameService.Guess("wrong");
        gameService.Guess("wrong");
        gameService.Guess("wrong");
        gameService.Guess("wrong");

        //Assert
        Assert.False(string.IsNullOrEmpty(gameState.ChosenWord));
        Assert.Equal(GameStatus.Lost, gameState.Status);
    }
    
    [Fact]
    public void GameService_GuessedCorrectly_GameWon()
    {
        //Arrange
        var wordServiceMock = new Mock<IWordService>();
        const string chosenWord = "slide";
        wordServiceMock.Setup(ws => ws.GetWord()).Returns(chosenWord);
        
        var gameService = new GameService(wordServiceMock.Object);
        var gameState = gameService.GameState;

        //Act
        gameService.NewGame();
        gameService.Guess("slide");

        //Assert
        Assert.False(string.IsNullOrEmpty(gameState.ChosenWord));
        Assert.Equal(GameStatus.Won, gameState.Status);
    }
}