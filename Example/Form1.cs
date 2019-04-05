using MetalUp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Template
{
    public partial class Form1 : System.Windows.Forms.Form
    {

        private CharacterMappedDisplay display;

        public Form1()
        {
            InitializeComponent();
            //TODO: Pass in pictureBox so CMD can determine size
            display = new CharacterMappedDisplay(pictureBox1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BringToFront();
            Focus();
            KeyPreview = true;
            WindowState = FormWindowState.Maximized;
            KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            display.CurrentBackground = Color.Salmon;
           // display.FillScreenWithBackgroundColor();
            display.MoveCursorTo(MetalUp.Position.TopLeft);
            var c = Graphic.Solid(Color.Red, Color.Blue);
            display.WriteGraphic(c, 40);
            display.CurrentForeground = Color.Black;
            display.WriteText("Now is the time for all good men to come to the aid of the party.");

            display.WriteGraphic(c,90);
            var d = c.NegativeVersion();
            display.WriteGraphic(d,70);

            display.MoveCursorTo(20, 30);
            //display.DrawBlock(5, 7, Color.Yellow);
        }
    }
}
