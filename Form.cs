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
            OX.BeginPoint = new GEOPoint(-map.Width / 2, 0);
            OX.EndPoint = new GEOPoint(map.Width / 2, 0);

            Line OY = new Line();
            OY.BeginPoint = new GEOPoint(0, map.Height / 2);
            OY.EndPoint = new GEOPoint(0, -map.Height / 2);

            var pointX = new Point();
            pointX.Style = new PointStyle("Arial", Convert.ToByte('X'), 14, System.Drawing.Color.Black);
            pointX.Position = new GEOPoint(map.Width / 2 - 20, pointX.Style.SymbolSize);

            var pointY = new Point();
            pointY.Style = new PointStyle("Arial", Convert.ToByte('Y'), 14, System.Drawing.Color.Black);
            pointY.Position = new GEOPoint(pointX.Style.SymbolSize, map.Height / 2 - 20);

            var polyLine = new PolyLine();
            polyLine.AddNode(new GEOPoint(300, 200));
            polyLine.AddNode(new GEOPoint(700, 100));
            polyLine.AddNode(new GEOPoint(200, -500));
            polyLine.AddNode(new GEOPoint(-800, 75));

            var polygon = new Polygon();
            polygon.AddNode(new GEOPoint(50, 0));
            polygon.AddNode(new GEOPoint(200, 0));
            polygon.AddNode(new GEOPoint(200, 100));
            polygon.AddNode(new GEOPoint(50, 100));

            var layer1 = new Layer();
            layer1.AddMapObject(OX);
            layer1.AddMapObject(OY);
            layer1.AddMapObject(pointX);
            layer1.AddMapObject(pointY);
            layer1.AddMapObject(polyLine);
            layer1.AddMapObject(polygon);
            map.AddLayer(layer1);

            map.CurrentTool = Tool.Select;
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
                map.CurrentTool = currentTool;
                CurrentToolBtn.Checked = false;
                CurrentToolBtn = btn;
                CurrentToolBtn.Checked = true;
            }
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            Refresh();
        }

        private void mapControl_MouseMove(object sender, MouseEventArgs e)
        {
            string mapCenter = "MapCenter: " + map.MapCenter.X.ToString("0.0") + " : " + map.MapCenter.Y.ToString("0.0");
            string mapScale = "MapScale: " + map.MapScale.ToString("0.0");
            string screenCoord1 = "ScreenCoords1: " + e.X.ToString("0.0") + " : " + e.Y.ToString("0.0");
            string mapCoord = "MapCoords: " + map.ScreenToMap(e.Location).X.ToString("0.0") + " : " + map.ScreenToMap(e.Location).Y.ToString("0.0");
            string screenCoord2 = "ScreenCoords2: " + map.MapToScreen(map.ScreenToMap(e.Location)).X.ToString("0.0") + " : " + map.MapToScreen(map.ScreenToMap(e.Location)).Y.ToString("0.0");
            toolStripStatusLabel.Text = mapCenter + " | " + mapScale + " | " + screenCoord1 + " | " + screenCoord2 + " | " + mapCoord;
        }

        private void entireViewBtn_Click(object sender, EventArgs e)
        {
            var bounds = map.GEOBounds;
            double Dx = Math.Abs(bounds.XMax - bounds.XMin);
            double Dy = Math.Abs(bounds.YMax - bounds.YMin);
            GEOPoint newCenter = new GEOPoint((bounds.XMin + bounds.XMax) / 2.0, (bounds.YMin + bounds.YMax) / 2.0);
            map.MapCenter = newCenter;
            if(map.Width / Dx < map.Height / Dy)
            {
                map.MapScale = map.Width / Dx - 0.01;
            }
            else
            {
                map.MapScale = map.Height / Dy - 0.01;
            }
            map.Refresh();
        }
    }
}