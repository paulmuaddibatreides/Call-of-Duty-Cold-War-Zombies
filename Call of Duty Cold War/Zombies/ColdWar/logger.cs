using System;

public static class Logger
{
    public static void Log(string message, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
        Console.ResetColor();
    }

    public static void Info(string message)
    {
        Log(message, ConsoleColor.Cyan);
    }

    public static void Warning(string message)
    {
        Log(message, ConsoleColor.Yellow);
    }

    public static void Error(string message)
    {
        Log(message, ConsoleColor.Red);
    }
}
