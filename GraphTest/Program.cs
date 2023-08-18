using static EntryPoints;

class Program
{
    public static void Main()
    {  
        var g = new XYGraph() { xMin = 0, xMax = Math.PI *2 };
        g.Plot(Math.Sin);
        g.Plot(Math.Cos);
        Display.DrawGraph(g);
        Keyboard.readKeyWithoutPrinting();
    }

    public static double square(double x) => Math.Pow(x, 2);

    public static double straight(double x) => 3 + x * 1.5;

    public static double exp(double x) => Math.Pow(Math.E,-x);

    public static double halfSin(double x) => Math.Abs(Math.Sin(x/180*Math.PI));


}
