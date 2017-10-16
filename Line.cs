using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MiniGIS
{
    public class Line : MapObject
    {
        public GEOPoint BeginPoint { get; set; }
        public GEOPoint EndPoint { get; set; }
        public LineStyle Style { get; set; }

        public Line()
        {
            BeginPoint = new GEOPoint();
            EndPoint = new GEOPoint();
            Type = MapObjectType.Line;
            Style = new LineStyle();
        }

        public override void Draw(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            var screenLocaltionBeginPoint = Layer.Map.MapToScreen(BeginPoint);
            var screenLocaltionEndPoint = Layer.Map.MapToScreen(EndPoint);

            var lineWidth = Style.Width;
            var lineColor = Style.Color;
            var pen = new Pen(lineColor, lineWidth);

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
    }
}