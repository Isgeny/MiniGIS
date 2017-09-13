using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MiniGIS
{
    public class Point : MapObject
    {
        public GEOPoint Position { get; set; }
        public PointStyle Style { get; set; }

        public Point()
        {
            Position = new GEOPoint();
            Type = MapObjectType.Point;
            Style = new PointStyle();
        }

        public override void Draw(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            var screenLocaltion = Layer.Map.MapToScreen(Position);
            var font = new Font(Style.FontFamily, Style.SymbolSize);
            var brush = new SolidBrush(Style.SymbolColor);

            var symbol = Convert.ToChar(Style.Symbol).ToString();
            var symbolSize = graphics.MeasureString(symbol, font);
            screenLocaltion.X -= (int)(symbolSize.Width / 2);
            screenLocaltion.Y -= (int)(symbolSize.Height / 2);

            graphics.DrawString(symbol, font, brush, screenLocaltion);
        }
    }
}