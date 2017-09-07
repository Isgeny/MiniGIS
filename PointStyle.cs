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
        public int SymbolSize { get; set; }
        public Color SymbolColor { get; set; }

        public PointStyle()
        {
            FontFamily = "Wingdings";
            Symbol = '0';
            SymbolSize = 5;
            SymbolColor = Color.Black;
        }

        public PointStyle(string font, char symbol, int symbolSize, Color symbolColor)
        {
            FontFamily = font;
            Symbol = symbol;
            SymbolSize = symbolSize;
            SymbolColor = symbolColor;
        }
    }
}