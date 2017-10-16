using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public Layer Layer {get; set; }
        public GEORect GEOBounds
        { 
            get
            {
                return GetBounds();
            }
        }

        public abstract void Draw(PaintEventArgs e);
        public abstract GEORect GetBounds();
    }
}