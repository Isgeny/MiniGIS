using System.Drawing;

namespace MiniGIS
{
    public class PolygonStyle
    {
        public Color Color { get; set; }

        public PolygonStyle()
        {
            Color = Color.Aqua;
        }

        public PolygonStyle(Color color)
        {
            Color = color;
        }
    }
}