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
            Point p1 = new Point();
            p1.Position = new GEOPoint(10, 10);
            Layer layer1 = new Layer();
            layer1.AddMapObject(p1);
            mapControl.AddLayer(layer1);
    
        }
    }
}