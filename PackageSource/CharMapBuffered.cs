public class CharMapBuffered : CharMap
{
    private Character[,] buffer;

    public CharMapBuffered() : base()
    {
        buffer = new Character[width, height];
    }

    //Overload all versions without colours
    public override void putChar(int col, int row, char c, Colour foreground, Colour background)
    {
        buffer[col, row] = new Character(c, foreground, background);
    }

    public  void setQuarterBlock(double col, double row, Colour foreground, Colour background) {
        throw new NotImplementedException();
        //Get int values for row col.
        //Set standard monchrome colours for this char location
        //Get existing char & map to binary - or 0 as default
        //Get binary value for new quarter
        //And with existing
        //Set char position with char for the new binary value
        // Note that you cannot have quarters of differnet colours within one character position
    }


    public void DrawHighResBar(int col, int row, Direction d, double length,  Colour foreground, Colour background)
    {
        throw new NotImplementedException();
    }

    public void DrawLine(int col, int row, Direction d, int length, Colour foreground, Colour background)
    {
        throw new NotImplementedException();
    }

    public void DrawSolid(int col, int row, int width, int height,  Colour foreground, Colour background)
    {
        throw new NotImplementedException();
    }

    public void DrawThinFrame(int col, int row, int width, int height, Colour foreground, Colour background)
    {
        throw new NotImplementedException();
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