using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MiniGIS
{
    public enum Tool
    {
        Select,
        Pan,
        ZoomIn,
        ZoomOut
    }

    public partial class Map : UserControl
    {
        public List<Layer> Layers;
        public double MapScale { get; set; }
        public GEOPoint MapCenter { get; set; }
        public Tool CurrentTool { get; set; }
        public bool IsMouseDown { get; set; }
        public System.Drawing.Point MouseDownPosition { get; set; }
        private const int shake = 5;
        private Layer cosmeticLayer;
        public List<MapObject> SelectedObjects { get; }

        public Map()
        {
            InitializeComponent();
            MapScale = 1.0;
            MapCenter = new GEOPoint();
            Layers = new List<Layer>();
            IsMouseDown = false;
            SelectedObjects = new List<MapObject>();

            cosmeticLayer = new Layer("Cosmetic");
            cosmeticLayer.Visible = false;
            cosmeticLayer.Map = this;
        }

        public GEORect GEOBounds
        {
            get
            {
                var result = new GEORect(0.0, 0.0, 0.0, 0.0);
                foreach(var layer in Layers)
                {
                    if(layer.Visible)
                    {
                        result = GEORect.Union(result, layer.GEOBounds);
                    }
                }
                return result;
            }
        }

        private void Map_Paint(object sender, PaintEventArgs e)
        {
            int countLayers = CountLayers();
            for(int i = countLayers - 1; i >= 0; --i)
            {
                if(Layers[i].Visible)
                {
                    Layers[i].Draw(e);
                }
            }
            cosmeticLayer.Draw(e);
        }

        // Перевод координат карты в координаты экрана
        public System.Drawing.Point MapToScreen(GEOPoint mapPoint)
        {
            var screenPoint = new System.Drawing.Point();
            screenPoint.X = (int)((mapPoint.X - MapCenter.X) * MapScale + this.Width / 2 + 0.5);
            screenPoint.Y = (int)(-(mapPoint.Y - MapCenter.Y) * MapScale + this.Height / 2 + 0.5);
            return screenPoint;
        }

        // Перевод координат экрана в координаты карты
        public GEOPoint ScreenToMap(System.Drawing.Point screenPoint)
        {
            var mapPoint = new GEOPoint();
            mapPoint.X = (screenPoint.X - this.Width / 2) / MapScale + MapCenter.X;
            mapPoint.Y = -(screenPoint.Y - this.Height / 2) / MapScale + MapCenter.Y;
            return mapPoint;
        }

        public void AddLayer(Layer layer)
        {
            layer.Map = this;
            Layers.Add(layer);
        }

        public void InsertLayer(int index, Layer layer)
        {
            Layers.Insert(index, layer);
        }

        public void RemoveLayer(int index)
        {
            Layers.RemoveAt(index);
        }

        public int CountLayers()
        {
            return Layers.Count;
        }

        public void SwapLayers(int layerIndex1, int layerIndex2)
        {
            Layer temp = Layers[layerIndex1];
            Layers[layerIndex1] = Layers[layerIndex2];
            Layers[layerIndex2] = temp;
        }

        public void EntireView()
        {
            var bounds = GEOBounds;
            double Dx = Math.Abs(bounds.XMax - bounds.XMin);
            double Dy = Math.Abs(bounds.YMax - bounds.YMin);
            GEOPoint newCenter = new GEOPoint((bounds.XMin + bounds.XMax) / 2.0, (bounds.YMin + bounds.YMax) / 2.0);
            MapCenter = newCenter;
            if(Width / Dx < Height / Dy)
            {
                MapScale = Width / Dx - 0.01;
            }
            else
            {
                MapScale = Height / Dy - 0.01;
            }
            Refresh();
        }

        private void Map_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            MouseDownPosition = e.Location;
            if(CurrentTool == Tool.Pan)
            {
                Cursor = Cursors.Hand;
            }
        }

        private void Map_MouseMove(object sender, MouseEventArgs e)
        {
            switch(CurrentTool)
            {
                case Tool.Pan:
                    if(IsMouseDown)
                    {
                        double dx, dy;
                        dx = MouseDownPosition.X - e.X;
                        dy = MouseDownPosition.Y - e.Y;

                        MapCenter.X += dx / MapScale;
                        MapCenter.Y -= dy / MapScale;

                        MouseDownPosition = e.Location;

                        Refresh();
                    }
                    break;
                case Tool.Select:
                case Tool.ZoomIn:
                    if(Math.Abs(MouseDownPosition.X - e.X) > shake || Math.Abs(MouseDownPosition.Y - e.Y) > shake)
                    {
                        if(IsMouseDown)
                        {
                            Refresh();
                            var g = CreateGraphics();

                            var frame = new PolyLine();
                            frame.AddNode(ScreenToMap(new System.Drawing.Point(MouseDownPosition.X, MouseDownPosition.Y)));
                            frame.AddNode(ScreenToMap(new System.Drawing.Point(e.X, MouseDownPosition.Y)));
                            frame.AddNode(ScreenToMap(new System.Drawing.Point(e.X, e.Y)));
                            frame.AddNode(ScreenToMap(new System.Drawing.Point(MouseDownPosition.X, e.Y)));
                            frame.AddNode(ScreenToMap(new System.Drawing.Point(MouseDownPosition.X, MouseDownPosition.Y)));

                            cosmeticLayer.Clear();
                            cosmeticLayer.AddMapObject(frame);
                        }
                    }
                    break;
            }
        }

        private void Map_MouseUp(object sender, MouseEventArgs e)
        {
            switch(CurrentTool)
            {
                case Tool.Select:
                    cosmeticLayer.Clear();
                    double Dx1 = Math.Abs(MouseDownPosition.X - e.X);
                    double Dy1 = Math.Abs(MouseDownPosition.Y - e.Y);

                    // Точеченое выделение
                    if(Dx1 < shake && Dy1 < shake)
                    {
                        GEOPoint searchCenter = ScreenToMap(e.Location);
                        double xMin = searchCenter.X - shake / 2.0;
                        double xMax = searchCenter.X + shake / 2.0;
                        double yMin = searchCenter.Y - shake / 2.0;
                        double yMax = searchCenter.Y + shake / 2.0;
                        GEORect searchRect = new GEORect(xMin, xMax, yMin, yMax);
                        MapObject result = FindObject(searchRect);
                        if(result != null)
                        {
                            // Множественное выделение объектов с помощью ctrl
                            if(ModifierKeys.HasFlag(Keys.Control))
                            {
                                result.Selected = !result.Selected;
                            }
                            // Одиночное выделение объекта
                            else
                            {
                                foreach(var selectedObject in SelectedObjects)
                                {
                                    selectedObject.Selected = false;
                                }
                                SelectedObjects.Clear();
                                result.Selected = true;
                            }
                            SelectedObjects.Add(result);
                        }
                        // Если кликнули не по объекту то очищаем выделение всех объектов
                        else
                        {
                            foreach(var selectedObject in SelectedObjects)
                            {
                                selectedObject.Selected = false;
                            }
                            SelectedObjects.Clear();
                        }
                    }
                    // Выделение прямоугольной областью
                    else
                    {
                        GEOPoint begin = ScreenToMap(MouseDownPosition);
                        GEOPoint end = ScreenToMap(e.Location);
                        double xMin = Math.Min(begin.X, end.X);
                        double xMax = Math.Max(begin.X, end.X);
                        double yMin = Math.Min(begin.Y, end.Y);
                        double yMax = Math.Max(begin.Y, end.Y);
                        GEORect searchRect = new GEORect(xMin, xMax, yMin, yMax);
                        var selectedObjects = FindMapObjects(searchRect);

                        foreach(var selectedObject in SelectedObjects)
                        {
                            selectedObject.Selected = false;
                        }
                        SelectedObjects.Clear();
                        
                        foreach(var selectedObject in selectedObjects)
                        {
                            selectedObject.Selected = true;
                            SelectedObjects.Add(selectedObject);
                        }
                    }
                    IsMouseDown = false;
                    Refresh();
                    break;
                case Tool.Pan:
                    IsMouseDown = false;
                    Cursor = Cursors.Default;
                    break;
                case Tool.ZoomIn:
                    cosmeticLayer.Clear();
                    double Dx = Math.Abs(MouseDownPosition.X - e.X);
                    double Dy = Math.Abs(MouseDownPosition.Y - e.Y);

                    // Приблежение c помощью рамки
                    if(Dx > shake || Dy > shake)
                    {
                        GEOPoint newCenter = ScreenToMap(new System.Drawing.Point((e.X + MouseDownPosition.X) / 2, (e.Y + MouseDownPosition.Y) / 2));
                        MapCenter = newCenter;
                        double newMapScale = 0.0;
                        if(Width / Dx > Height / Dy)
                        {
                            newMapScale = MapScale * Width / Dx;
                        }
                        else
                        {
                            newMapScale = MapScale * Height / Dy;
                        }
                        MapScale = (newMapScale < 10.0) ? newMapScale : 10.0;
                    }
                    // Точечное приближение
                    else
                    {
                        MapCenter = ScreenToMap(MouseDownPosition);
                        double newMapScale1 = MapScale * 2.0;
                        MapScale = (newMapScale1 < 10.0) ? newMapScale1 : 10.0;
                    }
                    IsMouseDown = false;
                    Refresh();
                    break;
                case Tool.ZoomOut:
                    IsMouseDown = false;
                    MapCenter = ScreenToMap(MouseDownPosition);
                    double newMapScale2 = MapScale / 2.0;
                    MapScale = (newMapScale2 > 0.1) ? newMapScale2 : 0.1;
                    Refresh();
                    break;
            }
        }

        private MapObject FindObject(GEORect searchRect)
        {
            for(int i = Layers.Count - 1; i >= 0; --i)
            {
                var layer = Layers[i];
                if(layer.Visible)
                {
                    var result = layer.FindObject(searchRect);
                    if(result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }

        private List<MapObject> FindMapObjects(GEORect searchRect)
        {
            var mapObjects = new List<MapObject>();
            for(int i = Layers.Count - 1; i >= 0; --i)
            {
                var layer = Layers[i];
                if(layer.Visible)
                {
                    var layerObjects = layer.FindObjects(searchRect);
                    foreach(var layerObject in layerObjects)
                    {
                        mapObjects.Add(layerObject);
                    }
                }
            }
            return mapObjects;
        }

        public void DeleteSelectedObjects()
        {
            foreach(var selectedObject in SelectedObjects)
            {
                foreach(var layer in Layers)
                {
                    layer.RemoveMapObject(selectedObject);
                }
            }
            SelectedObjects.Clear();
            Refresh();
        }
    }
}