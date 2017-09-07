using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MiniGIS
{
    public class LineStyle
    {
        public int Width { get; set; }
        public string Type { get; set; }
        public Color Color { get; set; }

        public LineStyle()
        {
            Width = 5;
            Type = "";
            Color = Color.Black;
        }
    }
}