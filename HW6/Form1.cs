namespace HW6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

        private void image_MouseDown(object sender, MouseEventArgs e)
        {
            if (image == null || image.Image == null)
                return;

            int x = e.X;
            int y = e.Y;

            if (grayscale.Checked)
            { // Do a grayscale zoom
                byte[,] grayPxls = getGrayScaleSegment(x, y);
                grayPxls = resizeImage(grayPxls);

                Bitmap zoomImage = new Bitmap(100, 100);
                for (int i = 0; i < 100; i++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        int c = (int)grayPxls[i, j];
                        zoomImage.SetPixel(i, j, Color.FromArgb(c, c, c));
                    }
                }

                zoom.Image = zoomImage;
                zoom.Size = zoom.Image.Size;
                zoom.Visible = true;
            } 
            
            else if (colored.Checked)
            { // Do a colored zoom

            }
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
    }
}