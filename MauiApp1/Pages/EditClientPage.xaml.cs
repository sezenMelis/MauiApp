using MauiApp1.Models;

namespace MauiApp1.Pages;

public partial class EditClientPage : ContentPage
{
    public event EventHandler<Client> ClientUpdated;
    public Client Client { get; private set; }

    public EditClientPage(Client client)
    {
        InitializeComponent();
        Client = client;

        // Set BindingContext to the client being edited
        BindingContext = Client;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        ClientUpdated?.Invoke(this, Client);
        // Close the modal and pass the updated client back
        await Navigation.PopModalAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {

        await Navigation.PopModalAsync();
    }

    private async void OnChangeImageClicked(object sender, EventArgs e)
    {
        try
        {
            // Pick an image using the device's media picker
            var result = await MediaPicker.PickPhotoAsync();
            if (result != null)
            {
                // Load the image as a stream
                using var stream = await result.OpenReadAsync();
                byte[] imageBytes = new byte[stream.Length];
                await stream.ReadAsync(imageBytes, 0, imageBytes.Length);

                // Convert the image to a Base64 string for API compatibility
                Client.Image = Convert.ToBase64String(imageBytes);

                // Update the UI
                ClientImage.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Image selection failed: {ex.Message}", "OK");
        }
    }
}
