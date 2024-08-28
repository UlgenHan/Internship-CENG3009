using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace FutbolSolution.WPF.ViewModels.PlayerViewModel
{
    /// <summary>
    /// Interaction logic for CreatePlayerView.xaml
    /// </summary>
    public partial class CreatePlayerView : UserControl
    {
        public CreatePlayerView(CreatePlayerViewModel createPlayerViewModel)
        {
            InitializeComponent();
            DataContext = createPlayerViewModel;
        }
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePathTextBox.Text = openFileDialog.FileName;
            }
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text;

            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                MessageTextBlock.Text = "Please select a valid file.";
                return;
            }

            try
            {
                // Read the file into a byte array
                byte[] fileBytes = File.ReadAllBytes(filePath);
                // Set the PlayerImage property in the ViewModel
                var viewModel = (CreatePlayerViewModel)this.DataContext;
                viewModel.PlayerImage = fileBytes;
                viewModel.IsFileUploaded = true; // Update the flag to indicate the file has been uploaded

                MessageTextBlock.Text = "File uploaded successfully!";
            }
            catch (Exception ex)
            {
                MessageTextBlock.Text = "File upload failed: " + ex.Message;
            }
        }
    }
}
