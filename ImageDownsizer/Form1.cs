using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageDownSizer
{
    public partial class Form1 : Form
    {
        private Image selectedImage;
        private CancellationTokenSource cancellationTokenSource;
        private Stopwatch parallelSw = new Stopwatch();
        private Stopwatch sequentialSw = new Stopwatch();

        public Form1()
        {
            InitializeComponent();
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.Value = 0;
        }

        private const int ScaleFactor = 100;
        private const int BytesPerPixel = 3;

        // Calculates the new width based on the original width and scaleFactor percentage
        private static int CalculateNewDimension(int originalDimension, double scaleFactor, int baseFactor)
        {
            return (int)(originalDimension * scaleFactor / baseFactor);
        }

        // Downscales the given image based on the specified scaleFactor
        // This method internally calculates the new width and height and sends them to the main method
        public static Bitmap DownscaleImageParallel(Image originalImage, double scaleFactor, IProgress<int> progress, CancellationToken cancellationToken)
        {
            int newWidth = CalculateNewDimension(originalImage.Width, scaleFactor, ScaleFactor);
            int newHeight = CalculateNewDimension(originalImage.Height, scaleFactor, ScaleFactor);

            return DownscaleImageParallel(originalImage, newWidth, newHeight, progress, cancellationToken);
        }

        // Downscales the original image to the specified newWidth and newHeight dimensions
        // The method uses unsafe code to directly manipulate image data for quicker processing
        public static Bitmap DownscaleImageSequential(Image originalImage, int newWidth, int newHeight, IProgress<int> progress, CancellationToken cancellationToken)
        {
            double widthScaleFactor = (double)newWidth / originalImage.Width;
            double heightScaleFactor = (double)newHeight / originalImage.Height;

            Bitmap CreateNewBitmap(int width, int height)
            {
                return new Bitmap(width, height, PixelFormat.Format24bppRgb);
            }

            BitmapData LockBitmapBits(Bitmap bitmap, ImageLockMode lockMode)
            {
                return bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), lockMode, PixelFormat.Format24bppRgb);
            }

            Bitmap newImage = CreateNewBitmap(newWidth, newHeight);
            BitmapData originalData = LockBitmapBits((Bitmap)originalImage, ImageLockMode.ReadOnly);
            BitmapData newData = LockBitmapBits(newImage, ImageLockMode.WriteOnly);

            unsafe
            {
                byte* sourceImageDataPtr = (byte*)originalData.Scan0;
                byte* newImageDataPtr = (byte*)newData.Scan0;

                int sourceImageStride = originalData.Stride;
                int newImageStride = newData.Stride;

                int totalPixels = newWidth * newHeight;
                int processedPixels = 0;

                int rowsPerUpdate = Math.Max(1, newHeight / 100); ;
                int rowsProcessed = 0;

                for (int y = 0; y < newHeight; y++)
                {
                    for (int x = 0; x < newWidth; x++)
                    {
                        if (cancellationToken.IsCancellationRequested)
                            return null;

                        int originalX = (int)(x / widthScaleFactor);
                        int originalY = (int)(y / heightScaleFactor);

                        byte* originalPixel = sourceImageDataPtr + originalY * sourceImageStride + originalX * BytesPerPixel;
                        byte* newPixel = newImageDataPtr + y * newImageStride + x * BytesPerPixel;

                        newPixel[2] = originalPixel[2]; // Red
                        newPixel[1] = originalPixel[1]; // Green
                        newPixel[0] = originalPixel[0]; // Blue

                        processedPixels++;

                        // Within the loop:
                        if (++rowsProcessed >= rowsPerUpdate || y == newHeight - 1)
                        {
                            int percentComplete = (int)(((double)(y + 1) / newHeight) * 100);
                            progress?.Report(percentComplete);
                            rowsProcessed = 0;
                        }
                    }
                }
            }

            ((Bitmap)originalImage).UnlockBits(originalData);
            newImage.UnlockBits(newData);

            return newImage;
        }

        // Downscales the original image to the specified newWidth and newHeight dimensions
        // The method uses unsafe code to directly manipulate image data for quicker processing
        public static Bitmap DownscaleImageParallel(Image originalImage, int newWidth, int newHeight, IProgress<int> progress, CancellationToken cancellationToken)
        {
            double widthScaleFactor = (double)newWidth / originalImage.Width;
            double heightScaleFactor = (double)newHeight / originalImage.Height;

            Bitmap CreateNewBitmap(int width, int height)
            {
                return new Bitmap(width, height, PixelFormat.Format24bppRgb);
            }

            BitmapData LockBitmapBits(Bitmap bitmap, ImageLockMode lockMode)
            {
                return bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), lockMode, PixelFormat.Format24bppRgb);
            }

            Bitmap newImage = CreateNewBitmap(newWidth, newHeight);
            BitmapData originalData = LockBitmapBits((Bitmap)originalImage, ImageLockMode.ReadOnly);
            BitmapData newData = LockBitmapBits(newImage, ImageLockMode.WriteOnly);

            unsafe
            {
                byte* sourceImageDataPtr = (byte*)originalData.Scan0;
                byte* newImageDataPtr = (byte*)newData.Scan0;

                int sourceImageStride = originalData.Stride;
                int newImageStride = newData.Stride;

                int totalPixels = newWidth * newHeight;
                int processedPixels = 0;

                int rowsPerUpdate = Math.Max(1, newHeight / 100); // Update progress every 1% of rows processed
                int rowsProcessed = 0;

                int lastReportedProgress = 0; // Track the last reported progress value

                Parallel.For(0, newHeight, y =>
                {
                    for (int x = 0; x < newWidth; x++)
                    {
                        if (cancellationToken.IsCancellationRequested)
                            return;

                        int originalX = (int)(x / widthScaleFactor);
                        int originalY = (int)(y / heightScaleFactor);

                        byte* originalPixel = sourceImageDataPtr + originalY * sourceImageStride + originalX * BytesPerPixel;
                        byte* newPixel = newImageDataPtr + y * newImageStride + x * BytesPerPixel;

                        newPixel[2] = originalPixel[2]; // Red
                        newPixel[1] = originalPixel[1]; // Green
                        newPixel[0] = originalPixel[0]; // Blue

                        // Update processed pixels count
                        Interlocked.Increment(ref processedPixels);

                        if (++rowsProcessed >= rowsPerUpdate || y == newHeight - 1)
                        {
                            int percentComplete = (int)(((double)(y + 1) / newHeight) * 100);

                            // Update progress if it changed significantly
                            if (percentComplete - lastReportedProgress >= 1)
                            {
                                progress?.Report(percentComplete);
                                lastReportedProgress = percentComplete;
                            }

                            rowsProcessed = 0;
                        }
                    }
                });
            }

    ((Bitmap)originalImage).UnlockBits(originalData);
            newImage.UnlockBits(newData);

            return newImage;
        }


        private void SetProgressBarValue(int value)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new Action<int>(SetProgressBarValue), value);
                return;
            }

            progressBar.Value = Math.Max(progressBar.Minimum, Math.Min(progressBar.Maximum, value));
        }

        private void EnableButtons()
        {
            if (buttonSave.InvokeRequired)
            {
                buttonSave.Invoke(new Action(EnableButtons));
            }
            else
            {
                buttonSave.Enabled = true;
            }
        }

        private void DisableButtons()
        {
            if (buttonSave.InvokeRequired)
            {
                buttonSave.Invoke(new Action(DisableButtons));
            }
            else
            {
                buttonSave.Enabled = false;
            }
        }

        private async void DownsizeImage_Click(object sender, EventArgs e)
        {
            if (selectedImage == null)
            {
                MessageBox.Show("No image selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double scaleFactor;
            bool isValidInput = double.TryParse(textBoxFactor.Text, out scaleFactor);

            if (!isValidInput || scaleFactor <= 0 || scaleFactor > 100)
            {
                MessageBox.Show("Please enter a valid downscaling factor between 1 and 100.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            try
            {
                var progress = new Progress<int>(value => SetProgressBarValue(value));

                DisableButtons();

                int newWidth = CalculateNewDimension(selectedImage.Width, scaleFactor, ScaleFactor);
                int newHeight = CalculateNewDimension(selectedImage.Height, scaleFactor, ScaleFactor);

                // Parallel downsizing
                parallelSw.Restart();
                Bitmap downscaledImage = await Task.Run(() => DownscaleImageParallel(selectedImage, newWidth, newHeight, progress, cancellationToken), cancellationToken);
                parallelSw.Stop();
                labelParallelTime.Text = $"Parallel downsizing took: {parallelSw.ElapsedMilliseconds} ms";

                // Sequential downsizing
                sequentialSw.Restart();
                Bitmap downscaledSequentialImage = await Task.Run(() => DownscaleImageSequential(selectedImage, newWidth, newHeight, progress, cancellationToken), cancellationToken);
                sequentialSw.Stop();
                labelSequentialTime.Text = $"Sequential downsizing took: {sequentialSw.ElapsedMilliseconds} ms";

                pictureBoxDownsized.Image = downscaledImage;
                pictureBoxSequential.Image = downscaledSequentialImage;
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Image downsizing was canceled.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                EnableButtons();
                if (cancellationTokenSource != null)
                {
                    cancellationTokenSource.Dispose(); // Ensure proper disposal
                }
            }
        }

        private void OpenImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.png; *.bmp; )|*.jpg;*.png;*.bmp;";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFile = openFileDialog.FileName;
                selectedImage = Image.FromFile(selectedFile);
                pictureBoxOriginal.Image = selectedImage;
            }
        }

        private void DownloadImage_Click(object sender, EventArgs e)
        {
            if (pictureBoxDownsized.Image != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png|Bitmap Image|*.bmp";
                saveFileDialog.Title = "Save Downsized Image";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                {
                    // Get the image format based on the selected file type
                    ImageFormat imageFormat = ImageFormat.Jpeg; // Default to JPEG
                    switch (saveFileDialog.FilterIndex)
                    {
                        case 2:
                            imageFormat = ImageFormat.Bmp;
                            break;
                        case 3:
                            imageFormat = ImageFormat.Png;
                            break;
                    }

                    // Save the downsized image
                    pictureBoxDownsized.Image.Save(saveFileDialog.FileName, imageFormat);

                    // Show success notification
                    MessageBox.Show("Image successfully saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please downsize an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
