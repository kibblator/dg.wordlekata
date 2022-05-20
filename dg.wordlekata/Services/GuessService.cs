using System.Collections.Generic;
using dg.wordlekata.Models;

namespace dg.wordlekata.Services;

public interface IGuessService
{
    Guess SubmitGuess(string selectedWord, string guessedWord);
}

public class GuessService : IGuessService
{
    public Guess SubmitGuess(string selectedWord, string guessedWord)
    {
        var guessResult = new List<GuessResult>();
        var letterCounts = GetLetterCounts(selectedWord);

        for (var i = 0; i < guessedWord.Length; i++)
        {
            var result = new GuessResult
            {
                Letter = guessedWord[i].ToString()
            };
            
            if (selectedWord.Contains(guessedWord[i]) == false)
            {
                result.LetterMatchType = LetterMatchType.Grey;
            } 
            else if (selectedWord[i] == guessedWord[i])
            {
                result.LetterMatchType = LetterMatchType.Green;
                letterCounts[guessedWord[i]]--;
            }
            else
            {
                if (letterCounts[guessedWord[i]] > 0)
                    result.LetterMatchType = LetterMatchType.Yellow;
                else
                    result.LetterMatchType = LetterMatchType.Grey;
                
                letterCounts[guessedWord[i]]--;
            }

            guessResult.Add(result);
        }

        return new Guess
        {
            GuessResult = guessResult
        };
    }

    private static Dictionary<char, int> GetLetterCounts(string selectedWord)
    {
        var letterCounts = new Dictionary<char, int>();
        
        foreach (var character in selectedWord)
        {
            if (letterCounts.ContainsKey(character))
            {
                letterCounts[character]++;
            }
            else
            {
                letterCounts[character] = 1;
            }
        }

        return letterCounts;
    }
}