using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;

namespace Proiect1
{
    public partial class CautareHotel : ContentPage
    {
        // Dictionary to track scores for each personality type
        private readonly Dictionary<string, int> _scores = new()
        {
            { "Adventurer", 0 },
            { "Cultural Enthusiast", 0 },
            { "Relaxation Seeker", 0 },
            { "Urban Explorer", 0 }
        };

        private bool _prefersGroupTravel; // Variable for Switch input

        public CautareHotel()
        {
            InitializeComponent();
        }

        // Event handler for RadioButton CheckedChanged
        private void OnRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value) // If the RadioButton is selected
            {
                if (sender is RadioButton radioButton && radioButton.Value is string selectedValue)
                {
                    ResetRadioButtonScores(); // Reset scores for other RadioButtons
                    _scores[selectedValue]++;
                }
            }
        }

        // Reset scores for RadioButton selections
        private void ResetRadioButtonScores()
        {
            foreach (var key in _scores.Keys.ToList())
            {
                _scores[key] = 0;
            }
        }

        // Event handler for CheckBox CheckedChanged
        private void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                string key = checkBox.AutomationId; // Use AutomationId to identify the personality type
                if (!string.IsNullOrEmpty(key) && _scores.ContainsKey(key))
                {
                    _scores[key] += e.Value ? 1 : -1; // Increment or decrement score based on check state
                }
            }
        }

        // Event handler for Switch Toggled
        private void OnSwitchToggled(object sender, ToggledEventArgs e)
        {
            _prefersGroupTravel = e.Value;
        }

        // Event handler for Submit Button Clicked
        private async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            // Determine the highest score
            var topPersonality = _scores.OrderByDescending(kvp => kvp.Value).FirstOrDefault();

            // Check if no options were selected
            if (topPersonality.Value == 0)
            {
                await DisplayAlert("No Selection", "Please answer the questions to determine your personality type.", "OK");
                return;
            }

            // Append the group travel preference
            string groupTravelMessage = _prefersGroupTravel
                ? "You prefer group travel."
                : "You prefer solo travel.";

            // Show the result in a popup
            await DisplayAlert("Your Travel Personality", $"You are a {topPersonality.Key}!\n{groupTravelMessage}", "OK");
        }
    }
}
