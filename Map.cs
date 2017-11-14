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
        public List<MapObject> SelectedObjects { get; set; }

        public Map()
        {
            InitializeComponent();
            MapScale = 1.0;
            MapCenter = new GEOPoint();
            Layers = new List<Layer>();
            IsMouseDown = false;
            SelectedObjects = new List<MapObject>();

            cosmeticLayer = new Layer();
            AddLayer(cosmeticLayer);
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
            foreach(var layer in Layers)
            {
                if(layer.Visible)
                {
                    layer.Draw(e);
                }
            }
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

        public void RemoveLayer(Layer layer)
        {
            Layers.Remove(layer);
        }

        public int CountLayers()
        {
            return Layers.Count;
        }

        private void Map_MouseDown(object sender, MouseEventArgs e)
        {
            switch(CurrentTool)
            {
                case Tool.Select:
                    IsMouseDown = true;
                    MouseDownPosition = e.Location;
                    break;
                case Tool.Pan:
                    IsMouseDown = true;
                    MouseDownPosition = e.Location;
                    Cursor = Cursors.Hand;
                    break;
                case Tool.ZoomIn:
                    IsMouseDown = true;
                    MouseDownPosition = e.Location;
                    break;
                case Tool.ZoomOut:
                    IsMouseDown = true;
                    MouseDownPosition = e.Location;
                    break;
            }
        }

        private void Map_MouseMove(object sender, MouseEventArgs e)
        {
            switch(CurrentTool)
            {
                case Tool.Select:
                    break;
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
                case Tool.ZoomOut:
                    break;
            }
        }

        private void Map_MouseUp(object sender, MouseEventArgs e)
        {
            switch(CurrentTool)
            {
                case Tool.Select:
                    double Dx1 = Math.Abs(MouseDownPosition.X - e.X);
                    double Dy1 = Math.Abs(MouseDownPosition.Y - e.Y);
                    if(Dx1 < shake && Dy1 < shake)
                    {
                        GEOPoint searchCenter = ScreenToMap(e.Location);
                        double xMin = searchCenter.X - shake / 2.0 / MapScale;
                        double xMax = searchCenter.X + shake / 2.0 / MapScale;
                        double yMin = searchCenter.Y - shake / 2.0 / MapScale;
                        double yMax = searchCenter.Y + shake / 2.0 / MapScale;
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
            for(int i = Layers.Count - 1; i > 0; --i)
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
    }
}