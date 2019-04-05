using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MetalUp
{
    public class CharacterMappedDisplay
    {

        public Font Font { get; set; }
        public int Columns { get; private set; }
        public int Rows { get; private set; }

        private Graphics g;
        #region Constructor
        public CharacterMappedDisplay(PictureBox picBox)
        {
            g = picBox.CreateGraphics();
            Columns = picBox.Size.Width / Graphic.CharWidth;
            Rows = picBox.Size.Height / Graphic.CharHeight;
            Scrolling = true;
            Font = new Font(
               new FontFamily("Consolas"),
               14,
               FontStyle.Regular,
               GraphicsUnit.Pixel);
        }
        #endregion

        #region Cursor
        /// <summary>
        /// Cursor X position (measured in characters, not pixels), zero-based
        /// </summary>
        public int CursorX { get;  set; }
        /// <summary>
        /// Cursor Y position (measured in characters, not pixels), zero-based
        /// </summary>
        public int CursorY { get;  set; }
        /// <summary>
        /// Move the cursor
        /// </summary>
        /// <param name="movement">Direction/Location specified using Cursor enum</param>
        /// <param name="repeat">Number of such movements (defaults to 1)</param>
        public void MoveCursor(Direction movement, int repeat = 1)
        {
            for (int i = 0; i < repeat; i++)
            {
                switch (movement)
                {
                    case Direction.Up:
                        if (CursorY > 0) CursorY--;
                        break;
                    case Direction.Down:
                        if (CursorY < Rows - 1)
                        { CursorY++; }
                        else if (Scrolling)
                        { ScrollNow();}
                        break;
                    case Direction.Left:
                        if (CursorX > 0) CursorX--;
                        break;
                    case Direction.Right:
                        if (CursorX < Columns - 1) CursorX++;
                        break;
                    default:
                        throw new Exception(string.Format("Unhandled value for Cursor: {0}", Enum.GetName(typeof(Direction), movement)));
                }
            }

            //TODO: Keep cursor in bounds (paying attention to scrolling)
        }

        /// <summary>
        /// Move cursor to specified coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MoveCursorTo(Position loc)
        {
            switch (loc)
            {
                case Position.TopLeft:
                    CursorX = 0;
                    CursorY = 0;
                    break;
                case Position.TopRight:
                    CursorX = Columns - 1;
                    CursorY = 0;
                    break;
                case Position.BottomLeft:
                    CursorX = 0;
                    CursorY = Rows - 1;
                    break;
                case Position.BottomRight:
                    CursorX = Columns - 1;
                    CursorY = Rows - 1;
                    break;
                case Position.TopEdge:
                    CursorY = 0;
                    break;
                case Position.BottomEdge:
                    CursorY = Rows - 1;
                    break;
                case Position.LeftEdge:
                    CursorX = 0;
                    break;
                case Position.RightEdge:
                    CursorX = Columns - 1;
                    break;
                default:
                    throw new Exception(string.Format("Unhandled value for Cursor: {0}", Enum.GetName(typeof(Direction), loc)));
            }
        }

        /// <summary>
        /// Move cursor to specified coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MoveCursorTo(int x, int y)
        {
            CursorX = x;
            CursorY = y;
        }

        public void MoveCursorRightWithWrapAndScroll()
        {
            if (CursorX < Columns - 1)
            {
                MoveCursor(Direction.Right);
            }
            else
            {
                MoveCursorTo(Position.LeftEdge);
                MoveCursor(Direction.Down);
            }
        }

        public Point TopLeftCornerOfCurrentCursor()
        {
            return new Point(CursorX * Graphic.CharWidth, CursorY * Graphic.CharHeight);
        }
        #endregion

        #region Setting colours
        public Color CurrentForeground { get; set; }
        public Color CurrentBackground { get; set; }

        public void FillScreenWithBackgroundColor() {
            MoveCursorTo(Position.TopLeft);
            DrawBlock(Columns, Rows, CurrentBackground);
        }
        public void ClearScreen() { }
        #endregion 

        #region Writing  text & characters

        public void WriteText(string text) {
            foreach (char c in text)
            {
                WriteChar(c);
            }
        }

        public void WriteChar(char c)
        {
            Brush brush = new SolidBrush(CurrentForeground);
            var point = TopLeftCornerOfCurrentCursor();
            point.Offset(0, -2);
            g.DrawString(c.ToString(), Font, brush, point);
            MoveCursorRightWithWrapAndScroll();
        }

        /// <summary>
        /// Writes character at current cursor position, then advances cursor by one.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="repeat"></param>
        public void WriteGraphic(Graphic character, int repeat = 1)
        {
            for (int i = 0; i < repeat; i++)
            {
                g.DrawImage(character.Bitmap, CursorX * Graphic.CharWidth, CursorY * Graphic.CharHeight); //TODO replace with cursor-derived postion
                MoveCursorRightWithWrapAndScroll();
            }
        }



        public void DrawLine(Direction direction, int length) {

            //See draw block
        }

        public void DrawBlock(int width, int height, Color color) {
            //TODO: Too slow to paint each character as a solid. Redo, using more direct method of drawing a block of colour equivalent to the character grid size
            throw new NotImplementedException();
        }

        public void DrawBox(int width, int height) { }

        //Copies from current cursor position
        public Graphic[,] Copy(int width, int height) { throw new NotImplementedException(); }

        //Will throw error if pasted array will not fit within bounds
        public void Paste(Graphic[,] block) { }
        #endregion

        #region Scrolling
        //Note: If set to true, scrolling is permitted (upwards only)
        public bool Scrolling { get; set; }

        //Moves existing characters up by specified number of steps
        public void ScrollNow(int steps = 1) { }
        #endregion

        #region Input
        //Generates flashing cursor at the current cursor position, allowing user to type in a value and hit Enter.
        //Optional field length parameter constraints the user to type within a constrainted number of characters (0 means 'unlimited')
        public string InputText(int fieldLength = 0) { return null; }
        public int InputInteger(int maxValue = int.MaxValue) { return 0; }
        public double InputDouble(int fieldLength = 0) { return 0; }
        #endregion

        #region Get
        public char GetKey() { return ' '; }
        public void ClearKeyBuffer() { }
        #endregion

        #region Pixel level
        //Draws specified from the top-left of the current cursor position, using foreground (for True pixel) & background (for False) colours
        public void DrawPixels(bool[,] pixels) { }
        #endregion

        #region Getting characters 


        public static List<Graphic> CharactersFromString(string text) { throw new NotImplementedException(); }

        public static Graphic CharacterFromChar(char text) { throw new NotImplementedException(); }

        public static Graphic CharacterFromASCII(int ascii) { throw new NotImplementedException(); }

        public static Graphic CharacterFromGraphic(Graphic g) { throw new NotImplementedException(); }
        #endregion
    }
}
