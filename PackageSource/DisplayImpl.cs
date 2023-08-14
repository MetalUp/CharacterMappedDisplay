
using System.Text;

public class DisplayImpl
{
    public const char empty = ' ';

    public Colour foregroundColour { get; set; }
    public Colour backgroundColour { get; set; }

    public readonly int width;
    public readonly int height;

    public DisplayImpl()
    {
        //TODO specify size in constructor (with default option) and make the console window that size.
        width = Console.WindowWidth;
        height = Console.WindowHeight;

        Console.OutputEncoding = Encoding.UTF8;
    }

    public void setCursor(int col, int row)
    {
        Console.SetCursorPosition(col, row);
    }

    //Clears all characters and then paints whole screen with background color. Clear at start, after setting background, if you want a different background.
    public void paintBackground()
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                putChar(col, row, empty);
            }
        }
    }

    public void clearAll()
    {
        paintBackground();
    }

    public void putBlock(int col, int row)
    {
        putChar(col, row, Symbol.block, foregroundColour);
    }

    public void putBlock(int col, int row, Colour colour)
    {
        putChar(col, row, Symbol.block, colour);
    }

    public void putChar(int col, int row, char c)
    {
        putChar(col, row, c, foregroundColour);
    }


    public void putChar(int col, int row, char c, Colour foreground)
    {
        putChar(col, row, c, foreground, backgroundColour);
    }

    public void putChar(int col, int row, char c, Colour foreground, Colour background)
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

    public void clear(int col, int row)
    {
        putChar(col, row, empty);
    }

    public void printLine(string text)
    {
        Console.WriteLine(text);
    }

    public void print(string text)
    {
        Console.Write(text);
    }

    public void clear()
    {
        Console.Clear();
    }



}
