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
    public partial class Map : UserControl
    {
        public List<Layer> Layers { get; set; }
        public double MapScale { get; set; }
        public GEOPoint MapCenter { get; set; }

        public Map()
        {
            InitializeComponent();

            Layers = new List<Layer>();
            MapScale = 1.0;
            MapCenter = new GEOPoint();
        }       

        private void Map_Paint(object sender, PaintEventArgs e)
        {
            
        }

        public System.Drawing.Point MapToScreen(GEOPoint mapPoint)
        {
            var screenPoint = new System.Drawing.Point();
            screenPoint.X = (this.Width / 2 + Convert.ToInt32(mapPoint.X) + Convert.ToInt32(MapCenter.X)) * Convert.ToInt32(MapScale);
            screenPoint.Y = (this.Height / 2 - Convert.ToInt32(mapPoint.Y) + Convert.ToInt32(MapCenter.Y)) * Convert.ToInt32(MapScale);
            return screenPoint;
        }

        public GEOPoint ScreenToMap(System.Drawing.Point screenPoint)
        {
            var mapPoint = new GEOPoint();
            mapPoint.X = (screenPoint.X - this.Width / 2 - MapCenter.X) * MapScale;
            mapPoint.Y = (this.Height / 2 - screenPoint.Y - MapCenter.Y) * MapScale;
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
    }
}