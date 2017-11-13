using System;
using System.Collections.Generic;

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

        // Существует ли такой прямоугольник
        public static bool IsExist(GEORect geoRect)
        {
            if(geoRect.XMin == 0.0 && geoRect.XMax == 0.0 && geoRect.YMin == 0.0 && geoRect.YMax == 0.0)
            {
                return false;
            }
            return true;
        }

        // Пересекаются ли два прямоугольника
        public static bool IsIntersect(GEORect geoRect1, GEORect geoRect2)
        {
            return (geoRect1.YMin < geoRect2.YMax && geoRect1.YMax > geoRect2.YMin && geoRect1.XMax > geoRect2.XMin && geoRect1.XMin < geoRect2.XMax);
        }

        // Пересекается ли прямоугольник с отрезком
        public static bool IsCrossRectLines(GEORect geoRect, Line line)
        {
            // Разбиваем прямоугольник на 4 отрезка
            List<Line> rectLines = GEORectToLines(geoRect);

            // Проверяем пересечение каждой стороны прямоугольника с исходным отрезком
            foreach(var rectLine in rectLines)
            {
                if(Line.IsCrossLines(line, rectLine))
                {
                    return true;
                }
                // взаимное расположение двух отрезков определяется с помощью векторных произведений
                // рассматривается 4 векторных произведения для 2 отрезков
                // если векторы положительно ориентированы, то векторное произведение положительно, иначе отрицательно
                // для того, чтобы выполнялось пересечение отрезков необходимо, чтобы векторные произведения имели противоположные знаки
                //double v1 = (line.EndPoint.X - line.BeginPoint.X) * (rectLine.BeginPoint.Y - line.BeginPoint.Y) - (line.EndPoint.Y - line.BeginPoint.Y) * (rectLine.BeginPoint.X - line.BeginPoint.X);
                //double v2 = (line.EndPoint.X - line.BeginPoint.X) * (rectLine.EndPoint.Y - line.BeginPoint.Y) - (line.EndPoint.Y - line.BeginPoint.Y) * (rectLine.EndPoint.X - line.BeginPoint.X);
                //double v3 = (rectLine.EndPoint.X - rectLine.BeginPoint.X) * (line.BeginPoint.Y - rectLine.BeginPoint.Y) - (rectLine.EndPoint.Y - rectLine.BeginPoint.Y) * (line.BeginPoint.X - rectLine.BeginPoint.X);
                //double v4 = (rectLine.EndPoint.X - rectLine.BeginPoint.X) * (line.EndPoint.Y - rectLine.BeginPoint.Y) - (rectLine.EndPoint.Y - rectLine.BeginPoint.Y) * (line.EndPoint.X - rectLine.BeginPoint.X);
                //if(v1 * v2 < 0 && v3 * v4 < 0)
                //{
                //    return true;
                //}
            }
            return false;
        }

        // Преобразует прямоугольник в список из 4х отрезков - его сторон
        public static List<Line> GEORectToLines(GEORect geoRect)
        {
            var topLine = new Line();
            topLine.BeginPoint = new GEOPoint(geoRect.XMin, geoRect.YMax);
            topLine.EndPoint = new GEOPoint(geoRect.XMax, geoRect.YMax);

            var rightLine = new Line();
            rightLine.BeginPoint = new GEOPoint(geoRect.XMax, geoRect.YMax);
            rightLine.EndPoint = new GEOPoint(geoRect.XMax, geoRect.YMin);

            var bottomLine = new Line();
            bottomLine.BeginPoint = new GEOPoint(geoRect.XMax, geoRect.YMin);
            bottomLine.EndPoint = new GEOPoint(geoRect.XMin, geoRect.YMin);

            var leftLine = new Line();
            leftLine.BeginPoint = new GEOPoint(geoRect.XMin, geoRect.YMin);
            leftLine.EndPoint = new GEOPoint(geoRect.XMin, geoRect.YMax);

            var lines = new List<Line>(4);
            lines.Add(topLine);
            lines.Add(rightLine);
            lines.Add(bottomLine);
            lines.Add(leftLine);
            return lines;
        } 

        // Объединение двух прямоугольник в один большой прямоугольник
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