using System.Drawing;


namespace MetalUp
{

    //TODO: Get rid of this class. Use only monochrome BitMap.  Have a static class that can produce bitmaps for various symbols.  
    //Bitmap transformed into colours when painted? 
    public class Graphic
    {
        public const int CharWidth = 8;
        public const int CharHeight = 16;

        public Color Foreground { get; set; }
        public Color Background { get; set; }
        public Bitmap Bitmap { get; private set; }

        public Graphic(Bitmap bm, Color fore, Color back)
        {
            Bitmap = bm;
            Foreground = fore;
            Background = back;
        }

        public Graphic(Graphic from, Color newForeground, Color newBackground) : this(from.Bitmap, newForeground, newBackground)
        {
            var bm1 = from.Bitmap;
            var bm2 = new Bitmap(Bitmap.Width, Bitmap.Height);
            for (int x = 0; x < Bitmap.Width; x++)
            {
                for (int y = 0; y < Bitmap.Height; y++)
                {
                    if (bm1.GetPixel(x, y).ToArgb() ==from.Foreground.ToArgb())  //ToArgb compares color values, because colour name is not preserved apparently!
                    {
                        bm2.SetPixel(x, y, newForeground);
                    } else
                    {
                        bm2.SetPixel(x, y, newBackground);
                    }
                }
            }
            Bitmap = bm2;
        }

        /// <summary>
        /// Returns a new Character based on this one, but with foreground & background colours swapped.
        /// </summary>
        public Graphic NegativeVersion()
        {
            return new Graphic(this, Background, Foreground);
        }

        public static Graphic Solid(Color foreground, Color background)
        {
            var bm = new Bitmap(CharWidth, CharHeight);
            for (int x = 0; x < CharWidth; x++)
            {
                for (int y = 0; y < CharHeight; y++)
                {
                    bm.SetPixel(x, y, foreground);
                }
            }
            return new Graphic(bm, foreground, background);
        }

    }
}
