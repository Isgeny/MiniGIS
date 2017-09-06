using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MiniGIS
{
    public class PointStyle
    {
        public string FontFamily { get; set; }
        public char Symbol { get; set; }
        public int FontSize { get; set; }
        public Color SymbolColor { get; set; }

        public PointStyle()
        {
            FontFamily = "Wingdings";
        }
    }
}