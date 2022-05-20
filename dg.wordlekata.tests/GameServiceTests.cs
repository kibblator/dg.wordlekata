using dg.wordlekata.Models;
using dg.wordlekata.Services;
using Moq;
using Xunit;

namespace dg.wordlekata.tests;

public class WordSelectionServiceTests
{
    private readonly Mock<IWordService> _wordServiceMock;
    private readonly Mock<IGuessService> _guessServiceMock;
    private const string ChosenWord = "slide";
    public WordSelectionServiceTests()
    {
        _wordServiceMock = new Mock<IWordService>();
        _wordServiceMock.Setup(ws => ws.GetWord()).Returns(ChosenWord);
        
        _guessServiceMock = new Mock<IGuessService>();
    }
    
    [Fact]
    public void WhenGameStarts_WordIsPicked()
    {
        //Arrange
        var gameService = new GameService(_wordServiceMock.Object, _guessServiceMock.Object);

        //Act
        gameService.NewGame();
        var gameState = gameService.GameState;

        //Assert
        _wordServiceMock.Verify(ws => ws.GetWord(), Times.Once);
        Assert.False(string.IsNullOrEmpty(gameState.ChosenWord));
        Assert.Equal(ChosenWord, gameState.ChosenWord);
    }
    
    [Fact]
    public void GameService_GuessLimitReached_GameOver()
    {
        //Arrange
        var wordServiceMock = new Mock<IWordService>();
        const string chosenWord = "slide";
        wordServiceMock.Setup(ws => ws.GetWord()).Returns(chosenWord);
        
        var gameService = new GameService(_wordServiceMock.Object, _guessServiceMock.Object);
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
        
        var gameService = new GameService(_wordServiceMock.Object, _guessServiceMock.Object);
        var gameState = gameService.GameState;

        //Act
        gameService.NewGame();
        gameService.Guess("slide");

        //Assert
        Assert.False(string.IsNullOrEmpty(gameState.ChosenWord));
        Assert.Equal(GameStatus.Won, gameState.Status);
    }
}