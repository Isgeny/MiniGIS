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
        public Map()
        {
            InitializeComponent();

            Layers = new List<Layer>();
            MapScale = 1.0;
            MapCenter = new GEOPoint();
        }

        public List<Layer> Layers { get; set; }
        public double MapScale { get; set; }
        public GEOPoint MapCenter { get; set; }

        private void Map_Paint(object sender, PaintEventArgs e)
        {
            
        }

        public System.Drawing.Point MapToScreen(GEOPoint mapPoint)
        {
            var screenPoint = new System.Drawing.Point();
            screenPoint.X = this.Width / 2 + Convert.ToInt32(mapPoint.X);
            screenPoint.Y = this.Height / 2 - Convert.ToInt32(mapPoint.Y);
            return screenPoint;
        }

        public GEOPoint ScreenToMap(System.Drawing.Point screenPoint)
        {
            var mapPoint = new GEOPoint();
            mapPoint.X = screenPoint.X - this.Width / 2;
            mapPoint.Y = this.Height / 2 - screenPoint.Y;
            return mapPoint;
        }
    }
}