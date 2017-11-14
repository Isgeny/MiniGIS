using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MiniGIS
{
    public class Polygon : PolyLine
    {
        public PolygonStyle PolygonStyle { get; set; }

        public Polygon() : base()
        {
            Type = MapObjectType.Polygon;
            PolygonStyle = new PolygonStyle();
        }

        public Polygon(List<GEOPoint> nodes, LineStyle lineStyle, PolygonStyle polygonStyle) : base(nodes, lineStyle)
        {
            Type = MapObjectType.Polygon;
            PolygonStyle = polygonStyle;
        }

        public override void Draw(PaintEventArgs e)
        {
            var graphics = e.Graphics;

            var lineWidth = LineStyle.Width;
            var lineColor = LineStyle.Color;
            var pen = new Pen(lineColor, lineWidth);
            if(Selected)
            {
                pen.DashPattern = new float[] { 4.0f, 2.0f };
            }

            var polygonColor = PolygonStyle.Color;
            var brush = new SolidBrush(polygonColor);

            var points = new System.Drawing.Point[CountNodes()];
            for(int i = 0; i < CountNodes(); ++i)
            {
                points[i] = Layer.Map.MapToScreen(Nodes[i]);
            }

            graphics.FillPolygon(brush, points);
            graphics.DrawPolygon(pen, points);
        }

        public override bool IsInside(GEORect geoRect)
        {
            var bounds = GetBounds();
            var geoRectCenter = new GEOPoint((geoRect.XMin + Math.Abs(geoRect.XMin - geoRect.XMax) / 2.0), geoRect.YMin + Math.Abs(geoRect.YMin - geoRect.YMax) / 2.0);
            var geoPointObject = new GEOPoint(bounds.XMax + 1.0, geoRectCenter.Y);

            var CrossLine = new Line();
            CrossLine.BeginPoint = geoRectCenter;
            CrossLine.EndPoint = geoPointObject;

            var count = 0;
            for(int i = 0; i < CountNodes() - 1; ++i)
            {
                var line = new Line();
                line.BeginPoint = Nodes[i];
                line.EndPoint = Nodes[i + 1];
                if(Line.IsCrossLines(CrossLine, line))
                {
                    count++;
                }
            }
            
            //Ребро с начальной и конечной вершиной
            var lastLine = new Line();
            lastLine.BeginPoint = Nodes[0];
            lastLine.EndPoint = Nodes[CountNodes() - 1];
            if(Line.IsCrossLines(CrossLine, lastLine))
            {
                count++;
            }

            return !(count % 2 == 0);
        }
    }
}