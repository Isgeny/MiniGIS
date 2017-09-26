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
            MiniGIS.GEOPoint geoPoint1 = new MiniGIS.GEOPoint();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.selectBtn = new System.Windows.Forms.ToolStripButton();
            this.panBtn = new System.Windows.Forms.ToolStripButton();
            this.zoomInBtn = new System.Windows.Forms.ToolStripButton();
            this.zoomOutBtn = new System.Windows.Forms.ToolStripButton();
            this.entireViewBtn = new System.Windows.Forms.ToolStripButton();
            this.mapControl = new MiniGIS.Map();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectBtn,
            this.panBtn,
            this.zoomInBtn,
            this.zoomOutBtn,
            this.entireViewBtn});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(782, 27);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
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
            // 
            // mapControl
            // 
            this.mapControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapControl.CurrentTool = MiniGIS.Tool.Select;
            this.mapControl.IsMouseDown = false;
            this.mapControl.Location = new System.Drawing.Point(12, 12);
            geoPoint1.X = 0D;
            geoPoint1.Y = 0D;
            this.mapControl.MapCenter = geoPoint1;
            this.mapControl.MapScale = 1D;
            this.mapControl.MouseDownPosition = new System.Drawing.Point(0, 0);
            this.mapControl.Name = "mapControl";
            this.mapControl.Size = new System.Drawing.Size(758, 729);
            this.mapControl.TabIndex = 0;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 753);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.mapControl);
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MiniGIS";
            this.Resize += new System.EventHandler(this.Form_Resize);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Map mapControl;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton selectBtn;
        private System.Windows.Forms.ToolStripButton panBtn;
        private System.Windows.Forms.ToolStripButton zoomInBtn;
        private System.Windows.Forms.ToolStripButton zoomOutBtn;
        private System.Windows.Forms.ToolStripButton entireViewBtn;
    }
}

