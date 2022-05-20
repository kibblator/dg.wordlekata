using System.Collections.Generic;

namespace dg.wordlekata.Models;

public class GameState
{
    public GameState()
    {
        Guesses = new List<string>();
    }
    
    public string ChosenWord { get; set; }
    public List<string> Guesses { get; set; }

    public void Clear()
    {
        ChosenWord = "";
        Guesses.Clear();
    }
}