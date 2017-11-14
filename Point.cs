using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniGIS
{
    public class Point : MapObject
    {
        public GEOPoint Position { get; set; }
        public PointStyle Style { get; set; }

        public Point()
        {
            Type = MapObjectType.Point;
            Position = new GEOPoint();
            Style = new PointStyle();
        }

        public Point(GEOPoint position, PointStyle style)
        {
            Type = MapObjectType.Point;
            Position = position;
            Style = style;
        }

        public override void Draw(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            var screenLocaltion = Layer.Map.MapToScreen(Position);
            var font = new Font(Style.FontFamily, Style.SymbolSize);
            var brush = new SolidBrush(Style.SymbolColor);
            var symbol = Convert.ToChar(Style.Symbol).ToString();
            var symbolSize = graphics.MeasureString(symbol, font);

            // Отрисовываем символ с центром в screenLocation
            screenLocaltion.X -= (int)(symbolSize.Width / 2);
            screenLocaltion.Y -= (int)(symbolSize.Height / 2);

            if(Selected)
            {
                var pen = new Pen(Color.Black, 1.0f);
                pen.DashPattern = new float[] { 4.0f, 2.0f };
                var rect = new Rectangle(screenLocaltion.X, screenLocaltion.Y, (int)symbolSize.Width, (int)symbolSize.Height - 4);
                graphics.DrawRectangle(pen, rect);
            }
            graphics.DrawString(symbol, font, brush, screenLocaltion);
        }

        // Почему мы не учитываем размеры символа?
        public override GEORect GetBounds()
        {
            return new GEORect(Position.X, Position.X, Position.Y, Position.Y);
        }

        public override bool IsInside(GEORect geoRect)
        {
            var graphics = Layer.Map.CreateGraphics();
            var font = new Font(Style.FontFamily, Style.SymbolSize);
            var symbol = Convert.ToChar(Style.Symbol).ToString();
            var symbolSize = graphics.MeasureString(symbol, font);
            var pointRect = new GEORect(Position.X, Position.X + symbolSize.Width, Position.Y, Position.Y + symbolSize.Height);
            return GEORect.IsIntersect(pointRect, geoRect);
        }
    }
}