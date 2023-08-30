public class CharacterMapLive: CharacterMap
{

    public override void putChar(int col, int row, char c, Colour foreground, Colour background)
    {
        var currentForeground = Console.ForegroundColor;
        var currentBackground = Console.BackgroundColor;
        var (currentX, currentY) = Console.GetCursorPosition();
        var bufx = Console.BufferWidth;
        var bufy = Console.BufferWidth;
        Console.SetCursorPosition(col, row);
        Console.ForegroundColor = (ConsoleColor)(int)foreground;
        Console.BackgroundColor = (ConsoleColor)(int)background;
        Console.Write(c);
        Console.SetCursorPosition(currentX, currentY);
        Console.ForegroundColor = currentForeground;
        Console.BackgroundColor = currentBackground;
    }

}