
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

    #region XYGraphs
    public void DrawXYGraph(XYGraph graph)
    {
        DrawXYAxes(graph);
        DrawAllXYPoints(graph);
    }

    private void DrawXYAxes(XYGraph g)  //TODO: Add start end values
    {

        Console.SetCursorPosition(g.originCol, g.originRow);
        Console.Write(Symbol.cross);
        for (int c = g.offsetCol + 1; c <= g.offsetCol + g.xPoints / 2; c++)
        {
            Console.SetCursorPosition(c, g.originRow);
            Console.Write(Symbol.line_H);
        }
        Console.SetCursorPosition(g.offsetCol + g.xPoints / 2 + 1, g.originRow) ;
        Console.Write(" " + g.xAxisName);

        for (int r = g.offsetRow - 1; r >= g.offsetRow - g.yPoints / 2; r--)
        {
            Console.SetCursorPosition(g.originCol, r);
            Console.Write(Symbol.line_V);
        }
        Console.SetCursorPosition(g.originCol - 3, g.offsetRow - g.yPoints /2 -1);
        Console.Write(g.yAxisName);
    }


    private void DrawAllXYPoints(XYGraph g)
    {
        var chars = g.getCharsRepresentingPoints();
        foreach (var key in chars.Keys)
        {
            Console.SetCursorPosition(key.Item1 + g.offsetCol, g.offsetRow - key.Item2);
            Console.Write(chars[key]);
        }
    }
    #endregion
}
