public class CharacterMapBuffered : CharacterMap
{
    private Character[,] buffer;

    public CharacterMapBuffered() : base()
    {
        buffer = new Character[width, height];
    }

    public override void putChar(int col, int row, char c, Colour foreground, Colour background)
    {
        buffer[col, row] = new Character(c, foreground, background);
    }

    /// <summary>
    /// Clears the console screen and displays the current contents of this buffer
    /// </summary>
    public void display()
    {
        Console.Clear();
        for (int row = 0; row < buffer.GetLength(1); row++)
        {
            for (int col = 0; col < buffer.GetLength(0); col++)
            {
                var c = buffer[col, row];
                if (c.foreground != foregroundColour)
                {
                    foregroundColour = c.foreground;
                    Console.ForegroundColor = (ConsoleColor)foregroundColour;
                }
                if (c.background != backgroundColour)
                {
                    backgroundColour = c.background;
                    Console.BackgroundColor = (ConsoleColor)backgroundColour;
                }
                if (c.ch != empty)
                {

                }
                Console.Write(c.ch);
            }
        }
    }
}