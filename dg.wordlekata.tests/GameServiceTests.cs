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
        Assert.NotNull(gameState);
        Assert.False(string.IsNullOrEmpty(gameState.ChosenWord));
        Assert.Equal(chosenWord, gameState.ChosenWord);
    }
}