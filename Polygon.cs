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

            // Проверяем полное вхождение прямоугольной области полигона в выделенную область
            if(GEORect.Contains(bounds, geoRect))
            {
                return true;
            }

            // Проверяем пересечение границ выделяемой прямоугольной области с полигоном
            int countNodes = CountNodes();
            var rectLines = GEORect.GEORectToLines(geoRect);
            foreach(var line in rectLines)
            {
                for(int i = 0; i < countNodes; ++i)
                {
                    var polygonLine = new Line();
                    if(i != countNodes - 1)
                    {
                        polygonLine.BeginPoint = Nodes[i];
                        polygonLine.EndPoint = Nodes[i + 1];
                        
                    }
                    else
                    {
                        polygonLine.BeginPoint = Nodes[0];
                        polygonLine.EndPoint = Nodes[countNodes - 1];
                    }
                    if(Line.IsCrossLines(line, polygonLine))
                    {
                        return true;
                    }
                }
            }

            // Проверяем когда выделяемая прямоугольная область полностью внутри полигона
            var geoRectCenter = new GEOPoint((geoRect.XMin + Math.Abs(geoRect.XMin - geoRect.XMax) / 2.0), geoRect.YMin + Math.Abs(geoRect.YMin - geoRect.YMax) / 2.0);
            var geoPointObject = new GEOPoint(bounds.XMax, geoRectCenter.Y);

            var crossLine = new Line();
            crossLine.BeginPoint = geoRectCenter;
            crossLine.EndPoint = geoPointObject;

            int count = 0;
            for(int i = 0; i < countNodes; ++i)
            {
                var line = new Line();
                if(i != countNodes - 1)
                {
                    line.BeginPoint = Nodes[i];
                    line.EndPoint = Nodes[i + 1];
                }
                else
                {
                    line.BeginPoint = Nodes[0];
                    line.EndPoint = Nodes[countNodes - 1];
                }
                if(Line.IsCrossLines(crossLine, line))
                {
                    count++;
                }
            }
            return !(count % 2 == 0);
        }
    }
}