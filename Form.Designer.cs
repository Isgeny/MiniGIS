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
            this.mapControl = new MiniGIS.Map();
            this.SuspendLayout();
            // 
            // mapControl
            // 
            this.mapControl.Location = new System.Drawing.Point(12, 12);
            geoPoint1.X = 0D;
            geoPoint1.Y = 0D;
            this.mapControl.MapCenter = geoPoint1;
            this.mapControl.MapScale = 1D;
            this.mapControl.Name = "mapControl";
            this.mapControl.Size = new System.Drawing.Size(758, 729);
            this.mapControl.TabIndex = 0;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 753);
            this.Controls.Add(this.mapControl);
            this.Name = "Form";
            this.Text = "MiniGIS";
            this.ResumeLayout(false);

        }

        #endregion

        private Map mapControl;
    }
}

