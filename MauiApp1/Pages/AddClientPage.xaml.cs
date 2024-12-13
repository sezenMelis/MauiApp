using MauiApp1.Models;
using System.Net;

namespace MauiApp1.Pages;

public partial class AddClientPage : ContentPage
{
    private readonly ApiService _apiService;

    // Event to notify the main page of a successful addition
    public event EventHandler<Client> ClientAdded;

    public AddClientPage(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Validate inputs
        if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
            string.IsNullOrWhiteSpace(LastNameEntry.Text) ||
            string.IsNullOrWhiteSpace(EmailEntry.Text) ||
            string.IsNullOrWhiteSpace(PasswordEntry.Text) ||
            string.IsNullOrWhiteSpace(ImageUrlEntry.Text))
        {
            await DisplayAlert("Error", "All fields are required.", "OK");
            return;
        }

        // Create a new Client object
        var newClient = new Client
        {
            Name = NameEntry.Text,
            LastName = LastNameEntry.Text,
            Email = EmailEntry.Text,
            Password = PasswordEntry.Text,
            Image = ImageUrlEntry.Text
        };

        string url = "https://localhost:7232/api/Clients";
        var response = await _apiService.AddClientAsync(url, newClient);

        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("Success", "Client added successfully", "OK");

            // Notify the main page
            ClientAdded?.Invoke(this, newClient);
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            await DisplayAlert("Error", $"Failed to add client: {errorMessage}", "OK");
        }

        // Close the modal
        await Navigation.PopModalAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        // Close the modal without saving
        await Navigation.PopModalAsync();
    }
}
