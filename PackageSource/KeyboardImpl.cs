using System.Text;

public class KeyboardImpl
{
    public char readKey()
    {
        return Console.ReadKey().KeyChar;
    }

    public char readKeyWithoutPrinting()
    {
        Console.CursorVisible = false;
        var standardOut = Console.Out;
        Console.SetOut(new NoEcho());
        var k = Console.ReadKey().KeyChar;
        Console.SetOut(standardOut);
        Console.CursorVisible=true;
        return k;
    }

    public bool keyHasBeenPressed() => Console.KeyAvailable;

    //Ensures that there are no previously hit keys waiting to be read
    public void clearBuffer()
    {
        while (Console.KeyAvailable)
            readKeyWithoutPrinting();
    }

    class NoEcho : TextWriter
    {
        public override void Write(char value) { }

        public override Encoding Encoding
        {
            get { return Encoding.Unicode; }
        }
    }


}