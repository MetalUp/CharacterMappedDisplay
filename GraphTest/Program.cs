using static EntryPoints;

class Program
{
    public static void Main()
    {  
        var g = new XYGraph() { xMin = 0, xMax = Math.PI *2 };
        g.PlotFunction(Math.Sin);
        g.PlotFunction(Math.Cos);
        Display.DrawXYGraph(g);
        Keyboard.readKeyWithoutPrinting();
    }
}
