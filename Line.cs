using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniGIS
{
    public class Line : MapObject
    {
        public GEOPoint BeginPoint { get; set; }
        public GEOPoint EndPoint { get; set; }
        public LineStyle Style { get; set; }

        public Line()
        {
            Type = MapObjectType.Line;
            BeginPoint = new GEOPoint();
            EndPoint = new GEOPoint();
            Style = new LineStyle();
        }

        public Line(GEOPoint beginPoint, GEOPoint endPoint, LineStyle style)
        {
            Type = MapObjectType.Line;
            BeginPoint = beginPoint;
            EndPoint = endPoint;
            Style = style;
        }

        public override void Draw(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            var screenLocaltionBeginPoint = Layer.Map.MapToScreen(BeginPoint);
            var screenLocaltionEndPoint = Layer.Map.MapToScreen(EndPoint);

            var lineWidth = Style.Width;
            var lineColor = Style.Color;
            var pen = new Pen(lineColor, lineWidth);
            if(Selected)
            {
                pen.DashPattern = new float[] { 4.0f, 2.0f };
            }
      
            graphics.DrawLine(pen, screenLocaltionBeginPoint, screenLocaltionEndPoint);
        }

        public override GEORect GetBounds()
        {
            double xMin = Math.Min(BeginPoint.X, EndPoint.X);
            double xMax = Math.Max(BeginPoint.X, EndPoint.X);
            double yMin = Math.Min(BeginPoint.Y, EndPoint.Y);
            double yMax = Math.Max(BeginPoint.Y, EndPoint.Y);
            return new GEORect(xMin, xMax, yMin, yMax);
        }

        public override bool IsInside(GEORect geoRect)
        {
            return GEORect.IsCrossRectLines(geoRect, this);
        }

        public static bool IsCrossLines(Line line1, Line line2)
        {
            double v1 = (line1.EndPoint.X - line1.BeginPoint.X) * (line2.BeginPoint.Y - line1.BeginPoint.Y) - (line1.EndPoint.Y - line1.BeginPoint.Y) * (line2.BeginPoint.X - line1.BeginPoint.X);
            double v2 = (line1.EndPoint.X - line1.BeginPoint.X) * (line2.EndPoint.Y - line1.BeginPoint.Y) - (line1.EndPoint.Y - line1.BeginPoint.Y) * (line2.EndPoint.X - line1.BeginPoint.X);
            double v3 = (line2.EndPoint.X - line2.BeginPoint.X) * (line1.BeginPoint.Y - line2.BeginPoint.Y) - (line2.EndPoint.Y - line2.BeginPoint.Y) * (line1.BeginPoint.X - line2.BeginPoint.X);
            double v4 = (line2.EndPoint.X - line2.BeginPoint.X) * (line1.EndPoint.Y - line2.BeginPoint.Y) - (line2.EndPoint.Y - line2.BeginPoint.Y) * (line1.EndPoint.X - line2.BeginPoint.X);
            if(v1 * v2 < 0 && v3 * v4 < 0)
            {
                return true;
            }
            return false;
        }
    }
}