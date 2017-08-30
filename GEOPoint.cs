using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGIS
{
    public class GEOPoint
    {
        public double X { get; set; }
        public double Y { get; set; }

        public GEOPoint()
        {
            X = 0.0;
            Y = 0.0;
        }

        public GEOPoint(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}