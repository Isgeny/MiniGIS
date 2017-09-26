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
    }

    public partial class Map : UserControl
    {
        public List<Layer> Layers;
        public double MapScale { get; set; }
        public GEOPoint MapCenter { get; set; }
        public Tool CurrentTool { get; set; }
        public bool IsMouseDown { get; set; }
        public System.Drawing.Point MouseDownPosition {get; set; }

        public Map()
        {
            InitializeComponent();
            MapScale = 1.0;
            MapCenter = new GEOPoint();
            Layers = new List<Layer>();
            IsMouseDown = false;
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
            screenPoint.X = (int)((mapPoint.X + MapCenter.X) * MapScale + this.Width / 2 + 0.5);
            screenPoint.Y = (int)(-(mapPoint.Y + MapCenter.Y) * MapScale + this.Height / 2 + 0.5);
            return screenPoint;
        }

        public GEOPoint ScreenToMap(System.Drawing.Point screenPoint)
        {
            var mapPoint = new GEOPoint();
            mapPoint.X = (screenPoint.X - this.Width / 2) / MapScale - MapCenter.X;
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
            IsMouseDown = true;
            MouseDownPosition = e.Location;
        }

        private void Map_MouseMove(object sender, MouseEventArgs e)
        {
            if(IsMouseDown)
            {
                double dx, dy;
                dx = MouseDownPosition.X - e.X;
                dy = MouseDownPosition.Y - e.Y;

                MapCenter.X -= dx;
                MapCenter.Y += dy;

                MouseDownPosition = e.Location;

                Refresh();
            }
        }

        private void Map_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
        }
    }
}