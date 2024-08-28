using FutbolSolution.WPF.ViewModels.TeamViewModel;
using Microsoft.Win32;
using System.Windows;
using System;
using System.Windows.Controls;
using System.IO;

namespace FutbolSolution.WPF.Views.TeamView
{
    /// <summary>
    /// Interaction logic for UpdateTeamView.xaml
    /// </summary>
    public partial class UpdateTeamView : UserControl
    {
        public UpdateTeamView(UpdateTeamViewModel updateTeamViewModel)
        {
            InitializeComponent();
            DataContext = updateTeamViewModel;
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
                var viewModel = (UpdateTeamViewModel)this.DataContext;
                viewModel.teamImage.ImageData = fileBytes;
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
