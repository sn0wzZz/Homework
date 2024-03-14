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

        // This method takes the original image, new width, new height, progress reporter, and cancellation token
        // It creates a new bitmap with the specified dimensions for the downscaled image
        // The method divides the image into sections based on the number of CPU cores available
        // For each section, it creates a thread to process that section of the image
        // Each thread calls the ProcessImageSection method to perform the downsizing
        // After all threads finish processing, the method returns the downscaled image
        public static Bitmap DownscaleImageParallel(Image originalImage, int newWidth, int newHeight, IProgress<int> progress, CancellationToken cancellationToken)
        {
            // Make a local copy of image dimensions
            int originalWidth = originalImage.Width;
            int originalHeight = originalImage.Height;

            double widthScaleFactor = (double)newWidth / originalWidth;
            double heightScaleFactor = (double)newHeight / originalHeight;

            Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);

            unsafe
            {
                BitmapData originalData = ((Bitmap)originalImage).LockBits(new Rectangle(0, 0, originalWidth, originalHeight), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                BitmapData newData = newImage.LockBits(new Rectangle(0, 0, newWidth, newHeight), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

                try
                {
                    byte* sourceImageDataPtr = (byte*)originalData.Scan0;
                    byte* newImageDataPtr = (byte*)newData.Scan0;

                    int sourceImageStride = originalData.Stride;
                    int newImageStride = newData.Stride;

                    int numThreads = Environment.ProcessorCount; // Get the number of CPU cores

                    List<Thread> threads = new List<Thread>();

                    // Divide the image into equal sections for each thread
                    int sectionHeight = newHeight / numThreads;

                    for (int i = 0; i < numThreads; i++)
                    {
                        int startY = i * sectionHeight;
                        int endY = (i == numThreads - 1) ? newHeight : startY + sectionHeight;

                        Thread thread = new Thread(() =>
                        {
                            ProcessImageSection(startY, endY, newWidth, widthScaleFactor, heightScaleFactor, sourceImageDataPtr, newImageDataPtr, sourceImageStride, newImageStride, cancellationToken, progress);
                        });

                        thread.Start();
                        threads.Add(thread);
                    }

                    // Wait for all threads to finish
                    foreach (Thread thread in threads)
                    {
                        thread.Join();
                    }
                }
                finally
                {
                    ((Bitmap)originalImage).UnlockBits(originalData);
                    newImage.UnlockBits(newData);
                }
            }

            return newImage;
        }

        // This method is called by each thread to process a specific section of the image
        // It iterates over the pixels within the specified section and calculates the corresponding pixels in the original image
        // The pixel values are copied from the original image to the new downscaled image
        private unsafe static void ProcessImageSection(int startY, int endY, int newWidth, double widthScaleFactor, double heightScaleFactor,
    byte* sourceImageDataPtr, byte* newImageDataPtr, int sourceImageStride, int newImageStride, CancellationToken cancellationToken, IProgress<int> progress)
        {
            for (int y = startY; y < endY; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    if (cancellationToken.IsCancellationRequested)
                        return;

                    int originalX = (int)(x / widthScaleFactor);
                    int originalY = (int)(y / heightScaleFactor);

                    byte* originalPixel = sourceImageDataPtr + originalY * sourceImageStride + originalX * 3;
                    byte* newPixel = newImageDataPtr + y * newImageStride + x * 3;

                    newPixel[2] = originalPixel[2]; // Red
                    newPixel[1] = originalPixel[1]; // Green
                    newPixel[0] = originalPixel[0]; // Blue
                }

                // Report progress
                int progressValue = (int)(((double)(y + 1) / (endY - startY)) * 100);
                progress?.Report(progressValue);
            }
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
                    cancellationTokenSource.Dispose();
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

        //private void Cancel_Click(object sender, EventArgs e)
        //{
        //    if (cancellationTokenSource != null)
        //    {
        //        cancellationTokenSource.Cancel();
        //    }
        //}


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
                    ImageFormat imageFormat = ImageFormat.Jpeg;
                    switch (saveFileDialog.FilterIndex)
                    {
                        case 2:
                            imageFormat = ImageFormat.Bmp;
                            break;
                        case 3:
                            imageFormat = ImageFormat.Png;
                            break;
                    }

                    pictureBoxDownsized.Image.Save(saveFileDialog.FileName, imageFormat);
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
