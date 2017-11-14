using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MiniGIS
{
    public class PolyLine : MapObject
    {
        public List<GEOPoint> Nodes { get; set; }
        public LineStyle LineStyle { get; set; }

        public PolyLine()
        {
            Type = MapObjectType.PolyLine;
            Nodes = new List<GEOPoint>();
            LineStyle = new LineStyle();
        }

        public PolyLine(List<GEOPoint> nodes, LineStyle style)
        {
            Type = MapObjectType.PolyLine;
            int nodesCount = CountNodes();
            Nodes = new List<GEOPoint>(nodesCount);
            foreach(var node in nodes)
            {
                var point = new GEOPoint(node.X, node.Y);
                Nodes.Add(point);
            }
            LineStyle = style;
        }

        public void AddNode(GEOPoint point)
        {
            Nodes.Add(point);
        }

        public void InsertNode(int index, GEOPoint point)
        {
            Nodes.Insert(index, point);
        }

        public void RemoveNode(GEOPoint point)
        {
            Nodes.Remove(point);
        }

        public void RemoveNode(int index)
        {
            Nodes.RemoveAt(index);
        }

        public void ClearNodes()
        {
            Nodes.Clear();
        }

        public int CountNodes()
        {
            return Nodes.Count;
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

            var points = new System.Drawing.Point[CountNodes()];
            for(int i = 0; i < CountNodes(); ++i)
            {
                points[i] = Layer.Map.MapToScreen(Nodes[i]);
            }

            graphics.DrawLines(pen, points);
        }

        public override GEORect GetBounds()
        {
            double xMin = Nodes[0].X, xMax = Nodes[0].X, yMin = Nodes[0].Y, yMax = Nodes[0].Y;
            foreach(var node in Nodes)
            {
                xMin = Math.Min(xMin, node.X);
                xMax = Math.Max(xMax, node.X);
                yMin = Math.Min(yMin, node.Y);
                yMax = Math.Max(yMax, node.Y);
            }
            return new GEORect(xMin, xMax, yMin, yMax);
        }

        public override bool IsInside(GEORect geoRect)
        {
            for(int i = 0; i < CountNodes() - 1; ++i)
            {
                var line = new Line();
                line.BeginPoint = Nodes[i];
                line.EndPoint = Nodes[i + 1];
                if(GEORect.IsCrossRectLines(geoRect, line))
                {
                    return true;
                }
            }
            return false;
        }
    }
}