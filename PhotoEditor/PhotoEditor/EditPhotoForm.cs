using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PhotoEditor
{
    public partial class EditPhotoForm : Form
    {
        private bool isCancelled = false;
        private string photoPath;
        private CancellationTokenSource cancellationTokenSource;

        public EditPhotoForm(string path)
        {
            photoPath = path;
            InitializeComponent();
            saveButton.Enabled = false;
        }

        private async void invertButton_Click(object sender, EventArgs e)
        {
            var transformingForm = new TransformingForm();
            transformingForm.Show();
            transformingForm.FormClosed += TransformingForm_FormClosed;

            var transformedBitmap = new Bitmap(photoPictureBox.Image);

            transformingForm.progressBar1.Minimum = 0;
            transformingForm.progressBar1.Maximum = transformedBitmap.Height;

            this.Enabled = false;
            // This could take a long time... should be done in a thread
            await InvertColors(transformedBitmap, transformingForm);
            this.Enabled = true;
            this.BringToFront();

            transformingForm.Close();

            if (!isCancelled)
            {
                saveButton.Enabled = true;
                photoPictureBox.Image = transformedBitmap;
            }
        }

        // Inverts each pixel
        private async Task InvertColors(Bitmap transformedBitmap, TransformingForm transformingForm)
        {
            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

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

                        // Leave the loop if the operation has been cancelled
                        if (token.IsCancellationRequested)
                            break;
                    }

                    // Leave the loop if the operation has been cancelled
                    if (token.IsCancellationRequested)
                    {
                        isCancelled = true;
                        break;
                    }

                    try
                    {
                        Invoke(() =>
                        {
                            transformingForm.progressBar1.Value++;
                        });
                    }
                    catch (ObjectDisposedException)
                    {
                        // The form was closed before the thread completed.  No big deal.
                    }
                }
            }, token);

            if (!token.IsCancellationRequested)
                isCancelled = false;
        }

        private async void colorButton_Click(object sender, EventArgs e)
        {
            var transformedBitmap = new Bitmap(photoPictureBox.Image);

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                var transformingForm = new TransformingForm();
                transformingForm.Show();
                transformingForm.FormClosed += TransformingForm_FormClosed;

                transformingForm.progressBar1.Minimum = 0;
                transformingForm.progressBar1.Maximum = transformedBitmap.Height;

                this.Enabled = false;
                // This could take a long time... should be done in a thread
                await AlterColors(transformedBitmap, colorDialog1.Color, transformingForm);
                this.Enabled = true;
                this.BringToFront();

                transformingForm.Close();

                if (!isCancelled)
                {
                    saveButton.Enabled = true;
                    photoPictureBox.Image = transformedBitmap;
                }
            }
        }

        private async Task AlterColors(Bitmap transformedBitmap, Color chosenColor, TransformingForm transformingForm)
        {
            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token; 

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

                        // Leave the loop if the operation has been cancelled
                        if (token.IsCancellationRequested)
                            break;
                    }

                    // Leave the loop if the operation has been cancelled
                    if (token.IsCancellationRequested)
                    {
                        isCancelled = true;
                        break;
                    }

                    try
                    {
                        Invoke(() =>
                        {
                            transformingForm.progressBar1.Value++;
                        });
                    }
                    catch (ObjectDisposedException)
                    {
                        // The form was closed before the thread completed.  No big deal.
                    }

                }
            }, token);

            if (!token.IsCancellationRequested)
                isCancelled = false;
        }

        private async void brightnessTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            var transformingForm = new TransformingForm();
            transformingForm.Show();
            transformingForm.FormClosed += TransformingForm_FormClosed;

            var transformedBitmap = new Bitmap(photoPictureBox.Image);

            transformingForm.progressBar1.Minimum = 0;
            transformingForm.progressBar1.Maximum = transformedBitmap.Height;

            this.Enabled = false;
            await ChangeBrightness(transformedBitmap, brightnessTrackBar.Value, transformingForm);
            this.Enabled = true;
            this.BringToFront();

            transformingForm.Close();

            if (!isCancelled)
            {
                saveButton.Enabled = true;
                photoPictureBox.Image = transformedBitmap;
            }
        }

        // brightness is a value between 0 – 100. Values < 50 makes the image darker, > 50 makes lighter
        private async Task ChangeBrightness(Bitmap transformedBitmap, int brightness, TransformingForm transformingForm)
        {
            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

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

                        // Leave the loop if the operation has been cancelled
                        if (token.IsCancellationRequested)
                            break;
                    }

                    // Leave the loop if the operation has been cancelled
                    if (token.IsCancellationRequested)
                    {
                        isCancelled = true;
                        break;
                    }

                    try
                    {
                        Invoke(() =>
                        {
                            transformingForm.progressBar1.Value++;
                        });
                    }
                    catch (ObjectDisposedException)
                    {
                        // The form was closed before the thread completed.  No big deal.
                    }

                }
            }, token);

            if (!token.IsCancellationRequested)
                isCancelled = false;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            photoPictureBox.Image.Save(photoPath, ImageFormat.Jpeg);
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TransformingForm_FormClosed()
        {
            // The transformation of image was cancelled
            // Cancel the task
            cancellationTokenSource.Cancel();
        }

        // Used code from https://www.c-sharpcorner.com/uploadfile/mahesh/savefiledialog-in-C-Sharp/
        private void saveAsButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = photoPath;
            saveFileDialog1.Filter = "Image Files|*.jpg";
            saveFileDialog1.DefaultExt = "jpg";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                photoPictureBox.Image.Save(saveFileDialog1.FileName);
                Close();
            }
        }
    }
}
