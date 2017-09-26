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
        public ToolStripButton CurrentToolBtn { get; set; }

        public Form()
        {
            InitializeComponent();

            Line OX = new Line();
            OX.BeginPoint = new GEOPoint(-this.Width / 2, 0);
            OX.EndPoint = new GEOPoint(this.Width / 2, 0);

            Line OY = new Line();
            OY.BeginPoint = new GEOPoint(0, this.Height / 2);
            OY.EndPoint = new GEOPoint(0, -this.Height / 2);

            var pointX = new Point();
            pointX.Style = new PointStyle("Arial", Convert.ToByte('X'), 14, System.Drawing.Color.Black);
            pointX.Position = new GEOPoint(this.Width / 2 - 40, pointX.Style.SymbolSize);

            var pointY = new Point();
            pointY.Style = new PointStyle("Arial", Convert.ToByte('Y'), 14, System.Drawing.Color.Black);
            pointY.Position = new GEOPoint(pointX.Style.SymbolSize, this.Height / 2 - 60);

            var polyLine = new PolyLine();
            polyLine.AddNode(new GEOPoint(0, 0));
            polyLine.AddNode(new GEOPoint(-50, -50));
            polyLine.AddNode(new GEOPoint(-180, -250));
            polyLine.AddNode(new GEOPoint(180, 100));

            var polygon = new Polygon();
            polygon.AddNode(new GEOPoint(45, 75));
            polygon.AddNode(new GEOPoint(85, -50));
            polygon.AddNode(new GEOPoint(100, 0));
            polygon.AddNode(new GEOPoint(-75, 50));

            var layer1 = new Layer();
            layer1.AddMapObject(OX);
            layer1.AddMapObject(OY);
            layer1.AddMapObject(pointX);
            layer1.AddMapObject(pointY);
            layer1.AddMapObject(polyLine);
            layer1.AddMapObject(polygon);
            mapControl.AddLayer(layer1);

            mapControl.CurrentTool = Tool.Select;
            CurrentToolBtn = selectBtn;
            CurrentToolBtn.Checked = true;
        }

        private void OnToolStripBtnClicked(object sender, EventArgs e)
        {
            var btn = sender as ToolStripButton;
            var toolBtnType = btn.Text;
            Tool currentTool;
            if(Enum.TryParse(toolBtnType, out currentTool))
            {
                mapControl.CurrentTool = currentTool;
                CurrentToolBtn.Checked = false;
                CurrentToolBtn = btn;
                CurrentToolBtn.Checked = true;
            }
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}