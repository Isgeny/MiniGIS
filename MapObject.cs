using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGIS
{
    public enum MapObjectType
    {
        Point,
        Line,
        PolyLine,
        Polygon
    }

    public abstract class MapObject
    {
        public MapObjectType Type { get; set; }
    }
}