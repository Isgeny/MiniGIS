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
            var font = new Font(Style.FontFamily, Style.SymbolSize);
            var brush = new SolidBrush(Style.SymbolColor);
            var symbol = Convert.ToChar(Style.Symbol).ToString();
            var symbolSize = graphics.MeasureString(symbol, font);

            // Отрисовываем символ с центром в screenLocation
            var screenLocaltion = Layer.Map.MapToScreen(Position);
            screenLocaltion.X -= (int)(symbolSize.Width / 2);
            screenLocaltion.Y -= (int)(symbolSize.Height / 2);

            if(Selected)
            {
                var pen = new Pen(Color.Black, 1.0f);
                pen.DashPattern = new float[] { 4.0f, 2.0f };
                var rect = new Rectangle(screenLocaltion.X, screenLocaltion.Y, (int)symbolSize.Width, (int)symbolSize.Height);
                graphics.DrawRectangle(pen, rect);
            }
            graphics.DrawString(symbol, font, brush, screenLocaltion);
        }

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

            var topLeft = Layer.Map.MapToScreen(Position);
            topLeft.X -= (int)(symbolSize.Width / 2);
            topLeft.Y -= (int)(symbolSize.Height / 2);
            var bottomRight = Layer.Map.MapToScreen(Position);
            bottomRight.X -= (int)(symbolSize.Width / 2);
            bottomRight.X += (int)symbolSize.Width;
            bottomRight.Y -= (int)(symbolSize.Height / 2);
            bottomRight.Y += (int)symbolSize.Height;

            var topLeftLocation = Layer.Map.ScreenToMap(topLeft);
            var bottomRightLocation = Layer.Map.ScreenToMap(bottomRight);

            var pointRect = new GEORect(topLeftLocation.X, bottomRightLocation.X, bottomRightLocation.Y, topLeftLocation.Y);
            return GEORect.IsIntersect(pointRect, geoRect);
        }

        public override double Perimeter()
        {
            return 0.0;
        }

        public override double Area()
        {
            return 0.0;
        }
    }
}