using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace Proiect1
{
    public partial class MainPage : ContentPage
    {
        //creez instanta 
        private DatabaseHelper _databaseHelper;

        // ObservableCollection to store the list of destinations
        public ObservableCollection<MainPageDestinatii> Destinations { get; set; } = new ObservableCollection<MainPageDestinatii>();

        public MainPage()
        {
            InitializeComponent();

     


            // Initialize the database
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Destinations.db3");
            _databaseHelper = new DatabaseHelper(dbPath);

            // Load destinations from the database
            LoadDestinations();


            // Bind the CollectionView's ItemsSource to the ObservableCollection
            BindingContext = this;
        }

       

        private async void LoadDestinations()
        {
            var destinations = await _databaseHelper.GetDestinationsAsync();
            Destinations.Clear();
            foreach (var destination in destinations)
            {
                Destinations.Add(destination);
            }
        }

        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            // Get the text from the Entry element
            var newDestinationName = destinationEntry.Text?.Trim();

            // Add the text to the ObservableCollection if not null or empty
            if (!string.IsNullOrEmpty(newDestinationName))
            {
                var newDestination = new MainPageDestinatii { Name = newDestinationName };
                await _databaseHelper.SaveDestinationAsync(newDestination);

                // Update the ObservableCollection
                Destinations.Add(newDestination);


                // Clear the Entry element after adding
                destinationEntry.Text = string.Empty;
            }
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var destinationToRemove = button?.BindingContext as MainPageDestinatii;

            if (destinationToRemove != null)
            {
                await _databaseHelper.DeleteDestinationAsync(destinationToRemove);
                Destinations.Remove(destinationToRemove);
            }
        }

    }

}
