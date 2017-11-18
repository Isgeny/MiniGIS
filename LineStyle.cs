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
        }
    }
}