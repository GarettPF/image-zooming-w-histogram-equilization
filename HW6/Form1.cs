namespace HW6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            zoom.BringToFront();
            this.Width = 160;
            this.Height = 142;
        }

        private void load_image_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image Files(*.jpg;*.jpeg; *.gif; *.bmp; *.png)| *.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (file.ShowDialog() == DialogResult.OK)
            {
                image.Image = new Bitmap(file.FileName);
                image.Size = image.Image.Size;

                this.Height = image.Image.Height + 60;
                this.Width = image.Image.Width + 165;
            }
        }

        private byte[,] getGrayScaleSegment(int x, int y)
        {
            Bitmap img = (Bitmap)image.Image;
            byte[,] result = new byte[50, 50];

            for (int i = -25; i < 25; i++)
            {
                for (int j = -25; j < 25; j++)
                {
                    try
                    {
                        Color c = img.GetPixel(x + i, y + j);
                        byte Y = (byte)((c.R * 0.3) + (c.G * 0.59) + (c.B * 0.11));
                        result[25 + i, 25 + j] = Y;
                    } catch
                    {
                        result[25 + i, 25 + j] = 0;
                    }
                }
            }

            return result;
        }

        private void getRGBsegment(int x, int y, ref byte[,] R, ref byte[,] G, ref byte[,] B)
        {
            Bitmap img = (Bitmap)image.Image;

            for (int i = -25; i < 25; i++)
            {
                for (int j = -25; j < 25; j++)
                {
                    try
                    {
                        Color c = img.GetPixel(x + i, y + j);
                        R[25 + i, 25 + j] = c.R;
                        G[25 + i, 25 + j] = c.G;
                        B[25 + i, 25 + j] = c.B;
                    }
                    catch
                    {
                        R[25 + i, 25 + j] = 0;
                        G[25 + i, 25 + j] = 0;
                        B[25 + i, 25 + j] = 0;
                    }
                }
            }
        }

        private void image_MouseDown(object sender, MouseEventArgs e)
        {
            if (image == null || image.Image == null)
                return;

            int x = e.X;
            int y = e.Y;

            Bitmap zoomImage = new Bitmap(100, 100);
            if (grayscale.Checked)
            { // Do a grayscale zoom
                byte[,] grayPxls = getGrayScaleSegment(x, y);
                grayPxls = resizeImage(grayPxls);
                equilization(ref grayPxls);

                for (int i = 0; i < 100; i++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        byte c = grayPxls[i, j];
                        zoomImage.SetPixel(i, j, Color.FromArgb(c, c, c));
                    }
                }
            } 
            
            else if (colored.Checked)
            { // Do a colored zoom
                byte[,] R = new byte[50, 50], 
                        G = new byte[50, 50], 
                        B = new byte[50, 50];
                getRGBsegment(x, y, ref R, ref G, ref B);
                R = resizeImage(R);
                G = resizeImage(G);
                B = resizeImage(B);
                equilization(ref R);
                equilization(ref G);
                equilization(ref B);

                for (int i = 0; i < 100; i++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        byte r = R[i, j];
                        byte g = G[i, j];
                        byte b = B[i, j];
                        zoomImage.SetPixel(i, j, Color.FromArgb(r, g, b));
                    }
                }
            }

            zoom.Image = zoomImage;
            zoom.Size = zoom.Image.Size;
            zoom.Visible = true;
            if (moveZoom.Checked)
                zoom.Location = new Point(x + 20, y - 75);
            else
                zoom.Location = new Point(12, 128);
        }
        private void image_MouseUp(object sender, MouseEventArgs e)
        {
            zoom.Visible = false;
        }

        private byte[,] resizeImage(byte[,] layer)
        {
            byte[,] wide = new byte[100, 50];
            byte[,] resize = new byte[100, 100];
            float value1, value2;

            // Double the horizontal resolution
            for (int y = 0; y < 50; y++)
            {   // for the left edge
                value1 = layer[0, y] * 0.75f;
                value2 = layer[1, y] * 0.25f;
                wide[0, y] = (byte)(value1 + value2);
                wide[1, y] = (byte)(value1 + value2);

                for (int x = 1; x < 49; x++)
                {
                    // calculate left side of pixel
                    value1 = layer[x - 1, y] * 0.25f;
                    value2 = layer[x, y] * 0.75f;
                    wide[x * 2, y] = (byte)(value1 + value2);
                    // calculate right side of pixel
                    value1 = layer[x + 1, y] * 0.25f;
                    wide[x * 2 + 1, y] = (byte)(value1 + value2);
                }
                // for the right edge
                value1 = layer[49, y] * 0.75f;
                value2 = layer[48, y] * 0.25f;
                wide[99, y] = (byte)(value1 + value2);
                wide[98, y] = (byte)(value1 + value2);
            }

            // double the vertical resolution
            for (int x = 0; x < 100; x++)
            {   // for the top edge
                value1 = wide[x, 0] * 0.75f;
                value2 = wide[x, 1] * 0.25f;
                resize[x, 0] = (byte)(value1 + value2);
                resize[x, 1] = (byte)(value1 + value2);

                for (int y = 1; y < 50 - 1; y++)
                {
                    // calculate top side of pixel
                    value1 = wide[x, y] * 0.75f;
                    value2 = wide[x, y - 1] * 0.25f;
                    resize[x, y * 2] = (byte)(value1 + value2);
                    // calculate bottom side of pixel
                    value2 = wide[x, y + 1] * 0.25f;
                    resize[x, y * 2 + 1] = (byte)(value1 + value2);
                }
                // for the bottom edge
                value1 = wide[x, 49] * 0.75f;
                value2 = wide[x, 48] * 0.25f;
                resize[x, 99] = (byte)(value1 + value2);
                resize[x, 98] = (byte)(value1 + value2);
            }

            return resize;
        }

        private void equilization(ref byte[,] layer)
        {
            float[] cdf = new float[256];
            int[] f = new int[256];

            // read the intensities
            for (int x = 0; x < 100; x++)
                for (int y = 0; y < 100; y++)
                    f[layer[x, y]]++;

            cdf[0] = f[0];

            // equilize the values
            for (int i = 1; i < 256; i++)
                cdf[i] = cdf[i - 1] + f[i];
            for (int j = 0; j < 256; j++)
                cdf[j] = cdf[j] / cdf[255];

            // modify the layer
            for (int x = 0; x < 100; x++)
                for (int y = 0; y < 100; y++)
                    layer[x, y] = (byte)(255f * cdf[layer[x, y]]);
        }

        private void image_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.Y < 0 || e.X > image.Width || e.Y > image.Height)
            {
                image_MouseUp(sender, e);
                return;
            }


            if (e.Button == MouseButtons.Left)
            {
                image_MouseDown(sender, e);
            }
            else
            {
                image_MouseUp(sender, e);
            }
        }
    }
}