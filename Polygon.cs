using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace MiniGIS
{
    public class Polygon : PolyLine
    {
        public PolygonStyle PolygonStyle {get; set; }

        public Polygon() : base()
        {
            Type = MapObjectType.Polygon;
            PolygonStyle = new PolygonStyle();
        }

        public override void Draw(PaintEventArgs e)
        {
            var graphics = e.Graphics;

            var lineWidth = LineStyle.Width;
            var lineColor = LineStyle.Color;
            var pen = new Pen(lineColor, lineWidth);

            var polygonColor = PolygonStyle.Color;
            var brush = new SolidBrush(polygonColor);

            var points = new System.Drawing.Point[CountNodes()];
            for(int i = 0; i < CountNodes(); ++i)
            {
                points[i] = Layer.Map.MapToScreen(Nodes[i]);
            }

            graphics.DrawPolygon(pen, points);
            graphics.FillPolygon(brush, points);
        }
    }
}