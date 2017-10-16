using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniGIS
{
    public enum Tool
    {
        Select,
        Pan,
        ZoomIn,
        ZoomOut,
        EntireView
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

        public Map()
        {
            InitializeComponent();
            MapScale = 1.0;
            MapCenter = new GEOPoint();
            Layers = new List<Layer>();
            IsMouseDown = false;

            cosmeticLayer = new Layer();
            AddLayer(cosmeticLayer);
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

        public System.Drawing.Point MapToScreen(GEOPoint mapPoint)
        {
            var screenPoint = new System.Drawing.Point();
            screenPoint.X = (int)((mapPoint.X - MapCenter.X) * MapScale + this.Width / 2 + 0.5);
            screenPoint.Y = (int)(-(mapPoint.Y - MapCenter.Y) * MapScale + this.Height / 2 + 0.5);
            return screenPoint;
        }

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
                case Tool.EntireView:
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
                    break;
                case Tool.ZoomOut:
                    break;
                case Tool.EntireView:
                    break;
            }
        }

        private void Map_MouseUp(object sender, MouseEventArgs e)
        {
            switch(CurrentTool)
            {
                case Tool.Select:
                    break;
                case Tool.Pan:
                    IsMouseDown = false;
                    Cursor = Cursors.Default;
                    break;
                case Tool.ZoomIn:
                    cosmeticLayer.Clear();
                    double Dx = Math.Abs(MouseDownPosition.X - e.X);
                    double Dy = Math.Abs(MouseDownPosition.Y - e.Y);
                    if(Dx > shake || Dy > shake)
                    {
                        GEOPoint newCenter = ScreenToMap(new System.Drawing.Point((e.X + MouseDownPosition.X) / 2, (e.Y + MouseDownPosition.Y) / 2));
                        MapCenter = newCenter;
                        if(Width / Dx > Height / Dy)
                        {
                            MapScale *= Width / Dx;
                        }
                        else
                        {
                            MapScale *= Height / Dy;
                        }
                    }
                    else
                    {
                        MapCenter = ScreenToMap(MouseDownPosition);
                        MapScale *= 2;
                        Cursor = Cursors.Cross;
                    }
                    IsMouseDown = false;
                    Refresh();
                    break;
                case Tool.ZoomOut:
                    IsMouseDown = false;
                    MapCenter = ScreenToMap(MouseDownPosition);
                    MapScale /= 2;
                    Refresh();
                    break;
                case Tool.EntireView:

                    break;
            }
        }
    }
}