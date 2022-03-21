namespace HW6
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.load_image = new System.Windows.Forms.Button();
            this.image = new System.Windows.Forms.PictureBox();
            this.grayscale = new System.Windows.Forms.RadioButton();
            this.colored = new System.Windows.Forms.RadioButton();
            this.zoom = new System.Windows.Forms.PictureBox();
            this.moveZoom = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoom)).BeginInit();
            this.SuspendLayout();
            // 
            // load_image
            // 
            this.load_image.Location = new System.Drawing.Point(12, 12);
            this.load_image.Name = "load_image";
            this.load_image.Size = new System.Drawing.Size(122, 25);
            this.load_image.TabIndex = 0;
            this.load_image.Text = "Load Image";
            this.load_image.UseVisualStyleBackColor = true;
            this.load_image.Click += new System.EventHandler(this.load_image_Click);
            // 
            // image
            // 
            this.image.Location = new System.Drawing.Point(140, 12);
            this.image.Name = "image";
            this.image.Size = new System.Drawing.Size(129, 77);
            this.image.TabIndex = 1;
            this.image.TabStop = false;
            this.image.MouseDown += new System.Windows.Forms.MouseEventHandler(this.image_MouseDown);
            this.image.MouseMove += new System.Windows.Forms.MouseEventHandler(this.image_MouseMove);
            this.image.MouseUp += new System.Windows.Forms.MouseEventHandler(this.image_MouseUp);
            // 
            // grayscale
            // 
            this.grayscale.Checked = true;
            this.grayscale.Location = new System.Drawing.Point(12, 43);
            this.grayscale.Name = "grayscale";
            this.grayscale.Size = new System.Drawing.Size(122, 21);
            this.grayscale.TabIndex = 2;
            this.grayscale.TabStop = true;
            this.grayscale.Text = "GrayScale";
            this.grayscale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.grayscale.UseVisualStyleBackColor = true;
            // 
            // colored
            // 
            this.colored.Location = new System.Drawing.Point(12, 70);
            this.colored.Name = "colored";
            this.colored.Size = new System.Drawing.Size(122, 23);
            this.colored.TabIndex = 3;
            this.colored.Text = "Colored";
            this.colored.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.colored.UseVisualStyleBackColor = true;
            // 
            // zoom
            // 
            this.zoom.Location = new System.Drawing.Point(21, 128);
            this.zoom.Name = "zoom";
            this.zoom.Size = new System.Drawing.Size(100, 100);
            this.zoom.TabIndex = 4;
            this.zoom.TabStop = false;
            // 
            // moveZoom
            // 
            this.moveZoom.Checked = true;
            this.moveZoom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.moveZoom.Location = new System.Drawing.Point(12, 99);
            this.moveZoom.Name = "moveZoom";
            this.moveZoom.Size = new System.Drawing.Size(122, 23);
            this.moveZoom.TabIndex = 5;
            this.moveZoom.Text = "Move Zoom";
            this.moveZoom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.moveZoom.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 242);
            this.Controls.Add(this.moveZoom);
            this.Controls.Add(this.zoom);
            this.Controls.Add(this.colored);
            this.Controls.Add(this.grayscale);
            this.Controls.Add(this.image);
            this.Controls.Add(this.load_image);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button load_image;
        private PictureBox image;
        private RadioButton grayscale;
        private RadioButton colored;
        private PictureBox zoom;
        private CheckBox moveZoom;
    }
}