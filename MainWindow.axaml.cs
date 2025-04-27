using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Interactivity;
using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AzureOcrDesktopApp
{
    public partial class MainWindow : Window
    {
        private readonly string endpoint = "https://form-recog-1-test.cognitiveservices.azure.com/";
        private readonly string key = "CYeJCrgOEwwLuDQJxIn0qL10UrOejedPSHxMgUnsROculDBgZEtdJQQJ99BDACYeBjFXJ3w3AAALACOGSifU";

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void OnUploadClicked(object? sender, RoutedEventArgs e)
        {
            try
            {
                // Open file dialog
                var fileDialog = new OpenFileDialog
                {
                    AllowMultiple = false
                };
                var result = await fileDialog.ShowAsync(this);

                if (result != null && result.Length > 0)
                {
                    string filePath = result[0];
                    if (File.Exists(filePath))
                    {
                        // Load image into the preview control
                        ImagePreview.Source = new Bitmap(filePath);
                        ResultBox.Text = "Analyzing image...";

                        // Show progress bar while processing
                        ProgressBar.IsVisible = true;
                        ProgressBar.Value = 0;

                        // Run OCR
                        string extractedText = await AnalyzeImageAsync(filePath);

                        // Hide progress bar and show result
                        ProgressBar.IsVisible = false;
                        ResultBox.Text = extractedText;
                    }
                    else
                    {
                        ResultBox.Text = "File not found!";
                    }
                }
                else
                {
                    ResultBox.Text = "No file selected!";
                }
            }
            catch (Exception ex)
            {
                // Log any exception to the console
                Console.WriteLine($"Error: {ex.Message}");
                ResultBox.Text = "An error occurred during image processing.";
            }
        }

        private async Task<string> AnalyzeImageAsync(string filePath)
        {
            try
            {
                var client = new DocumentAnalysisClient(new Uri(endpoint), new AzureKeyCredential(key));

                using var stream = File.OpenRead(filePath);
                var result = await client.AnalyzeDocumentAsync(WaitUntil.Completed, "prebuilt-read", stream);

                // Animate progress bar
                await AnimateProgressBar();

                var output = "";
                foreach (var page in result.Value.Pages)
                {
                    foreach (var line in page.Lines)
                    {
                        output += line.Content + "\n";
                    }
                }

                return output;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"OCR Error: {ex.Message}");
                return "Error during OCR analysis.";
            }
        }

        private async Task AnimateProgressBar()
        {
            try
            {
                for (int i = 0; i <= 100; i++)
                {
                    ProgressBar.Value = i;
                    await Task.Delay(50);  // Delay for animation effect
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Progress Bar Animation Error: {ex.Message}");
            }
        }

        private async void OnSaveClicked(object? sender, RoutedEventArgs e)
        {
            try
            {
                var saveDialog = new SaveFileDialog();
                var savePath = await saveDialog.ShowAsync(this);

                if (!string.IsNullOrEmpty(savePath))
                {
                    File.WriteAllText(savePath, ResultBox.Text);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Save Error: {ex.Message}");
            }
        }

        private void OnClearClicked(object? sender, RoutedEventArgs e)
        {
            ResultBox.Text = "";
            ImagePreview.Source = null;
        }
    }
}
