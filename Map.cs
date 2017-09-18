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
        public List<Layer> Layers;
    
        public double MapScale { get; set; }
        public GEOPoint MapCenter { get; set; }

        public Map()
        {
            InitializeComponent();
            MapScale = 1.0;
            MapCenter = new GEOPoint();
            Layers = new List<Layer>();
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
   }
}
 