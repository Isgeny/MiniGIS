using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGIS
{
    public class PolyLine : MapObject
    {
        public List<GEOPoint> Nodes { get; set; }

        public PolyLine()
        {
            Nodes = new List<GEOPoint>();
            Type = MapObjectType.PolyLine;
        }

        public PolyLine(List<GEOPoint> nodes)
        {
            int nodesCount = nodes.Count;
            Nodes = new List<GEOPoint>(nodesCount);
            foreach(var node in nodes)
            {
                var point = new GEOPoint(node.X, node.Y);
                Nodes.Add(point);
            }
            Type = MapObjectType.PolyLine;
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
    }
}