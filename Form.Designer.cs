namespace MiniGIS
{
    partial class Form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Points");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Lines");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("PolyLines");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Polygons");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Axis");
            MiniGIS.GEOPoint geoPoint1 = new MiniGIS.GEOPoint();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.selectBtn = new System.Windows.Forms.ToolStripButton();
            this.panBtn = new System.Windows.Forms.ToolStripButton();
            this.zoomInBtn = new System.Windows.Forms.ToolStripButton();
            this.zoomOutBtn = new System.Windows.Forms.ToolStripButton();
            this.entireViewBtn = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.listViewLayers = new System.Windows.Forms.ListView();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.lblType = new System.Windows.Forms.Label();
            this.lblPerimeter = new System.Windows.Forms.Label();
            this.lblArea = new System.Windows.Forms.Label();
            this.textBoxType = new System.Windows.Forms.TextBox();
            this.textBoxPerimeter = new System.Windows.Forms.TextBox();
            this.textBoxArea = new System.Windows.Forms.TextBox();
            this.groupBoxLayers = new System.Windows.Forms.GroupBox();
            this.groupBoxMap = new System.Windows.Forms.GroupBox();
            this.groupBoxCalculation = new System.Windows.Forms.GroupBox();
            this.map = new MiniGIS.Map();
            this.toolStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBoxLayers.SuspendLayout();
            this.groupBoxMap.SuspendLayout();
            this.groupBoxCalculation.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectBtn,
            this.panBtn,
            this.zoomInBtn,
            this.zoomOutBtn,
            this.entireViewBtn});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(132, 27);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip";
            // 
            // selectBtn
            // 
            this.selectBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.selectBtn.Image = global::MiniGIS.Properties.Resources.Select;
            this.selectBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectBtn.Name = "selectBtn";
            this.selectBtn.Size = new System.Drawing.Size(24, 24);
            this.selectBtn.Text = "Select";
            this.selectBtn.Click += new System.EventHandler(this.OnToolStripBtnClicked);
            // 
            // panBtn
            // 
            this.panBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.panBtn.Image = global::MiniGIS.Properties.Resources.Pan;
            this.panBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.panBtn.Name = "panBtn";
            this.panBtn.Size = new System.Drawing.Size(24, 24);
            this.panBtn.Text = "Pan";
            this.panBtn.Click += new System.EventHandler(this.OnToolStripBtnClicked);
            // 
            // zoomInBtn
            // 
            this.zoomInBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomInBtn.Image = global::MiniGIS.Properties.Resources.ZoomIn;
            this.zoomInBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomInBtn.Name = "zoomInBtn";
            this.zoomInBtn.Size = new System.Drawing.Size(24, 24);
            this.zoomInBtn.Text = "ZoomIn";
            this.zoomInBtn.Click += new System.EventHandler(this.OnToolStripBtnClicked);
            // 
            // zoomOutBtn
            // 
            this.zoomOutBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomOutBtn.Image = global::MiniGIS.Properties.Resources.ZoomOut;
            this.zoomOutBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomOutBtn.Name = "zoomOutBtn";
            this.zoomOutBtn.Size = new System.Drawing.Size(24, 24);
            this.zoomOutBtn.Text = "ZoomOut";
            this.zoomOutBtn.Click += new System.EventHandler(this.OnToolStripBtnClicked);
            // 
            // entireViewBtn
            // 
            this.entireViewBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.entireViewBtn.Image = global::MiniGIS.Properties.Resources.EntireView;
            this.entireViewBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.entireViewBtn.Name = "entireViewBtn";
            this.entireViewBtn.Size = new System.Drawing.Size(24, 24);
            this.entireViewBtn.Text = "EntireView";
            this.entireViewBtn.Click += new System.EventHandler(this.entireViewBtn_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 767);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(940, 25);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(143, 20);
            this.toolStripStatusLabel.Text = "toolStripStatusLabel";
            // 
            // listViewLayers
            // 
            this.listViewLayers.CheckBoxes = true;
            listViewItem1.Checked = true;
            listViewItem1.StateImageIndex = 1;
            listViewItem2.Checked = true;
            listViewItem2.StateImageIndex = 1;
            listViewItem3.Checked = true;
            listViewItem3.StateImageIndex = 1;
            listViewItem4.Checked = true;
            listViewItem4.StateImageIndex = 1;
            listViewItem5.Checked = true;
            listViewItem5.StateImageIndex = 1;
            this.listViewLayers.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5});
            this.listViewLayers.Location = new System.Drawing.Point(6, 21);
            this.listViewLayers.MultiSelect = false;
            this.listViewLayers.Name = "listViewLayers";
            this.listViewLayers.Size = new System.Drawing.Size(188, 199);
            this.listViewLayers.TabIndex = 3;
            this.listViewLayers.UseCompatibleStateImageBehavior = false;
            this.listViewLayers.View = System.Windows.Forms.View.List;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(6, 167);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(188, 35);
            this.btnCalculate.TabIndex = 5;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(35, 27);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(44, 17);
            this.lblType.TabIndex = 6;
            this.lblType.Text = "Type:";
            // 
            // lblPerimeter
            // 
            this.lblPerimeter.AutoSize = true;
            this.lblPerimeter.Location = new System.Drawing.Point(6, 75);
            this.lblPerimeter.Name = "lblPerimeter";
            this.lblPerimeter.Size = new System.Drawing.Size(73, 17);
            this.lblPerimeter.TabIndex = 7;
            this.lblPerimeter.Text = "Perimeter:";
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(37, 121);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(42, 17);
            this.lblArea.TabIndex = 8;
            this.lblArea.Text = "Area:";
            // 
            // textBoxType
            // 
            this.textBoxType.Location = new System.Drawing.Point(85, 24);
            this.textBoxType.Name = "textBoxType";
            this.textBoxType.ReadOnly = true;
            this.textBoxType.Size = new System.Drawing.Size(109, 22);
            this.textBoxType.TabIndex = 9;
            // 
            // textBoxPerimeter
            // 
            this.textBoxPerimeter.Location = new System.Drawing.Point(85, 72);
            this.textBoxPerimeter.Name = "textBoxPerimeter";
            this.textBoxPerimeter.ReadOnly = true;
            this.textBoxPerimeter.Size = new System.Drawing.Size(109, 22);
            this.textBoxPerimeter.TabIndex = 10;
            // 
            // textBoxArea
            // 
            this.textBoxArea.Location = new System.Drawing.Point(85, 118);
            this.textBoxArea.Name = "textBoxArea";
            this.textBoxArea.ReadOnly = true;
            this.textBoxArea.Size = new System.Drawing.Size(109, 22);
            this.textBoxArea.TabIndex = 11;
            // 
            // groupBoxLayers
            // 
            this.groupBoxLayers.Controls.Add(this.listViewLayers);
            this.groupBoxLayers.Location = new System.Drawing.Point(731, 30);
            this.groupBoxLayers.Name = "groupBoxLayers";
            this.groupBoxLayers.Size = new System.Drawing.Size(199, 229);
            this.groupBoxLayers.TabIndex = 12;
            this.groupBoxLayers.TabStop = false;
            this.groupBoxLayers.Text = "Layers";
            // 
            // groupBoxMap
            // 
            this.groupBoxMap.Controls.Add(this.map);
            this.groupBoxMap.Location = new System.Drawing.Point(12, 30);
            this.groupBoxMap.Name = "groupBoxMap";
            this.groupBoxMap.Size = new System.Drawing.Size(713, 726);
            this.groupBoxMap.TabIndex = 13;
            this.groupBoxMap.TabStop = false;
            this.groupBoxMap.Text = "Map";
            // 
            // groupBoxCalculation
            // 
            this.groupBoxCalculation.Controls.Add(this.textBoxType);
            this.groupBoxCalculation.Controls.Add(this.btnCalculate);
            this.groupBoxCalculation.Controls.Add(this.lblType);
            this.groupBoxCalculation.Controls.Add(this.textBoxArea);
            this.groupBoxCalculation.Controls.Add(this.lblPerimeter);
            this.groupBoxCalculation.Controls.Add(this.textBoxPerimeter);
            this.groupBoxCalculation.Controls.Add(this.lblArea);
            this.groupBoxCalculation.Location = new System.Drawing.Point(731, 265);
            this.groupBoxCalculation.Name = "groupBoxCalculation";
            this.groupBoxCalculation.Size = new System.Drawing.Size(199, 210);
            this.groupBoxCalculation.TabIndex = 14;
            this.groupBoxCalculation.TabStop = false;
            this.groupBoxCalculation.Text = "Calculation";
            // 
            // map
            // 
            this.map.CurrentTool = MiniGIS.Tool.Select;
            this.map.IsMouseDown = false;
            this.map.Location = new System.Drawing.Point(6, 21);
            geoPoint1.X = 0D;
            geoPoint1.Y = 0D;
            this.map.MapCenter = geoPoint1;
            this.map.MapScale = 1D;
            this.map.MouseDownPosition = new System.Drawing.Point(0, 0);
            this.map.Name = "map";
            this.map.Size = new System.Drawing.Size(700, 700);
            this.map.TabIndex = 0;
            this.map.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapControl_MouseMove);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 792);
            this.Controls.Add(this.groupBoxCalculation);
            this.Controls.Add(this.groupBoxMap);
            this.Controls.Add(this.groupBoxLayers);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip);
            this.DoubleBuffered = true;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MiniGIS";
            this.Resize += new System.EventHandler(this.Form_Resize);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBoxLayers.ResumeLayout(false);
            this.groupBoxMap.ResumeLayout(false);
            this.groupBoxCalculation.ResumeLayout(false);
            this.groupBoxCalculation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Map map;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton selectBtn;
        private System.Windows.Forms.ToolStripButton panBtn;
        private System.Windows.Forms.ToolStripButton zoomInBtn;
        private System.Windows.Forms.ToolStripButton zoomOutBtn;
        private System.Windows.Forms.ToolStripButton entireViewBtn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ListView listViewLayers;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblPerimeter;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.TextBox textBoxType;
        private System.Windows.Forms.TextBox textBoxPerimeter;
        private System.Windows.Forms.TextBox textBoxArea;
        private System.Windows.Forms.GroupBox groupBoxLayers;
        private System.Windows.Forms.GroupBox groupBoxMap;
        private System.Windows.Forms.GroupBox groupBoxCalculation;
    }
}

