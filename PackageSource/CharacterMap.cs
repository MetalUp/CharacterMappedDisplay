public abstract class CharacterMap
{
    public const char empty = ' ';

    public Colour foregroundColour { get; set; }
    public Colour backgroundColour { get; set; }

    public readonly int width;
    public readonly int height;

    protected int cursorCol = 0;
    protected int cursorRow = 0;

    public CharacterMap()
    {
        //TODO specify size in constructor (with default option) and make the console window that size.
        width = Console.WindowWidth;
        height = Console.WindowHeight;
        Console.OutputEncoding = Encoding.UTF8;
    }

    public void setCursor(int col, int row)
    {
        cursorCol = col;
        cursorRow = row;
    }

    //Clears all characters and then paints whole screen with background color. Clear at start, after setting background, if you want a different background.
    public void fillBackground()
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
        fillBackground();
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

    public abstract void putChar(int col, int row, char c, Colour foreground, Colour background);


    public void clear(int col, int row)
    {
        putChar(col, row, empty);
    }


    protected class Character
    {
        public char ch;
        public Colour foreground;
        public Colour background;
        public Character(char ch, Colour foreground, Colour background)
        {
            this.ch = ch;
            this.foreground = foreground;
            this.background = background;
        }

        public override string ToString() => $"{ch} + ({foreground}, {background})";
    }
}