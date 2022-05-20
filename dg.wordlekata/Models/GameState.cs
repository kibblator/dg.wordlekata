using System.Collections.Generic;

namespace dg.wordlekata.Models;

public class GameState
{
    public GameState()
    {
        Guesses = new List<Guess>();
    }
    
    public string ChosenWord { get; set; }
    public List<Guess> Guesses { get; }
    public GameStatus Status { get; set; }

    public void Clear()
    {
        ChosenWord = "";
        Guesses.Clear();
        Status = GameStatus.None;
    }
}