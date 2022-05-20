using System;
using System.Text;
using dg.wordlekata.Models;

namespace dg.wordlekata.console.Helpers;

public static class ConsoleHelpers
{
    public static void ClearCurrentConsoleLine()
    {
        Console.SetCursorPosition(0, Console.CursorTop);
        var cursorTop = Console.GetCursorPosition().Top;
        Console.Write(new string(' ', Console.WindowWidth)); 
        Console.SetCursorPosition(0, cursorTop);
    }

    public static string ReadLineWithCancel()
    {
        string result = null;

        var buffer = new StringBuilder();
        var info = Console.ReadKey(true);
        while (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Escape)
        {
            if (info.Key == ConsoleKey.Backspace && buffer.Length > 0)
            {
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Console.Write(' ');
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                buffer.Remove(buffer.Length - 1, 1);
                info = Console.ReadKey(true);
            }
            else if (info.Key != ConsoleKey.Backspace)
            {
                Console.Write(info.KeyChar);
                buffer.Append(info.KeyChar);
                info = Console.ReadKey(true);
            }
        }

        if (info.Key == ConsoleKey.Escape)
        {
            Environment.Exit(0);
        }

        if (info.Key == ConsoleKey.Enter)
        {
            result = buffer.ToString();
        }

        return result;
    }

    public static void WriteGuessWord(Guess guess)
    {
        foreach (var letterDetail in guess.GuessResult)
        {
            if (letterDetail.LetterMatchType == LetterMatchType.Grey)
            {
                Console.ForegroundColor = ConsoleColor.White;
            } 
            else if (letterDetail.LetterMatchType == LetterMatchType.Green)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            Console.Write(letterDetail.Letter);
            Console.ResetColor();
        }
        Console.WriteLine();
    }
}