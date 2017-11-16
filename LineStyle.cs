using System.Drawing;

namespace MiniGIS
{
    public class LineStyle
    {
        public Color Color { get; set; }
        public float Width { get; set; }
        
        public LineStyle()
        {
            Color = Color.Black;
            Width = 1;
        }

        public LineStyle(Color color, float width)
        {
            Color = color;
            Width = width;

            //int c = 0xff0000;
            //byte red = (byte)((c >> 16) & 0xff);
            //byte green = (byte)((c >> 8) & 0xff);
            //byte blue = (byte)(c & 0xff);
            //Color col = Color.FromArgb(red, green, blue);
            //Color mred = Color.Red;
        }
    }
}