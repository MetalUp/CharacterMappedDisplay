using System.Text;
using static System.Math;

public class XYGraph
{
    // Configuration properties
    public int originOffsetCol { get; set; } = 8; //of char containing pixel[0,0] relative to character grid
    public int originOffsetRow { get; set; } = 27; //of char containing pixel[0,0] relative to character grid
    public int xPixels { get; set; } = 200;
    public int yPixels { get; set; } = 50;

    public int xScalePowerOf10 { get; set; } = 0; // 0 = 0-.99, 1 = range 0-9.9, 2 = 0-99, can be negative also
    public int yScalePowerOf10 { get; set; } = 0; // but will be recalculated automatically based on y values

    public XYGraph()
    {
        Console.OutputEncoding = Encoding.UTF8;
    }

    private double xMaxValue =>  Pow(10, xScalePowerOf10)*10;
    private double incrementPerPixel => xMaxValue / xPixels;
    private readonly List<(int,int)> pixels = new();

    private void SetPixel(int x, int y)
    {
        pixels.Add((x, y));
    }

    public void ClearAllPixels()
    {
        pixels.Clear();
    }

    public void Plot(Func<double, double> f)
    {
        ClearAllPixels();
        double[] yValues = new double[xPixels];
        for (int i = 0; i < xPixels; i++)
        {
            var xValue = i * incrementPerPixel;
            yValues[i] = f(xValue);
        }
        var maxY = yValues.Max();
        //var minY = yValues.Min();

        yScalePowerOf10 =(int) Log10(maxY);

        var yScaleFactor = (yPixels - 1) / Pow(10,yScalePowerOf10+1); 

        for (int i = 0; i < xPixels; i++)
        {
            int y =  (int)(yValues[i] * yScaleFactor);  //Check
            SetPixel(i, y);
        }
    }

    public void Draw()
    {
        DrawXAxis();
        DrawYAxis();
        DrawAllPixels();
 
    }

    public void DrawAllPixels()
    {
        var chars = new Dictionary<(int, int), int>();
        foreach (var pixel in pixels)
        {
            var x = pixel.Item1+1;
            var y = pixel.Item2+1;
            var charPos = (x / 2 + originOffsetCol, originOffsetRow - y / 2);
            var bit = (x % 2 + 1) * (y % 2 * 3 + 1);
            if (chars.ContainsKey(charPos))
            {
                chars[charPos] |= bit;
            }
            else
            {
                chars[charPos] = bit;
            }
        }
        foreach (var key in chars.Keys)
        {
            Console.SetCursorPosition(key.Item1, key.Item2);
            var binaryVal = chars[key];
            Console.Write(quarterChars[binaryVal]);
        }
    }

    private void DrawXAxis()  //TODO: label axes and gradations
    {

        for (int c = originOffsetCol; c < originOffsetCol + xPixels / 2; c += 2)
        {
            Console.SetCursorPosition(c, originOffsetRow);
            Console.Write('\u252c');
            Console.Write(Symbol.line_H);
        }
        for (int i = 0; i < 10; i++)
        {
            Console.SetCursorPosition(originOffsetCol + i * 10, originOffsetRow + 1);
            Console.Write(i);
        }
        Console.Write("       " + scaleFactor(xScalePowerOf10));
    }

    private void DrawYAxis() { 
        for (int r = originOffsetRow; r > originOffsetRow - yPixels/2; r--)
        {
            Console.SetCursorPosition(originOffsetCol, r);
            Console.Write(Symbol.cross);
        }
        for (int i = 0; i < 10; i += 2)
        {
            Console.SetCursorPosition(originOffsetCol - 4, (int) (originOffsetRow - i * 2.5));
            Console.Write(i);
        }
        Console.SetCursorPosition(originOffsetCol - 3, originOffsetRow - yPixels / 2);
        Console.Write(scaleFactor(yScalePowerOf10));
    }



    //Character values for different combinations of quarter-blocks, corresponding to binary values 0000 to 1111:
    private List<char> quarterChars = new() { ' ', '\u2596', '\u2597', '\u2584', '\u2598', '\u258c', '\u259a', '\u2599', '\u259D', '\u259e', '\u2590', '\u259f', '\u2580', '\u259b', '\u259c', '\u2588' };

    private string scaleFactor(int p) =>
        p == 0 ? "" : "x10" + p switch
     {
         _ when p < 0 => '\u207B' + scaleFactor(Abs(p)),
         0 => "\u2070",
         1 => "\u00b9",
         2 => "\u00b2",
         3 => "\u00b3",
         4 => "\u2074",
         5 => "\u2075",
         6 => "\u2076",
         7 => "\u2077",
         8 => "\u2078",
         9 => "\u2079",
         _ => "error"
     };

}
