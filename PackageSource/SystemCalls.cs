

public static class SystemCalls
{
    #region Console output
    public static void printLine(string text)
    {
        Console.WriteLine(text);
    }

    public static void print(object text)
    {
        Console.Write(text);
    }

    #endregion

    #region Keyboard input
    public static string input(string prompt = "")
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }

    public static char readKey(bool writeKey = false)
    {
        char k;
        if (writeKey)
        {
            k = Console.ReadKey().KeyChar;
        }
        else
        {
            Console.CursorVisible = false;
            var standardOut = Console.Out;
            Console.SetOut(new NoEcho());
            k = Console.ReadKey().KeyChar;
            Console.SetOut(standardOut);
            Console.CursorVisible = true;
        }
        return k;
    }

    public static bool keyHasBeenPressed() => Console.KeyAvailable;

    //Ensures that there are no previously hit keys waiting to be read
    public static void clearKeyBuffer()
    {
        while (Console.KeyAvailable)
            readKey();
    }

    class NoEcho : TextWriter
    {
        public override void Write(char value) { }

        public override Encoding Encoding
        {
            get { return Encoding.Unicode; }
        }
    }

    #endregion


    #region Files

    #endregion

    #region Random number generation
    public static double random() => new Random().NextDouble();

    public static int random(int max) => (int) new Random().NextInt64(max + 1);

    public static int random(int min, int max) => (int)new Random().NextInt64(min, max + 1);

    public static double random(double max) => new Random().NextDouble() * max;

    public static double random(double min, double max) => new Random().NextDouble() * (max-min)+min;
    #endregion

    #region Timing

    public static DateOnly today()
    {
        var dt = DateTime.Today;
        return new DateOnly(dt.Year, dt.Month, dt.Day);
    }

    public static TimeOnly now()
    {
        var dt = DateTime.Now;
        return new TimeOnly(dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
    }

    public static void pause(int milliseconds)
    {
        var t = DateTime.Now.Ticks + milliseconds*10000;
        while (DateTime.Now.Ticks < t) { }
    }
    #endregion

    #region Asserts

    public static void assertTrue(bool actual) { throw new NotImplementedException(); }

    public static void assertFalse(bool actual) { throw new NotImplementedException(); }

    public static void assertEqual(object expected, object actual) { throw new NotImplementedException(); }

    public static void assertListEqual(object expected, object actual) { throw new NotImplementedException(); }

    #endregion

    #region Type conversions

    #endregion

    #region HoFs

    #endregion

}
