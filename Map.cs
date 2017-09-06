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

            MapCenter = new GEOPoint();
        }

        public List<Layer> Layers { get; set; }
        public double MapScale { get; set; }
        public GEOPoint MapCenter { get; set; }

        private void Map_Paint(object sender, PaintEventArgs e)
        {
            
        }

        public System.Drawing.Point MapToScreen(GEOPoint point)
        {
            return new System.Drawing.Point();
        }

        public GEOPoint ScreenToMap(System.Drawing.Point point)
        {
            return new GEOPoint();
        }
    }
}