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

        private async void invertButton_Click(object sender, EventArgs e)
        {
            var transformingForm = new TransformingForm();
            transformingForm.Show();

            var transformedBitmap = new Bitmap(photoPictureBox.Image);
            // This could take a long time... should be done in a thread
            await InvertColors(transformedBitmap);
            photoPictureBox.Image = transformedBitmap;
        }

        // Inverts each pixel
        private async Task InvertColors(Bitmap transformedBitmap)
        {
            await Task.Run(() =>
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
            });
        }

        private async void colorButton_Click(object sender, EventArgs e)
        {
            var transformedBitmap = new Bitmap(photoPictureBox.Image);

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                var transformingForm = new TransformingForm();
                transformingForm.Show();
                // This could take a long time... should be done in a thread
                await AlterColors(transformedBitmap, colorDialog1.Color);
            }

            photoPictureBox.Image = transformedBitmap;
        }

        private async Task AlterColors(Bitmap transformedBitmap, Color chosenColor)
        {
            await Task.Run(() =>
            {
                for (int y = 0; y < transformedBitmap.Height; y++)
                {
                    for (int x = 0; x < transformedBitmap.Width; x++)
                    {
                        var color = transformedBitmap.GetPixel(x, y);
                        int ave = (color.R + color.G + color.B) / 3;
                        double percent = ave / 255.0;
                        int newRed = Convert.ToInt32(chosenColor.R * percent);
                        int newGreen = Convert.ToInt32(chosenColor.G * percent);
                        int newBlue = Convert.ToInt32(chosenColor.B * percent);
                        var newColor = Color.FromArgb(newRed, newGreen, newBlue);
                        transformedBitmap.SetPixel(x, y, newColor);
                    }

                }
            });
        }

        private async void brightnessTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            var transformingForm = new TransformingForm();
            transformingForm.Show();

            var transformedBitmap = new Bitmap(photoPictureBox.Image);
            await ChangeBrightness(transformedBitmap, brightnessTrackBar.Value);
            photoPictureBox.Image = transformedBitmap;
        }

        // brightness is a value between 0 – 100. Values < 50 makes the image darker, > 50 makes lighter
        private async Task ChangeBrightness(Bitmap transformedBitmap, int brightness)
        {
            await Task.Run(() =>
            {
                // Calculate amount to change RGB
                int amount = Convert.ToInt32(2 * (50 - brightness) * 0.01 * 255);
                for (int y = 0; y < transformedBitmap.Height; y++)
                {
                    for (int x = 0; x < transformedBitmap.Width; x++)
                    {
                        var color = transformedBitmap.GetPixel(x, y);
                        int newRed = Math.Max(0, Math.Min(color.R - amount, 255));
                        int newGreen = Math.Max(0, Math.Min(color.G - amount, 255));
                        int newBlue = Math.Max(0, Math.Min(color.B - amount, 255));
                        var newColor = Color.FromArgb(newRed, newGreen, newBlue);
                        transformedBitmap.SetPixel(x, y, newColor);
                    }
                }
            });
        }
    }
}
