using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            
        }
    }
}