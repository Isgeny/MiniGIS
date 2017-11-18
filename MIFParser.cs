using System;
using System.IO;
using System.Drawing;
using System.Globalization;
using System.Collections.Generic;

namespace MiniGIS
{
    public class MIFParser
    {
        public int Version { get; set; }
        public string Charset { get; set; }
        public char Delimiter { get; set; }
        public List<int> Unique { get; set; }
        public List<int> Index { get; set; }
        public List<int> Transform { get; set; }
        
        public class Column
        {
            public string Name { get; set; }
            public string Type { get; set; }

            public Column(string name, string type)
            {
                Name = name;
                Type = type;
            }
        }

        public int ColumnsN { get; set; }
        public List<Column> Columns { get; set; }
        public List<MapObject> Data { get; set; }

        public MIFParser(string layerFilename)
        {
            Version = 300;
            Charset = "";
            Delimiter = '\t';
            Unique = new List<int>();
            Index = new List<int>();
            Transform = new List<int>(4);
            ColumnsN = 0;
            Data = new List<MapObject>();

            using(var sr = new StreamReader(layerFilename))
            {
                // Версия
                Version = Convert.ToInt32(sr.ReadLine().Split(' ')[1]);

                // Кодировка
                string charset = sr.ReadLine().Split(' ')[1];
                Charset = charset.Substring(1, charset.Length - 2);

                string[] line = sr.ReadLine().Split(' ');

                // Разделитель
                if(line[0] == "Delimiter")
                {
                    Delimiter = line[1].Substring(1, line[1].Length - 2)[0];
                    line = sr.ReadLine().Split(' ');
                }

                // Уникальная колонка
                if(line[0] == "Unique")
                {
                    string[] uniqueStr = line[1].Split(',');
                    for(int i = 0; i < uniqueStr.Length; ++i)
                    {
                        Unique[i] = Convert.ToInt32(uniqueStr[i]);
                    }
                    line = sr.ReadLine().Split(' ');
                }

                // Индекс
                if(line[0] == "Index")
                {
                    string[] indexStr = line[1].Split(',');
                    for(int i = 0; i < indexStr.Length; ++i)
                    {
                        Index[i] = Convert.ToInt32(indexStr[i]);
                    }
                    line = sr.ReadLine().Split(' ');
                }

                // Преобразование
                if(line[0] == "Transform")
                {
                    string[] transformStr = line[1].Split(',');
                    for(int i = 0; i < transformStr.Length; ++i)
                    {
                        Transform[i] = Convert.ToInt32(transformStr[i]);
                    }
                    line = sr.ReadLine().Split(' ');
                }

                // Колонки
                ColumnsN = Convert.ToInt32(line[1]);
                Columns = new List<Column>(ColumnsN);
                for(int i = 0; i < ColumnsN; ++i)
                {
                    line = sr.ReadLine().Split(' ');
                    var column = new Column(line[0], line[1]);
                }

                // Слово Data
                sr.ReadLine();

                // Данные
                while(!sr.EndOfStream)
                {
                    line = sr.ReadLine().Split(' ');
                    if(line[0] == "Point")
                    {
                        // Координаты точки
                        double x = double.Parse(line[1], CultureInfo.InvariantCulture);
                        double y = double.Parse(line[2], CultureInfo.InvariantCulture);

                        // Symbol
                        byte symbolByte = Convert.ToByte(line[4].Substring(1, line[4].Length - 2));
                        Color color = IntToColor(Convert.ToInt32(line[5].Substring(0, line[5].Length - 1)));
                        int symbolSize = Convert.ToInt32(line[6].Substring(0, line[6].Length - 1));
                        string fontFamily = line[7].Substring(1, line[7].Length - 3);

                        // Добавление точки в основной список
                        var point = new Point(new GEOPoint(x, y), new PointStyle(fontFamily, symbolByte, symbolSize, color));
                        Data.Add(point);
                    }
                    else if(line[0] == "Line")
                    {
                        // Координаты начала и конца линии
                        double x1 = double.Parse(line[1], CultureInfo.InvariantCulture);
                        double y1 = double.Parse(line[2], CultureInfo.InvariantCulture);
                        double x2 = double.Parse(line[3], CultureInfo.InvariantCulture);
                        double y2 = double.Parse(line[4], CultureInfo.InvariantCulture);

                        // Pen
                        float width = float.Parse(line[6].Substring(1, line[6].Length - 1));
                        Color color = IntToColor(Convert.ToInt32(line[8].Substring(0, line[8].Length - 1)));

                        // Добавление линии в основной список
                        var mapLine = new Line(new GEOPoint(x1, y1), new GEOPoint(x2, y2), new LineStyle(color, width));
                        Data.Add(mapLine);
                    }
                    else if(line[0] == "Pline")
                    {
                        // Количество полилиний
                        int n = Convert.ToInt32(sr.ReadLine().Split(' ')[0]);

                        // Вершины полилинии
                        var pointsList = new List<GEOPoint>();
                        for(int i = 0; i < n; ++i)
                        {
                            line = sr.ReadLine().Split(' ');
                            double x = double.Parse(line[0], CultureInfo.InvariantCulture);
                            double y = double.Parse(line[1], CultureInfo.InvariantCulture);
                            var point = new GEOPoint(x, y);
                            pointsList.Add(point);
                        }

                        // Pen
                        line = sr.ReadLine().Split(' ');
                        float width = float.Parse(line[1].Substring(1, line[0].Length - 2));
                        Color color = IntToColor(Convert.ToInt32(line[3].Substring(0, line[3].Length - 1)));

                        // Добавление полилинии в основной список
                        var polyline = new PolyLine(pointsList, new LineStyle(color, width));
                        Data.Add(polyline);
                    }
                    else if(line[0] == "Region")
                    {
                        // Количество регионов
                        int regionsNumb = Convert.ToInt32(line[1]);

                        // Вершины полигонов
                        var polygonsList = new List<Polygon>();
                        for(int i = 0; i < regionsNumb; ++i)
                        {
                            line = sr.ReadLine().Split(' ');
                            int pointsNumb = Convert.ToInt32(line[0]);
                            var pointList = new List<GEOPoint>();
                            for(int j = 0; j < pointsNumb; ++j)
                            {
                                line = sr.ReadLine().Split(' ');
                                double x = double.Parse(line[0], CultureInfo.InvariantCulture);
                                double y = double.Parse(line[1], CultureInfo.InvariantCulture);
                                var point = new GEOPoint(x, y);
                                pointList.Add(point);
                            }

                            // Добавление полигонов во временный список
                            var polygon = new Polygon(pointList, new LineStyle(), new PolygonStyle());
                            polygonsList.Add(polygon);
                        }

                        // Pen
                        line = sr.ReadLine().Split(' ');
                        float width = float.Parse(line[1].Substring(1, line[0].Length - 2));
                        Color penColor = IntToColor(Convert.ToInt32(line[3].Substring(0, line[3].Length - 1)));

                        // Brush
                        line = sr.ReadLine().Split(' ');
                        Color brushColor = IntToColor(Convert.ToInt32(line[2].Substring(0, line[2].Length - 1)));

                        // Добавление полигонов в основной список и задание стилей 
                        foreach(var polygon in polygonsList)
                        {
                            polygon.LineStyle.Width = width;
                            polygon.LineStyle.Color = penColor;
                            polygon.PolygonStyle.Color = brushColor;
                            Data.Add(polygon);
                        }
                    }
                }
            }
        }

        private Color IntToColor(int dec)
        {
            byte red = (byte)((dec >> 16) & 0xff);
            byte green = (byte)((dec >> 8) & 0xff);
            byte blue = (byte)(dec & 0xff);
            return (Color.FromArgb(red, green, blue));
        }
    }
}