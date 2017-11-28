using System;
using System.Windows.Forms;

namespace MiniGIS
{
    public partial class Form : System.Windows.Forms.Form
    {
        public ToolStripButton CurrentToolBtn { get; set; }

        public Form()
        {
            InitializeComponent();

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
            map.EntireView();
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
            map.EntireView();
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
                Layer selectedLayer = map.Layers[selectedItemIndex];
                map.RemoveLayer(selectedItemIndex);
                map.InsertLayer(itemDragIndex, selectedLayer);
                listViewLayers.Items.Insert(itemDragIndex, selectedItem);
            }
            map.Refresh();
        }

        private void listViewLayers_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int itemIndex = e.Item.Index;
            map.Layers[itemIndex].Visible = e.Item.Checked;
            map.Refresh();
        }

        private void listViewLayers_KeyUp(object sender, KeyEventArgs e)
        {
            if(listViewLayers.SelectedItems.Count != 0 && e.KeyCode == Keys.Delete)
            {
                int itemIndex = listViewLayers.SelectedItems[0].Index;
                map.RemoveLayer(itemIndex);
                listViewLayers.Items.RemoveAt(itemIndex);
                map.Refresh();
            }
        }

        private void map_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                map.DeleteSelectedObjects();
            }
        }

        private void openLayerBtn_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "MIF|*.mif";
            fileDialog.Multiselect = true;
            if(fileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] filePathName = fileDialog.FileNames;
                string[] filename = fileDialog.SafeFileNames;
                for(int i = 0; i < filePathName.Length; ++i)
                {
                    int extansIndex = filename[i].IndexOf(".");
                    string layerName = filename[i].Substring(0, extansIndex);
                    var layer = new Layer(layerName);
                    var parser = new MIFParser(filePathName[i]);
                    foreach(var mapObject in parser.Data)
                    {
                        layer.AddMapObject(mapObject);
                    }
                    map.AddLayer(layer);
                    var listViewItem = new ListViewItem(layerName);
                    listViewItem.Checked = true;
                    listViewLayers.Items.Add(listViewItem);
                }
            }
            map.EntireView();
        }

        // Снятие выделения при взаимодействии со слоями
        private void listViewLayers_MouseDown(object sender, MouseEventArgs e)
        {
            foreach(var selectedObject in map.SelectedObjects)
            {
                selectedObject.Selected = false;
            }
            map.SelectedObjects.Clear();
            map.Refresh();
        }
    }
}