using static ElanConsole;

class Program
{
    public static void Main()
    {
        var g = new XYGraph() { xScalePowerOf10 = 1};
        g.Plot(square);
        g.Draw();
        Keyboard.readKeyWithoutPrinting();
    }

    public static double square(double x) => Math.Pow(x, 2);

    public static double straight(double x) => 3 + x * 1.5;

    public static double exp(double x) => Math.Pow(Math.E,x);




}
