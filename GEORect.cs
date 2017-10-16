using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGIS
{
    public class GEORect
    {
        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }
        
        public GEORect(double xMin, double xMax, double yMin, double yMax)
        {
            XMin = xMin;
            XMax = xMax;
            YMin = yMin;
            YMax = yMax;
        }

        public static bool IsExist(GEORect geoRect)
        {
            if(geoRect.XMin == 0.0 && geoRect.XMax == 0.0 && geoRect.YMin == 0.0 && geoRect.YMax == 0.0)
            {
                return false;
            }
            return true;
        }

        public static GEORect Union(GEORect geoRect1, GEORect geoRect2)
        {
            if(!IsExist(geoRect1))
            {
                return geoRect2;
            }
            else if(!IsExist(geoRect2))
            {
                return geoRect1;
            }
            else if(!IsExist(geoRect1) && !IsExist(geoRect2))
            {
                return new GEORect(0.0, 0.0, 0.0, 0.0);
            }
            double xMin = Math.Min(geoRect1.XMin, geoRect2.XMin);
            double xMax = Math.Max(geoRect1.XMax, geoRect2.XMax);
            double yMin = Math.Min(geoRect1.YMin, geoRect2.YMin);
            double yMax = Math.Max(geoRect1.YMax, geoRect2.YMax);  
            return new GEORect(xMin, xMax, yMin, yMax);
        }
    }
}