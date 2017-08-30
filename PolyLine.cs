using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGIS
{
    public class PolyLine
    {
        public List<GEOPoint> Nodes { get; set; }

        public PolyLine()
        {
            Nodes = new List<GEOPoint>();
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
        }

        public void AddNode(GEOPoint point)
        {
            Nodes.Add(point);
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