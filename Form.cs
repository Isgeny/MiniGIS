using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniGIS
{
    public partial class Form : System.Windows.Forms.Form
    {
        
        public Form()
        {
            InitializeComponent();

            var point1 = new Point();
            point1.Position = new GEOPoint(100, 100);

            Line OX = new Line();
            OX.BeginPoint = new GEOPoint(-this.Width / 2, 0);
            OX.EndPoint = new GEOPoint(this.Width / 2, 0);

            Line OY = new Line();
            OY.BeginPoint = new GEOPoint(0, this.Height / 2);
            OY.EndPoint = new GEOPoint(0, -this.Height / 2);

            var layer1 = new Layer();
            layer1.AddMapObject(point1);
            layer1.AddMapObject(OX);
            layer1.AddMapObject(OY);
            mapControl.AddLayer(layer1);
    
        }
    }
}