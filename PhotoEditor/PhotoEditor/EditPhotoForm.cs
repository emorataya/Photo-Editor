using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEditor
{
    public partial class EditPhotoForm : Form
    {
        public EditPhotoForm()
        {
            InitializeComponent();
        }

        private void invertButton_Click(object sender, EventArgs e)
        {
            var transformedBitmap = new Bitmap(photoPictureBox.Image);

            // This could take a long time... should be done in a thread
            InvertColors(transformedBitmap);

            photoPictureBox.Image = transformedBitmap;
        }

        // Inverts each pixel
        private void InvertColors(Bitmap transformedBitmap)
        {
            for (int y = 0; y < transformedBitmap.Height; y++)
            {
                for (int x = 0; x < transformedBitmap.Width; x++)
                {
                    var color = transformedBitmap.GetPixel(x, y);
                    int newRed = Math.Abs(color.R - 255);
                    int newGreen = Math.Abs(color.G - 255);
                    int newBlue = Math.Abs(color.B - 255);
                    Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
                    transformedBitmap.SetPixel(x, y, newColor);
                }
            }
        }
    }
}
