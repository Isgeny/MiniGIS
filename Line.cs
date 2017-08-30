using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGIS
{
    public class Line
    {
        public GEOPoint BeginPoint { get; set; }
        public GEOPoint EndPoint { get; set; }

        public Line()
        {
            BeginPoint = new GEOPoint();
            EndPoint = new GEOPoint();
        }
    }
}