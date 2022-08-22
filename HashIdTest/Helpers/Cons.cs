namespace HashIdTest.Helpers;

public static class Cons
{
    public enum PositionType
    {
        Absolute,
        FromCurrentPosition
    }
    
    private static void SetPositionFromLeft(PositionType positionType, int xPosition)
    {
        var (left, top) = Console.GetCursorPosition();
        var position = positionType == PositionType.Absolute
            ? xPosition
            : left + xPosition;
        Console.SetCursorPosition(position, top);
    }

    public static void WriteTitle(object data, char fillChar, ConsoleColor color)
    {
        var line = new string(fillChar, Console.WindowWidth - 1);
        Console.ResetColor();
        Console.WriteLine(line);
        Console.ForegroundColor = color;
        Console.WriteLine(data);
        Console.ResetColor();
        Console.WriteLine(line);
    }
    
    public static void Write(
        object data, int xPosition, ConsoleColor color, PositionType positionType = PositionType.Absolute)
    {
        SetPositionFromLeft(positionType, xPosition);
        Console.ForegroundColor = color;
        Console.Write(data);
        Console.ResetColor();
    }

    public static void Write(
        object data, int xPosition, PositionType positionType = PositionType.Absolute)
    {
        SetPositionFromLeft(positionType, xPosition);
        Console.ResetColor();
        Console.Write(data);
    }
    
    public static void WriteLine(
        object data, int xPosition, ConsoleColor color, PositionType positionType = PositionType.Absolute)
    {
        SetPositionFromLeft(positionType, xPosition);
        Console.ForegroundColor = color;
        Console.WriteLine(data);
        Console.ResetColor();
    }
    
    public static void WriteLine(
        object data, int xPosition, PositionType positionType = PositionType.Absolute)
    {
        SetPositionFromLeft(positionType, xPosition);
        Console.ResetColor();
        Console.WriteLine(data);
    }
}
