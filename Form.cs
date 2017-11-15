using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            OX.BeginPoint = new GEOPoint(-map.Width / 2.0, 0.0);
            OX.EndPoint = new GEOPoint(map.Width / 2.0, 0.0);

            Line OY = new Line();
            OY.BeginPoint = new GEOPoint(0.0, map.Height / 2.0);
            OY.EndPoint = new GEOPoint(0.0, -map.Height / 2.0);

            var pointX = new Point();
            pointX.Style = new PointStyle("Arial", Convert.ToByte('X'), 14, System.Drawing.Color.Black);
            pointX.Position = new GEOPoint(map.Width / 2.0 - 20.0, pointX.Style.SymbolSize);

            var pointY = new Point();
            pointY.Style = new PointStyle("Arial", Convert.ToByte('Y'), 14, System.Drawing.Color.Black);
            pointY.Position = new GEOPoint(pointX.Style.SymbolSize, map.Height / 2.0 - 20.0);

            var layerAxis = new Layer("Axis");
            layerAxis.AddMapObject(OX);
            layerAxis.AddMapObject(OY);
            layerAxis.AddMapObject(pointX);
            layerAxis.AddMapObject(pointY);
            map.AddLayer(layerAxis);

            var polygon1 = new Polygon();
            polygon1.AddNode(new GEOPoint(10.0, -10.0));
            polygon1.AddNode(new GEOPoint(200.0, -10.0));
            polygon1.AddNode(new GEOPoint(10.0, -150.0));

            var polygon2 = new Polygon();
            polygon2.PolygonStyle.Color = System.Drawing.Color.Gold;
            polygon2.AddNode(new GEOPoint(240.0, -60.0));
            polygon2.AddNode(new GEOPoint(330.0, -35.0));
            polygon2.AddNode(new GEOPoint(310.0, -330.0));
            polygon2.AddNode(new GEOPoint(250.0, -250.0));
            polygon2.AddNode(new GEOPoint(200.0, -345.0));
            polygon2.AddNode(new GEOPoint(70.0, -300.0));
            polygon2.AddNode(new GEOPoint(90.0, -230.0));
            polygon2.AddNode(new GEOPoint(270.0, -210.0));
            polygon2.AddNode(new GEOPoint(320.0, -95.0));

            var layerPolygons = new Layer("Polygons");
            layerPolygons.AddMapObject(polygon1);
            layerPolygons.AddMapObject(polygon2);
            map.AddLayer(layerPolygons);

            var polyline1 = new PolyLine();
            polyline1.LineStyle.Color = System.Drawing.Color.Blue;
            polyline1.AddNode(new GEOPoint(-350.0, -200.0));
            polyline1.AddNode(new GEOPoint(-280.0, -20.0));
            polyline1.AddNode(new GEOPoint(-200.0, -150.0));
            polyline1.AddNode(new GEOPoint(-100.0, -30.0));
            polyline1.AddNode(new GEOPoint(-10.0, -200.0));
            polyline1.AddNode(new GEOPoint(-330.0, -300.0));

            var polyline2 = new PolyLine();
            polyline2.LineStyle.Color = System.Drawing.Color.Brown;
            polyline2.LineStyle.Width = 5.0f;
            polyline2.AddNode(new GEOPoint(-50.0, -300.0));
            polyline2.AddNode(new GEOPoint(-340.0, -80.0));
            polyline2.AddNode(new GEOPoint(-20.0, -80.0));

            var layerPolyLines = new Layer("PolyLines");
            layerPolyLines.AddMapObject(polyline1);
            layerPolyLines.AddMapObject(polyline2);
            map.AddLayer(layerPolyLines);

            var line1 = new Line(new GEOPoint(-150.0, 300.0), new GEOPoint(-200.0, 50.0), new LineStyle(System.Drawing.Color.Green, 2.0f));
            var line2 = new Line(new GEOPoint(-100.0, 110.0), new GEOPoint(-300.0, 300.0), new LineStyle(System.Drawing.Color.Red, 4.0f));

            var layerLines = new Layer("Lines");
            layerLines.AddMapObject(line1);
            layerLines.AddMapObject(line2);
            map.AddLayer(layerLines);

            var pointPlane = new Point(new GEOPoint(100.0, 100.0), new PointStyle("Wingdings", 0x51, 30, System.Drawing.Color.Gray));
            var pointBomb = new Point(new GEOPoint(300.0, 200.0), new PointStyle("Wingdings", 0x4D, 20, System.Drawing.Color.Black));
            var pointSun = new Point(new GEOPoint(150.0, 300.0), new PointStyle("Wingdings", 0x52, 40, System.Drawing.Color.Orange));

            var layerPoints = new Layer("Points");
            layerPoints.AddMapObject(pointPlane);
            layerPoints.AddMapObject(pointBomb);
            layerPoints.AddMapObject(pointSun);
            map.AddLayer(layerPoints);

            map.CurrentTool = Tool.Select;
            CurrentToolBtn = selectBtn;
            CurrentToolBtn.Checked = true;

            listViewLayers.InsertionMark.Color = System.Drawing.Color.Black;
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

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            textBoxType.Text = "";
            textBoxPerimeter.Text = "";
            textBoxArea.Text = "";

            var selectedObjects = map.SelectedObjects;
            if(selectedObjects.Count == 1)
            {
                var mapObject = selectedObjects[0];
                textBoxType.Text = mapObject.Type.ToString();
                textBoxPerimeter.Text = mapObject.Perimeter().ToString("0.0");
                textBoxArea.Text = mapObject.Area().ToString("0.0");
            }
        }

        private void listViewLayers_ItemDrag(object sender, ItemDragEventArgs e)
        {
            listViewLayers.DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void listViewLayers_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void listViewLayers_DragOver(object sender, DragEventArgs e)
        {
            if(listViewLayers.SelectedItems.Count == 0)
            {
                return;
            }
            System.Drawing.Point point = listViewLayers.PointToClient(new System.Drawing.Point(e.X, e.Y));
            ListViewItem dragItem = listViewLayers.GetItemAt(point.X, point.Y);
            if(dragItem == null)
            {
                return;
            }
            int itemDragIndex = dragItem.Index;
            ListViewItem selectedItem = listViewLayers.SelectedItems[0];
            int selectedItemIndex = selectedItem.Index;
            if(itemDragIndex != selectedItemIndex)
            {
                listViewLayers.Items.Remove(selectedItem);
                listViewLayers.Items.Insert(itemDragIndex, selectedItem);
            }
        }

        private void listViewLayers_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int itemIndex = e.Item.Index;
            map.Layers[map.CountLayers() - itemIndex - 1].Visible = e.Item.Checked;
            map.Refresh();
        }
    }
}