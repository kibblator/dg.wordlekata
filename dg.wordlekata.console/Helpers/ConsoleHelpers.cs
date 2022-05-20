using System;
using System.Text;

namespace dg.wordlekata.console.Helpers;

public static class ConsoleHelpers
{
    public static void ClearCurrentConsoleLine()
    {
        var currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth)); 
        Console.SetCursorPosition(0, currentLineCursor);
    }

    public static string ReadLineWithCancel()
    {
        string result = null;

        var buffer = new StringBuilder();
        var info = Console.ReadKey(true);
        while (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Escape)
        {
            Console.Write(info.KeyChar);
            buffer.Append(info.KeyChar);
            info = Console.ReadKey(true);
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
}